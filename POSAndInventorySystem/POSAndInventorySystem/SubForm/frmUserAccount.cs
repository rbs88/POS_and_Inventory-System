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
    public partial class frmUserAccount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        MainForm f;   
        public frmUserAccount(MainForm frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = frm;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmUserAccount_Resize(object sender, EventArgs e)
        {
            //metroTabControl1.Left = (this.Width - metroTabControl1.Width) / 2;
            //metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != txtRetypePassword.Text)
                {
                    MessageBox.Show("Password did not match", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                cn.Open();
                cm = new SqlCommand("INSERT INTO tblUser(username,password,role,name)VALUES(@username,@password,@role,@name)", cn);
                cm.Parameters.AddWithValue("@username", txtUserName.Text);
                cm.Parameters.AddWithValue("@password", txtPassword.Text);
                cm.Parameters.AddWithValue("@role", cboRole.Text);
                cm.Parameters.AddWithValue("@name", txtName.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("New account has saved!");
                Clear();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void Clear()
        {
            txtUserName.Clear();
            txtPassword.Clear();
            txtRetypePassword.Clear();
            cboRole.Text = "";
            txtName.Clear();
            txtUserName.Focus();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnSave1_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtOldPassword.Text != f._pass)
                {
                    MessageBox.Show("Old password did not matched!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                if (txtNewPassword.Text != txtRetypeNewPassword.Text)
                {
                    MessageBox.Show("Confirm new password did not matched!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                cn.Open();
                cm = new SqlCommand("UPDATE tblUser SET password = @password WHERE username = @username", cn);
                cm.Parameters.AddWithValue("@password", txtNewPassword.Text);
                cm.Parameters.AddWithValue("username", txtUsername1.Text);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("Password has been successfully changed!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);           
                txtRetypeNewPassword.Clear();
                txtOldPassword.Clear();
                txtNewPassword.Clear();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cboRole_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void frmUserAccount_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblUser WHERE username = @username", cn);
                cm.Parameters.AddWithValue("@username", textBox1.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    checkBox2.Checked = bool.Parse(dr["isactive"].ToString());
                }
                else
                {
                    checkBox2.Checked = false;
                }
                dr.Close();
                cn.Close();
            }
            catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);                  
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                cn.Open();
                bool found = true;
                cm = new SqlCommand("SELECT * FROM tblUser WHERE username = @username", cn);
                cm.Parameters.AddWithValue("@username", textBox1.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                }
                else
                {
                    found = false;
                }
                dr.Close();
                cn.Close();


                if (found == true)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblUser SET isactive = @isactive WHERE username = @username", cn);
                    cm.Parameters.AddWithValue("@isactive", checkBox2.Checked.ToString());
                    cm.Parameters.AddWithValue("@username", textBox1.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Account status has been successfully updated", "Update Status", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox1.Clear();
                    checkBox2.Checked = false;
                }
                else
                {
                    MessageBox.Show("Account not exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }


            }
            catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          
        }
    }
}
