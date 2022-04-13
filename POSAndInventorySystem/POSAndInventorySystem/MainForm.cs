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
using Tulpep.NotificationWindow;

namespace POSAndInventorySystem
{
    public partial class MainForm : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        Login.frmSecurity f;
        public string _pass, username = "";
        public MainForm(Login.frmSecurity f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = f;
            this.KeyPreview = true;
            NotifyCriticalItems();          
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
           
        }

        //POP UP CRITICAL ITEMS
        public void NotifyCriticalItems()
        {
            string critical = "";
            
            cn.Open();
            cm = new SqlCommand("SELECT COUNT(*) FROM vwCriticalItems", cn);
            string count = cm.ExecuteScalar().ToString();
            cn.Close();

            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM vwCriticalItems ", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {             
                i++;
                critical += i + ". " +  dr["pdesc"].ToString() + " | " + dr["brand"].ToString() + Environment.NewLine;
            }
            dr.Close();
            cn.Close();

            if (critical == "")
            {
                return;
            }
            else
            {
                PopupNotifier popup = new PopupNotifier();
                popup.Image = Properties.Resources.x;
                popup.TitleText = count + " CRITICAL ITEM(S)";
                popup.ContentText = critical;
                popup.Popup();
            }         

        }

        private void btManageBrand_Click(object sender, EventArgs e)
        {
            SubForm.FrmBrandList frm = new SubForm.FrmBrandList();
            frm.TopLevel = false;
            panelMain.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadRecords();
            frm.Show();
        }

        private void btManageCategory_Click(object sender, EventArgs e)
        {
            SubForm.frmCategoryList frm = new SubForm.frmCategoryList();
            frm.TopLevel = false;
            panelMain.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadRecords();
            frm.Show();
        }

        private void btManageProduct_Click(object sender, EventArgs e)
        {
            SubForm.frmProductList frm = new SubForm.frmProductList();
            frm.TopLevel = false;
            panelMain.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadRecords();
            frm.Show();
        }

        private void btnStockIn_Click(object sender, EventArgs e)
        {
            SubForm.frmStockIn frm = new SubForm.frmStockIn();
            frm.TopLevel = false;
            panelMain.Controls.Add(frm);
            frm.BringToFront();
           // frm.LoadStockIn();
            frm.Show();
        }

        private void btManageSales_Click(object sender, EventArgs e)
        {
            SubForm.frmPOS frm = new SubForm.frmPOS(null);
            frm.ShowDialog();
        }

        private void btUserSettings_Click(object sender, EventArgs e)
        {
            SubForm.frmUserAccount frm = new SubForm.frmUserAccount(this);
            frm.TopLevel = false;
            panelMain.Controls.Add(frm);
            frm.txtUsername1.Text = username;
            frm.BringToFront();
            frm.Show();
        }

        private void btLogout_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("LOGOUT APPLICATION?", "CONFIRM", MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                this.Hide();
                var frm = new Login.frmSecurity();
                frm.ShowDialog();
            }       
        }

        private void btnSalesHistory_Click(object sender, EventArgs e)
        {
            SubForm.frmSoldItems frm = new SubForm.frmSoldItems();
            frm.ShowDialog();
        }

        private void btRecords_Click(object sender, EventArgs e)
        {
            SubForm.frmRecords frm = new SubForm.frmRecords();
            frm.TopLevel = false;
            frm.LoadCriticalItems();
            frm.LoadInventoryItems();
            frm.CancelledOrders();
            frm.SumTotal();
            frm.SumOfSoldItems();
            frm.LoadSoldItem();
            frm.LoadStockInHistory();
            panelMain.Controls.Add(frm);
            frm.BringToFront();
            frm.Show();
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                btLogout_Click(sender, e);
            }
        }

        private void btSystemSettings_Click(object sender, EventArgs e)
        {
            SubForm.frmStore frm = new SubForm.frmStore();
            frm.LoadRecords();
            frm.ShowDialog();
        }

        private void btDashBoard_Click(object sender, EventArgs e)
        {
            DashBoard();
        }

        public void DashBoard() 
        {
            SubForm.frmDashBoard frm = new SubForm.frmDashBoard();
            frm.TopLevel = false;
            panelMain.Controls.Add(frm);
            frm.lblDailySales.Text = dbcon.DailySales().ToString("#,##0.00");
            frm.lblProductLine.Text = dbcon.ProductLine().ToString("#,##0");
            frm.lblStockonhand.Text = dbcon.StockOnHand().ToString("#,##0");
            frm.lblCriticalItems.Text = dbcon.CriticalItems().ToString("#,##0");
            frm.BringToFront();
            //frm.LoadRecords();
            frm.Show();
        }

        private void btnVendor_Click(object sender, EventArgs e)
        {
            SubForm.frmVendorList frm = new SubForm.frmVendorList();
            frm.TopLevel = false;          
            panelMain.Controls.Add(frm);
            frm.BringToFront();
            frm.LoadRecords();
            frm.Show();
        }

        private void btnStockAdjustment_Click(object sender, EventArgs e)
        {
            SubForm.frmAdjustment frm = new SubForm.frmAdjustment(this);
            //frm.TopLevel = false;
            //panelMain.Controls.Add(frm);
            //frm.BringToFront();
            frm.LoadRecords();
            frm.txtUser.Text = lblAdmin.Text;
            frm.ReferenceNo();
            frm.ShowDialog();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            DashBoard();
        }
    }
}
