using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translations_comparison.project
{
    public class Cell
    {
        private string _Content;
        private int _RowID;

        public string Content { get => _Content; set => _Content = value; }
        public int RowID { get => _RowID; set => _RowID = value; }


        public Cell()
        {
            Content = "";
            RowID = 1;
        }

        public Cell(String content)
        {
            Content = content;
        }

        public Cell(int row)
        {
            RowID = row;
        }

        public Cell(String content, int row)
        {
            Content = content;
            RowID = row;
        }

        public Cell(Cell cell)

        {
            Content = cell.Content;
        }

        public bool HasSameContentAs(Cell cell)
        {
            if (cell.IsCellEmpty()==false)
            {
                return this.ToString().Equals(cell.ToString());
            }
            else
            {
                return false;
            }
        }

        public bool IsCellEmpty()
        {
            Cell src = new Cell(this);
            if (String.IsNullOrWhiteSpace(src.Content))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DiffersInCase(string cellcontent)
        {
            Cell src = new Cell(this.ToString().Trim().ToLower());
            Cell trgt = new Cell(cellcontent.Trim().ToLower());
            if (src.Equals(trgt) && !(this.ToString().Equals(cellcontent)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool FillWithContent(Cell cell)
        {
            if (cell.IsCellEmpty())
            {
                cell.Content = this.Content;
                return true;
            }

            else
            {
                return false;
            }
        }


        public override String ToString()
        {
            return this.Content;
        }
    }
}
