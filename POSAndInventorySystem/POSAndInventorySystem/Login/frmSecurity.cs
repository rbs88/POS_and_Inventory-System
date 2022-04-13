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

namespace POSAndInventorySystem.Login
{
    public partial class frmSecurity : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public string _pass="";
        public bool isactive = false;
        public frmSecurity()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.KeyPreview = true;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtPassword.Clear();
            txtUsername.Clear();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string _username = "";
            string _role = "";
            string _name = "";
            try
            {
                bool found = false;
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblUser WHERE username = @username AND password = @password", cn);
                cm.Parameters.AddWithValue("@username", txtUsername.Text);
                cm.Parameters.AddWithValue("@password", txtPassword.Text);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    _username = dr["username"].ToString();
                    _role = dr["role"].ToString();
                    _name = dr["name"].ToString();
                    _pass = dr["password"].ToString();
                    isactive = bool.Parse(dr["isactive"].ToString());
                }
                else
                {
                    found = false;
                }              
                dr.Close();
                cn.Close();

                if (found == true)
                {
                    if (isactive == false)
                    {
                        MessageBox.Show("Account is inactive. Unable to login", "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (_role == "Cashier")
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Clear();
                        txtUsername.Clear();
                        this.Hide();
                        SubForm.frmPOS frm = new SubForm.frmPOS(this);
                        frm.lblUser.Text = _name +" | "+ _role;
                        frm.lblUsername.Text = _username;
                        
                        frm.ShowDialog();
                    }
                    else if(_role =="Administrator")
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtPassword.Clear();
                        txtUsername.Clear();
                        this.Hide();
                        MainForm frm = new MainForm(this);
                        frm.lblAdminUser.Text = _name;
                        frm.lblAdmin.Text = _role;
                        frm._pass = _pass;
                        frm.username = _username;
                        frm.ShowDialog();
                    }                
                }
                else
                {
                    MessageBox.Show("Invalid username or password!", "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message,"Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("EXIT APPLICATION?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                Application.Exit();
            }         
        }

        private void txtPassword_Click(object sender, EventArgs e)
        {

        }
        private void frmSecurity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }       
            if (e.KeyCode == Keys.Escape)
            {
                if (MessageBox.Show("EXIT APPLICATION?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
        }

        private void frmSecurity_Load(object sender, EventArgs e)
        {

        }
    }
}
