using SmallERP.Core.Log;
using SmallERP.Data.Controllers;
using SmallERP.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SmallERP.WinApp.Mapping
{
    public class BindingControl
    {
        public static void BindingDept2Combo(ComboBox cbo)
        {
            try
            {
                HR_DepartmentController _controller = new HR_DepartmentController();
                List<HR_Department> _parent = _controller.GetDepartments(-1, -1, 0, string.Empty);
                List<HR_Department> _listOrder = new List<HR_Department>();
                foreach (HR_Department d in _parent)
                {
                    _listOrder.Add(d);
                    List<HR_Department> _child = _controller.GetDepartments(-1, d.Id, 1, string.Empty);
                    foreach (HR_Department dc in _child)
                    {
                        dc.Name = "---- " + dc.Name;
                        _listOrder.Add(dc);
                    }
                }

                HR_Department _item = new HR_Department();
                _item.Id = -1;
                _item.Name = "-- Chọn phòng ban --";
                _listOrder.Insert(0, _item);
                cbo.DataSource = _listOrder;
                cbo.DisplayMember = "Name";
                cbo.ValueMember = "Id";

            }
            catch (Exception ex)
            {
                SingletonLogger.Instance.Error("BindingCntrol.BindingDept2Combo", ex);
            }
        }

        public static void BindingJob2Combo(ComboBox cbo)
        {
            try
            {
                HR_JobPositionController _controller = new HR_JobPositionController();
                List<HR_JobPosition> _list = _controller.GetJobPositions(-1, -1, string.Empty);

                HR_JobPosition _item = new HR_JobPosition();
                _item.Id = -1;
                _item.Name = "-- Chọn vị trí --";
                _list.Insert(0, _item);
                cbo.DataSource = _list;
                cbo.DisplayMember = "Name";
                cbo.ValueMember = "Id";

            }
            catch (Exception ex)
            {
                SingletonLogger.Instance.Error("BindingCntrol.BindingJob2Combo", ex);
            }
        }

        public static void BindingManager2Combo(ComboBox cbo)
        {
            HR_EmployeeController _controller = new HR_EmployeeController();
            List<HR_Employee> _list = _controller.GetEmployees(-1, -1, -1, string.Empty);

            HR_Employee _item = new HR_Employee();
            _item.Id = -1;
            _item.FullName = "-- Chọn quản lý --";
            _list.Insert(0, _item);
            cbo.DataSource = _list;
            cbo.DisplayMember = "FullName";
            cbo.ValueMember = "Id";
        }
    }
}
