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
    public partial class frmQty : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
       // string title = "POS Software";

        private String pcode;
        private double price;
        private String transno;
        private int qty;

        frmPOS f;
        public frmQty(frmPOS f1)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = f1;
            this.KeyPreview = true;
        }

        private void frmQty_Load(object sender, EventArgs e)
        {
            f.txtSearch.Clear();
        }

        public void ProductDetails(String pcode, double price, String transno, int qty)
        {
            this.pcode = pcode;
            this.price = price;
            this.transno = transno;
            this.qty = qty;
        }

        private void txtQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar == 13) && (txtQty.Text != String.Empty))
            {
                string id = "";
                int cart_qty = 0;
                bool found = false;
              
                cn.Open();
                cm = new SqlCommand("SELECT * FROM tblCart WHERE transno = @transno and pcode = @pcode", cn);
                cm.Parameters.AddWithValue("@transno", f.lblTransno.Text);
                cm.Parameters.AddWithValue("@pcode", pcode);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    found = true;
                    id = dr["id"].ToString();
                    cart_qty = int.Parse(dr["qty"].ToString());
                }
                else
                {
                    found = false;
                }
                dr.Close();
                cn.Close();


                if (found == true)
                {
                    if (qty < (int.Parse(txtQty.Text) + cart_qty))
                    {
                        MessageBox.Show("Unable to Proceed Qty On Hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    cn.Open();
                    cm = new SqlCommand("UPDATE tblCart SET qty = (qty + " + int.Parse(txtQty.Text)+ ") WHERE id = '" + id +"'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    f.txtSearch.Clear();
                    f.txtSearch.Focus();
                    f.LoadCart();
                    this.Dispose();
                }
                else
                {
                    if (qty < int.Parse(txtQty.Text))
                    {
                        MessageBox.Show("Unable to Proceed Qty On Hand is " + qty, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    } 

                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblCart (transno, pcode, price, qty, sdate, cashier)VALUES(@transno, @pcode, @price, @qty, @sdate, @cashier)", cn);
                    cm.Parameters.AddWithValue("@transno", transno);
                    cm.Parameters.AddWithValue("@pcode", pcode);
                    cm.Parameters.AddWithValue("@price", price);
                    cm.Parameters.AddWithValue("@qty", int.Parse(txtQty.Text));
                    cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                    cm.Parameters.AddWithValue("@cashier", f.lblUsername.Text);
                    cm.ExecuteNonQuery();

                    cn.Close();
                    f.txtSearch.Clear();
                    f.txtSearch.Focus();
                    f.LoadCart();
                    this.Dispose();
                }              
            }
        }

        private void frmQty_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }
    }
}
