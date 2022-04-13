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
    public partial class frmStore : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        public frmStore()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecords() 
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblStore", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                txtAddress.Text = dr["address"].ToString();
                txtStore.Text = dr["store"].ToString();
            }
            cn.Close();
            dr.Close();
          
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("SAVE STORE DETAILS?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    int count;
                    cn.Open();
                    cm = new SqlCommand("SELECT count(*) FROM tblStore", cn);
                    count = int.Parse(cm.ExecuteScalar().ToString());
                    cn.Close();

                    if (count > 0)
                    {
                        cn.Open();
                        cm = new SqlCommand("UPDATE tblStore SET store = @store, address = @address", cn);
                        cm.Parameters.AddWithValue("@store", txtStore.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                    else
                    {
                        cn.Open();
                        cm = new SqlCommand("INSERT into tblStore (store, address)VALUES(@store, @address)", cn);
                        cm.Parameters.AddWithValue("@store", txtStore.Text);
                        cm.Parameters.AddWithValue("@address", txtAddress.Text);
                        cm.ExecuteNonQuery();
                        cn.Close();
                    }
                    MessageBox.Show("STORE DETAILS HAS BEEN SUCCESSFULLY SAVED", "STORE DETAILS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }  
    }
}
