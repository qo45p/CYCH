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
            //return @"Server=DESKTOP-DI3GBI4\SQLEXPRESS;Database=master;Trusted_Connection=True";
        }
    }
}