using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace translations_comparison
{
    public class Term
    {
        private string _Name;
        private string _Location;
        private string _Type;
        private string _ID;
        private string _Role;
        private int _Row;

        public string Name { get => _Name; set => _Name = value; }
        public string Location { get => _Location; set => _Location = value; }
        public string Type { get => _Type; set => _Type = value; }
        public string ID { get => _ID; set => _ID = value; }
        public string Role { get => _Role; set => _Role = value; }
        public int Row { get => _Row; set => _Row = value; }

        public Term(string name, string location, string type, string iD, string role, int row)
        {
            Name = name;
            Location = location;
            Type = type;
            ID = iD;
            Role = role;
            Row = row;
        }

        public Term()
        {
            Name = "";
            Location = "";
            Type = "";
            ID = "";
            Role = "";
            Row = 2;
        }

        public Term(Term term)
        {
            Name = term.Name;
            Location = term.Location;
            Type = term.Type;
            ID = term.ID;
            Role = term.Role;
            Row = term.Row;
        }

        public bool CompareUniqueTerms(Term targetterm)
        {
            if (!(targetterm.Equals(this)))
            {
                if (targetterm.Name == this.Name)
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }
            else
            {
                return true;
            }
        }

        public bool CompareTermWithAList(List<Term> termlist)
        {
            foreach (Term term in termlist)
            {
                if term.Name
            }
        }

    }
}
