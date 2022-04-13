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
    public partial class frmCategory : Form
    {
     
        SqlConnection cn;
        SqlCommand cm;
        DBConnection dbcon = new DBConnection();
        frmCategoryList f;     
        public frmCategory()
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());          
        }

        public frmCategory(frmCategoryList f)
            :this()
        {
            this.f = f;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Clear() 
        {
            txtBrandName.Clear();
        }
        private void btSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to save this category?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblCategory(category)VALUES(@category)", cn);
                    cm.Parameters.AddWithValue("@category", txtBrandName.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully saved","POS",MessageBoxButtons.YesNo,MessageBoxIcon.Information);
                    Clear();
                   f.LoadRecords();
                }
            }
            catch (Exception ex)
            {
              
                    MessageBox.Show(ex.Message, "POS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
               
            }
        }

        private void btUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this brand?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblCategory SET category = @category WHERE id LIKE'" + label5.Text + "'", cn);
                    cm.Parameters.AddWithValue("@category", txtBrandName.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Brand has been successfully updated.");
                    this.Close();
                    f.LoadRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "POS", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }
    }
}
