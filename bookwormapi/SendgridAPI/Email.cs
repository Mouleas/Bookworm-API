using SendGrid.Helpers.Mail;
using SendGrid;
using bookwormapi.Dao;
using System.Runtime.InteropServices.ComTypes;

namespace bookwormapi.SendgridAPI
{
    public class Email
    {
        public void sendOrderDetails(List<OrderItemsModelDao> orderItems)
        {
            var tableStyle = "border: 1px solid #dddddd;text-align: left;  padding: 8px;";

            string apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            Console.WriteLine(apiKey);
            if (apiKey != null)
            {
                try
                {
                    string email = "warmouleas@gmail.com";
                    var client = new SendGridClient(apiKey);
                    var from = new EmailAddress("warmouleas@gmail.com", "Your order details");
                    var subject = "Your Order got placed";
                    var to = new EmailAddress(email, "User");
                    float total = 0;
                    var plainTextContent = "";
                    var htmlContent =
                        "<table>" +
                            "<tr style='{tableStyle}'>" +
                                $"<th style='{tableStyle}'>Book name</th>" +
                                $"<th style='{tableStyle}'>Book Price</th>" +
                                $"<th style='{tableStyle}'>Book Quantity</th>"
                            + "</tr>";

                    foreach (var item in orderItems)
                    {
                        total += item.BookPrice * item.BookQuantity;
                        htmlContent +=
                            "<tr>" +
                                $"<td style='{tableStyle}'>{item.BookName}</td>" +
                                 $"<td style='{tableStyle}'>{item.BookPrice * item.BookQuantity}</td>" +
                                  $"<td style='{tableStyle}'>{item.BookQuantity}</td>"
                            + "</tr>";
                    }
                    htmlContent += "</table>";
                    htmlContent += $"<b>Grand total: {total}</b>";
                    var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                    DateTime sendDateTime = DateTime.Now;
                    msg.SendAt = new DateTimeOffset(sendDateTime).ToUnixTimeSeconds();
                    var response = client.SendEmailAsync(msg).Result;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                
            }
        }
    }
}
