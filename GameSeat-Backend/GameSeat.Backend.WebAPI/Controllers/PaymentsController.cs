using GameSeat.Backend.Business.Services;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;

namespace GameSeat.Backend.WebAPI.Controllers
{
    [ApiController]
    [Route("payments")]
    public class PaymentsController : ControllerBase
    {
        private readonly StripeService _stripeService;
        private readonly IPaymentsService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly IReservationService _reservationService;
        private readonly IPdfService _pdfService;


        public PaymentsController(IPaymentsService paymentService, StripeService stripeService, IConfiguration configuration, IReservationService reservationService, IPdfService pdfService)
        {
            _paymentService = paymentService;
            _stripeService = stripeService;
            _configuration = configuration;
            _reservationService = reservationService;
            _pdfService = pdfService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentModel>>> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentModel>> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            return Ok(payment);
        }

        [HttpPost]
        public async Task<ActionResult> AddPayment([FromBody] PaymentModel payment)
        {
            await _paymentService.AddPaymentAsync(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = payment.Id }, payment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] PaymentModel payment)
        {
            if (id != payment.Id)
            {
                return BadRequest();
            }

            var existingPayment = await _paymentService.GetPaymentByIdAsync(id);
            if (existingPayment == null)
            {
                return NotFound();
            }

            await _paymentService.UpdatePaymentAsync(payment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null)
            {
                return NotFound();
            }

            await _paymentService.DeletePaymentAsync(id);
            return NoContent();
        }

        [HttpPost("create-payment")]
        public async Task<IActionResult> CreatePayment([FromBody] ReservationRequest request)
        {
            var successUrl = _configuration["Stripe:SuccessUrl"];
            var cancelUrl = _configuration["Stripe:CancelUrl"];

            // Calculate total price based on reservation duration
            var service = new ProductService();

            var sessionUrl = await _stripeService.CreateCheckoutSessionAsync(request.TotalAmount, "eur", successUrl!, cancelUrl!, 1, request.reservationID);
            return Ok(new { Url = sessionUrl });
        }

        [HttpPost("execute-payment")]
        public async Task<IActionResult> ExecutePayment([FromQuery] string paymentId, [FromQuery] string payerId)
        {
            var payment = await _stripeService.ExecutePayment(paymentId, payerId);

            if (payment.Status.ToLower() != "succeeded")
            {
                return BadRequest("Payment not approved.");
            }

            // Register the payment in your database here
            var newPayment = new PaymentModel
            {
                ReservationId = int.Parse(payment.Metadata["reservation_id"]),
                Amount = payment.AmountReceived / 100m, // Stripe amounts are in cents
                PaymentMethod = payment.PaymentMethodTypes[0],
                CreatedAt = DateTime.UtcNow
            };
            await _paymentService.AddPaymentAsync(newPayment);

            return Ok(payment);
        }

        private decimal CalculateTotalAmount(decimal hours, decimal price)
        {
            return (decimal)hours*price;
        }

        [HttpPost("verify-payment")]
        public async Task<IActionResult> VerifyPayment([FromBody] PaymentVerificationRequest request)
        {
            var service = new SessionService();
            var session = await service.GetAsync(request.SessionId);

            if (session.PaymentStatus == "paid")
            {
                if (string.IsNullOrEmpty(session.ClientReferenceId))
                {
                    return BadRequest(new { success = false, message = "ClientReferenceId is null or empty." });
                }

                if (!int.TryParse(session.ClientReferenceId, out int reservationId))
                {
                    return BadRequest(new { success = false, message = "ClientReferenceId is not a valid integer." });
                }

                if (!session.AmountTotal.HasValue)
                {
                    await _reservationService.CancelOrConfirmReservationAsync(reservationId, 1);
                    return BadRequest(new { success = false, message = "AmountTotal is null." });
                }

                // Verifica si la reserva existe antes de agregar el pago
                var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
                
                if (reservation == null)
                {
                    return BadRequest(new { success = false, message = "Reservation does not exist." });
                }

                // Actualiza tu base de datos con el estado del pago
                var payment = new PaymentModel
                {
                    ReservationId = reservationId,
                    Amount = session.AmountTotal.Value / 100m, // Convertir de centavos a la unidad monetaria
                    PaymentMethod = "stripe",
                    CreatedAt = DateTime.UtcNow
                };

                await _paymentService.AddPaymentAsync(payment);

                await _reservationService.CancelOrConfirmReservationAsync(reservationId, 2);

                return Ok(new { success = true });
            }
            else
            {
                if (!int.TryParse(session.ClientReferenceId, out int reservationId))
                {
                    reservationId = 0; // Si no puede convertir, no tiene sentido cancelar la reserva
                }

                await _reservationService.CancelOrConfirmReservationAsync(reservationId, 1);
                return BadRequest(new { success = false, message = "Payment not successful." });
            }
        }

        [HttpGet("download")]
        public async Task<IActionResult> DownloadPdf()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            var paymentsList = payments.ToList();  // Convertir a lista
            var pdf = _pdfService.GeneratePdf(paymentsList);

            return File(pdf, "application/pdf", "payments.pdf");
        }
    }

    public class ReservationRequest
    {
        public decimal TotalAmount { get; set; }
        public int reservationID {get; set; }
    }

    public class PaymentVerificationRequest
    {
        public string SessionId { get; set; }
    }
}

