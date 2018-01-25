using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using System.Windows.Threading;
using ExcelDataReader;
using DataSet = System.Data.DataSet;
using DataTable = System.Data.DataTable;
using DataRow = System.Data.DataRow;
using DataColumn = System.Data.DataColumn;
using System.Xml;
using System.Xml.Xsl;
using System.Data;

namespace translations_comparison
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LogboxUpdate("Terminology Comparison 1.1 \n\n\n\n");
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

            ExcelFile sourcefile = new ExcelFile(File1PathBox.Text, false);
            ExcelFile targetfile = new ExcelFile(File2PathBox.Text, false);

            string targetDirectory = Path.GetDirectoryName(File2PathBox.Text);


            if (!(Languages.Text == "" || Languages.Text == null))
            {
                int sourceColumn = sourcefile.LanguageAvailableInColumn(Languages.Text);
                int targetColumn = targetfile.LanguageAvailableInColumn(Languages.Text);
                if (!(sourceColumn == 0))
                {
                    LogboxUpdate("\n\nLanguage " + Languages.Text + " found in source file in column " + ColumnIndexToColumnLetter(sourceColumn));

                    if (!(targetColumn == 0))
                    {
                        LogboxUpdate("\n\nLanguage " + Languages.Text + " found in target file in column " + ColumnIndexToColumnLetter(targetColumn));
                    }
                    else
                    {
                        LogboxUpdate("\n\nLanguage " + Languages.Text + " was missing in target file and added in column " + ColumnIndexToColumnLetter(targetColumn));
                    }


                    foreach (UI ui in targetfile.UIList)
                    {
                        ui.EqualTermRow = ui.CompareUIWithEachTermFromAList(sourcefile.UIList);
                        if (!(ui.EqualTermRow == 0))
                        {
                            targetfile.Sheet.Rows[ui.Row][targetColumn] = sourcefile.Sheet.Rows[ui.EqualTermRow][sourceColumn].ToString();
                            string test = targetfile.Sheet.Rows[ui.Row][targetColumn].ToString();
                        }
                    }

                    targetfile.Rows = targetfile.Sheet.Rows.Count;
                    targetfile.Columns = targetfile.Sheet.Columns.Count;

                    CreateExcelFile.CreateExcelDocument(targetfile.Book, targetDirectory + "\\newUI.xls");
                    LogboxUpdate("\n\nNew File created:" + targetDirectory + "\\newUI.xls");
                }
                else
                {
                    Logbox.Foreground = new SolidColorBrush(Colors.Red);
                    LogboxUpdate("\n\nLanguage " + Languages.Text + " not found in source file!");
                }
            }

            else
            {
                LogboxUpdate("\n\nYou haven't selected a language!");
            }

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
            ExcelFile sourcefile = new ExcelFile(File1PathBox.Text,true);
            ExcelFile targetfile = new ExcelFile(File2PathBox.Text,true);

            string targetDirectory = Path.GetDirectoryName(File2PathBox.Text);


            if (!(Languages.Text == "" || Languages.Text == null))
            {
                int sourceColumn = sourcefile.LanguageAvailableInColumn(Languages.Text);
                int targetColumn = targetfile.LanguageAvailableInColumn(Languages.Text);
                if (!(sourceColumn == 0))
                {
                    LogboxUpdate("\n\nLanguage " + Languages.Text + " found in source file in column " + ColumnIndexToColumnLetter(sourceColumn));

                    if (!(targetColumn == 0))
                    {
                        LogboxUpdate("\n\nLanguage " + Languages.Text + " found in target file in column " + ColumnIndexToColumnLetter(targetColumn));
                    }
                    else
                    {
                        LogboxUpdate("\n\nLanguage " + Languages.Text + " was missing in target file and added in column " + ColumnIndexToColumnLetter(targetColumn));
                    }


                    foreach (Term term in targetfile.TermList)
                    {
                        int test1 = term.Row;
                        test1 = term.Row;
                        term.EqualTermRow = term.CompareTermWithEachTermFromAList(sourcefile.TermList);
                        if (!(term.EqualTermRow == 0))
                        {
                            targetfile.Sheet.Rows[term.Row][targetColumn-1] = sourcefile.Sheet.Rows[term.EqualTermRow][sourceColumn-1].ToString();
                            string test = targetfile.Sheet.Rows[term.Row][targetColumn].ToString();
                        }
                    }

                    targetfile.Rows = targetfile.Sheet.Rows.Count;
                    targetfile.Columns = targetfile.Sheet.Columns.Count;

                    CreateExcelFile.CreateExcelDocument(targetfile.Book, targetDirectory+"\\newTerminology.xlsx");
                    LogboxUpdate("\n\nNew File created:" + targetDirectory + "\\newTerminology.xlsx");

                }
                else
                {
                    Logbox.Foreground = new SolidColorBrush(Colors.Red);
                    LogboxUpdate("\n\nLanguage " + Languages.Text + " not found in source file!");
                }
            }

            else
            {
                LogboxUpdate("\n\nYou haven't selected a language!");
            }

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