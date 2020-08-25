using CY_Project.Models;
using CY_Project.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CY_Project.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult index()
        {

            
            PresentModel p = new PresentModel();

            //IEnumerable<PresentViewModel> vm_PatientID = p.GetPatientID();
            //string jsonSeries_PatientID = JsonConvert.SerializeObject(vm_PatientID);
            //ViewBag.item_PatientID = jsonSeries_PatientID;

            IEnumerable<PresentViewModel> vm_Patient = p.GetPatient();
            string jsonSeries = JsonConvert.SerializeObject(vm_Patient);
            ViewBag.item = jsonSeries;

            IEnumerable<PresentViewModel> vm2 = p.GetMed_Dormicum();
            string jsonSeries2 = JsonConvert.SerializeObject(vm2);
            ViewBag.item2 = jsonSeries2;

            IEnumerable<PresentViewModel> vm3 = p.GetMed_TARGO();
            string jsonSeries3 = JsonConvert.SerializeObject(vm3);
            ViewBag.item3 = jsonSeries3;

            IEnumerable<PresentViewModel> vm4 = p.GetMed_MEPE2();
            string jsonSeries4 = JsonConvert.SerializeObject(vm4);
            ViewBag.item4 = jsonSeries4;

            IEnumerable<PresentViewModel> vm_MedRecord = p.GetMedRecord();
            string jsonSerie_MedRecord = JsonConvert.SerializeObject(vm_MedRecord);
            ViewBag.item_MedRecord = jsonSerie_MedRecord;

            IEnumerable<PresentViewModel> vm_med = p.GetMed();

            return View(vm_med);
        }

        //public ActionResult Test()
        //{
        //    PresentModel p = new PresentModel();         
        //    IEnumerable<PresentViewModel> vm_med = p.GetMed();

        //    return View(vm_med);
        //}

        //    public ActionResult SearchByMed(string med)
        //{
        //    string searchMed = null;
        //    if (!String.IsNullOrEmpty(searchMed))
        //    {
        //    using(Models.PresentModel p = new Models.PresentModel())
        //    {
        //        var result = (from s in p.GetMed()
        //                     where s.Med_Code == searchMed
        //                     select s).ToList();

        //        return View("Test", result);
        //    }
        //    }
        //    else
        //    {
        //        return View("Test", new List<Models.PresentModel>());
        //    }
            
        //}


        public ActionResult Test(string id)
        {
            PresentModel p = new PresentModel();
            PresentViewModel vm = new PresentViewModel();

            vm = p.GetPatientID();


            IEnumerable<PresentViewModel> vm_EHRContent = p.GetEHRContent();
            string jsonSeries = JsonConvert.SerializeObject(vm_EHRContent);
            ViewBag.item = jsonSeries;


            IEnumerable<PresentViewModel> vm3 = p.GetMed_TARGO();
            string jsonSeries3 = JsonConvert.SerializeObject(vm3);
            ViewBag.item3 = jsonSeries3;

            return View(vm);
        }

        public ActionResult SearchPatient(string SearchPatientID)
        {
            PresentModel p = new PresentModel();

            IEnumerable<PresentViewModel> vm = p.ForTest(SearchPatientID);
            string jsonSeries = JsonConvert.SerializeObject(vm);
            ViewBag.item = jsonSeries;

            IEnumerable<PresentViewModel> vm2 = p.GetMed_Dormicum();
            string jsonSeries2 = JsonConvert.SerializeObject(vm2);
            ViewBag.item2 = jsonSeries2;

            IEnumerable<PresentViewModel> vm3 = p.GetMed_TARGO();
            string jsonSeries3 = JsonConvert.SerializeObject(vm3);
            ViewBag.item3 = jsonSeries3;

            IEnumerable<PresentViewModel> vm4 = p.GetMed_MEPE2();
            string jsonSeries4 = JsonConvert.SerializeObject(vm4);
            ViewBag.item4 = jsonSeries4;

            IEnumerable<PresentViewModel> vm_MedRecord = p.GetMedRecord();
            string jsonSerie_MedRecord = JsonConvert.SerializeObject(vm_MedRecord);
            ViewBag.item_MedRecord = jsonSerie_MedRecord;

            //IEnumerable<PresentViewModel> vm_med = p.GetMed();

            return PartialView(vm); 
        }
    }
}