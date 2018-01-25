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
        private int _EqualTermRow;

        public string Name { get => _Name; set => _Name = value; }
        public string Location { get => _Location; set => _Location = value; }
        public string Type { get => _Type; set => _Type = value; }
        public string ID { get => _ID; set => _ID = value; }
        public string Role { get => _Role; set => _Role = value; }
        public int Row { get => _Row; set => _Row = value; }
        public int EqualTermRow { get => _EqualTermRow; set => _EqualTermRow = value; }


        public Term(string name, string location, string type, string iD, string role, int row)
        {
            Name = name;
            Location = location;
            Type = type;
            ID = iD;
            Role = role;
            Row = row;
            EqualTermRow=0;
        }

        public Term()
        {
            Name = "";
            Location = "";
            Type = "";
            ID = "";
            Role = "";
            Row = 2;
            EqualTermRow = 0;

        }

        public Term(Term term)
        {
            Name = term.Name;
            Location = term.Location;
            Type = term.Type;
            ID = term.ID;
            Role = term.Role;
            Row = term.Row;
            EqualTermRow=term.EqualTermRow;
        }

        public int CompareTermWithEachTermFromAList(List<Term> termlist)
        {
            List<Term> equalterms = new List<Term>();
            foreach (Term term in termlist)
            {
                TermListFunctionFix(equalterms, CompareTermNames(term) == true, false,term);
            }

            if (equalterms.Count > 1)
            {
                foreach (Term term in equalterms)
                {
                    TermListFunction(equalterms, CompareTermLocations(term) == false, true);
                }

                if (equalterms.Count > 1)
                {
                    foreach (Term term in equalterms)
                    {
                        TermListFunction(equalterms, CompareTermTypes(term) == false, true);
                    }
                    if (equalterms.Count > 1)
                    {
                        foreach (Term term in equalterms)
                        {
                            TermListFunction(equalterms, CompareTermRoles(term) == false, true);
                        }
                        if (equalterms.Count > 1)
                        {
                            foreach (Term term in equalterms)
                            {
                                TermListFunction(equalterms, CompareTermIDs(term) == false, true);
                            }
                            if (equalterms.Count > 1)
                            {
                                return equalterms[0].Row;
                            }
                        }
                    }
                }
            }

            if (equalterms.Count == 1)
            {
                return equalterms[0].Row;
            }

            else
            {
                return 0;
            }

        }

        private void TermListFunction(List<Term> list, bool condition,bool deleting)
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

        private void TermListFunctionFix(List<Term> list, bool condition, bool deleting,Term term)
        {
            if (condition)
            {
                if (deleting)
                {
                    list.Remove(term);
                }

                else
                {
                    list.Add(term);
                }
            }
        }

        private bool CompareTermNames(Term targetterm)
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

        private bool CompareTermLocations(Term targetterm)
        {
            if (!(this.Location == null && targetterm.Location == null))
            {
                if (targetterm.Location == this.Location)
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

        private bool CompareTermTypes(Term targetterm)
        {
            if (targetterm.Type == this.Type)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private bool CompareTermIDs(Term targetterm)
        {
            if (targetterm.ID == this.ID)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        private bool CompareTermRoles(Term targetterm)
        {
            if (targetterm.Role == this.Role)
            {
                return true;
            }

            else
            {
                return false;
            }
        }

        public List<Term> GroupTermsByNameAndSortThemOut(List<Term> termlist)
        {
            List<Term> result = new List<Term>();
            result.Add(this);
            termlist.Remove(this);
            foreach (Term term in termlist)
            {
                if (Name.Equals(term.Name))
                {
                    result.Add(term);
                    termlist.Remove(term);
                }
            }
            return result;
        }
    }
}
