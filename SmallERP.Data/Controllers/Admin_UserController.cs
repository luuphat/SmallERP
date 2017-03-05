using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.Data.Controllers
{
    public class Admin_UserController : AdminDataContext
    {
        public Admin_UserController()
            : base(Core.Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "Admin_UserController"; }
        }

        public List<Admin_User> GetUsers(int userId, string searchKey)
        {
            try
            {
                var _list = (from u in this.Admin_Users.Where(o => (o.Id == userId || userId == -1) &&
                                 (o.Firstname.ToLower().Contains(searchKey.ToLower()) ||
                                 o.Lastname.ToLower().Contains(searchKey.ToLower()) || 
                                 o.UserName.ToLower().Contains(searchKey.ToLower())))
                             select u).ToList();
                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName + _ex.Message, _ex.InnerException);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Add(Admin_User _user)
        {
            try
            {
                this.Admin_Users.InsertOnSubmit(_user);
                this.SubmitChanges();
                return Core.Variable.DB_INSERT_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName + _ex.Message, _ex.InnerException);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Edit(Admin_User _user)
        {
            try
            {
                Admin_User _o = this.Admin_Users.Single(o => o.Id == _user.Id);
                _o.Firstname = _user.Firstname;
                _o.Lastname = _user.Lastname;
                _o.FullName = _user.FullName;
                _o.Email = _user.Email;
                _o.PhoneNo = _user.PhoneNo;
                this.SubmitChanges();
                return Core.Variable.DB_UPDATE_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName + _ex.Message, _ex.InnerException);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int ChangePassword(int _id, string _username, string _password)
        {
            try
            {
                Admin_User _o = this.Admin_Users.Single(o => o.Id == _id);
                if (_o.UserName.Equals(_username))
                    _o.Password = _password;
                this.SubmitChanges();
                return Core.Variable.DB_UPDATE_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName + _ex.Message, _ex.InnerException);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }
    }
}
