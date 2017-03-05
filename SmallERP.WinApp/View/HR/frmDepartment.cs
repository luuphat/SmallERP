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
    public partial class frmDepartment : Form
    {
        public frmDepartment()
        {
            InitializeComponent();
        }

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            BindingDept();
            BindingDept2Combobox();
            BindingEmployee2Combobox();
        }
        /// <summary>
        /// get dept list, limit 2 level, parent & child
        /// </summary>
        private void BindingDept()
        {
            treeViewDepartment.Nodes.Clear();
            HR_DepartmentController _controller = new HR_DepartmentController();
            List<HR_Department> _parent = _controller.GetDepartments(-1, -1, 0, string.Empty);

            foreach (HR_Department d in _parent)
            {
                TreeNode _pNode = treeViewDepartment.Nodes.Add(d.Id.ToString(), d.Name);

                List<HR_Department> _child = _controller.GetDepartments(-1, d.Id, 1, string.Empty);
                foreach (HR_Department c in _child)
                {
                    _pNode.Nodes.Add(c.Id.ToString(), c.Name);
                }
            }
            treeViewDepartment.ExpandAll();
        }

        private void BindingDept2Combobox()
        {
            HR_DepartmentController _controller = new HR_DepartmentController();
            List<HR_Department> _parent = _controller.GetDepartments(-1, -1, 0, string.Empty);

            HR_Department _item = new HR_Department();
            _item.Id = -1;
            _item.Name = "-- Chọn phòng ban --";
            _parent.Insert(0, _item);
            cboParentDept.DataSource = _parent;
            cboParentDept.DisplayMember = "Name";
            cboParentDept.ValueMember = "Id";
        }

        private void BindingEmployee2Combobox()
        {
            HR_EmployeeController _controller = new HR_EmployeeController();
            List<HR_Employee> _list = _controller.GetEmployees(-1, -1, -1, string.Empty);

            HR_Employee _item = new HR_Employee();
            _item.Id = -1;
            _item.FullName = "-- Chọn quản lý --";
            _list.Insert(0, _item);
            cboManager.DataSource = _list;
            cboManager.DisplayMember = "FullName";
            cboManager.ValueMember = "Id";
        }

        private void ResetControl()
        {
            txtDeptId.Text = "";
            txtDeptName.Text = "";
            txtCreated.Text = "";
            cboParentDept.SelectedIndex = 0;
            cboManager.SelectedIndex = 0;

            txtDeptName.Focus();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ResetControl();
            txtDeptName.ReadOnly = false;
            cboParentDept.Enabled = true;
            cboManager.Enabled = true;
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtDeptName.ReadOnly = false;
            cboParentDept.Enabled = true;
            cboManager.Enabled = true;
        }

        private bool ValidateData()
        {
            if (txtDeptName.Text.Equals(string.Empty))
            {
                MessageBox.Show("Thông báo", "Bạn chưa nhập tên phòng ban", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            return true;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            HR_DepartmentController _controller = new HR_DepartmentController();

            if (!ValidateData())
                return;

            if (txtDeptId.Text.Equals(string.Empty))
            {
                HR_Department _item = new HR_Department();
                _item.Name = txtDeptName.Text;
                _item.ParentId = Convert.ToInt32(cboParentDept.SelectedValue);
                if (cboParentDept.SelectedIndex > 0)
                    _item.Level = 1;
                //if (cboManager.SelectedIndex > 0)
                _item.ManagerId = Convert.ToInt32(cboManager.SelectedValue);
                _item.Created = DateTime.Now;

                _controller.Add(_item);
            }
            else
            {
                HR_Department _item = _controller.GetByID(Convert.ToInt32(txtDeptId.Text));
                _item.Name = txtDeptName.Text;
                _item.ParentId = Convert.ToInt32(cboParentDept.SelectedValue);
                if (cboParentDept.SelectedIndex > 0)
                    _item.Level = 1;
                else
                    _item.Level = 0;
                //if (cboManager.SelectedIndex > 0)
                _item.ManagerId = Convert.ToInt32(cboManager.SelectedValue);
                _controller.Edit(_item);
            }

            BindingDept();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControl();
        }

        private void treeViewDepartment_DoubleClick(object sender, EventArgs e)
        {
            if (treeViewDepartment.Nodes.Count <= 0)
                return;
            TreeNode _node = treeViewDepartment.SelectedNode;
            if (_node != null)
            {
                HR_DepartmentController _controller = new HR_DepartmentController();
                HR_Department _item = _controller.GetByID(Convert.ToInt32(_node.Name));
                if (_item != null)
                {
                    txtDeptId.Text = _node.Name;
                    txtDeptName.Text = _item.Name;
                    txtCreated.Text = _item.Created.ToString("dd/MM/yyyy");
                    if (_item.ParentId > 0)
                        cboParentDept.SelectedValue = _item.ParentId;
                    else
                        cboParentDept.SelectedIndex = 0;
                    if (_item.ManagerId > 0)
                        cboManager.SelectedValue = _item.ManagerId;
                    else
                        cboManager.SelectedIndex = 0;
                }
            }
        }
    }
}
