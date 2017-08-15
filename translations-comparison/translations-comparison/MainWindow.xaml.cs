using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using Microsoft.Win32;

namespace translations_comparison
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void File1UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                File1PathBox.Text = openFileDialog.FileName;
        }

        public void File2UploadButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                File2PathBox.Text = openFileDialog.FileName;
        }

        protected void File1PathBox_TextChanged(object sender, EventArgs e)
        {
        }


        protected void File2PathBox_TextChanged(object sender, EventArgs e)
        {
        }

        public void TranslationOfUI_Click(object sender, RoutedEventArgs e)
        {

        }


        public void Terminology_Click(object sender, RoutedEventArgs e)
        {
            Logbox.Text = "Terminology Comparison 1.1 \n\n";
            ExcelFile sourcefile = new ExcelFile(File1PathBox.Text);
            ExcelFile targetfile = new ExcelFile(File2PathBox.Text);
            string targetDirectory = System.IO.Path.GetDirectoryName(File2PathBox.Text);

            if (!(Languages.Text == "" || Languages.Text==null))
            {
                int sourcelanguage = sourcefile.LanguageAvailableInColumnOrNull(Languages.Text);
                int targetlanguage = targetfile.LanguageAvailableInColumnOrNull(Languages.Text);
                if (!(sourcelanguage == 0))
                {
                    Logbox.Text = Logbox.Text + "\nLanguage " + Languages.Text + " found in source file in column " + ColumnIndexToColumnLetter(sourcelanguage);

                    if (!(targetlanguage == 0))
                    {
                        Logbox.Text = Logbox.Text + "\nLanguage " + Languages.Text + " found in target file in column " + ColumnIndexToColumnLetter(targetlanguage);
                    }
                    else
                    {
                        targetlanguage = targetfile.CreateLanguageInColumn(Languages.Text);
                        Logbox.Text = Logbox.Text + "\nLanguage " + Languages.Text + " was missing in target file and added in column " + ColumnIndexToColumnLetter(targetlanguage);
                    }
                }
                else
                {
                    Logbox.Foreground = new SolidColorBrush(Colors.Red);
                    Logbox.Text = Logbox.Text + "\nLanguage " + Languages.Text + " not found in source file!";
                }
                sourcefile.CleanUp();
                targetfile.Workbook.Save();
                targetfile.CleanUp();
            }
            else
            {
                Logbox.Text = Logbox.Text + "\nYou haven't selected a language!";
            }

            System.IO.File.WriteAllText(@targetDirectory + "\\Log.txt", Logbox.Text);
        }

        private string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter;
        }
    }
}