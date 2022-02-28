using System;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using BrownSugarBakes.UI.MVC.Models;

namespace BrownSugarBakes.UI.MVC.Controllers
{
    public class ContactController : Controller
    {
        
        public ActionResult Contact()
        {
            ViewBag.Message = "Your Contacts Page.";
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactViewModel cvm)
        {
            //When a class has validation (data annotations), that validation should be checked BEFORE
            //attempting to process data
            if (!ModelState.IsValid)
            {
                //send them back to the form, and pass the object that was created (cvm) back to the view as well.
                //This will populate all of the textboxes with whatever the user has entered
                return View(cvm);
            }//If we reach this point, we have passed validation, and are ready to send the email
            #region Send Email Functionality
            //step1) Build the email message body (content for the email)
            string message = $"You have received an email from {cvm.Name} with the subject - {cvm.Subject}. Please respond to {cvm.Email} with your response to the following message: <br><cite>{cvm.Message}</cite>";
            //Step 2) Create the MailMessage object, and customize
            MailMessage msg = new MailMessage(
                //FROM - your domain email (admin@yourdomain.com)
                "admin@stephent.net",
                //To - where the email lands (should be sent to your personal email
                "s.tate19th@gmail.com",
                //SUBJECT
                cvm.Subject,
                //BODY
                message
                );

            //allow HTML formatting
            msg.IsBodyHtml = true;
            msg.Priority = MailPriority.High;//can change priority
                                             //CC or BCC other recipients
            msg.CC.Add("s.tate19th@gmail.com");

            //Step 3) Create the smtpClient that will send the email
            //The client will need info from the host to route the email
            SmtpClient client = new SmtpClient("mail.stephent.net");
            client.Credentials = new NetworkCredential("admin@stephent.net", "Junior2017!");
            //client.Port = 8889;
            //Step 4) Attempt sending email
            try
            {
                client.Send(msg);
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = $"Sorry, something went wrong. Please try again later or review the stacktrace <br>{ex.StackTrace}";
                return View(cvm);
            }

            //If all goes well, and the email was able to send, we will send the user to a 
            //confirmation view
            return View("EmailConfirmation", cvm);
            #endregion

        }
    }
}