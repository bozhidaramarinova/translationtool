using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translations_comparison.project
{
    class Column
    {
        private string _Title;
        private Cell[] _Cells;
        private string _ColumnID;

        public string Title { get => _Title; set => _Title = value; }
        public Cell[] Cells { get => _Cells; set => _Cells = value; }
        public string ColumnID { get => _ColumnID; set => _ColumnID = value; }

        public Column()
        {
            Title = "";
            Cells = new Cell[0];
        }
    }
}
