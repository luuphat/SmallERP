using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmallERP.Core.Log;
using SmallERP.Data.Models;
using SmallERP.Data.Controllers;

namespace SmallERP.WinApp.View.HR
{
    public partial class frmEmployee : Form
    {
        public frmEmployee()
        {
            InitializeComponent();
        }

        private void frmEmployee_Load(object sender, EventArgs e)
        {
            BindingCombo();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            try
            {
                HR_EmployeeController _controller = new HR_EmployeeController();
                List<HR_Employee> _list = _controller.GetEmployees(-1, Convert.ToInt32(cboDeptFind.SelectedValue), Convert.ToInt32(cboJobFind.SelectedValue), txtSearchKey.Text);
                if (_list != null)
                    dgvEmployees.DataSource = _list;
                else
                    dgvEmployees.DataSource = new List<HR_Employee>();
            }
            catch (Exception ex)
            {
                Message.ErrorMessage("Lỗi khi nạp danh sách nhân viên");
                SingletonLogger.Instance.Error(this.Name, ex);
            }
        }

        private void BindingCombo()
        {
            Mapping.BindingControl.BindingDept2Combo(cboDeptFind);
            Mapping.BindingControl.BindingJob2Combo(cboJobFind);
            Mapping.BindingControl.BindingDept2Combo(cboDept);
            Mapping.BindingControl.BindingJob2Combo(cboJob);
            Mapping.BindingControl.BindingManager2Combo(cboManager);
            //Mapping.BindingCntrol.BindingDept2Combo(cboDept2);
        }

        private void ResetControl()
        {

        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
