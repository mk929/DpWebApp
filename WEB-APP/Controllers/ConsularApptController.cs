using DpWebApp;
using DpWebApp.BLL;
using DpWebApp.Models;
using DpWebApp.Models.Data;
using DpWebApp.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Routing;

namespace DpWebApp.Controllers
{
    public class ConsularApptController : Controller
    {
        private readonly string _formTile = "Visa Application Appointment Form";


        public ActionResult AgencyForm()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AgencyForm(AgencyAppointmentVM model)
        {
            return View(model);
        }
        

        // GET: ConsularAppt/Create
        public ActionResult Create(int Id)
        {
            ViewBag.Title = _formTile;
            if (Request.IsAuthenticated)
                ViewBag.Title = _formTile + " (Admin)";

            var model = new ConsularApptVM();
            model.RequesterType = Id;
            if (DpWebAppConfig.PrefilledFormTest)
                model = CATestData.GetNewAppt(Id);
            
            ViewBag.PartialHtml = this.GetPartialHtmlViewName(model.RequesterType);

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "RequesterType,TotalNumberOfApplications,AppointmentDate,AppointmentType,Name,Gender,DateOfBirth,PlaceOfBirth,Nationality,NRIC_No,PassportNumber,PassportIssuedDate,ContactAddr1,ContactAddr2,ContactPhone,ContactEmail,Note")] ConsularApptVM consularApptVM)
        {
            if (!Request.IsAuthenticated)
            {
                ViewBag.Title = _formTile;
                DpWebAppDb.AddConsularAppt(consularApptVM, DpWebAppConfig.QueueNumberInitial);

                Email email = this.GetConfirmationRequestEmail(consularApptVM);
                using (var smtp = new SmtpClient())
                {
                    smtp.Prep();
                    await smtp.SendMailAsync(email.Message);
                }

                ViewBag.PartialHtml = "_MsgApptReceived";
            }
            else
            {
                ViewBag.Title = _formTile + " (Admin)";
                consularApptVM.Note = "[ADMIN]" + consularApptVM.Note;
                DpWebAppDb.AddConsularAppt(consularApptVM, DpWebAppConfig.QueueNumberInitial);
                DateTime? confirmedApptDate = null;
                int? confirmedQueNumber = 0;
                ConsularApptVM consularApptVM2 = DpWebAppDb.ConfirmConsularAppt(consularApptVM.ID, consularApptVM.ActivationCode, ref confirmedApptDate, ref confirmedQueNumber);

                consularApptVM.QueueNumber = confirmedQueNumber.GetValueOrDefault();

                ViewBag.PartialHtml = "_MsgApptConfirmedByAdmin";
            }

            AppointmentType appointmentType = ConsularAppointmentTypes.GetAppointmentType(consularApptVM.AppointmentType);
            ViewBag.AppointmentType = appointmentType.Description;

            return View(consularApptVM);
        }

        // GET: ConsularAppt
        [Authorize]  /* For Testing Prefilled Forms */
        public ActionResult Index()
        {
            ViewBag.Title = _formTile + " (Admin)";

            ConsularApptVM model = CATestData.GetNewAppt(0);            
            ViewBag.PartialHtml = this.GetPartialHtmlViewName(model.RequesterType);
            return View(model);
        }

        [Authorize]  /* For Testing Prefilled Forms */
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "RequesterType,TotalNumberOfApplications,AppointmentDate,AppointmentType,Name,Gender,DateOfBirth,PlaceOfBirth,Nationality,NRIC_No,PassportNumber,PassportIssuedDate,ContactAddr1,ContactAddr2,ContactPhone,ContactEmail,Note")] ConsularApptVM consularApptVM)
        {
            if (!ModelState.IsValid)
                this.LogModelStateError();

            ViewBag.Title = _formTile + " (Admin)";

            consularApptVM.Note = "[ADMIN]" + consularApptVM.Note;
            DpWebAppDb.AddConsularAppt(consularApptVM, DpWebAppConfig.QueueNumberInitial);

            DateTime? confirmedApptDate = null;
            int? confirmedQueNumber = 0;
            ConsularApptVM consularApptVM2 = DpWebAppDb.ConfirmConsularAppt(consularApptVM.ID, consularApptVM.ActivationCode, ref confirmedApptDate, ref confirmedQueNumber);

            consularApptVM.QueueNumber = confirmedQueNumber.GetValueOrDefault();

            ViewBag.PartialHtml = "_MsgApptConfirmedByAdmin";

            AppointmentType appointmentType = ConsularAppointmentTypes.GetAppointmentType(consularApptVM.AppointmentType);
            ViewBag.AppointmentType = appointmentType.Description;

            return View(consularApptVM);
        }

        // GET: ConsularAppt/Confirmed/5
        [HttpGet]
        public ActionResult Confirmed(int id, string code)
        {
            ViewBag.ConfirmedId = id;
            ViewBag.ConfirmedCode = code;
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> ConfirmPosted(string confirmedId, string confirmedCode)
        {
            if (!Request.IsAjaxRequest())
                return Content("Request Error.");

            int id = int.Parse(confirmedId);
            string code = confirmedCode;

            DateTime? confirmedApptDate = null;
            int? confirmedQueNumber = 0;
            ConsularApptVM consularApptVM = DpWebAppDb.ConfirmConsularAppt(id, code, ref confirmedApptDate, ref confirmedQueNumber);
            if (consularApptVM == null)
            {
                return RedirectToAction("Error");
            }

            string confirmedEmailBody = this.GetConfirmedEmailBody(consularApptVM);
            string confirmationLetter = this.GetConfirmationLetter(consularApptVM);
            // To do: Maybe convert the confirmationLetter to PDF, or get fillable PDF letter 

            // Get Required Form Attachment List
            AppointmentType appointmentType = ConsularAppointmentTypes.GetAppointmentType(consularApptVM.AppointmentType);
            List<BLL.Attachment> formAttachments = appointmentType.Attachments?.List;

            // Start preparing for Email
            Email embassyMail = new Email
            {
                From = DpWebAppConfig.EmailAddress,
                DisplayName = DpWebAppConfig.EmailUser,
                To = consularApptVM.ContactEmail,
                Subject = DpWebAppConfig.EmailSubj_Confirmed,
                Body = confirmedEmailBody,
                IsHtml = true
            };

            // using (MemoryStream ms = new MemoryStream())
            string pdfTemplateFile = DpWebAppConfig.CnslrLtrPdfTmplPath + appointmentType.ConsularLtrPdfTmplFilename;

            using (MemoryStream ms = ConfirmationLetterPdf.GetAppointmentLetterStream(pdfTemplateFile, consularApptVM))
            using (MailMessage mailMsg = embassyMail.Message)
            using (var smtp = new SmtpClient())
            {

                System.Net.Mail.Attachment coverLetterPdf = Email.GetPdfAttachmentFromPdfStream(ms, "AppointmentLetter.pdf");
                // System.Net.Mail.Attachment coverLetterPdf = Email.GetPdfAttachmentFromHtmlString(confirmationLetter, ms, "AppointmentLetter.pdf");
                mailMsg.Attachments.Add(coverLetterPdf);

                if (formAttachments != null)
                {
                    foreach (var attachment in formAttachments)
                    {
                        if (System.IO.File.Exists(attachment.FullPath))
                        {
                            var emailAttachment = new System.Net.Mail.Attachment(attachment.FullPath, new ContentType(MediaTypeNames.Application.Pdf));
                            mailMsg.Attachments.Add(emailAttachment);
                        }
                    }
                }

                smtp.Prep();
                await smtp.SendMailAsync(mailMsg);
                // return View(consularApptVM);
                return PartialView("_MsgApptConfirmed", consularApptVM);
            }
        }
    }

}
