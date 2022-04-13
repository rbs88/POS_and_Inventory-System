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
    public partial class frmCategoryList : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();     
        public frmCategoryList()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }
     
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory(this);
            frm.btSave.Enabled = true;
            frm.btUpdate.Enabled = false;
            frm.ShowDialog();
        }
        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblCategory ORDER BY category", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i += 1;
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["category"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Edit")
            {
                frmCategory frm = new frmCategory(this);
                frm.txtBrandName.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                frm.label5.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                frm.btSave.Enabled = false;
                frm.btUpdate.Enabled = true;
                frm.ShowDialog();
            }
            else if (colName =="Delete")
            {
                if (MessageBox.Show("Are you sure you want to delete this record?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblCategory WHERE id LIKE '" + dataGridView1[1, e.RowIndex].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully deleted", "POS", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadRecords();
                }
            }
        }
    }
}
