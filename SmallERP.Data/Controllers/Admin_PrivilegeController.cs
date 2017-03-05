using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.Data.Controllers
{
    public class Admin_PrivilegeController : AdminDataContext
    {
        public Admin_PrivilegeController()
            : base(Core.Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "Admin_PrivilegeController"; }
        }

        public List<Admin_Privilege> GetPrivileges(int _privilegeId, string _searchKey)
        {
            try
            {
                var _list = (from p in this.Admin_Privileges.Where(o => (o.Id == _privilegeId || _privilegeId == -1) &&
                                 o.Name.ToLower().Contains(_searchKey.ToLower()))
                             select p).ToList();
                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Add(Admin_Privilege _privilege)
        {
            try
            {
                this.Admin_Privileges.InsertOnSubmit(_privilege);
                this.SubmitChanges();
                return Core.Variable.DB_INSERT_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Edit(Admin_Privilege _privilege)
        {
            try
            {
                Admin_Privilege _o = this.Admin_Privileges.Single(o => o.Id == _privilege.Id);
                _o.Name = _privilege.Name;
                this.SubmitChanges();
                return Core.Variable.DB_UPDATE_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

    }
}
