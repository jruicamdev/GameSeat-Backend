using iText.Kernel.Pdf;
using iText.Layout.Element;
using GameSeat.Backend.Infrastructure.Data.Models;
using GameSeat.Backend.Infrastructure.Interfaces;
using iText.Kernel.Colors;
using iText.Layout.Borders;
using iText.Layout.Properties;
using iText.Layout;

namespace GameSeat.Backend.Business.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GeneratePdf(List<PaymentModel> payments)
        {
            using (var ms = new MemoryStream())
            {
                var writer = new PdfWriter(ms);
                var pdf = new PdfDocument(writer);
                var document = new Document(pdf);

                // Agregar título con estilo
                var title = new Paragraph("Payments List")
                    .SetTextAlignment(TextAlignment.CENTER)
                    .SetFontSize(20)
                    .SetBold()
                    .SetMarginBottom(20);
                document.Add(title);

                // Crear tabla con cabecera
                var table = new Table(new float[] { 1, 2, 2, 2, 2, 3, 2 }).UseAllAvailableWidth();
                table.AddHeaderCell(CreateCell("ID", true));
                table.AddHeaderCell(CreateCell("Reservation ID", true));
                table.AddHeaderCell(CreateCell("Amount", true));
                table.AddHeaderCell(CreateCell("Payment Method", true));
                table.AddHeaderCell(CreateCell("Created At", true));
                table.AddHeaderCell(CreateCell("User", true));
                table.AddHeaderCell(CreateCell("Chair", true)); // Añadir la columna para la silla

                // Añadir filas de datos
                foreach (var payment in payments)
                {
                    table.AddCell(CreateCell(payment.Id.ToString()));
                    table.AddCell(CreateCell(payment.ReservationId.ToString()));
                    table.AddCell(CreateCell(payment.Amount.ToString("C")));
                    table.AddCell(CreateCell(payment.PaymentMethod));
                    table.AddCell(CreateCell(payment.CreatedAt.ToString("g")));
                    table.AddCell(CreateCell(payment.Reservation?.User?.Email ?? "N/A"));
                    table.AddCell(CreateCell(payment.Reservation?.Chair?.Description ?? "N/A")); // Incluir el nombre de la silla
                }

                document.Add(table);
                document.Close();
                return ms.ToArray();
            }
        }

        private Cell CreateCell(string content, bool isHeader = false)
        {
            var cell = new Cell().Add(new Paragraph(content));
            if (isHeader)
            {
                cell.SetBackgroundColor(ColorConstants.LIGHT_GRAY);
                cell.SetBold();
                cell.SetTextAlignment(TextAlignment.CENTER);
            }
            else
            {
                cell.SetTextAlignment(TextAlignment.LEFT);
            }
            cell.SetPadding(5);
            cell.SetBorder(Border.NO_BORDER);
            return cell;
        }
    }
}

