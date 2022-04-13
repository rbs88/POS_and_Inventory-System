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
    public partial class frmDashBoard : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        public frmDashBoard()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadChart();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        public void LoadChart() 
        {
            cn.Open();
            cn.Close();
            SqlDataAdapter da = new SqlDataAdapter("SELECT Year(sdate) as year, isnull(SUM(total),0.0) as total FROM tblCart  WHERE status LIKE 'Sold' GROUP BY Year(sdate)", cn);
            DataSet ds = new DataSet();

            da.Fill(ds, "Sales");
            chart1.DataSource = ds.Tables["Sales"];
            Series series1 = chart1.Series["Series1"];
            series1.ChartType = SeriesChartType.Doughnut;

            series1.Name = "SALES";

            var chart = chart1;
            chart.Series[series1.Name].XValueMember = "year";
            chart.Series[series1.Name].YValueMembers = "total";
            chart.Series[0].IsValueShownAsLabel = true;
            chart.Series[0].LabelFormat = "{#,##0.00}";
            //  chart.Series[0].LegendText = "#";
            cn.Close();
        }

        private void frmDashBoard_Load(object sender, EventArgs e)
        {
            panel1.Left = (this.Width - panel1.Width) / 2;
        }
    }
}
