using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace POSAndInventorySystem.SubForm
{
    public partial class frmCancelDetails : Form
    {
        frmSoldItems f;
        public frmCancelDetails(frmSoldItems frm)
        {
            InitializeComponent();
            f = frm;
        }

        private void cboAddToInventory_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            try
            {
                if ((cboAction.Text!=string.Empty) && (txtQty.Text!=string.Empty) && (txtReason.Text!=string.Empty))
                {
                    if (int.Parse(txtQty.Text) >= int.Parse(txtCancelQty.Text))
                    {
                        frmVoid f = new frmVoid(this);
                        f.ShowDialog();
                    }
                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void RefreshList() 
        {
            f.LoadRecord();
        }
    }
}
