using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using CMWA.Packager.Tools.Bytes;
using TopMachine.Desktop.Controls;
using CMWA.Packager.Tools;

namespace TopMachine.Desktop
{
    public partial class Register : TitledUserControl
    {
        public Register()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
/*            string product = ((Button)sender).Tag.ToString();

            ByteAccessor ba = new ByteAccessor();
            string key = RegistrationKeys.GetRegistrationKey(product);

            txtCode.Text = key;

            txtVerification.Text = RegistrationKeys.GetVerificationString(key);
*/
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
               /* Packages.Instance.AddDictionaryReg(
                    ((Button)sender).Tag.ToString(), openFileDialog1.FileName, "", LocationType.CustomFiles);
                */
            }
        }
    }
}
