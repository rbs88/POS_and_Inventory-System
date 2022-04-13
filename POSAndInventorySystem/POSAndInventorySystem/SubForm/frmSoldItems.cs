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
    public partial class frmSoldItems : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        //  frmPOS frm;
       public string suser;
        public frmSoldItems()
        {
            InitializeComponent();
            //this.frm = frm;
            cn = new SqlConnection(dbcon.MyConnection());
            dt1.Value = DateTime.Now;
            dt2.Value = DateTime.Now;
            LoadRecord();
            LoadCashier();
            this.KeyPreview = true;
        }

        public void LoadRecord()
        {
            int i = 0;
            double _total = 0;
            dataGridView1.Rows.Clear();
            cn.Open();

            if (cboCashier.Text == "All Cashier")
            {
                cm = new SqlCommand("SELECT c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total FROM tblCart as c INNER JOIN tblProduct as p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '" + dt1.Value + "' AND '" + dt2.Value + "'", cn);
            }
            else
            {
                cm = new SqlCommand("SELECT c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc, c.total FROM tblCart as c INNER JOIN tblProduct as p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '" + dt1.Value + "' AND '" + dt2.Value + "' AND cashier LIKE '" + cboCashier.Text + "'", cn);
            }

            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                _total += double.Parse(dr["total"].ToString());
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["transno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), dr["total"].ToString());
            }
            dr.Close();
            cn.Close();
            lblTotal.Text = _total.ToString("#,##0.00");
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void dt1_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dt2_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ReportForm.frmReportSold frm = new ReportForm.frmReportSold(this);
            frm.LoadReport();
            frm.ShowDialog();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void LoadCashier() 
        {
            cboCashier.Items.Clear();
            cboCashier.Items.Add("All Cashier");
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblUser WHERE role LIKE 'Cashier'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCashier.Items.Add(dr["username"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void cboCashier_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName =="colCancel")
            {
                frmCancelDetails f = new frmCancelDetails(this);
                f.txtID.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                f.txtTransactionNo.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.txtProductCode.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.txtPrice.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.txtQty.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.txtDiscount.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.txtTotal.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                f.txtCancelledBy.Text = suser;
                f.ShowDialog();
            }
        }

        private void frmSoldItems_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.P)
            {
                btnPrint_Click(sender, e);
            }
        }
    }
}
