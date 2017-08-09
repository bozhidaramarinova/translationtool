using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;

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

        public Excel.Application App { get => _App; set => _App = value; }
        public Excel.Workbook Workbook { get => _Workbook; set => _Workbook = value; }
        public Excel._Worksheet Worksheet { get => _Worksheet; set => _Worksheet = value; }
        public Excel.Range Range { get => _Range; set => _Range = value; }
        public int Rows { get => _Rows; set => _Rows = value; }
        public int Columns { get => _Columns; set => _Columns = value; }

        public ExcelFile(ExcelFile excelfile)
        {
            Workbook = excelfile.Workbook;
            Worksheet = excelfile.Workbook.Sheets[1];
            Range = excelfile.Workbook.Sheets[1].UsedRange;
            Rows = excelfile.Workbook.Sheets[1].UsedRange.Rows.Count;
            Columns = excelfile.Workbook.Sheets[1].UsedRange.Columns.Count;
        }

        public ExcelFile(string filepath)
        {
            App = new Excel.Application()
            {
                Visible = true
            };
            Workbook = App.Workbooks.Open(@filepath);
            Worksheet = Workbook.Sheets[1];
            Range = Worksheet.UsedRange;
            Rows = Range.Rows.Count;
            Columns = Range.Columns.Count;
        }

        public int SearchLanguageInTargetFileOrNull(ExcelFile targetExcelFile, string languageCode)
        {
            int column = targetExcelFile.LanguageAvailableInColumnOrNull(languageCode);
            if (column == 0)
            {
                column = CreateLanguageInTargetFile(targetExcelFile,languageCode);
            }
            return column;
        }

        public int LanguageAvailableInColumnOrNull(string languageCode)
        {
            int i = 0;
            do
            {
                i++;
            } while (!(Worksheet.Cells[1, i].ToString().Trim().toLower().Equals(languageCode.Trim().ToLower())) && i <= Columns);

            if (!(Worksheet.Cells[1, i].ToString().Trim().toLower().Equals(languageCode.Trim().ToLower())))
            {
                return i;
            }

            else
            {
                return 0;
            }
        }

        public int CreateLanguageInTargetFile(ExcelFile file, string languageCode)
        {
            file.Columns++;
            Worksheet.Cells[1, file.Columns] = languageCode;
            return file.Columns;
        }

        public void CleanUp()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Marshal.ReleaseComObject(Range);
            Marshal.ReleaseComObject(Worksheet);
            Workbook.Close();
            Marshal.ReleaseComObject(Workbook);
            App.Quit();
            Marshal.ReleaseComObject(App);
        }

        public void CopyContentFromOneFileIntoAnother(ExcelFile src,ExcelFile tgt)
        {
            int i = 0;
            int j = 0;
            do
            {
                j++;
                do
                {
                    i++;
                    tgt.Worksheet.Cells[i, j] = src.Worksheet.Cells[i, j];
                } while (i < tgt.Rows);
                j++;
                i = 0;
            } while (j<tgt.Columns);
        }
    }
}
