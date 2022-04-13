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
    public partial class frmSearchProductStockin : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string title = "POS System";
        frmStockIn f;

        public frmSearchProductStockin(frmStockIn f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = f;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadProduct()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT pcode, pdesc, price, qty FROM tblProduct WHERE pdesc LIKE '%" + txtSearch.Text + "%' OR pcode LIKE '%" + txtSearch.Text + "%' ORDER BY pdesc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            }
            cn.Close();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "SELECT")
            {
                if (f.txtReference.Text == string.Empty)
                {
                    MessageBox.Show("Please enter reference no", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    f.txtReference.Focus();
                    return;
                }

                if (f.txtStockInBy.Text == string.Empty)
                {
                    MessageBox.Show("Please enter stock in by", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    f.txtStockInBy.Focus();
                    return;
                }

                if (MessageBox.Show("Add This item?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblStockin (refno, pcode, sdate, stockinby, vendorid)VALUES(@refno, @pcode, @sdate, @stockinby, @vendorid)", cn);
                    cm.Parameters.AddWithValue("@refno", f.txtReference.Text);
                    cm.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
                    cm.Parameters.AddWithValue("@sdate", f.dtStockInDate.Value);
                    cm.Parameters.AddWithValue("@stockinby", f.txtStockInBy.Text);
                    cm.Parameters.AddWithValue("@vendorid", f.lblVendor.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Successfully added!", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadStockIn();
                }
            }
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadProduct();
        }

        private void txtSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
