using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.IO;
using Microsoft.Win32;
using System.Windows.Threading;
using ExcelDataReader;
using DataSet = System.Data.DataSet;
using DataTable = System.Data.DataTable;
using DataRow = System.Data.DataRow;
using DataColumn = System.Data.DataColumn;
using ClosedXML.Excel;

namespace translations_comparison
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LogboxUpdate("Terminology Comparison 1.1 \n\n");
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

        protected void Logbox_TextChanged(object sender, EventArgs e)
        {

        }

        private void LogboxUpdate(object additionaltext)
        {
            DispatcherOperation op = Dispatcher.BeginInvoke((Action)(() => {
                Logbox.Text = Logbox.Text + additionaltext;
            }));
        }


        public void Terminology_Click(object sender, RoutedEventArgs e)
        {
            ExcelFile sourcefile = new ExcelFile(File1PathBox.Text);
            ExcelFile targetfile = new ExcelFile(File2PathBox.Text);

            string targetDirectory = System.IO.Path.GetDirectoryName(File2PathBox.Text);

            bool temp = false;
            do
            {
                temp = LanguageCheck(sourcefile, targetfile);
            } while (temp == false);

            int sourceColumn = sourcefile.LanguageAvailableInColumnOrNull(Languages.Text);
            int targetColumn = targetfile.LanguageAvailableInColumnOrNull(Languages.Text);

            List<Term> sourceTermsCopy = sourcefile.TermList;
            List<Term> targetTermsCopy = targetfile.TermList;

            foreach (Term term in sourceTermsCopy)
            {
                int termrow = term.CompareTermWithEachTermFromAList(targetTermsCopy);
                if (!(termrow == 0))
                {
                    targetfile.Sheet.[termrow, targetColumn].Value = sourcefile.Sheet.[term.Row, sourceColumn].Value.ToString();
                }
            }

            XLWorkbook wb = new XLWorkbook();
            wb.Worksheets.Add(targetfile.Sheet, "WorksheetName");
            wb.SaveAs(File2PathBox.Text);


            System.IO.File.WriteAllText(@targetDirectory + "\\Log.txt", Logbox.Text);
        }

        public void PrintRows(DataSet dataSet)
        {
            // For each table in the DataSet, print the row values.
            foreach (DataTable table in dataSet.Tables)
            {
                foreach (DataRow row in table.Rows)
                {
                    foreach (DataColumn column in table.Columns)
                    {
                        LogboxUpdate(row[column]);
                    }
                }
            }
        }

        private bool LanguageCheck(ExcelFile sourcefile, ExcelFile targetfile)
        {
            if (!(Languages.Text == "" || Languages.Text == null))
            {
                int sourcelanguage = sourcefile.LanguageAvailableInColumnOrNull(Languages.Text);
                int targetlanguage = targetfile.LanguageAvailableInColumnOrNull(Languages.Text);
                if (!(sourcelanguage == 0))
                {
                    LogboxUpdate("\nLanguage " + Languages.Text + " found in source file in column " + ColumnIndexToColumnLetter(sourcelanguage));

                    if (!(targetlanguage == 0))
                    {
                        LogboxUpdate("\nLanguage " + Languages.Text + " found in target file in column " + ColumnIndexToColumnLetter(targetlanguage));
                        return true;
                    }
                    else
                    {
                        targetlanguage = targetfile.CreateLanguageInColumn(Languages.Text);
                        LogboxUpdate("\nLanguage " + Languages.Text + " was missing in target file and added in column " + ColumnIndexToColumnLetter(targetlanguage));
                        return true;
                    }
                }
                else
                {
                    Logbox.Foreground = new SolidColorBrush(Colors.Red);
                    LogboxUpdate("\nLanguage " + Languages.Text + " not found in source file!");
                    return false;
                }
            }
            else
            {
                LogboxUpdate("\nYou haven't selected a language!");
                return false;
            }
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

        private void File1PathBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void Logbox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private IExcelDataReader Read_File(string path)
        {
            using (var stream = File.Open(path, FileMode.Open, FileAccess.Read))
            {
                var reader = ExcelReaderFactory.CreateReader(stream);
                {
                    do
                    {
                        while (reader.Read())
                        {
                            // reader.GetDouble(0);
                        }
                    } while (reader.NextResult());
                }
                return reader;
            }
        }
    }
}