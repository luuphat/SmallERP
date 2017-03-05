using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.Data.Controllers
{
    public class HR_DepartmentController : HRDataContext
    {
        public HR_DepartmentController()
            : base(Core.Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "HR_DepartmentController"; }
        }

        public HR_Department GetByID(int departmentId)
        {
            return GetDepartments(departmentId, -1, -1, string.Empty).FirstOrDefault();
        }

        public List<HR_Department> GetDepartments(int departmentId, int parentId, int level, string searchKey)
        {
            try
            {
                var _list = (from d in this.HR_Departments.Where(o => (o.Id == departmentId || departmentId == -1) &&
                    (o.ParentId == parentId || parentId == -1) && (o.Level == level || level == -1) && o.Name.ToLower().Contains(searchKey.ToLower()))
                             select d).ToList();
                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Add(HR_Department _depart)
        {
            try
            {
                this.HR_Departments.InsertOnSubmit(_depart);
                this.SubmitChanges();
                return Core.Variable.DB_INSERT_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Edit(HR_Department _depart)
        {
            try
            {
                HR_Department _o = this.HR_Departments.Single(o => o.Id == _depart.Id);
                _o.Name = _depart.Name;
                _o.ParentId = _depart.ParentId;
                _o.Level = _depart.Level;
                _o.ManagerId = _depart.ManagerId;
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
