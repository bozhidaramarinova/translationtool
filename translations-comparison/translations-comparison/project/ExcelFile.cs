using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Reflection;
using Excel = Microsoft.Office.Interop.Excel;
using System.Diagnostics;

namespace translations_comparison
{
    public class ExcelFile
    {
        private Excel.Application _App;
        private Excel.Workbook _Workbook;
        private Excel._Worksheet _Worksheet;
        private Excel.Range _Range;
        private int _Rows;
        private int _Columns;
        private List<Term> _Terms;

        public Excel.Application App { get => _App; set => _App = value; }
        public Excel.Workbook Workbook { get => _Workbook; set => _Workbook = value; }
        public Excel._Worksheet Worksheet { get => _Worksheet; set => _Worksheet = value; }
        public Excel.Range Range { get => _Range; set => _Range = value; }
        public int Rows { get => _Rows; set => _Rows = value; }
        public int Columns { get => _Columns; set => _Columns = value; }
        public Process excelProcess { get; set; }
        public List<Term> Terms { get => _Terms; set => _Terms = value; }

        public ExcelFile(ExcelFile excelfile)
        {
            App = new Excel.Application();
            App.Visible = true;
            Workbook = excelfile.Workbook;
            Worksheet = excelfile.Workbook.Sheets[1];
            Range = excelfile.Workbook.Sheets[1].UsedRange;
            Rows = excelfile.Workbook.Sheets[1].UsedRange.Rows.Count;
            Columns = excelfile.Workbook.Sheets[1].UsedRange.Columns.Count;
            Terms = excelfile.Terms;
        }

        public ExcelFile(string filepath)
        {
            App = new Excel.Application();
            App.Visible = true;
            Workbook = App.Workbooks.Open(@filepath);
            Worksheet = Workbook.Sheets[1];
            Range = Worksheet.UsedRange;
            Rows = Range.Rows.Count;
            Columns = Range.Columns.Count;
            Terms = new List<Term>();
            AddTerms();
        }

        public int LanguageAvailableInColumnOrNull(string languageCode)
        {
            int i = 0;
            do
            {
                i++;
            } while (i <= Columns && this.CompareValues(Worksheet.Cells[1, i].Value,languageCode) == false);


            if (this.CompareValues(Worksheet.Cells[1, i].Value, languageCode) == false)
            {
                return 0;
            }

            else
            {
                return i;
            }
        }

        public int CreateLanguageInColumn(string languageCode)
        {
            this.Columns++;
            Worksheet.Cells[1, this.Columns].Value = languageCode;
            return this.Columns;
        }

        public bool CompareValues(string sourceValue,string valueToCompareWith)
        {
            if (!(sourceValue == null || sourceValue == ""))
            {
                if (!(valueToCompareWith == null || valueToCompareWith == ""))
                {
                    return sourceValue.Trim().ToLower().Equals(valueToCompareWith.Trim().ToLower());
                }
                else
                {
                    return false;
                }
            }

            else
            {
                return false;
            }
        }

        private void AddTerms()
        {
            {
                int i = 1;
                do
                {
                    i++;
                    Terms.Add(new Term(Worksheet.Cells[i, 1].Value, Worksheet.Cells[i, 2].Value, Worksheet.Cells[i, 3].Value, Worksheet.Cells[i, 4].Value, Worksheet.Cells[i, 5].Value,i));
                } while (i <= Rows);
            }
        }

        //___________________________________________//

        public void CleanUp()
        {
            CloseSheet(this);
        }

        public static void CloseSheet(ExcelFile thisExcel)
        {
            if (thisExcel.excelProcess != null)
            {
                try
                {
                    thisExcel.excelProcess.Kill();
                    thisExcel.excelProcess.Dispose();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Something went wrong: {0}", ex.Source);

                }
            }
            else
            {
                thisExcel.Workbook.Close(0);
                thisExcel.App.Quit();
            }
            releaseObject(thisExcel.Worksheet);
            releaseObject(thisExcel.Workbook);
            releaseObject(thisExcel.App);
            releaseObject(thisExcel.excelProcess);
            releaseObject(thisExcel);
        }

        public static void releaseObject(object obj)
        {
            try
            {
                Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
            }
        }
    }
}