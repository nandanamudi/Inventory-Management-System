using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace InventoryManager.Models
{
    public class MessageServices
    {

        public static void SendEmail(string email, string subj, string mess)
        {
            try
            {
                var fromAddress = new MailAddress("inventory.managersauce@gmail.com", "Inventory Manager");
                var toAddress = new MailAddress("inventory.manager@outlook.com", "Nick");
                const string subject = "Subject";
                const string fromPassword = "Firetruck55";

                var smtp = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
                };
                using (var message = new MailMessage(fromAddress, toAddress)
                {
                    Subject = subject,
                    Body = mess
                })
                {
                    smtp.Send(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}