using CY_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Documents;

namespace CY_Project.Models
{
    public class PresentModel
    {
        string connstr;
        //連線資料庫
        public PresentModel()
        {
            ConnectionModel c = new ConnectionModel();
            connstr = c.mssqlconn();
        }

       
        //下拉式選單
        public PresentViewModel GetDropList()
        {
            PresentViewModel vm = new PresentViewModel();
            string SQL = " select distinct PID from [CYCH].[dbo].[allPatientData] order by PID asc";

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    IEnumerable<SelectListItem> query = from r in dt.AsEnumerable()
                                                        select new SelectListItem
                                                        {
                                                            Value = r.Field<string>("PID") ?? "",
                                                            Text = r.Field<string>("PID") ?? "",

                                                        };
                    vm.P_ID = query;
                }
            }
            return vm;

        }

        //查詢選擇的值:病患的基本數值
        public IEnumerable<PresentViewModel> GetP_ID_Value(PresentViewModel vm)
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string PatientIDstr;
            PatientIDstr = "PID = '" + vm.P_ID_Value + "'";
            string SQL = "select Time13, BT, HR, SBP, DBP, RR  from [CYCH].[dbo].[allPatientData] where " + PatientIDstr;
          
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    var query = from r in dt.AsEnumerable()
                                select new PresentViewModel
                                {
                                    
                                    SBP = r.Field<string>("SBP") ?? "null",
                                    DBP = r.Field<string>("DBP") ?? "null",                                 
                                    BT = r.Field<string>("BT") ?? "null",
                                    HR = r.Field<string>("HR") ?? "null",
                                    RR = r.Field<string>("RR") ?? "null",
                                    Time13 = r.Field<string>("Time13") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }

        //藥物劑量:接SearchPatient選擇的PatientID
        public List<Dictionary<string, object>> GetMedDose(PresentViewModel vm)
        {
            List<Dictionary<string, object>> mRet = new List<Dictionary<string, object>>();
            string PatientIDstr = "PID = '" + vm.P_ID_Value + "'";
            string SQL = "  select MedPRS, doseUnit, RealDose, Time13, PID, 學名 from [CYCH].[dbo].[MedPRS_Name] where "
                        + PatientIDstr + "  order by MedPRS, Time13 asc ";
           

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Dictionary<string, object> aSeries = new Dictionary<string, object>();

                        aSeries["MedPRS"] = dr["MedPRS"];                      
                        aSeries["Time13"] = dr["Time13"];
                        aSeries["Dose"] = dr["RealDose"];
                        aSeries["DoseUnit"] = dr["doseUnit"];
                        aSeries["MedPRS_Name"] = dr["學名"];

                        mRet.Add(aSeries);
                    }
                }
            }
            return mRet;

        }
        //病歷時間點
        public List<string> GetMedTime(PresentViewModel vm)
        {
            List<string> mRet = new List<string>();
            string PatientIDstr = "";
            PatientIDstr = "PID ='" + vm.P_ID_Value +"'";
            string SQL = "SELECT distinct [DateTime] FROM [CYCH].[dbo].[t20190221_tokensentance] where " + PatientIDstr;

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            mRet.Add(row[column].ToString());
                        }
                    }
                }
            }
            return mRet;

        }


        //查詢選擇的值:病患的基本數值 Session丟資料
        public IEnumerable<PresentViewModel> GetP_ID_Value_Session(string str)
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string PatientIDstr;
            PatientIDstr = "PID = '" + str + "'";
            string SQL = "select Time13, BT, HR, SBP, DBP, RR  from [CYCH].[dbo].[allPatientData] where " + PatientIDstr;

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    var query = from r in dt.AsEnumerable()
                                select new PresentViewModel
                                {

                                    SBP = r.Field<string>("SBP") ?? "null",
                                    DBP = r.Field<string>("DBP") ?? "null",
                                    BT = r.Field<string>("BT") ?? "null",
                                    HR = r.Field<string>("HR") ?? "null",
                                    RR = r.Field<string>("RR") ?? "null",
                                    Time13 = r.Field<string>("Time13") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }
        //抓取側邊欄藥物名(不重複)
        public IEnumerable<PresentViewModel> GetMed(string str)
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string PatientIDstr = " PID = '" + str + "'";
            string SQL = "select distinct MedPRS from  [CYCH].[dbo].[MedPRS_Name] where " + PatientIDstr;

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    var query = from r in dt.AsEnumerable()
                                select new PresentViewModel
                                {

                                    MedPRS = r.Field<string>("MedPRS") ?? "",                                 

                                };
                    mRet = query;
                }
            }
            return mRet;

        }     
        //左方欄位抓到藥物名稱後，進入資料庫搜尋劑量和時間，使用IEnumerable<string>接資料
        public List<Dictionary<string, object>> GetChecklistMed(IEnumerable<string> Data,string Pstr)
        {
            List<Dictionary<string, object>> mRet = new List<Dictionary<string, object>>();
            string sqlMedStr = "";
            string PatientIDstr = "PID = '" + Pstr + "'";
            string SQL = "";

            if (Data is null)
            {
                 SQL = "  select MedPRS, doseUnit, RealDose, Time13, Pat_NO, 學名 from [CYCH].[dbo].[MedPRS_Name] " +
                " where " + PatientIDstr  +
                " order by MedPRS, Time13 asc ";
            }
            else
            {
                foreach (var item in Data)
                {
                    if ( item != "false")
                    {
                        sqlMedStr += " MedPRS = '" + item + "' or";
                    }              
                }
                sqlMedStr = sqlMedStr.Remove(sqlMedStr.Length - 2); //  remove 'or' characters
               
                 SQL = "  select MedPRS, doseUnit, RealDose, Time13, Pat_NO, 學名 from [CYCH].[dbo].[MedPRS_Name] " +
                " where " + PatientIDstr + " and (" + sqlMedStr + ")" +
                " order by MedPRS, Time13 asc ";
            }
            

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Dictionary<string, object> aSeries = new Dictionary<string, object>();

                        aSeries["MedPRS"] = dr["MedPRS"];
                        aSeries["Time13"] = dr["Time13"];
                        aSeries["Dose"] = dr["RealDose"];
                        aSeries["DoseUnit"] = dr["doseUnit"];
                        aSeries["MedPRS_Name"] = dr["學名"];

                        mRet.Add(aSeries);
                    }
                }
            }

            return mRet;
        }

        //抓取病歷內容
        public IEnumerable<PresentViewModel> GetTime(string dataTime,string Pstr)
        {
            

            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string PatientIDstr = Pstr;
            string SQL = "select * from [CYCH].[dbo].[t20190221_tokensentance] where PID = '" + PatientIDstr + "' and DateTime = '" + dataTime + "'"; //80005 = F
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand (SQL,conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if(dt.Rows.Count > 0)
                {
                    var query = from r in dt.AsEnumerable()
                                select new PresentViewModel
                                {
                                    TokenSentance = r.Field<string>("tokenSentance") ?? "",
                                    yyyyDate = r.Field<DateTime>("yyyyDate").ToString() ??""
                                };
                    mRet = query;
                }              
            }

            return mRet;
        }

        //病歷時間點:Session
        public List<string> GetMedTime_Session(string str)
        {
            List<string> mRet = new List<string>();
            string PatientIDstr = "";
            PatientIDstr = "PID ='" + str + "'";
            string SQL = "SELECT distinct [DateTime] FROM [CYCH].[dbo].[t20190221_tokensentance] where " + PatientIDstr;

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            mRet.Add(row[column].ToString());
                        }
                    }
                }
            }
            return mRet;

        }



        // ===============看上面的就好==============

        //測試用
        //抓取medRecord Time
        public List<string> GetMedTimeTest()
        {
            List<string> mRet = new List<string>();
            //IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "SELECT distinct [DateTime] FROM [CYCH].[dbo].[t20190221_tokensentance] where PID ='321'"; //Register = 80001

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn column in dt.Columns)
                        {
                            mRet.Add(row[column].ToString());                            
                        }
                    }                 
                }
            }
            return mRet;

        }


        //病患的基本數值
        public IEnumerable<PresentViewModel> GetPIDTest()
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select Time13, BT, HR, SBP, DBP, RR  from [CYCH].[dbo].[allPatientData] where PID='321'";
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    var query = from r in dt.AsEnumerable()
                                select new PresentViewModel
                                {
                                   
                                    SBP = r.Field<string>("SBP") ?? "null",
                                    DBP = r.Field<string>("DBP") ?? "null",                                
                                    BT = r.Field<string >("BT") ?? "null",
                                    HR = r.Field<string>("HR") ?? "null",
                                    RR = r.Field<string>("RR") ?? "null",
                                    Time13 = r.Field<string>("Time13") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }

        //藥物劑量改寫版本 FOR Index TEST
        public List<Dictionary<string, object>> GetMedDoseTest()
        {
            List<Dictionary<string, object>> mRet = new List<Dictionary<string, object>>();

            string SQL = "  select MedPRS, doseUnit, RealDose, Time13, PID, 學名 from [CYCH].[dbo].[MedPRS_Name] where PID = '321'  order by MedPRS, Time13 asc ";

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        Dictionary<string, object> aSeries = new Dictionary<string, object>();

                        aSeries["MedPRS"] = dr["MedPRS"];
                        aSeries["Time13"] = dr["Time13"];
                        aSeries["Dose"] = dr["RealDose"];
                        aSeries["DoseUnit"] = dr["doseUnit"];
                        aSeries["MedPRS_Name"] = dr["學名"];

                        mRet.Add(aSeries);
                    }
                }
            }
            return mRet;

        }

        //抓取病患對照名字&其藥物名單(目前沒用到0702)
        public IEnumerable<PresentViewModel> GetPatientName(PresentViewModel vm)
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string PatientIDstr = "";
            PatientIDstr = "PatientID = '" + vm.P_ID_Value +"'" ;
            string SQL = "select distinct * from  [masterFakeData].[dbo].[藥物劑量0409] where " + PatientIDstr;
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    var query = from r in dt.AsEnumerable()
                                select new PresentViewModel
                                {
                                    
                                    Name = r.Field<string>("Name") ?? "",
                                    Gender = r.Field<string>("Gender") ?? "",
                                    Age = r.Field<string>("Age") ?? "",
                                    //Birth = r.Field<string>("Birth") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }
      
    }

}