using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestGitHub_DataAccessLayer_
{
 
        public static class clsDataAccessSettings
        {
            public static string DatabaseName;
            public static string UserID;
            public static string Password;
            public static string ConnectionString => $"Server=.;Database={DatabaseName};User Id={UserID};Password={Password};";

            // public static string ConnectionString = "Server=.;Database=CarRental;User Id=admin;Password=mode2010;";


        }

    
}
