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
using System.Data;
using DataSet = System.Data.DataSet;
using DataTable = System.Data.DataTable;
using DataRow = System.Data.DataRow;
using DataColumn = System.Data.DataColumn;
using ExcelDataReader;

namespace translations_comparison
{
    public class ExcelFile
    {
        //msdn.microsoft.com/de-de/library/system.data.dataset(v=vs.110).aspx <-- Zur Doku
        private DataSet _Book;
        private DataTable _Sheet;
        private int _Rows;
        private int _Columns;
        private List<Term> _TermList;

        public DataSet Book { get => _Book; set => _Book = value; }
        public int Rows { get => _Rows; set => _Rows = value; }
        public int Columns { get => _Columns; set => _Columns = value; }
        public List<Term> TermList { get => _TermList; set => _TermList = value; }
        public DataTable Sheet { get => _Sheet; set => _Sheet = value; }

        public ExcelFile(string path)
        {
            var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            Book = reader.AsDataSet();
            Sheet = new DataTable();
            Sheet = Book.Tables[0];
            Rows = Sheet.Rows.Count;
            Columns = Sheet.Columns.Count;
        }


        public int LanguageAvailableInColumnOrNull(string languageCode)
        {
            int i = 0;

            while (i <= Columns && Sheet.Columns[i].ColumnName.Equals(languageCode) == false)
                {
                    i++;
                };


            if ((this.Sheet.Columns[i].ColumnName == null) || (Sheet.Columns[i].ColumnName.Equals(languageCode) == false))
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
            Sheet.Columns.Add(languageCode);
            this.Columns = Sheet.Columns.Count;
            return this.Columns;
        }

        public bool CompareValues(string sourceValue, string valueToCompareWith)
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
                int i = 0;
                do
                {
                    i++;
                    if (!(Sheet.Rows[i][0] == null || Sheet.Rows[i][0].ToString() == ""))
                    {
                        Term x = new Term(Sheet.Rows[i][0].ToString(), Sheet.Rows[i][1].ToString(), Sheet.Rows[i][2].ToString(), Sheet.Rows[i][3].ToString(), Sheet.Rows[i][4].ToString(), i);
                        TermList.Add(x);
                    }
                } while (i < Rows);
            }
        }

        //___________________________________________//
    }
}