using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SmallERP.WinApp.View.Core;
using SmallERP.Core.Log;
using System.Configuration;

namespace SmallERP.WinApp
{
    public partial class frmMainApp : Form
    {
        public frmMainApp()
        {
            InitializeComponent();
        }

        private void frmMainApp_Load(object sender, EventArgs e)
        {
            InitializeLogger();
        }

        #region Helper

        public static void InitializeLogger()
        {
            // Read and assign application wide logging severity
            string severity = Core.Settings.LogSeverity;
            SingletonLogger.Instance.Severity = (LogSeverity)Enum.Parse(typeof(LogSeverity), severity, true);

            // Send log messages to debugger console (output window). 
            // Btw: the attach operation is the Observer pattern.
            ILog log = new ObserverLogToConsole();
            SingletonLogger.Instance.Attach(log);

            // Send log messages to email (observer pattern)
            //string from = "notification@yourcompany.com";
            //string to = "webmaster@yourcompany.com";
            //string subject = "Webmaster: please review";
            //string body = "email text";
            //var smtpClient = new SmtpClient("mail.yourcompany.com");
            //log = new ObserverLogToEmail(from, to, subject, body, smtpClient);
            //SingletonLogger.Instance.Attach(log);

            // Other log output options

            //// Send log messages to a file
            log = new ObserverLogToFile();
            SingletonLogger.Instance.Attach(log);

            //// Send log message to event log
            //log = new ObserverLogToEventlog();
            //SingletonLogger.Instance.Attach(log);

            //// Send log messages to database (observer pattern)
            //log = new ObserverLogToDatabase();
            //SingletonLogger.Instance.Attach(log);
        }

        private Form IsExistsForm(Type formType)
        {
            foreach (Form f in this.MdiChildren)
            {
                if (f.GetType() == formType)
                    return f;

            }
            return null;
        }
        #endregion

        private void companyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = IsExistsForm(typeof(frmCompany));
            if (f != null)
                f.Activate();
            else
            {
                frmCompany _from = new frmCompany() { MdiParent = this };
                _from.Show();
            }
        }

        private void DeptManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = IsExistsForm(typeof(View.HR.frmDepartment));
            if (f != null)
                f.Activate();
            else
            {
                View.HR.frmDepartment _from = new View.HR.frmDepartment() { MdiParent = this };
                _from.Show();
            }
        }

        private void jobsPositionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = IsExistsForm(typeof(View.HR.frmJobPosition));
            if (f != null)
                f.Activate();
            else
            {
                View.HR.frmJobPosition _from = new View.HR.frmJobPosition() { MdiParent = this };
                _from.Show();
            }
        }

        private void employeeManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = IsExistsForm(typeof(View.HR.frmEmployee));
            if (f != null)
                f.Activate();
            else
            {
                View.HR.frmEmployee _from = new View.HR.frmEmployee() { MdiParent = this };
                _from.Show();
            }
        }

        private void userManagementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form f = IsExistsForm(typeof(View.Systems.frmUserManagement));
            if (f != null)
                f.Activate();
            else
            {
                View.Systems.frmUserManagement _from = new View.Systems.frmUserManagement() { MdiParent = this };
                _from.Show();
            }
        }
    }
}
