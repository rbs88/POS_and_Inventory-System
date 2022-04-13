
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
    public partial class frmDiscount : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        frmPOS f;
        public frmDiscount(frmPOS frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = frm;
            this.KeyPreview = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtDiscount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = Double.Parse(txtPrice.Text) * Double.Parse(txtPercent.Text);
                txtAmount.Text = discount.ToString("#,##0.00");
            }
            catch (Exception) 
            {
                txtAmount.Text = "0.00";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Add discount? Click yes to confirm","POS Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblCart SET disc = @disc, disc_percent = @disc_percent WHERE id = @id", cn);
                    cm.Parameters.AddWithValue("@disc", Double.Parse(txtAmount.Text));
                    cm.Parameters.AddWithValue("@disc_percent", Double.Parse(txtPercent.Text));
                    cm.Parameters.AddWithValue("@id", int.Parse(lblID.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();
                    f.LoadCart();
                    this.Close();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "POS Software", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void frmDiscount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                button1_Click(sender, e);
            }
        }

        private void frmDiscount_Load(object sender, EventArgs e)
        {

        }
    }
}
