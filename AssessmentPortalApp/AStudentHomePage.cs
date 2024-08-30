using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AssessmentPortalApp
{
   
    public partial class AStudentHomePage : Form
    {
        public int userId;
        public string userName;
        public int roleId;

        public AStudentHomePage()
        {
            InitializeComponent();
        }

        private void AStudentHomePage_Load(object sender, EventArgs e)
        {
            lblMsg.Text = userName;
        }

        private void lnkChangePassword_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ChangePasswordPage1 studentChangePassword_obj = new ChangePasswordPage1();
           


            studentChangePassword_obj.Show();
            this.Hide();
        }

        private void lnkLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FirstPage obj = new FirstPage();
            obj.Show();
            this.Hide();

        }
    }
}
