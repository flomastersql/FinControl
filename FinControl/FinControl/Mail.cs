using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;
using System.Threading;

namespace FinControl
{
    class Mail
    {

        public static void send_msg(string msg)
        {
            if (msg == "") { msg = "<b>Незаведенных обязательств не обнаружено</b>"; }

            send_mail("Финансовая дисциплина", msg, "Andrei.Sorokin@goodfood-dv.ru");
            //send_mail("Финансовая дисциплина", msg, "@goodfood-dv.ru");

        }


        static void send_mail(string subj, string body, string mail_to)
        {

            // отправитель - устанавливаем адрес и отображаемое в письме имя
            MailAddress from = new MailAddress("info@goodfood-dv.ru");
            // кому отправляем
            MailAddress to = new MailAddress(mail_to);
            // создаем объект сообщения
            MailMessage m = new MailMessage(from, to);
            // тема письма
            m.Subject = subj;
            // текст письма
            m.Body = body;
            // письмо представляет код html
            m.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient("mb.goodfood-dv.ru", 587);
            // логин и пароль
            smtp.Credentials = new NetworkCredential(@"PRIMINVESTOR\info", "Efo5By5uir");
            //smtp.EnableSsl = true;
            smtp.Send(m);

            Thread.Sleep(1000);

        }
    }
}
