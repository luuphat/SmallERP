using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.Data.Controllers
{
    public class Admin_RoleController : AdminDataContext
    {
        public Admin_RoleController()
            : base(Core.Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "Admin_RoleController"; }
        }

        public List<Admin_Role> GetRoles(int _roleId, bool _isActive, string _searchKey)
        {
            try
            {
                var _list = (from r in this.Admin_Roles.Where(o => (o.Id == _roleId || _roleId ==-1) &&
                                 o.Name.ToLower().Contains(_searchKey.ToLower()))
                             select r).ToList();
                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Add(Admin_Role _role)
        {
            try
            {
                this.Admin_Roles.InsertOnSubmit(_role);
                this.SubmitChanges();
                return Core.Variable.DB_INSERT_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Edit(Admin_Role _role)
        {
            try
            {
                Admin_Role _o = this.Admin_Roles.Single(o => o.Id == _role.Id);
                _o.IsActive = _role.IsActive;
                _o.Name = _role.Name;
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
