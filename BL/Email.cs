using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Mail;
namespace BL
{
    public class Email
    {
        public static async Task<ML.Result> SendEmail(string nombreNewAdmin, string perfil, string UserName, string Password)
        {
            ML.Result result = new ML.Result();

            var body =
            "<p style='font-weight:bold;'>Que tal " + nombreNewAdmin + "</p>" +
            "<p>Has sido dado de alta dentro del portal de administración de encuestas <b>Diagnostic4U</b> bajo el perfil:</p>" +
            "<ul>   <li>" + perfil + "</li>    </ul>" +
            "<p>Tus claves de acceso son las siguientes: </p>" +
            "<p><b>Nombre de usuario: </b>" + UserName + "</p>" +
            "<p><b>Password: </b>" + Password + "</p></br>" +
            "<p>Accede entrando a: <a href='http://demo.climalaboral.divisionautomotriz.com/LogInAdmin/LogIn'><b>Diagnostic4U</b></a></p>" +
            "<p><img src='http://demo.climalaboral.divisionautomotriz.com/img/logo.png'></p></ br>" +
            "<small>Si ya cuenta con un perfil anterior a este tenga en cuenta que las claves de acceso son las mismas.</small>";
            var message = new MailMessage();
            message.To.Add(new MailAddress(UserName));
            //message.From = new MailAddress("jamurillo@grupoautofin.com");
            message.Subject = "Bienvenida a Diagnostic4U";
            message.Body = string.Format(body, "DIAGNOSTIC4U", "jamurillo@grupoautofin.com", "Aqui se envian  las claves de acceso al portal");
            message.IsBodyHtml = true;

            using (var smtp = new SmtpClient())
            {


                try
                {
                    await smtp.SendMailAsync(message);
                    result.Correct = true;
                }
                catch (Exception ex)
                {
                    var error = ex.Message;
                    result.Correct = false;
                }
                finally
                {
                    smtp.Dispose();
                }

                return result;
            }
        }
    }
}
