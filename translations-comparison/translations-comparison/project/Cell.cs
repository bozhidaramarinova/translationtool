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

        public string Content { get => _Content; set => _Content = value; }

        public Cell()
        {
            Content = "";
        }

        public Cell(String content)
        {
            Content = content;
        }

        public Cell(Cell cell)

        {
            Content = cell.Content;
        }

        public bool HasSameContentAs(Cell cell)
        {
            if (cell.IsCellEmpty()==false)
            {
                Cell src = new Cell(this);
                Cell trgt = new Cell(cell);
                trgt.Content.Trim();
                trgt.Content.ToLower();
                src.Content.Trim();
                src.Content.ToLower();
                return src.Equals(trgt);
            }
            else
            {
                return false;
            }
        }

        public bool IsCellEmpty()
        {
            Cell src = new Cell(this);
            src.Content.Trim();
            src.Content.ToLower();
            if (src.Content == "" | src.Content == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool DiffersInCase(Cell cell)
        {
            Cell src = new Cell(this);
            Cell trgt = new Cell(cell);
            trgt.ToString().Trim().ToLower();
            src.ToString().Trim().ToLower();
            src.ToString().ToLower();
            if (src.Equals(trgt) && !(this.Equals(cell)))
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
            return this.Content.ToString();
        }
    }
}
