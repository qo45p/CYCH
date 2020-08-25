using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CY_Project.ViewModels
{
    public class PresentViewModel
    {
        public IEnumerable<SelectListItem> Test1 { get; set; }
        public IEnumerable<SelectListItem>  RegisterID { get; set; }

        public string TokenSentance { get; set; }
        public string DBP { get; set; }
        public string SBP { get; set; }

        public string BT_Max { get; set; }
        public string BT_TA { get; set; }
        public string BT_X { get; set; }
        public string RR { get; set; }
        public string HR { get; set; }
        public string Time13 { get; set; }
        public string Med_Code { get; set; }
        public string Dose { get; set; }
        public string MedRecord_Content { get; set; }
    }
}