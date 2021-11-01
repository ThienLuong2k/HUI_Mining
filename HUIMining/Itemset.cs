using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUIMining
{
    public class Itemset
    {
        private List<int> name;
        private float u;

        public List<int> Name { get => name; set => name = value; }

        public float utility { get => u; set => u = value; }

        public Itemset()
        {
            Name = new List<int>();
            utility = 0;
        }

        public Itemset(int _name, float _utility)
        {
            Name = new List<int>(1) { _name };
            utility = _utility;
        }

        public Itemset(List<int> _name, float _utility)
        {
            Name = _name;
            utility = _utility;
        }

        public Itemset(Itemset i)
        {
            this.Name = i.Name;
            this.utility = i.utility;
        }
    }
}
