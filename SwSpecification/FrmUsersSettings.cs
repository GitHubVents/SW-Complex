using System;
using System.Windows.Forms;

namespace SwSpecification
{
    public partial class FrmUsersSettings : Form
    {
        public FrmUsersSettings()
        {
            InitializeComponent();
        }

        private void FrmUsersSettings_Load(object sender, EventArgs e)
        {
            string[] iptext = new string[3];
            iptext[0] = "192.168.12.11";
            iptext[1] = "192.168.14.11";
            iptext[2] = "192.168.16.11";
            ComboBoxIP.Items.AddRange(iptext);
            ComboBoxIP.SelectedIndex = 1;
        }

        private void BtnUserSetOK_Click(object sender, EventArgs e)
        {
            
            Properties.Settings.Default.ComboBoxIP = ComboBoxIP.Text;

            if (ChkIns.Checked)
            {
                Properties.Settings.Default.ChkInsPrp = true;
            }
            else
            {
                Properties.Settings.Default.ChkInsPrp = false;
            }

            if (ChkStandard.Checked)
            {
                Properties.Settings.Default.ChkStandard = true;
            }
            else
            {
                Properties.Settings.Default.ChkStandard = false;
            }

          


            Properties.Settings.Default.Save();
            Close();
        }

        private void BtnUserSetOtmena_Click(object sender, EventArgs e)
        {
            Close();
        }

 




    }
}
