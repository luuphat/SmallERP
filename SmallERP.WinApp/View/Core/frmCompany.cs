using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SmallERP.Core;
using SmallERP.Data.Models;
using SmallERP.Data.Controllers;

namespace SmallERP.WinApp.View.Core
{
    public partial class frmCompany : Form
    {
        public frmCompany()
        {
            InitializeComponent();
        }

        private void frmCompany_Load(object sender, EventArgs e)
        {
            Core_CompanyController _controller = new Core_CompanyController();
            List<Core_Company> _list = _controller.GetCompanies(string.Empty);
            lstCompany.DataSource = _list;
            lstCompany.DisplayMember = "Name";
        }
    }
}
