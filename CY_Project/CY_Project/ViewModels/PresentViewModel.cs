using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CY_Project.ViewModels
{
    public class PresentViewModel
    {
        public IEnumerable<SelectListItem> P_ID { get; set; }
        public string P_ID_Value { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string Age { get; set; }
        public string Birth { get; set; }
   
        public string DoseUnit { get; set; }
        public string DBP { get; set; }
        public string SBP { get; set; }      
        public string BT { get; set; }
        public string RR { get; set; }
        public string HR { get; set; }
        public string Time13 { get; set; }
        public string yyyyDate { get; set; }
        public string Dose { get; set; }
        public string MedRecord_Content { get; set; }
        public string TokenSentance { get; set; }
        public string MedPRS { get;set; }
        public string MedPRS_Name { get; set; }
    }

  
}