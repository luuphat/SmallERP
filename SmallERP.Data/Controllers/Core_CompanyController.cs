using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core;

namespace SmallERP.Data.Controllers
{
    public class Core_CompanyController : CoreDataContext
    {
        public Core_CompanyController()
            : base(Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "Core_CompanyController"; }
        }

        public List<Core_Company> GetCompanies(string searchKey)
        {
            try
            {
                var _list = (from c in this.Core_Companies
                             select c).ToList();
                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
            }
        }
    
    }
}
