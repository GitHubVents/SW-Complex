using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SwSpecification
{
    public partial class addincheckbox : Form
    {
        public addincheckbox()
        {
            InitializeComponent();
        }

        public NewDoc NewDocForm;
        private void addincheckbox_Load(object sender, EventArgs e)
        {
            //Checkbox Off

            ChkElectro1.Enabled = false;
            ChkElectro2.Enabled = false;

            NewDocForm = null;

            //Fill DataGriad
            try
            {
                foreach (string line in System.IO.File.ReadAllLines("C:\\Program Files\\SW-Complex\\doc.txt"))
                {
                    DGDoc.Rows.Add(DocChb.Selected, line);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Файл Doc.txt отсутствует");
                this.Close();
            }
          
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnOk_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        #region " Устанавливать по электромнотажу "
            private void ChkElectro_CheckedChanged(object sender, EventArgs e)
            {
                if (ChkElectro.Checked == true)
                {
                    ChkElectro1.Enabled = true;
                    ChkElectro2.Enabled = true;
                }
                else
                {
                    ChkElectro1.Enabled = false;
                    ChkElectro1.Checked = false;
                    ChkElectro2.Enabled = false;
                    ChkElectro2.Checked = false;
                }
            }

            private void ChkElectro1_CheckedChanged_1(object sender, EventArgs e)
            {
                if (ChkElectro1.Checked == true)
                {
                    ChkElectro2.Checked = false;
                }
            }

            private void ChkElectro2_CheckedChanged_1(object sender, EventArgs e)
            {
                if (ChkElectro2.Checked == true)
                {
                    ChkElectro1.Checked = false;
                }
            }
        #endregion

        
    }
}
