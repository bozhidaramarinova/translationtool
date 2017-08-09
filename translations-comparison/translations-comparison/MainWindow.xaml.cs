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
            ExcelFile sourcefile = new ExcelFile(File1PathBox.Text);
            ExcelFile targetfile = new ExcelFile(File2PathBox.Text);
            ExcelFile extendedfile = new ExcelFile(targetfile);
        }
    }
}
