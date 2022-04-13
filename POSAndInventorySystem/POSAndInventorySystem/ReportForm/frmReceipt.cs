using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace POSAndInventorySystem.ReportForm
{
    public partial class frmReceipt : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
     //   SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        SubForm.frmPOS f;
        string store = "Ronel Software";
        string address = "74 Palayan, Quezon City";
      
        public frmReceipt(SubForm.frmPOS f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = f;
            this.KeyPreview = true;
        }

        private void frmReceipt_Load(object sender, EventArgs e)
        {

        }

        private void frmReceipt_Load_1(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }

        public void LoadReport(string pcash, string pchange)
        {
            ReportDataSource rptDataSoure;
            try
            {
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report1.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                DataSet1 ds = new DataSet1();
                SqlDataAdapter da = new SqlDataAdapter();

                cn.Open();
                da.SelectCommand = new SqlCommand("SELECT c.id, c.transno, c.pcode, c.price, c.qty, c.disc, c.total, p.pdesc, c.cashier FROM tblCart as c INNER JOIN tblProduct as p ON p.pcode = c.pcode WHERE transno LIKE '" + f.lblTransno.Text + "'", cn);
                da.Fill(ds.Tables["dtSold"]);
                cn.Close();

                ReportParameter pVatable1 = new ReportParameter("pVatable", f.lblVatable.Text);
                ReportParameter pVat1 = new ReportParameter("pVat", f.lblVat.Text);
                ReportParameter pDiscount1 = new ReportParameter("pDiscount", f.lblDiscount.Text);
                ReportParameter pTotal1 = new ReportParameter("pTotal", f.lblTotal.Text);
                ReportParameter pCash1 = new ReportParameter("pCash", pcash);
                ReportParameter pChange1 = new ReportParameter("pChange", pchange);
                ReportParameter pStore1 = new ReportParameter("pStore", store);
                ReportParameter pAddress1 = new ReportParameter("pAddress", address);
                ReportParameter pTransaction1 = new ReportParameter("pTransaction", "Invoice #:" + f.lblTransno.Text);
                ReportParameter pCashier1 = new ReportParameter("pCashier", f.lblUser.Text);

                reportViewer1.LocalReport.SetParameters(pVatable1);
                reportViewer1.LocalReport.SetParameters(pVat1);
                reportViewer1.LocalReport.SetParameters(pDiscount1);
                reportViewer1.LocalReport.SetParameters(pTotal1);
                reportViewer1.LocalReport.SetParameters(pCash1);
                reportViewer1.LocalReport.SetParameters(pChange1);
                reportViewer1.LocalReport.SetParameters(pStore1);
                reportViewer1.LocalReport.SetParameters(pAddress1);
                reportViewer1.LocalReport.SetParameters(pTransaction1);
                reportViewer1.LocalReport.SetParameters(pCashier1);

                rptDataSoure = new ReportDataSource("DataSet1", ds.Tables["dtSold"]);
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

        private void frmReceipt_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
