using System.Collections.Generic;
using System.IO;
using DataSet = System.Data.DataSet;
using DataTable = System.Data.DataTable;
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
        private List<UI> _UIList;
        private bool _IsTerminology;

        public DataSet Book { get => _Book; set => _Book = value; }
        public int Rows { get => _Rows; set => _Rows = value; }
        public int Columns { get => _Columns; set => _Columns = value; }
        public List<Term> TermList { get => _TermList; set => _TermList = value; }
        public DataTable Sheet { get => _Sheet; set => _Sheet = value; }
        public List<UI> UIList { get => _UIList; set => _UIList = value; }
        public bool IsTerminology { get => _IsTerminology; set => _IsTerminology = value; }

        public ExcelFile(string path, bool isTerminology)
        {
            var stream = File.Open(path, FileMode.Open, FileAccess.Read);
            var reader = ExcelReaderFactory.CreateReader(stream);
            Book = reader.AsDataSet();
            Sheet = new DataTable();
            Sheet = Book.Tables[0];
            Rows = Sheet.Rows.Count;
            Columns = Sheet.Columns.Count;
            TermList = new List<Term>();
            UIList = new List<UI>();
            IsTerminology = isTerminology;
            if (IsTerminology)
                AddTerms();
            else
                AddUI();
        }


        public int LanguageAvailableInColumn(string languageCode)
        {
            int i = 0;
            string x = Sheet.Rows[0][i].ToString();

            if (x == languageCode)
            {
                return i;
            }
            else
            {
                while (!(x == languageCode) && i < Columns)
                {
                    x = Sheet.Rows[0][i].ToString();
                    i++;
                }
                if (!(i < Columns))
                {
                    return CreateLanguageInColumn(languageCode, i);
                }

                else return i;
            }
        }

        public int CreateLanguageInColumn(string languageCode, int column)
        {
            Sheet.Columns.Add(languageCode);
            this.Columns = Sheet.Columns.Count;
            return column;
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

        public void Save()
        {

        }

        private void AddNames()
        {
            Rows = Sheet.Rows.Count;
            Columns = Sheet.Columns.Count;
            int i = 0;
            while (i < Columns-1)
            {
                Sheet.Columns[i].ColumnName = Sheet.Rows[0][i].ToString();
                i++;
            }
        }

        public string AusgabeTest()
        {
            string bla = "";
            int col = 0;
            int row = 0;
            while (col <= Columns-1)
            {
                bla = bla + Sheet.Columns[col].ColumnName + " | ";
                col++;
            }
            col = 0;
            while (col < Columns-1)
            {
                while (row < Rows - 1)
                {
                    bla = bla + Sheet.Rows[row][col].ToString() + " | ";
                    row++;
                }
                row = 0;
                col++;
            }
            return bla;
        }

        private void AddUI()
        {
            Rows = Sheet.Rows.Count;
            Columns = Sheet.Columns.Count;
            AddNames();
            int i = 0;
            while (i < Rows - 1)
            {
                i++;
                if (!(Sheet.Rows[i][0] == null || Sheet.Rows[i][0].ToString() == ""))
                {
                    UI x = new UI(Sheet.Rows[i][0].ToString(), i);
                    UIList.Add(x);
                }
            }
        }

        private void AddTerms()
        {
            Rows = Sheet.Rows.Count;
            Columns = Sheet.Columns.Count;
            AddNames();
            int i = 0;
            while (i < Rows-1)
            {
                i++;
                if (!(Sheet.Rows[i][0] == null || Sheet.Rows[i][0].ToString() == ""))
                {
                    Term x = new Term(Sheet.Rows[i][0].ToString(), Sheet.Rows[i][1].ToString(), Sheet.Rows[i][2].ToString(), Sheet.Rows[i][3].ToString(), Sheet.Rows[i][4].ToString(), i);
                    TermList.Add(x);
                }
            }
        }

        //___________________________________________//
    }
}