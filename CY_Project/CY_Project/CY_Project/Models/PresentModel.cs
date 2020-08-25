using CY_Project.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CY_Project.Models
{
    public class PresentModel
    {
        string connstr;
        public PresentModel()
        {
            ConnectionModel c = new ConnectionModel();
            connstr = c.mssqlconn();
        }

        //串content
        public IEnumerable<PresentViewModel> GetEHRContent()
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "SELECT [tokenSentance] FROM [dbo].[t20190221_tokensentance]";
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
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

                                    TokenSentance = r.Field<string>("tokenSentance") ?? ""
                                };
                    mRet = query;
                }
            }
            return mRet;
        }

        //catch patient ID
        public PresentViewModel GetPatientID()
        {
           // IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select distinct RegisterID FROM [dbo].[t20190221_splitprocessing]";

            PresentViewModel vm = new PresentViewModel();

            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    adt.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        IEnumerable<SelectListItem> query = from r in dt.AsEnumerable()
                                                            select new SelectListItem
                                                            {
                                                                Value = r.Field<string>("RegisterID") ?? "",
                                                                Text = r.Field<string>("RegisterID") ?? ""
                                                            };
                        vm.RegisterID = query;
                        //var query = from r in dt.AsEnumerable()
                        //            select new PresentViewModel
                        //            {
                        //                RegisterID = r.Field<string>("RegisterID") ?? ""
                        //            };
                        //mRet = query;
                    }
                }
            }
            return vm;
        }

        //patient info data: SBP DBP HR RR...
        public IEnumerable<PresentViewModel> GetPatient()
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select *  from  [dbo].[pat2_Countine]";
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
                                    SBP = r.Field<string>("SBP1") ?? "",
                                    DBP = r.Field<string>("DBP1") ?? "",
                                    BT_Max = r.Field<string>("BT_Max") ?? "", //抓溫度最高
                                    BT_TA = r.Field<string>("BT(TA)") ?? "",
                                    BT_X = r.Field<string>("BT(X)") ?? "",
                                    HR = r.Field<string>("HR") ?? "",
                                    RR = r.Field<string>("RR") ?? "",
                                    Time13 = r.Field<string>("time13") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }

       

        //抓取醫療紀錄time
        public IEnumerable<PresentViewModel> GetMedRecord()
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select DATEDIFF(S,'1970-1-1 00:00:00',DateTime) as DateTime2, [Content] FROM [dbo].[t20190221] order by DateTime";
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
                                    MedRecord_Content = r.Field<string>("Content") ?? "",
                                    Time13 = r.Field<Int32>("DateTime2").ToString() ??""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }


        //catch medicine name
        public IEnumerable<PresentViewModel> GetMed()
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select distinct Med_Code from  [dbo].[pat2_med]";
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
                                    Med_Code = r.Field<string>("Med_Code") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }

        //catch detail of medicine info
        public IEnumerable<PresentViewModel> GetMed_Dormicum()
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select * from  [dbo].[pat2_med]  where [Med_Code] = 'Dormicum'";
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
                                    Med_Code = r.Field<string>("Med_Code") ?? "",
                                    Dose = r.Field<string>("dose") ?? "",
                                    Time13 = r.Field<string>("time13") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }

        public IEnumerable<PresentViewModel> GetMed_TARGO()
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select * from  [dbo].[pat2_med]  where [Med_Code] = 'MAXII'";
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
                                    Med_Code = r.Field<string>("Med_Code") ?? "",
                                    Dose = r.Field<string>("dose") ?? "",
                                    Time13 = r.Field<string>("time13") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }

        public IEnumerable<PresentViewModel> GetMed_MEPE2()
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select * from  [dbo].[pat2_med]  where [Med_Code] = 'NS5'";
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
                                    Med_Code = r.Field<string>("Med_Code") ?? "",
                                    Dose = r.Field<string>("dose") ?? "",
                                    Time13 = r.Field<string>("time13") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;

        }

        public IEnumerable<PresentViewModel> GetSBP_DBP()
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select * from  [dbo].[pat1]";
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
                                    SBP = r.Field<string>("SBP1") ?? "",
                                    DBP = r.Field<string>("DBP1") ?? "",
                                    BT_TA = r.Field<string>("BT(TA)") ?? "",

                                    Time13 = r.Field<string>("time13") ?? "",

                                };
                    mRet = query;
                }
            }
            return mRet;

        }

        public List<Dictionary<string, object>> GetData()
        {
            List<Dictionary<string, object>> mRet = new List<Dictionary<string, object>>();
            string SQL = "  select * from [dbo].[hospital]";
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

                        aSeries["DBP"] = dr["DBP1"];
                        aSeries["BW"] = dr["BW"];
                        aSeries["BT_TA"] = dr["BT(TA)"];
                        aSeries["BH"] = dr["BH"];
                        aSeries["SBP"] = dr["SBP1"]; 
                        aSeries["HR"] = dr["HR"];
                        aSeries["RR"] = dr["RR"];
                        aSeries["Date"] = dr["Date"];
                        aSeries["Time"] = dr["Time"];

                        mRet.Add(aSeries);
                    }
                }
            }
            return mRet;
        }


        public IEnumerable<PresentViewModel> ForTest(string SearchPatientID)
        {
            IEnumerable<PresentViewModel> mRet = Enumerable.Empty<PresentViewModel>();
            string SQL = "select * , replace(isnull((SELECT MAX(c) FROM  ( VALUES ( [BT€]), ( [BT(TA)]) ,([BT(X)])) AS cValues(c)),0),0,36.5) AS BT_Max from  [dbo].[pat2] "+
                "where RegisterID = @RegisterID";
            using (DataTable dt = new DataTable())
            {
                using (SqlConnection conn = new SqlConnection(connstr))
                using (SqlCommand cmd = new SqlCommand(SQL, conn))
                using (SqlDataAdapter adt = new SqlDataAdapter(cmd))
                {
                    conn.Open();
                    cmd.Parameters.Add("RegisterID", SqlDbType.VarChar).Value = SearchPatientID;
                    adt.Fill(dt);
                }
                if (dt.Rows.Count > 0)
                {
                    var query = from r in dt.AsEnumerable()
                                select new PresentViewModel
                                {
                                    SBP = r.Field<string>("SBP1") ?? "",
                                    DBP = r.Field<string>("DBP1") ?? "",
                                    BT_Max = r.Field<string>("BT_Max") ?? "", //抓溫度最高
                                    BT_TA = r.Field<string>("BT(TA)") ?? "",
                                    BT_X = r.Field<string>("BT(X)") ?? "",
                                    HR = r.Field<string>("HR") ?? "",
                                    RR = r.Field<string>("RR") ?? "",
                                    Time13 = r.Field<string>("time13") ?? ""

                                };
                    mRet = query;
                }
            }
            return mRet;


        }

    }
}