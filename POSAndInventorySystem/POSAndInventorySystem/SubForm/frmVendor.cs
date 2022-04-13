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
    public partial class frmVendor : Form
    {
        SqlConnection cn = new SqlConnection();
        SqlCommand cm = new SqlCommand();
        SqlDataReader dr;
        DBConnection dbcon = new DBConnection();

        frmVendorList f;
        public frmVendor(frmVendorList frm)
        {
            InitializeComponent();
            cn = new SqlConnection(dbcon.MyConnection());
            this.f = frm;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("SAVE THIS RECORD?CLICK YES TO CONFIRM","CONFIRM", MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("INSERT INTO tblVendor (vendor,address,contactperson,telephone,email,fax)VALUES(@vendor,@address,@contactperson,@telephone,@email,@fax)", cn);
                    cm.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@contactperson", txtContactPerson.Text);
                    cm.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                    cm.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                    cm.Parameters.AddWithValue("@fax", txtFaxNo.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("RECORD HAS BEEN SUCCESSFULLY SAVED!", "INFORMATION", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                    f.LoadRecords();
                }              
            }
            catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, "WARNING", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        private void Clear() 
        {
            txtVendor.Clear();
            txtAddress.Clear();
            txtContactPerson.Clear();
            txtEmailAddress.Clear();
            txtFaxNo.Clear();
            txtTelephone.Clear();
            txtVendor.Focus();
            btnSave.Enabled = true;
            btnUpdate.Enabled = false;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are you sure you want to update this brand?", "Update Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblVendor SET vendor = @vendor, address = @address, contactperson = @contactperson, telephone = @telephone, email = @email, fax = @fax  WHERE id = @id", cn);
                    cm.Parameters.AddWithValue("@id", lblID.Text);
                    cm.Parameters.AddWithValue("@vendor", txtVendor.Text);
                    cm.Parameters.AddWithValue("@address", txtAddress.Text);
                    cm.Parameters.AddWithValue("@contactperson", txtContactPerson.Text);
                    cm.Parameters.AddWithValue("@telephone", txtTelephone.Text);
                    cm.Parameters.AddWithValue("@email", txtEmailAddress.Text);
                    cm.Parameters.AddWithValue("@fax", txtFaxNo.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("Record has been successfully updated.");
                    this.Close();
                    f.LoadRecords();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }
    }
}
