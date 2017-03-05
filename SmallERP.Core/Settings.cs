using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmallERP.Core
{
    public class Settings
    {
        public static string ConnectionString
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["ConnectionStringSmallERP"];
            }                
        }

        public static string LogSeverity
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["LogSeverity"];
            }
        }
    }
}
