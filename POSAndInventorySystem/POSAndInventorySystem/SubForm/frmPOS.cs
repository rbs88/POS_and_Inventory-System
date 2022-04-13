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

namespace POSAndInventorySystem.SubForm
{
    public partial class frmPOS : Form
    {
        string id;
        string price;
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        string title = "POS Software";
        Login.frmSecurity f;
        int qty;

        public frmPOS(Login.frmSecurity frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            lblDate.Text = DateTime.Now.ToLongDateString();
            NotifyCriticalItems();
            this.KeyPreview = true;
            this.f = frm;

        }

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
                critical += i + ". " + dr["pdesc"].ToString() + " | " + dr["brand"].ToString() + Environment.NewLine;
            }
            dr.Close();
            cn.Close();

            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.x;
            popup.TitleText = count + " CRITICAL ITEM(S)";
            popup.ContentText = critical;
            popup.Popup();

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                MessageBox.Show("Unable to logout, Please cancel the transcation","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.Close();
            Login.frmSecurity frm = new Login.frmSecurity();
            frm.Show();
        }

        public void GetTransno() 
        {
            try
            {
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                string transno;
                int count;
                cn.Open();
                cm = new SqlCommand("SELECT TOP 1 transno FROM tblCart WHERE transno LIKE '" + sdate + "%' ORDER BY id desc", cn);
                dr = cm.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    lblTransno.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    lblTransno.Text = transno;
                }
                dr.Close();
                cn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btDashBoard_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                return;
            }

            GetTransno();
            txtSearch.Enabled = true;
            txtSearch.Focus();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtSearch.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    String _pcode;
                    double _price;
                    int _qty;

                    cn.Open();
                    cm = new SqlCommand("SELECT * FROM tblProduct WHERE barcode LIKE'" + txtSearch.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        qty = int.Parse(dr["qty"].ToString());                      
                        _pcode = dr["pcode"].ToString();
                        _price = double.Parse(dr["price"].ToString());
                        _qty = int.Parse(txtQty.Text);

                        dr.Close();
                        cn.Close();

                        AddtoCart(_pcode, _price, _qty);
                 
                    }
                    else
                    {
                        dr.Close();
                        cn.Close();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void AddtoCart(String _pcode, double _price, int _qty) 
        {
            string id = "";
            bool found = false;
            int cart_qty = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblCart WHERE transno = @transno and pcode = @pcode", cn);
            cm.Parameters.AddWithValue("@transno", lblTransno.Text);
            cm.Parameters.AddWithValue("@pcode", _pcode);
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
                cm = new SqlCommand("UPDATE tblCart SET qty = (qty + " + _qty + ") WHERE id = '" + id + "'", cn);
                cm.ExecuteNonQuery();
                cn.Close();

                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                LoadCart();             
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
                cm.Parameters.AddWithValue("@transno", lblTransno.Text);
                cm.Parameters.AddWithValue("@pcode", _pcode);
                cm.Parameters.AddWithValue("@price", _price);
                cm.Parameters.AddWithValue("@qty", _qty);
                cm.Parameters.AddWithValue("@sdate", DateTime.Now);
                cm.Parameters.AddWithValue("@cashier", lblUsername.Text);
                cm.ExecuteNonQuery();
                cn.Close();

                txtSearch.SelectionStart = 0;
                txtSearch.SelectionLength = txtSearch.Text.Length;
                LoadCart();           
            }
        }

        public void LoadCart() 
        {
            try
            {
                Boolean hasrecord = false;

                dataGridView1.Rows.Clear();
                int i = 0;
                double total = 0;
                double discount = 0;
                cn.Open();
                cm = new SqlCommand("SELECT c.id, c. pcode, p.pdesc, c.price, c.qty, c.disc, c.total FROM tblCart as c inner join tblProduct as p on c.pcode = p.pcode WHERE transno LIKE '"+ lblTransno.Text +"'", cn);
                dr = cm.ExecuteReader();
                while (dr.Read())
                {
                    i++;
                    total += Double.Parse(dr["total"].ToString());
                    discount += Double.Parse(dr["disc"].ToString());
                    dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["price"].ToString(), dr["qty"].ToString(), dr["disc"].ToString(), Double.Parse(dr["total"].ToString()).ToString("#,##0.00"));
                    hasrecord = true;
                }
                dr.Close();
                cn.Close();
                lblTotal.Text = total.ToString("#,##0.00");
                lblDiscount.Text = discount.ToString("#,##0.00");
                GetCartTotal();
                
                if (hasrecord == true)
                {
                    btnSettle.Enabled = true;
                    btnDiscount.Enabled = true;
                    btnCancel.Enabled = true;
                }
                else
                {
                    btnSettle.Enabled = false;
                    btnDiscount.Enabled = false;
                    btnCancel.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lblTransno.Text == "0000000000000000")
            {
                return;
            }
            frmLookUp frm = new frmLookUp(this);
            frm.LoadRecords();
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName == "Delete")
            {
                if (MessageBox.Show("Remove this item?",title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblCart WHERE id like '" + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Item has been successfully removed", title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadCart();                    
                }
            }
            else if (colName == "colAdd")
            {
                cn.Open();
                int b = 0;
                cm = new SqlCommand("SELECT SUM(qty) as qty FROM tblProduct WHERE pcode LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'GROUP BY pcode", cn);
                b = int.Parse(cm.ExecuteScalar().ToString());            
                cn.Close();

                if (int.Parse(dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString()) < b)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblCart SET qty = qty + " + int.Parse(txtQty.Text) + " WHERE transno LIKE '" + lblTransno.Text + "' AND pcode LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    
                    LoadCart();
                }
                else
                {
                    MessageBox.Show("Remaining qty on hand is " + b + "!", "Out of Stock", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            else if (colName == "colMinus")
            {
                int i = 0;
                cn.Open();            
                cm = new SqlCommand("SELECT SUM(qty) as qty FROM tblCart WHERE pcode LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'AND transno LIKE '"+ lblTransno.Text +"' GROUP BY transno, pcode", cn);
                i = int.Parse(cm.ExecuteScalar().ToString());
                cn.Close();

                if (i > 1)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblCart SET qty = qty - " + int.Parse(txtQty.Text) + " WHERE transno LIKE '" + lblTransno.Text + "' AND pcode LIKE '" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();                  
                    LoadCart();
                }
               
                else
                {
                    MessageBox.Show("Remaining qty on cart is " + i + "!", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
        }

        public void GetCartTotal() 
        {
            // double sales = Double.Parse(lblTotal.Text);          
            double discount = double.Parse(lblDiscount.Text);
            double sales = Double.Parse(lblTotal.Text);
            double vat = sales * dbcon.GetVal();
            double vatable = sales-vat;

          //  lblTotal.Text = sales.ToString("#,##0.00");        
            lblVat.Text = vat.ToString("#,##0.00");
            lblVatable.Text = vatable.ToString("#,##0.00");
            lblDisplayTotal.Text = sales.ToString("#,##0.00");
        }

        private void btnDiscount_Click(object sender, EventArgs e)
        {
            frmDiscount frm = new frmDiscount(this);
            frm.lblID.Text = id;
            frm.txtPrice.Text = price;
            frm.ShowDialog();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            id = dataGridView1[1, i].Value.ToString();
            price = dataGridView1[7, i].Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString("hh:mm:ss tt");
            lblDate1.Text = DateTime.Now.ToLongDateString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void btnSettle_Click(object sender, EventArgs e)
        {
            frmSettle frm = new frmSettle(this);
            frm.txtSale.Text = lblDisplayTotal.Text;
            frm.ShowDialog();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnDailySales_Click(object sender, EventArgs e)
        {
            SubForm.frmSoldItems frm = new frmSoldItems();
            frm.dt1.Enabled = false;
            frm.dt2.Enabled = false;
            frm.cboCashier.Enabled = false;
            frm.cboCashier.Text = lblUsername.Text;
            frm.suser = lblUsername.Text;
            frm.LoadRecord();
            frm.ShowDialog();
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            frmChangePassword frm = new frmChangePassword(this);
            frm.ShowDialog();
        }

        private void frmPOS_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F1)
            {
                btDashBoard_Click(sender, e);
            }
            else if(e.KeyCode == Keys.F2)
            {
                button1_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDiscount_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                btnSettle_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnCancel_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnDailySales_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnChangePassword_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                button7_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F8)
            {
                txtSearch_TextChanged(sender, e);
                //txtSearch.SelectionStart = 0;
                //txtSearch.SelectionLength = txtSearch.Text.Length;
            }
        }

        private void btnSearchProduct_Click(object sender, KeyEventArgs e)
        {
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Remove all items from cart?", "Confirm",MessageBoxButtons.YesNo, MessageBoxIcon.Question)==DialogResult.Yes)
            {
                cn.Open();
                cm = new SqlCommand("DELETE FROM tblCart WHERE transno LIKE '" + lblTransno.Text + "'", cn);
                cm.ExecuteNonQuery();
                cn.Close();
                MessageBox.Show("All items has been successfully removed!", "Remove Item", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadCart();
            }
        }
    }
}
