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
using Microsoft.Reporting.WinForms;


namespace POSAndInventorySystem.ReportForm
{
    public partial class frmReportSold : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        SubForm.frmSoldItems f;
        public frmReportSold(SubForm.frmSoldItems f1)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            f = f1;
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void frmReportSold_Load(object sender, EventArgs e)
        {

        }

        public void LoadReport()
        {         
            try
            {
                ReportDataSource rptDataSoure;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report2.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                if (f.cboCashier.Text == "All Cashier")
                {
                    da.SelectCommand = new SqlCommand("SELECT c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc as discount, c.total FROM tblCart as c INNER JOIN tblProduct as p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '" + f.dt1.Value + "' AND '" + f.dt2.Value + "'", cn);
                }
                else
                {
                    da.SelectCommand = new SqlCommand("SELECT c.id, c.transno, c.pcode, p.pdesc, c.price, c.qty, c.disc as discount, c.total FROM tblCart as c INNER JOIN tblProduct as p ON c.pcode = p.pcode WHERE status LIKE 'Sold' AND sdate BETWEEN '" + f.dt1.Value + "' AND '" + f.dt2.Value + "' AND cashier LIKE '" + f.cboCashier.Text + "'", cn);
                }
               
                da.Fill(ds.Tables["dtSoldReport"]);
                cn.Close();

                ReportParameter pDate = new ReportParameter("pDate", "Date From: " + f.dt1.Value.ToShortDateString() + " To: " +f.dt2.Value.ToShortDateString());
                ReportParameter pCashier = new ReportParameter("pCashier", "Cashier:" + f.cboCashier.Text);
                ReportParameter pHeader = new ReportParameter("pHeader", "SALES REPORT");

                reportViewer1.LocalReport.SetParameters(pDate);
                reportViewer1.LocalReport.SetParameters(pCashier);
                reportViewer1.LocalReport.SetParameters(pHeader);

                rptDataSoure = new ReportDataSource("DataSet1", ds.Tables["dtSoldReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptDataSoure);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 75;
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

      
    }
}
