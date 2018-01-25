using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translations_comparison
{
    public class UI
    {
        private string _Key;
        private int _Row;
        private int _EqualTermRow;

        public string Key { get => _Key; set => _Key = value; }
        public int Row { get => _Row; set => _Row = value; }
        public int EqualTermRow { get => _EqualTermRow; set => _EqualTermRow = value; }


        public UI(string key, int row)
        {
            Key = key;
            Row = row;
            EqualTermRow = 0;
        }

        public UI()
        {
            Key = "";
            Row = 2;
            EqualTermRow = 0;

        }

        public UI(UI ui)
        {
            Key = ui.Key;
            Row = ui.Row;
            EqualTermRow = ui.EqualTermRow;
        }

        public int CompareUIWithEachTermFromAList(List<UI> uilist)
        {
            List<UI> equalterms = new List<UI>();
            foreach (UI ui in uilist)
            {
                UIListFunction(equalterms, CompareTermKeys(ui) == true, false);
            }

            if (!(equalterms.Count == 0))
            {
                return equalterms[0].Row;
            }

            else
            {
                return 0;
            }

        }

        private void UIListFunction(List<UI> list, bool condition, bool deleting)
        {
            if (condition)
            {
                if (deleting)
                {
                    list.Remove(this);
                }

                else
                {
                    list.Add(this);
                }
            }
        }

        private bool CompareTermKeys(UI target)
        {
            if (target.Key == this.Key)
            {
                return true;
            }

            else
            {
                return false;
            }
        }


        public List<UI> GroupUITermsByKeyAndSortThemOut(List<UI> list)
        {
            List<UI> result = new List<UI>();
            result.Add(this);
            list.Remove(this);
            foreach (UI ui in list)
            {
                if (Key.Equals(ui.Key))
                {
                    result.Add(ui);
                    list.Remove(ui);
                }
            }
            return result;
        }
    }
}
