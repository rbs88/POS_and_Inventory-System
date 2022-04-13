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
    public partial class frmAdjustment : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();

        MainForm f;
        int _qty;

        public frmAdjustment(MainForm frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = frm;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAdjustment_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string colName = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colName =="Select")
            {
                txtPcode.Text = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDescription.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + " " + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                _qty = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString());
            }
        }

        public void ReferenceNo()
        {
            Random rand = new Random();
            txtReference.Text = rand.Next().ToString();
        }

        public void LoadRecords()
        {
            int i = 0;
            dataGridView1.Rows.Clear();
            cn.Open();
            cm = new SqlCommand("SELECT p.pcode, p.barcode, p.pdesc, b.brand, c.category, p.price, p.qty, p.reorder FROM tblProduct as p inner join tblBrand as b on b.id = p.bid inner join tblCategory as c on c.id = p.cid WHERE p.pdesc LIKE '%" + txtSearch.Text + "%' ORDER by p.pdesc", cn);
            dr = cm.ExecuteReader();
            while (dr.Read())
            {
                i++;
                dataGridView1.Rows.Add(i, dr[0].ToString(), dr[1].ToString(), dr[2].ToString(), dr[3].ToString(), dr[4].ToString(), dr[5].ToString(), dr[6].ToString(), dr[7].ToString());
            }
            dr.Close();
            cn.Close();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            LoadRecords();
        }

        private void cboCommand_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (cboCommand.Text == String.Empty || txtRemarks.Text == string.Empty)
                {
                    MessageBox.Show("PLEASE SELECT AN ACTION AND REMARKS", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (int.Parse(txtQty.Text )> _qty)
                {
                    MessageBox.Show("STOCK ON HAND QUANTITY SHOULD BE GREATER THAN FROM ADJUSTMENT QTY", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                if (int.Parse(txtQty.Text) == 0)
                {
                    MessageBox.Show("NO VALUE TO ADD OR REMOVE", "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }           
              
                else if(cboCommand.Text == "REMOVE FROM INVENTORY")
                {
                    SqlStatement("UPDATE tblProduct SET qty = (qty - " + int.Parse(txtQty.Text) +") WHERE pcode LIKE '" +txtPcode.Text+ "'");
                }
                else if (cboCommand.Text == "ADD TO INVENTORY")
                {
                    SqlStatement("UPDATE tblProduct SET qty = (qty + " + int.Parse(txtQty.Text) + ") WHERE pcode LIKE '" + txtPcode.Text + "'");
                }

                SqlStatement("INSERT INTO tblAdjustment (referenceno,pcode,qty,action,remarks,sdate,users)VALUES('"+txtReference.Text+"','"+txtPcode.Text+"','"+int.Parse(txtQty.Text)+"','"+cboCommand.Text+"','"+txtRemarks.Text+"','"+DateTime.Now.ToShortDateString()+"','"+txtUser.Text+"')");
                MessageBox.Show("STOCK HAS BEEN SUCCESSFULLY ADJUSTED", "PROCESS COMPLETED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Clear();
                LoadRecords();
            }
            catch (Exception ex)
            {
                cn.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);              
            }
        }

        public void Clear() 
        {
            txtReference.Clear();
            txtPcode.Clear();
            txtDescription.Clear();
            txtQty.Clear();
            txtRemarks.Clear();
            cboCommand.Text = "";
         //   txtUser.Clear();
            ReferenceNo();
        }
        public void SqlStatement(string _sql) 
        {
            cn.Open();
            cm = new SqlCommand(_sql, cn);
            cm.ExecuteNonQuery();
            cn.Close();
        }
    }
}
