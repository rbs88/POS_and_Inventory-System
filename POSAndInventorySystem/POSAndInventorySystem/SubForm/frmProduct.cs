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
    public partial class frmProduct : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();
        frmProductList f;
        public frmProduct(frmProductList f)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = f;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void LoadCategory() 
        {
            cboCategory.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT category FROM tblCategory", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboCategory.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void LoadBrand()
        {
            cboBrand.Items.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT brand FROM tblBrand", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboBrand.Items.Add(dr[0].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void frmProduct_Load(object sender, EventArgs e)
        {
            LoadBrand();
            LoadCategory();
        }

        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this product?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "";
                    string cid = "";

                    // Get the id of tblBrand and pass to bid variable
                    cn.Open();
                    cm = new SqlCommand("SELECT id FROM tblBrand WHERE brand LIKE'" + cboBrand.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bid = dr[0].ToString();
                    }
                    dr.Close();
                    cn.Close();

                    //Get the id of tblCategory and pass to cid variable

                    cn.Open();
                    cm = new SqlCommand("SELECT id FROM tblCategory WHERE category LIKE'" + cboCategory.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();
                    }
                    dr.Close();
                    cn.Close();

                    
                    // Save Record to tblProduct
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblProduct (pcode, barcode, pdesc, bid, cid, price, reorder)VALUES(@pcode, @barcode, @pdesc, @bid, @cid, @price, @reorder)", cn);
                    cm.Parameters.AddWithValue("@pcode", txtProductCode.Text);
                    cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtDescription.Text);
                    cm.Parameters.AddWithValue("@bid", bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@price", txtPrice.Text);
                    cm.Parameters.AddWithValue("@reorder", txtReOrder.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully saved.");
                    Clear();
                    f.LoadRecords();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "POS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        public void Clear() 
        {
            txtPrice.Clear();
            txtDescription.Clear();
            txtProductCode.Clear();
            cboBrand.ResetText();
            cboCategory.ResetText();
            txtBarcode.Clear();
            txtProductCode.Focus();
            btSave.Enabled = true;
            btUpdate.Enabled = false;
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this product?", "Save Product", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "";
                    string cid = "";

                    //Get the id of tblBrand and pass to bid variable
                    cn.Open();
                    cm = new SqlCommand("SELECT id FROM tblBrand WHERE brand LIKE'" + cboBrand.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bid = dr[0].ToString();
                    }
                    dr.Close();
                    cn.Close();

                    
                    //Get the id of tblCategory and pass to cid variable
                    cn.Open();
                    cm = new SqlCommand("SELECT id FROM tblCategory WHERE category LIKE'" + cboCategory.Text + "'", cn);
                    dr = cm.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();
                    }
                    dr.Close();
                    cn.Close();

                    //Update tblProduct

                    cn.Open();
                    cm = new SqlCommand("UPDATE tblProduct SET barcode = @barcode, pdesc = @pdesc, price = @price, reorder = @reorder WHERE pcode = @pcode", cn);
                    cm.Parameters.AddWithValue("@barcode", txtBarcode.Text);
                    cm.Parameters.AddWithValue("@pcode", txtProductCode.Text);
                    cm.Parameters.AddWithValue("@pdesc", txtDescription.Text);
                    cm.Parameters.AddWithValue("@bid", bid);
                    cm.Parameters.AddWithValue("@cid", cid);
                    cm.Parameters.AddWithValue("@price", double.Parse(txtPrice.Text));
                    cm.Parameters.AddWithValue("@reorder", int.Parse(txtReOrder.Text));
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Product has been successfully updated.");
                    Clear();
                    f.LoadRecords();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "POS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void btCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ACCEPT NUMBERS ONLY
            if (e.KeyChar == 46)
            {
                //accept. character
            }
            else if (e.KeyChar == 8)
            {
                //accept backspace
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }

        private void txtReOrder_KeyPress(object sender, KeyPressEventArgs e)
        {
            //ACCEPT NUMBERS ONLY
            if (e.KeyChar == 46)
            {
                //accept. character
            }
            else if (e.KeyChar == 8)
            {
                //accept backspace
            }
            else if ((e.KeyChar < 48) || (e.KeyChar > 57))
            {
                e.Handled = true;
            }
        }
    }
}
