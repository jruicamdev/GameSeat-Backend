using Stripe;
using Stripe.Checkout;

namespace GameSeat.Backend.Business.Services
{
    public class StripeService
    {
        private readonly string _apiKey;

        public StripeService(string apiKey)
        {
            _apiKey = apiKey;
            StripeConfiguration.ApiKey = _apiKey;
        }

        public async Task<string> CreateCheckoutSessionAsync(decimal amount, string currency, string successUrl, string cancelUrl, int quantity, int reservationId)
        {
            var options = new SessionCreateOptions
            {
                PaymentMethodTypes = new List<string> { "card" },
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(amount * 100), // Stripe amounts are in cents
                            Currency = currency,
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = "Chair Reserve",
                                Images = new List<string> { "https://picsum.photos/id/0/400" } // Correctly initialize the list
                            }
                        },
                        Quantity = quantity,
                    },
                },
                Mode = "payment",
                SuccessUrl = $"{successUrl}?session_id={{CHECKOUT_SESSION_ID}}", // Incluir session_id
                CancelUrl = cancelUrl,
                ClientReferenceId = reservationId.ToString() // Aquí debes establecer el ID de referencia del cliente, por ejemplo, el ID de la reserva.
            };

            var service = new SessionService();
            Session session = await service.CreateAsync(options);

            return session.Url;
        }
        public async Task<PaymentIntent> ExecutePayment(string paymentId, string payerId)
        {
            var service = new PaymentIntentService();
            var options = new PaymentIntentConfirmOptions
            {
                PaymentMethod = payerId,
            };
            var paymentIntent = await service.ConfirmAsync(paymentId, options);
            return paymentIntent;
        }
    }
}
