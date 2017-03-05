using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.Data.Controllers
{
    public class HR_EmployeeController : HRDataContext
    {
        public HR_EmployeeController()
            : base(Core.Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "HR_EmployeeController"; }
        }

        public List<HR_Employee> GetEmployees(int employeeId, int departmentId, int jobId, string searchKey)
        {
            try
            {
                var _list = (from d in this.HR_Employees.Where(o => (o.Id == employeeId || employeeId == -1) &&
                    (o.DepartmentId == departmentId || departmentId == -1) && (o.JobId == jobId || jobId == -1) && o.FullName.ToLower().Contains(searchKey.ToLower()))
                             select d).ToList();
                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Add(HR_Employee employee)
        {
            try
            {
                this.HR_Employees.InsertOnSubmit(employee);
                this.SubmitChanges();
                return Core.Variable.DB_INSERT_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Edit(HR_Employee employee)
        {
            try
            {
                HR_Employee _o = this.HR_Employees.Single(o => o.Id == employee.Id);
                _o.FirstName = employee.FirstName;
                _o.LastName = employee.LastName;
                _o.FullName = employee.FullName;
                _o.DepartmentId = employee.DepartmentId;
                _o.JobId = employee.JobId;
                _o.WorkAddress = employee.WorkAddress;
                _o.Birthday = employee.Birthday;
                _o.Email = employee.Email;
                _o.MobilePhone = employee.MobilePhone;
                _o.WorkPhone = employee.WorkPhone;
                _o.WorkEmail = employee.WorkEmail;
                _o.EmployeeNo = employee.EmployeeNo;
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
