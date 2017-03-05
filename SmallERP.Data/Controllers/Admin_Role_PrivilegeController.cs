using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.Data.Controllers
{
    public class Admin_Role_PrivilegeController : AdminDataContext
    {
        public Admin_Role_PrivilegeController()
            : base(Core.Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "Admin_Role_PrivilegeController"; }
        }

        public List<Admin_Role_Privilege> GetRolePrivileges(int _roleId, int _privilegeId)
        {
            try
            {
                var _list = (from _rp in this.Admin_Role_Privileges.Where(o => (o.RoleId == _roleId || _roleId == -1) &&
                                 (o.PrivilegeId == _privilegeId || _privilegeId == -1))
                             select _rp).ToList();
                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Add(Admin_Role_Privilege _rp)
        {
            try
            {
                this.Admin_Role_Privileges.InsertOnSubmit(_rp);
                this.SubmitChanges();
                return Core.Variable.DB_INSERT_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Remove(Admin_Role_Privilege _rp)
        {
            try
            {
                this.Admin_Role_Privileges.DeleteOnSubmit(_rp);
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
