using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace POSAndInventorySystem.SubForm
{
    public partial class frmChangePassword : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        frmPOS f;
        public frmChangePassword(frmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = frm;
        }

        private void frmChangePassword_Load(object sender, EventArgs e)
        {

        }

        private void btnPasswordSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _oldpass = dbcon.GetPassword(f.lblUsername.Text);
                if (_oldpass != txtOldPassword.Text)
                {
                    MessageBox.Show("Old password did not matched!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else if (txtNewPassword.Text != txtConfirmNewPassword.Text)
                {
                    MessageBox.Show("Confirm new password did not matched!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                {
                    if (MessageBox.Show("Change Password", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        cn.Open();
                        cm = new SqlCommand("UPDATE tblUser SET password = @password WHERE username = @username", cn);
                        cm.Parameters.AddWithValue("@password", txtNewPassword.Text);
                        cm.Parameters.AddWithValue("@username", f.lblUsername.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                        MessageBox.Show("Password has been successfully saved!", "Save Changes", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message,"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
