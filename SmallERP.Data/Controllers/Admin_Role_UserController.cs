using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.Data.Controllers
{
    public class Admin_Role_UserController : AdminDataContext
    {
        public Admin_Role_UserController()
            : base(Core.Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "Admin_Role_UserController"; }
        }

        public List<Admin_Role_User> GetRoleUsers(int _roleId, int _userId)
        {
            try
            {
                var _list = (from _ru in this.Admin_Role_Users.Where(o => (o.RoleId == _roleId || _roleId == -1) &&
                                 (o.UserId == _userId || _userId ==-1))
                             select _ru).ToList();
                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Add(Admin_Role_User _ru)
        {
            try
            {
                this.Admin_Role_Users.InsertOnSubmit(_ru);
                this.SubmitChanges();
                return Core.Variable.DB_INSERT_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Remove(Admin_Role_User _ru)
        {
            try
            {
                this.Admin_Role_Users.DeleteOnSubmit(_ru);
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
