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
    public partial class FrmBrandList : Form
    {

        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public FrmBrandList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadRecords();           
        }     

        private void label3_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            FrmBrand f = new FrmBrand(this);
            f.ShowDialog();
        }

        public void LoadRecords() 
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblBrand ORDER BY brand", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i += 1;             
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["brand"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName =="Edit")
            {
                FrmBrand frm = new FrmBrand(this);
                frm.label5.Text = dataGridView1[1, e.RowIndex].Value.ToString();
                frm.txtBrandName.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                frm.btSave.Enabled = false;
                frm.ShowDialog();
            }
            else if (colName =="Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblBrand WHERE id LIKE '"+dataGridView1[1, e.RowIndex].Value.ToString()+"'", cn);                  
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully deleted","POS",MessageBoxButtons.OK,MessageBoxIcon.Information);                  
                    LoadRecords();
                }
            }
        }
    }
}
