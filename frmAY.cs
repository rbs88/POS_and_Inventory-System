using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace UI
{
    public partial class frmAY : Form
    {
        SqlConnection cn;
        SqlCommand cm;
        ClassDB db = new ClassDB();
        string _title = "Schoo Management System";
        frmAcademicYear f;
        public frmAY(frmAcademicYear f)
        {
            InitializeComponent();
            cn = new SqlConnection(db.GetConnection());
            this.f = f;
        }

        private void cboTerm_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtYear1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                txtYear2.Text = (long.Parse(txtYear1.Text) + 1).ToString();
            }
            catch (Exception)
            {
                txtYear2.Clear();
            }
           
        }

        public void Clear() 
        {
            txtYear1.Clear();
            txtYear2.Clear();
            cboTerm.ResetText();
            txtYear1.Focus();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string _aycode = txtYear1.Text + txtYear2.Text + cboTerm.Text;
                if (txtYear1.Text == string.Empty || txtYear2.Text == string.Empty || cboTerm.Text == string.Empty)
                {
                    MessageBox.Show("REQUIRED MISSING FIELDS", _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (MessageBox.Show("ADD NEW ACADEMIC YEAR? CLICK TO CONFIRM", _title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //UPDATE IF THERE IS OPEN ACADEMIC YEAR
                    cn.Open();
                    cm = new SqlCommand("UPDATE tblAY SET status = 'CLOSE'", cn);
                    cm.ExecuteNonQuery();
                    cn.Close();

                    //ADD NEW ACADEMIC YEAR
                    cn.Open();
                    cm = new SqlCommand("SP_AY_INSERT", cn);
                    cm.CommandType = CommandType.StoredProcedure;
                    cm.Parameters.AddWithValue("@aycode", _aycode);
                    cm.Parameters.AddWithValue("@year1", txtYear1.Text);
                    cm.Parameters.AddWithValue("@year2", txtYear2.Text);
                    cm.Parameters.AddWithValue("@term", cboTerm.Text);
                    cm.ExecuteNonQuery();
                    cn.Close();
                    MessageBox.Show("NEW ACADEMIC YEAR HAS SUCCESSFULLY SAVED", _title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    f.LoadRecords();
                    Clear();
                }
            }
            catch (Exception ex)
            {

                cn.Close();
                MessageBox.Show(ex.Message, _title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
