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
using System.Windows.Forms.DataVisualization.Charting;

namespace POSAndInventorySystem.SubForm
{
    public partial class frmRecords : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        public frmRecords()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void LoadRecord() 
        {
            cn.Open();
            int i = 0;
            dataGridView1.Rows.Clear();

            if (cboTopSelect.Text == "SORT BY QTY")
            {

                cm = new SqlCommand("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total FROM vwSoldItems WHERE sdate between '" + dateTimePicker1.Value.ToString() + "' AND '" + dateTimePicker2.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY qty DESC ", cn);             
            
            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {

                cm = new SqlCommand("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total FROM vwSoldItems WHERE sdate between '" + dateTimePicker1.Value.ToString() + "' AND '" + dateTimePicker2.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY total DESC ", cn);
               
            }

            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString(), double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
            }
            cn.Close();
        }

        public void CancelledOrders()
        {
            cn.Open();
            int i = 0;
            dataGridView5.Rows.Clear();          
            cm = new SqlCommand("SELECT * FROM vwCancelledOrder WHERE sdate BETWEEN '"+ dateTimePicker6.Value.ToString() +"' AND '"+ dateTimePicker5.Value.ToString() +"'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView5.Rows.Add(i, dr["transno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), int.Parse(dr["qty"].ToString()), dr["total"].ToString(), DateTime.Parse(dr["sdate"].ToString()).ToString("MM/dd/yyyy"), dr["voidby"].ToString(), dr["cancelledby"].ToString(), dr["reason"].ToString(), dr["action"].ToString());           
            }
            cn.Close();
            dr.Close();                  
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            if (cboTopSelect.Text == string.Empty)
            {
                MessageBox.Show("Please select from the dropdown list", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                LoadRecord();
                LoadChartTopSelling();
            }       
        }

        public void LoadChartTopSelling()
        {
            cn.Open();

            if (cboTopSelect.Text == "SORT BY QTY")
            {
                 da = new SqlDataAdapter("SELECT TOP 10 pdesc, isnull(sum(qty),0) as qty FROM vwSoldItems WHERE sdate between '" + dateTimePicker1.Value.ToString() + "' AND '" + dateTimePicker2.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pdesc ORDER BY qty DESC ", cn);
            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                 da = new SqlDataAdapter("SELECT TOP 10 pdesc, isnull(sum(total),0) as total FROM vwSoldItems WHERE sdate between '" + dateTimePicker1.Value.ToString() + "' AND '" + dateTimePicker2.Value.ToString() + "' AND status LIKE 'Sold' GROUP BY pdesc ORDER BY total DESC ", cn);
            }
            DataSet ds = new DataSet();
            da.Fill(ds, "TOPSELLING");
            chart1.DataSource = ds.Tables["TOPSELLING"];
            Series series = chart1.Series[0];
            series.ChartType = SeriesChartType.Doughnut;

            series.Name = "TOP SELLING";
            var chart = chart1;
            chart.Series[0].XValueMember = "pdesc";
            if (cboTopSelect.Text == "SORT BY QTY")
            {
                chart.Series[0].YValueMembers = "qty";
            }
            else if(cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                chart.Series[0].YValueMembers = "total";
            }

            chart.Series[0].IsValueShownAsLabel = true;

            if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                chart.Series[0].LabelFormat = "{#,##0.00}";
            }
            else if (cboTopSelect.Text == "SORT BY QTY")
            {
                chart.Series[0].LabelFormat = "{#,##0}";
            }
            cn.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void frmRecords_Load(object sender, EventArgs e)
        {

        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        public void LoadSoldItem()
        {
            try
            {
                cn.Open();
                int i = 0;
                dataGridView2.Rows.Clear();
                cm = new SqlCommand("SELECT c.pcode, p.pdesc, c.price, SUM(c.qty) as Total_Qty, SUM(c.disc) as TOTAL_DISC, SUM(c.total) as TOTAL FROM tblCart as c INNER JOIN tblProduct as p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '" + d2.Value.ToString() + "' AND '" + d3.Value.ToString() + "' GROUP BY c.pcode, p.pdesc, c.price", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView2.Rows.Add(i, dr["pcode"].ToString(), dr["pdesc"].ToString(), Double.Parse(dr["price"].ToString()).ToString("#,##0.00"), dr["Total_Qty"].ToString(), dr["TOTAL_DISC"].ToString(), Double.Parse(dr["TOTAL"].ToString()).ToString("#,##0.00"));                 
                }
                cn.Close();
                dr.Close();

            }
            catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void SumOfSoldItems()
        {
            if (dataGridView2.Rows.Count !=0)
            {
                cn.Open();
                cm = new SqlCommand("SELECT SUM(total) FROM tblCart WHERE status LIKE 'Sold' AND sdate BETWEEN '" + d2.Value.ToString() + "' AND '" + d3.Value.ToString() + "'", cn);
                lblTotal.Text = double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");
                cn.Close();
            }
            else
            {
                lblTotal.Text = "0.00";
            }
           
        }
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        public void LoadCriticalItems() 
        {
            try
            {
                dataGridView3.Rows.Clear();
                int i = 0;
                cn.Open();         
                cm = new SqlCommand("SELECT * FROM vwCriticalItems", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView3.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[6].ToString());
                }
                cn.Close();
                dr.Close();

            }
            catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                
            }
        }

        public void LoadInventoryItems()
        {
            try
            {
                dataGridView4.Rows.Clear();
                int i = 0;
                cn.Open();
                //Query from view
                cm = new SqlCommand("SELECT * FROM vwInventoryItems", cn);
                
                //LIKE THIS QUERY

                /*SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category,
                  p.price, p.qty, p.reorder FROM tblProduct as p INNER JOIN tblBrand
                  as b ON b.id = p.bid INNER JOIN tblCategory as c ON c.id = p.cid
                 */

                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    dataGridView4.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString(), dr[6].ToString());
                }
                cn.Close();
                dr.Close();

            }
            catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReportForm.frmInventoryReport frm = new ReportForm.frmInventoryReport();
            frm.LoadReport();
            frm.ShowDialog();
        }

        private void btnCancelledOrder_Click(object sender, EventArgs e)
        {
            CancelledOrders();
            SumTotal();
        }

        public void SumTotal() 
        {
            if (dataGridView5.Rows.Count != 0)
            {
                cn.Open();
                cm = new SqlCommand("SELECT SUM(total) FROM vwCancelledOrder WHERE sdate BETWEEN '" + dateTimePicker6.Value.ToString() + "' AND '" + dateTimePicker5.Value.ToString() + "'", cn);
                label4.Text = double.Parse(cm.ExecuteScalar().ToString()).ToString("#,##0.00");
                cn.Close();
            }
            else
            {
                label4.Text = "0.00";
            }
        }

        private void tabPage5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void LoadStockInHistory() 
        {
            dataGridView6.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM vwStockIn WHERE cast(sdate as date)  BETWEEN '" + dt1.Value + "' AND '" + dt2.Value + "' AND status LIKE 'Done'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView6.Rows.Add(i, dr["id"].ToString(), dr["refno"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString(), DateTime.Parse(dr["sdate"].ToString()).ToShortDateString(), dr["stockinby"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        private void btnPrintPreview_Click(object sender, EventArgs e)
        {
            ReportForm.frmInventoryReport frm = new ReportForm.frmInventoryReport();
            frm.LoadSoldItems("SELECT c.pcode, p.pdesc, c.price, SUM(c.qty) as Total_Qty, SUM(c.disc) as TOTAL_DISC, SUM(c.total) as TOTAL FROM tblCart as c INNER JOIN tblProduct as p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '" + d2.Value.ToString() + "' AND '" + d3.Value.ToString() + "' GROUP BY c.pcode, p.pdesc, c.price", "From: " + d2.Value.ToString() + " To: " + d3.Value.ToString());
            frm.ShowDialog();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            ReportForm.frmInventoryReport frm = new ReportForm.frmInventoryReport();
            if (cboTopSelect.Text == "SORT BY QTY")
            {

                frm.LoadTopSelling("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total FROM vwSoldItems WHERE sdate between '" + DateTime.Parse(dateTimePicker1.Value.ToString()).ToShortDateString() + "' AND '" + DateTime.Parse(dateTimePicker2.Value.ToString()).ToShortDateString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY qty DESC ", "From: " + dateTimePicker1.Value.ToString() + " To: " + dateTimePicker2.Value.ToString(), "TOP SELLING ITEMS SORT BY QTY");

            }
            else if (cboTopSelect.Text == "SORT BY TOTAL AMOUNT")
            {
                frm.LoadTopSelling("SELECT TOP 10 pcode, pdesc, isnull(sum(qty),0) as qty, isnull(sum(total),0) as total FROM vwSoldItems WHERE sdate between '" + DateTime.Parse(dateTimePicker1.Value.ToString()).ToShortDateString() + "' AND '" + DateTime.Parse(dateTimePicker2.Value.ToString()).ToShortDateString() + "' AND status LIKE 'Sold' GROUP BY pcode, pdesc ORDER BY total DESC ", "From: " + dateTimePicker1.Value.ToString() + " To: " + dateTimePicker2.Value.ToString(), "TOP SELLING ITEMS SORT BY TOTAL AMOUNT");

            }          
            frm.ShowDialog();
        }

        private void cboTopSelect_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            LoadSoldItem();
            SumOfSoldItems();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            frmChart frm = new frmChart();
            frm.lblTitle.Text = "SOLD ITEM/S ["+d2.Value.ToShortDateString()+" TO "+d3.Value.ToShortDateString()+"]";
            frm.LoadChartSoldItems("SELECT p.pdesc, SUM(c.total) as TOTAL FROM tblCart as c INNER JOIN tblProduct as p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '" + d2.Value.ToString() + "' AND '" + d3.Value.ToString() + "' GROUP BY p.pdesc ORDER BY total DESC");
            frm.ShowDialog();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReportForm.frmInventoryReport frm = new ReportForm.frmInventoryReport();
            string param = "DATE COVERED: " + dt1.Value.ToShortDateString() + " - " + dt2.Value.ToShortDateString();
            frm.LoadStockIn("SELECT * FROM vwStockIn WHERE cast(sdate as date)  BETWEEN '" + dt1.Value.ToShortDateString() + "' AND '" + dt2.Value.ToShortDateString() + "' AND status LIKE 'Done'", param);
            frm.ShowDialog();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            ReportForm.frmInventoryReport frm = new ReportForm.frmInventoryReport();
            string param = "DATE COVERED: " + dateTimePicker6.Value.ToShortDateString() + " - " + dateTimePicker5.Value.ToShortDateString();
            frm.LoadCancelledOrder("SELECT * FROM vwCancelledOrder WHERE sdate BETWEEN '" + dateTimePicker6.Value.ToString() + "' AND '" + dateTimePicker5.Value.ToString() + "'", param);
            frm.ShowDialog();
        }
    }
}
