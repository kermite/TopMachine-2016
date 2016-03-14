using CMWA.Packager;
using CMWA.Packager.Tools.Bytes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TopMachine.Topping.Dawg;

namespace GenerateDico
{
	public partial class Dico : Form
	{
		List<string> DicoWords;
		public Dico()
		{
			InitializeComponent();
		}

		private void textBox1_Click(object sender, EventArgs e)
		{

			if (openFileDialog1.ShowDialog() == DialogResult.OK)
			{
				textBox1.Text = openFileDialog1.FileName;
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			try
			{
				FileInfo fi = new FileInfo(textBox1.Text);
				if (fi.Extension == ".dawg")
				{
																
					var dawg = DicoUtils.GetDicoPath(textBox1.Text);
					System.Collections.ArrayList arlWords = new System.Collections.ArrayList();
					dawg.RetrieveWords(arlWords);
					StringBuilder s = new StringBuilder();
					foreach (string strWord in arlWords)
					{
						s.AppendLine(strWord);
					}

					textBox2.Text = s.ToString();
				}
				else {

					// Open the text file using a stream reader.
					using (StreamReader sr = new StreamReader(textBox1.Text))
					{
						// Read the stream to a string, and write the string to the console.
						String line = sr.ReadToEnd();
						textBox2.Text = line;
						
				
					}
				}
				textBox2.ReadOnly = false;
			}
			catch (Exception ex)
			{
				Console.WriteLine("Fichier Inconnu");
				Console.WriteLine(ex.Message);
			}

		}

		private void txtDestination_Click(object sender, EventArgs e)
		{
			if (saveFileDialog1.ShowDialog() == DialogResult.OK)
			{
				txtDestination.Text = saveFileDialog1.FileName;
			}
		}

		private void btSave_Click(object sender, EventArgs e)
		{
			DicoWords = textBox2.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
			if (MessageBox.Show(string.Format("vous êtes sur le point de générer {0} mots, confirmez votre choix  ?", DicoWords.Count), "Confirmation", MessageBoxButtons.YesNo) == DialogResult.Yes)
			{
				TopMachine.Desktop.Controls.Tools.Progress.Instance.Show(this);
				Dawg dawg = new Dawg();

				foreach (var word in DicoWords)
				{
					dawg.AddWord(word.ToUpper());
				}

				dawg.Optimisation();

				cpt cptTools = new cpt();
			
				byte[] lstBOutput = dawg.getByteArray();
				cptTools.WriteFile(txtDestination.Text, lstBOutput);
				TopMachine.Desktop.Controls.Tools.Progress.Instance.Hide(this);
			}
		}
	}
}
