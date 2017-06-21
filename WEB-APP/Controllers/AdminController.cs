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
using DpWebAppData;
using System.Web.Mvc.Html;

namespace DpWebApp.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        // GET: Admin
        [AllowAnonymous]
        public ActionResult AgentManager()
        {
            AgentViewModel vm = new AgentViewModel();
            vm.HandleRequest();
            return View(vm);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult AgentManager(AgentViewModel vm)
        {
            vm.HandleRequest();
            ModelState.Clear();
            return View(vm);
        }

        public ActionResult Index(string passportNumber, DateTime? appointmentDate)
        {
            List<ConsularApptVM> list = DpWebAppDb.GetConsularApptsAdmin(passportNumber, appointmentDate);
            if (list.Count > 0)
            {
                ViewBag.DownloadCsv = true;
                ViewBag.PassportNumber = passportNumber;
                ViewBag.AppointmentDate = appointmentDate;
            }
            else
            {
                ViewBag.DownloadCsv = false;
            }

            return View(list);
        }

        public ActionResult GetCSV(string ppNum, DateTime? apptDt)
        {
            List<ConsularApptVM> list = DpWebAppDb.GetConsularApptsAdmin(ppNum, apptDt);
            if (list.Count <= 0)
                return new EmptyResult();

            string csvString = DpWebAppDb.GetConsularApptsAdminCSV(list, ',');
            string csvFileName = String.Format("Appointments_{0}{1}.csv", apptDt == null ? "All" : apptDt.Value.ToString("yyyyMMdd"), string.IsNullOrEmpty(ppNum) ? string.Empty : ppNum);

            return File(new System.Text.UTF8Encoding().GetBytes(csvString), "text/csv", csvFileName);
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