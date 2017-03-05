using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.Data.Controllers
{
    public class HR_JobPositionController : HRDataContext
    {
        public HR_JobPositionController()
            : base(Core.Settings.ConnectionString)
        {
        }

        public string ClassName
        {
            get { return "HR_JobPositionController"; }
        }

        public HR_JobPosition GetById(int jobId)
        {
            return GetJobPositions(-1, jobId, string.Empty).FirstOrDefault();
        }

        public List<HR_JobPosition> GetJobPositions(int departmentId, int jobId, string searchKey)
        {
            try
            {
                var _list = (from jp in this.HR_JobPositions.Where(o => (o.Id == departmentId || departmentId == -1) && 
                    (o.Id == jobId || jobId == -1) && o.Name.ToLower().Contains(searchKey.ToLower()))
                             select jp).ToList();
                return _list;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Add(HR_JobPosition jp)
        {
            try
            {
                this.HR_JobPositions.InsertOnSubmit(jp);
                this.SubmitChanges();
                return Core.Variable.DB_INSERT_SUCCESS;
            }
            catch (Exception _ex)
            {
                throw new Exception(ClassName, _ex);
                //SingletonLogger.Instance.Error(ClassName, _ex); 
            }
        }

        public int Edit(HR_JobPosition jp)
        {
            try
            {
                HR_JobPosition _o = this.HR_JobPositions.Single(o => o.Id == jp.Id);
                _o.Name = jp.Name;
                _o.DepartmentId = jp.DepartmentId;
                _o.JobDescription = jp.JobDescription;
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
