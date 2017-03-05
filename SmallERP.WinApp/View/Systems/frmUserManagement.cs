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

namespace SmallERP.WinApp.View.Systems
{
    public partial class frmUserManagement : Form
    {
        public frmUserManagement()
        {
            InitializeComponent();
        }

        private void frmUserManagement_Load(object sender, EventArgs e)
        {
            BindingUsers();
        }

        private void BindingUsers()
        {
            try
            {
                Admin_UserController _controller = new Admin_UserController();
                List<Admin_User> _list = _controller.GetUsers(-1, txtSearchKey.Text);
                dgvUsers.DataSource = _list;
            }
            catch (Exception ex)
            {
                Message.ErrorMessage("Lỗi khi nạp danh sách người dùng");
                SingletonLogger.Instance.Error(this.Name, ex);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindingUsers();
        }

        private void ResetControl()
        {
            txtUserId.Text = string.Empty;
            txtUsername.Text = string.Empty;
            txtFirstname.Text = string.Empty;
            txtLastname.Text = string.Empty;
            txtEmail.Text = string.Empty;
            txtPhone.Text = string.Empty;

            txtUsername.ReadOnly = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Admin_UserController _controller = new Admin_UserController();

                Admin_User _user = new Admin_User();
                _user.Firstname = txtFirstname.Text;
                _user.Lastname = txtLastname.Text;
                _user.FullName = txtFirstname.Text + " " + txtLastname.Text;
                _user.Email = txtEmail.Text;
                _user.PhoneNo = txtPhone.Text;

                if (txtUserId.Text == string.Empty)
                {
                    _user.UserName = txtUsername.Text;
                    _user.Password = txtPassword.Text;
                    _user.Created = DateTime.Now;
                    _controller.Add(_user);
                }
                else
                {
                    _user.Id = Convert.ToInt32(txtUserId.Text);
                    _user.Modified = DateTime.Now;
                    _controller.Edit(_user);
                }
                
            }
            catch (Exception ex)
            {
                Message.ErrorMessage("Lỗi khi lưu người dùng");
                SingletonLogger.Instance.Error(this.Name, ex);
            }

            BindingUsers();
            tabControl1.SelectedIndex = 0;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetControl();
            tabControl1.SelectedIndex = 0;
        }

        private void addUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ResetControl();
            txtUsername.Focus();
            tabControl1.SelectedIndex = 1;
        }

        private void editUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvUsers.Rows.Count <= 0)
                return;

            if (dgvUsers.SelectedRows.Count <= 0)
                return;

            DataGridViewRow dr = dgvUsers.SelectedRows[0];

            txtUserId.Text = dr.Cells["Id"].Value.ToString();
            txtUsername.Text = dr.Cells["Username"].Value.ToString();
            txtFirstname.Text = dr.Cells["Firstname"].Value.ToString();
            txtLastname.Text = dr.Cells["Lastname"].Value.ToString();
            txtEmail.Text = dr.Cells["Email"].Value.ToString();
            txtPhone.Text = dr.Cells["PhoneNo"].Value.ToString();

            tabControl1.SelectedIndex = 1;
        }
    }
}
