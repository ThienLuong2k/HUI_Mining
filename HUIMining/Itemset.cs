using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUIMining
{
    public class Itemset
    {
        private int[] name;
        private int twu;
        private UtilityList ul;

        public int[] Name { get => name; set => name = value; }

        public int TWU { get => twu; set => twu = value; }

        public UtilityList UL { get => ul; set => ul = value; }

        public Itemset()
        {
            Name = new int[] { 0 };
            TWU = 0;
            UL = new UtilityList();
        }

        public Itemset(int[] _name)
        {
            Name = _name;
            TWU = 0;
            UL = new UtilityList();
        }

        public Itemset(int[] _name, int _twu, UtilityList _ul)
        {
            Name = _name;
            TWU = _twu;
            UL = _ul;
        }
    }
}
