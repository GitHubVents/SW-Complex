using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Deployment.Application;

namespace SwSpecification
{
    public partial class AboutSWComplex : Form
    {
        public AboutSWComplex()
        {
            InitializeComponent();
        }
        
        private void AboutSWComplex_Load(object sender, EventArgs e)
        {
            // Set the title of the form.

            Assembly assembly = Assembly.GetExecutingAssembly();
            FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
            string version = fvi.FileVersion;


            //string ApplicationTitle = null;
            //if (!string.IsNullOrEmpty(Application.ProductName))

            //{
            //    ApplicationTitle = fvi.CompanyName;
            //}
            //else
            //{
            //    ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(Application.ProductName);
            //}
            //this.Text = string.Format("About {0}", ApplicationTitle);
            //Initialize all of the text displayed on the About Box.
            //properties dialog (under the "Project" menu).
            //var ver = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            //var versionProgram = string.Format("Каталог электромоторов и вентиляторов ver. {0}.{1}.{2}.{3}", ver.Major, ver.Minor, ver.Build, ver.Revision);


            this.LabelProductName.Text = fvi.ProductName;

            this.LabelVersion.Text = string.Format("Version {0}", version);

            //this.LabelVersion.Text = string.Format(versionProgram);

            this.LabelCopyright.Text = fvi.LegalCopyright;

            this.LabelCompanyName.Text = fvi.CompanyName;
          
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
