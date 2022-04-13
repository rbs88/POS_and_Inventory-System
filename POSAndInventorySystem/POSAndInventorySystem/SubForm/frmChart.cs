using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Data.SqlClient;

namespace POSAndInventorySystem.SubForm
{
    public partial class frmChart : Form
    {
        SqlConnection cn = new SqlConnection();           
        DBConnection dbcon = new DBConnection();
        SqlDataAdapter da = new SqlDataAdapter();
        public frmChart()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadChartSoldItems(string sql)
        {
            cn.Open();
            da = new SqlDataAdapter(sql, cn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SOLD");
            chart1.DataSource = ds.Tables["SOLD"];
            Series series = chart1.Series[0];
            series.ChartType = SeriesChartType.Pie;          

            series.Name = "SOLD ITEMS";
            chart1.Series[0].XValueMember = "pdesc";
          //  chart1.Series[0]["PieLabelStyle"] = "Outside";
          //  chart1.Series[0].BorderColor = System.Drawing.Color.Gray;
            chart1.Series[0].YValueMembers = "total";
            chart1.Series[0].LabelFormat = "{#,##0.00}";
            chart1.Series[0].IsValueShownAsLabel = true;
            cn.Close();
        }
    }
}
