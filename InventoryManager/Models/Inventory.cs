using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace InventoryManager.Models
{
    public class Inventory
    {
        //public bool smtp { get; set; }

        [Key]
        public int ID { get; set; }
        public int SKU { get; set; }
        public string ItemName { get; set; }
        public int Quantity { get; set; }
        public string Location { get; set; }

        public int QuantityWarningLevel { get; set; }

        public int QuantityRefill { get; set; }

        public int QuantityBehavior { get; set; }

        //public void SendLowBalanceMail(string ProductName)
        //{
        //    string Body = "Hi, <br /> This is an automated mail from your system. <b>" + ProductName + "</b> is below minimum quantity in stock. Kindly note down and raise a purchase order for the same.<br />Thank You.";

        //    System.Net.Mail.SmtpClient smtpServer = new System.Net.Mail.SmtpClient("smtp.googlemail.com", 587);

        //    System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage(
        //    "AutoMail@InventoryManager.com", "inventory.manager@outlook.com", "Notification for low inventory.", Body);

        //    msg.IsBodyHtml = true;
        //    smtp.EnableSsl = true;
        //    smtpServer.Send(msg);
        //}
    }
}