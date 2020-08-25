using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CY_Project.Models
{
    public class ConnectionModel
    {
        public string mssqlconn()
        {
            return @"Server=.\SQLEXPRESS;Database=master;Trusted_Connection=True";
            //return @"Data Source=.\SQLEXPRESS;Initial Catalog=CYCH;User ID=Hello;Password=1234";
        }
    }
}