using DpWebApp.BLL;
using DpWebApp.Models;
using DpWebApp.Models.Data;
using DpWebApp.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DpWebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index(string passportNumber, DateTime? appointmentDate)
        {
            List<ConsularApptVM> list = DpWebApp.Models.Data.DpWebAppDb.GetConsularApptsAdmin(
                passportNumber, appointmentDate);
            return View(list);
        }
        
        // GET: Admin/PrintApptLtr/5
        public ActionResult PrintApptLtr(int id)
        {
            ConsularApptVM model = DpWebAppDb.GetConsularApptById(id);

            AppointmentType appointmentType = ConsularAppointmentTypes.GetAppointmentType(model.AppointmentType);
            string pdfTemplateFile = DpWebAppConfig.CnslrLtrPdfTmplPath + appointmentType.ConsularLtrPdfTmplFilename;
            MemoryStream pdfStream = ConfirmationLetterPdf.GetAppointmentLetterStream(pdfTemplateFile, model);

            //string confirmationLetter = this.GetConfirmationLetter(id);
            //MemoryStream pdfStream = new MemoryStream();
            //Pdf.WriteHtmlToPdfStream(confirmationLetter, pdfStream);

            return new FileStreamResult(pdfStream, "application/pdf");
        }
        // GET: ConsularAppt/Details/5
        public ActionResult Details(int id)
        {
            ConsularApptVM model = DpWebAppDb.GetConsularApptById(id);
            AppointmentType appointmentType = ConsularAppointmentTypes.GetAppointmentType(model.AppointmentType);
            ViewBag.AppointmentType = appointmentType.Description;

            return View(model);
        }
    }    
}