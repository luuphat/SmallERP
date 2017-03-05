using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmallERP.Data.Controllers;
using SmallERP.Data.Models;
using SmallERP.Core.Log;

namespace SmallERP.WinApp.View.HR
{
    public partial class frmJobPosition : Form
    {
        public frmJobPosition()
        {
            InitializeComponent();
        }

        private void frmJobPosition_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            BindingJobs(-1, string.Empty);
            BindingDept();
        }

        private void BindingJobs(int deptId, string searchKey)
        {
            try
            {
                HR_JobPositionController _controller = new HR_JobPositionController();
                List<HR_JobPosition> _list = _controller.GetJobPositions(deptId, -1, searchKey);
                if (_list == null)
                    _list = new List<HR_JobPosition>();
                dgvJobs.DataSource = _list;
            }
            catch (Exception ex)
            {
                SingletonLogger.Instance.Error(this.Name, ex);
            }
        }

        private void BindingDept()
        {
            Mapping.BindingControl.BindingDept2Combo(cboDept1);
            Mapping.BindingControl.BindingDept2Combo(cboDept2);
        }

        private void ResetControl()
        {
            txtJobId.Text = string.Empty;
            txtJobName.Text = string.Empty;
            txtDescription.Text = string.Empty;
            txtCreated.Text = string.Empty;
            cboDept2.SelectedIndex = 0;
            txtJobName.Focus();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetControl();
            tabControl1.SelectedIndex = 1;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvJobs.Rows.Count <= 0)
                return;

            if (dgvJobs.SelectedRows.Count <= 0)
                return;

            DataGridViewRow dr = dgvJobs.SelectedRows[0];
            txtJobId.Text = dr.Cells["Id"].Value.ToString();
            txtJobName.Text = dr.Cells["Name"].Value.ToString();
            txtDescription.Text = dr.Cells["JobDescription"].Value.ToString();
            txtCreated.Text = Convert.ToDateTime(dr.Cells["Created"].Value).ToString("dd/MM/yyyy");
            cboDept2.SelectedValue = dr.Cells["DepartmentId"].Value;

            txtJobName.Focus();
            tabControl1.SelectedIndex = 1;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            HR_JobPositionController _controller = new HR_JobPositionController();
            if (txtJobId.Text.Equals(string.Empty))
            {
                HR_JobPosition _item = new HR_JobPosition();
                _item.Name = txtJobName.Text;
                _item.JobDescription = txtDescription.Text;
                _item.DepartmentId = Convert.ToInt32(cboDept2.SelectedValue);
                _item.Created = DateTime.Now;

                _controller.Add(_item);
            }
            else
            {
                HR_JobPosition _item = _controller.GetById(Convert.ToInt32(txtJobId.Text));
                _item.Name = txtJobName.Text;
                _item.JobDescription = txtDescription.Text;
                _item.DepartmentId = Convert.ToInt32(cboDept2.SelectedValue);

                _controller.Edit(_item);
            }

            BindingJobs(-1, string.Empty);
            tabControl1.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControl();
            tabControl1.SelectedIndex = 0;
        }
    }
}
