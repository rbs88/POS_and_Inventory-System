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
    public partial class frmVoid : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();

        frmCancelDetails f;
        public frmVoid(frmCancelDetails frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = frm;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnVoid_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtPassword.Text != string.Empty)
                {
                    string user1;
                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tbluser WHERE username =@username AND password=@password", cn);
                    cm.Parameters.AddWithValue("@username", txtUsername.Text);
                    cm.Parameters.AddWithValue("@password", txtPassword.Text);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        user1 = dr["username"].ToString();
                        dr.Close();
                        cn.Close();

                        SaveCancelOrder(user1);
                        if (f.cboAction.Text =="Yes")
                        {
                            UpdateData("UPDATE tblProduct SET qty = qty + " + int.Parse(f.txtCancelQty.Text) + " WHERE pcode ='" + f.txtProductCode.Text + "'");
                        }

                        UpdateData("UPDATE tblCart SET qty = qty - " + int.Parse(f.txtCancelQty.Text) + " WHERE id LIKE '" + f.txtID.Text + "'");

                        MessageBox.Show("Order transaction successfully cancelled!","Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                        f.RefreshList();
                        f.Dispose();
                      

                    }
                    cn.Close();
                    dr.Close();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SaveCancelOrder(string user) 
        {
            cn.Open();
            cm = new SqlCommand("INSERT INTO tblCancel (transno, pcode, price, qty, sdate, voidby, cancelledby, reason, action)VALUES(@transno, @pcode, @price, @qty, @sdate, @voidby, @cancelledby, @reason, @action)", cn);
            cm.Parameters.AddWithValue("@transno", f.txtTransactionNo.Text);
            cm.Parameters.AddWithValue("@pcode", f.txtProductCode.Text);
            cm.Parameters.AddWithValue("@price", double.Parse(f.txtPrice.Text));
            cm.Parameters.AddWithValue("@qty", int.Parse(f.txtCancelQty.Text));
            cm.Parameters.AddWithValue("@sdate", DateTime.Now);
            cm.Parameters.AddWithValue("@voidby", user);
            cm.Parameters.AddWithValue("@cancelledby", f.txtCancelledBy.Text);
            cm.Parameters.AddWithValue("@reason", f.txtReason.Text);
            cm.Parameters.AddWithValue("@action", f.cboAction.Text);
            cm.ExecuteNonQuery();
            cn.Close();

        }

        public void UpdateData(string sql) 
        {
            cn.Open();
            cm = new SqlCommand(sql, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }

        private void frmVoid_Load(object sender, EventArgs e)
        {

        }
    }
}
