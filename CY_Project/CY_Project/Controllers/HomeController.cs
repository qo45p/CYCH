using CY_Project.Models;
using CY_Project.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CY_Project.Controllers
{
    public class HomeController : Controller
    {
        //搜尋病患頁面
        public ActionResult SearchPatient()
        {
            PresentViewModel vm = new PresentViewModel();
            PresentModel p = new PresentModel();
            vm = p.GetDropList();
            return View(vm);
        }
        //接收病患頁面 Submit 的資料
        [HttpPost]
        public ActionResult SearchPatient(PresentViewModel vm )
        {
            Session["PatientID"] = vm.P_ID_Value; //紀錄patientID
            PresentModel p = new PresentModel();

            //病患資料
            IEnumerable<PresentViewModel> getPatientData = p.GetP_ID_Value(vm);
            string jsonSeries = JsonConvert.SerializeObject(getPatientData);
            TempData["item"] = jsonSeries; //要跨Action要使用TempData才能記住資料

            //病患藥物劑量
            List<Dictionary<string, object>> mRet_Dose = new List<Dictionary<string, object>>();
            mRet_Dose = p.GetMedDose(vm);   

            List<Dictionary<string, object>> samedateDose = new List<Dictionary<string, object>>();           
            for (int i = 0; i < mRet_Dose.Count; i++)
            {   
                if (samedateDose.Count == 0 )
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    //拿MedPRS的時間
                    dict.Add("MedPRS", mRet_Dose[i]["MedPRS"]);
                    dict.Add("Time13", mRet_Dose[i]["Time13"]);
                    dict.Add("Dose", mRet_Dose[i]["Dose"]);
                    dict.Add("DoseUnit", mRet_Dose[i]["DoseUnit"]);
                    dict.Add("MedPRS_Name", mRet_Dose[i]["MedPRS_Name"]);
                    samedateDose.Add(dict);
                }
                else
                {
                    var mRet_DoseCount = samedateDose.Count;
                    bool IsExist = false;
                    for (int n = 0; n < mRet_DoseCount; n++)
                    {                                           
                        if (samedateDose[n].ContainsValue(mRet_Dose[i]["MedPRS"]) && samedateDose[n].ContainsValue(mRet_Dose[i]["Time13"]))
                        {
                            IsExist = true;
                            samedateDose[n]["Dose"] = float.Parse(samedateDose[n]["Dose"].ToString()) + float.Parse(mRet_Dose[i]["Dose"].ToString());
                            break;
                        }
                        else
                        {
                            IsExist = false;
                        }                        
                    }

                    if (IsExist == false)
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        //拿MedPRS的時間
                        dict.Add("MedPRS", mRet_Dose[i]["MedPRS"]);
                        dict.Add("Time13", mRet_Dose[i]["Time13"]);
                        dict.Add("Dose", mRet_Dose[i]["Dose"]);
                        dict.Add("DoseUnit", mRet_Dose[i]["DoseUnit"]);
                        dict.Add("MedPRS_Name", mRet_Dose[i]["MedPRS_Name"]);
                        samedateDose.Add(dict);
                    }
                }                                                            
            }
            string jsonSeries_mRet_Dose = JsonConvert.SerializeObject(samedateDose);
            TempData["item_MedDose"] = jsonSeries_mRet_Dose;

            //病歷時間
            List<string> vm_medTime = p.GetMedTime(vm);
            string medTimeStr = "";
            for (int i = 0; i < vm_medTime.Count; i++)
            {
                if (i == 0)
                {
                    medTimeStr = "[ { x:" + vm_medTime[i] + ", title:'R' }";
                }
                else if (i == (vm_medTime.Count - 1))
                {
                    medTimeStr = medTimeStr + ",{ x:" + vm_medTime[i] + ", title:'R' }]";
                }
                else
                {
                    medTimeStr = medTimeStr + ",{ x:" + vm_medTime[i] + ", title:'R'}";
                }
            }
            TempData["item_medTime"] = medTimeStr;

            return RedirectToAction("Index"); //將頁面導到 Index_0430TesT
        } 


        //病患資料主畫面
        public ActionResult Index()
        {
            ViewBag.item = TempData["item"].ToString();
            ViewBag.item_MedDose = TempData["item_MedDose"].ToString();
            ViewBag.item_medTime = TempData["item_medTime"].ToString();
            ViewBag.P_ID = Session["PatientID"];

            PresentModel p = new PresentModel();

            //抓側邊欄藥物名稱          
            IEnumerable<PresentViewModel> vm_med = p.GetMed(Session["PatientID"].ToString());           

            return View(vm_med);
        }
        //選擇藥物 refresh
        [HttpPost]
        public ActionResult Index(IEnumerable<string> chbx) 
        {
            ViewBag.P_ID = Session["PatientID"];
            PresentModel p = new PresentModel();

            //查詢選擇的藥物劑量          
            List<Dictionary<string, object>> mRet_Dose = new List<Dictionary<string, object>>();
            mRet_Dose = p.GetChecklistMed(chbx, Session["PatientID"].ToString());

            List<Dictionary<string, object>> samedateDose = new List<Dictionary<string, object>>();
            for (int i = 0; i < mRet_Dose.Count; i++)
            {
                if (samedateDose.Count == 0)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    //拿MedPRS的時間
                    dict.Add("MedPRS", mRet_Dose[i]["MedPRS"]);
                    dict.Add("Time13", mRet_Dose[i]["Time13"]);
                    dict.Add("Dose", mRet_Dose[i]["Dose"]);
                    dict.Add("DoseUnit", mRet_Dose[i]["DoseUnit"]);
                    dict.Add("MedPRS_Name", mRet_Dose[i]["MedPRS_Name"]);
                    samedateDose.Add(dict);
                }
                else
                {
                    var mRet_DoseCount = samedateDose.Count;
                    bool IsExist = false;
                    for (int n = 0; n < mRet_DoseCount; n++)
                    {
                        if (samedateDose[n].ContainsValue(mRet_Dose[i]["MedPRS"]) && samedateDose[n].ContainsValue(mRet_Dose[i]["Time13"]))
                        {
                            IsExist = true;
                            samedateDose[n]["Dose"] = float.Parse(samedateDose[n]["Dose"].ToString()) + float.Parse(mRet_Dose[i]["Dose"].ToString());
                            break;
                        }
                        else
                        {
                            IsExist = false;
                        }
                    }

                    if (IsExist == false)
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        //拿MedPRS的時間
                        dict.Add("MedPRS", mRet_Dose[i]["MedPRS"]);
                        dict.Add("Time13", mRet_Dose[i]["Time13"]);
                        dict.Add("Dose", mRet_Dose[i]["Dose"]);
                        dict.Add("DoseUnit", mRet_Dose[i]["DoseUnit"]);
                        dict.Add("MedPRS_Name", mRet_Dose[i]["MedPRS_Name"]);
                        samedateDose.Add(dict);
                    }
                }
            }
            string jsonSeries_mRet_Dose = JsonConvert.SerializeObject(samedateDose);
            ViewBag.item_MedDose = jsonSeries_mRet_Dose;


            /*以下是病患基本數值會用到的參數*/
            IEnumerable<PresentViewModel> getPatientData = p.GetP_ID_Value_Session(Session["PatientID"].ToString());
            string jsonSeries = JsonConvert.SerializeObject(getPatientData);
            ViewBag.item = jsonSeries;

            //抓側邊欄藥物名稱  
            IEnumerable<PresentViewModel> vm_med = p.GetMed(Session["PatientID"].ToString());

            //病歷時間
            List<string> vm_medTime = p.GetMedTime_Session(Session["PatientID"].ToString());
            string medTimeStr = "";
            for (int i = 0; i < vm_medTime.Count; i++)
            {
                if (i == 0)
                {
                    medTimeStr = "[ { x:" + vm_medTime[i] + ", title:'R' }";
                }
                else if (i == (vm_medTime.Count - 1))
                {
                    medTimeStr = medTimeStr + ",{ x:" + vm_medTime[i] + ", title:'R' }]";
                }
                else
                {
                    medTimeStr = medTimeStr + ",{ x:" + vm_medTime[i] + ", title:'R'}";
                }
            }
            ViewBag.item_medTime = medTimeStr;

            return View(vm_med);
        }
        //病歷內容呈現 ParticalView
        [HttpPost]
        public ActionResult SearchTime(string data)
        {
            ViewBag.SearchTime = data;
            
            PresentModel p = new PresentModel();
            IEnumerable<PresentViewModel> vm = p.GetTime(data, Session["PatientID"].ToString());

            

            return PartialView(vm);            
        }

       

        //For TEST
        public ActionResult IndexTest()
        {
            PresentModel p = new PresentModel();

            //病患資料
            IEnumerable<PresentViewModel> getPatientData = p.GetPIDTest();
            string jsonSeries = JsonConvert.SerializeObject(getPatientData);
            ViewBag.item = jsonSeries; //要跨Action要使用TempData才能記住資料

            //病患藥物劑量
            List<Dictionary<string, object>> mRet_Dose = new List<Dictionary<string, object>>();
            mRet_Dose = p.GetMedDoseTest();

            List<Dictionary<string, object>> samedateDose = new List<Dictionary<string, object>>();
            for (int i = 0; i < mRet_Dose.Count; i++)
            {
                if (samedateDose.Count == 0)
                {
                    Dictionary<string, object> dict = new Dictionary<string, object>();
                    //拿MedPRS的時間
                    dict.Add("MedPRS", mRet_Dose[i]["MedPRS"]);
                    dict.Add("Time13", mRet_Dose[i]["Time13"]);
                    dict.Add("Dose", mRet_Dose[i]["Dose"]);
                    dict.Add("DoseUnit", mRet_Dose[i]["DoseUnit"]);
                    dict.Add("MedPRS_Name", mRet_Dose[i]["MedPRS_Name"]);
                    samedateDose.Add(dict);
                }
                else
                {
                    var mRet_DoseCount = samedateDose.Count;
                    bool IsExist = false;
                    for (int n = 0; n < mRet_DoseCount; n++)
                    {
                        if (samedateDose[n].ContainsValue(mRet_Dose[i]["MedPRS"]) && samedateDose[n].ContainsValue(mRet_Dose[i]["Time13"]))
                        {
                            IsExist = true;
                            samedateDose[n]["Dose"] = float.Parse(samedateDose[n]["Dose"].ToString()) + float.Parse(mRet_Dose[i]["Dose"].ToString());
                            break;
                        }
                        else
                        {
                            IsExist = false;
                        }
                    }

                    if (IsExist == false)
                    {
                        Dictionary<string, object> dict = new Dictionary<string, object>();
                        //拿MedPRS的時間
                        dict.Add("MedPRS", mRet_Dose[i]["MedPRS"]);
                        dict.Add("Time13", mRet_Dose[i]["Time13"]);
                        dict.Add("Dose", mRet_Dose[i]["Dose"]);
                        dict.Add("DoseUnit", mRet_Dose[i]["DoseUnit"]);
                        dict.Add("MedPRS_Name", mRet_Dose[i]["MedPRS_Name"]);
                        samedateDose.Add(dict);
                    }
                }
            }
            string jsonSeries_mRet_Dose = JsonConvert.SerializeObject(samedateDose);
            ViewBag.item_MedDose = jsonSeries_mRet_Dose;

            //病歷時間
            List<string> vm_medTime = p.GetMedTimeTest();
            string medTimeStr = "";
            for (int i = 0; i < vm_medTime.Count; i++)
            {
                if (i == 0)
                {
                    medTimeStr = "[ { x:" + vm_medTime[i] + ", title:'R' }";
                }
                else if (i == (vm_medTime.Count - 1))
                {
                    medTimeStr = medTimeStr + ",{ x:" + vm_medTime[i] + ", title:'R' }]";
                }
                else
                {
                    medTimeStr = medTimeStr + ",{ x:" + vm_medTime[i] + ", title:'R'}";
                }
            }
            ViewBag.item_medTime = medTimeStr;

            Session["PatientID"] = "321";
            ViewBag.P_ID = Session["PatientID"];

            //抓側邊欄藥物名稱   
            var teststr = "321";
            IEnumerable<PresentViewModel> vm_med = p.GetMed(teststr);


            return View(vm_med);
        }

      

    }
}