using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Simon8029.EMPDemo.DeliveryEmail
{
    public class EmailHelper
    {
        public static void SendEmail(MailMessage message)
        {
            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;
            try
            {
                //smtp.Send(message);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}
