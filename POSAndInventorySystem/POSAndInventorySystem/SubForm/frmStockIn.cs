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
    public partial class frmStockIn : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        DBConnection dbcon = new DBConnection();
        SqlDataReader dr;
        string title = "POS System";
       
        public frmStockIn()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            LoadVendor();
          
        }

        public void LoadProduct() 
        {
            //dataGridView1.Rows.Clear();
            //int i = 0;
            //cn.Open();
            //cm = new SqlCommand("SELECT pcode, pdesc, price, qty FROM tblProduct WHERE pdesc LIKE '%"+ txtSearch.Text + "%' ORDER BY pdesc", cn);
            //dr = cm.ExecuteReader();
            //while (dr.Read())
            //{
            //    i++;
            //    dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString());
            //}
            //cn.Close();
                
        }
        private void frmStockIn_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            //if (colName == "SELECT")
            //{
            //    if (txtReference.Text == string.Empty)
            //    {
            //        MessageBox.Show("Please enter reference no",title,MessageBoxButtons.OK,MessageBoxIcon.Warning);
            //        txtReference.Focus();
            //        return;
            //    }

            //    if (txtStockInBy.Text == string.Empty)
            //    {
            //        MessageBox.Show("Please enter stock in by", title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        txtStockInBy.Focus();
            //        return;
            //    }

            //    if (MessageBox.Show("Add This item?",title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        cn.Open();
            //        cm = new SqlCommand("INSERT INTO tblStockin (refno, pcode, sdate, stockinby)VALUES(@refno, @pcode, @sdate, @stockinby)", cn);
            //        cm.Parameters.AddWithValue("@refno", txtReference.Text );
            //        cm.Parameters.AddWithValue("@pcode", dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString());
            //        cm.Parameters.AddWithValue("@sdate", dtStockInDate.Value);
            //        cm.Parameters.AddWithValue("@stockinby", txtStockInBy.Text);
            //        cm.ExecuteNonQuery();
            //        cn.Close();
            //        MessageBox.Show("Successfull added!",title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        LoadStockIn();
            //    }            
            //}
        }

        public void LoadStockIn() 
        {
            dataGridView2.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM vwStockIn WHERE refno ='" + txtReference.Text + "' AND status LIKE 'Pending'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView2.Rows.Add(i, dr["id"].ToString(), dr["refno"].ToString(), dr["vendor"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString(), DateTime.Parse(dr["sdate"].ToString()), dr["stockinby"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        public void LoadStockInHistory()
        {
            dataGridView1.Rows.Clear();
            int i = 0;
            cn.Open();
            cm = new SqlCommand("SELECT * FROM vwStockIn WHERE cast(sdate as date)  BETWEEN '"+ dt1.Value +"' AND '"+ dt2.Value +"' AND status LIKE 'Done'", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr["id"].ToString(), dr["refno"].ToString(), dr["vendor"].ToString(), dr["pcode"].ToString(), dr["pdesc"].ToString(), dr["qty"].ToString(), DateTime.Parse(dr["sdate"].ToString()).ToShortDateString(), dr["stockinby"].ToString());
            }
            dr.Close();
            cn.Close();
        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            LoadStockIn();
        }

        private void dataGridView1_KeyUp(object sender, KeyEventArgs e)
        {
            LoadStockIn();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            LoadStockIn();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView2.Columns[e.ColumnIndex].Name;
           
            if (colName == "Delete")
            {
                if (MessageBox.Show("Remove this item?", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                    cn.Open();
                    cm = new SqlCommand("DELETE FROM tblStockin WHERE id ='" + dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString() + "'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    LoadStockIn();
                    MessageBox.Show("Item has been successfully removed", title, MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            
            }        
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmSearchProductStockin frm = new frmSearchProductStockin(this);
            frm.LoadProduct();
            frm.ShowDialog();
        }

        public void Clear() 
        {
            txtStockInBy.Clear();
            txtReference.Clear();
            dtStockInDate.Value = DateTime.Now;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView2.Rows.Count > 0)
                {
                    if (MessageBox.Show("SAVE", title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            //Update product qty
                            cn.Open();
                            cm = new SqlCommand("UPDATE tblProduct SET qty = qty + " + int.Parse(dataGridView2.Rows[i].Cells[6].Value.ToString()) + " WHERE pcode LIKE '" + dataGridView2.Rows[i].Cells[4].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();

                            //Update tblstockin qty
                            cn.Open();
                            cm = new SqlCommand("UPDATE tblStockin SET qty = qty + " + int.Parse(dataGridView2.Rows[i].Cells[6].Value.ToString()) + ", status ='Done' WHERE id LIKE '" + dataGridView2.Rows[i].Cells[1].Value.ToString() + "'", cn);
                            cm.ExecuteNonQuery();
                            cn.Close();
                        }

                        Clear();
                        LoadStockIn();
                    }
                }
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadStockInHistory();
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void cboVendor_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        public void LoadVendor() 
        {
            cn.Open();
            cm = new SqlCommand("SELECt * FROM tblVendor", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                cboVendor.Items.Add(dr["vendor"].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void cboVendor_TextChanged(object sender, EventArgs e)
        {
            cn.Open();
            cm = new SqlCommand("SELECT * FROM tblVendor WHERE vendor LIKE '" + cboVendor.Text + "'", cn);
            dr = cm.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                lblVendor.Text = dr["id"].ToString();
                txtContactPerson.Text = dr["contactperson"].ToString();
                txtAddress.Text = dr["address"].ToString();
            }
            dr.Close();
            cn.Close();
        }

        private void metroLink1_Click(object sender, EventArgs e)
        {
            Random rand = new Random();
            txtReference.Clear();
            txtReference.Text += rand.Next();                           
        }

        private void txtReference_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
