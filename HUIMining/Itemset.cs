using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUIMining
{
    public class Item
    {
        private int name;
        private float u;

        public int Name { get => name; set => name = value; }

        public float utility { get => u; set => u = value; }

        public Item()
        {
            Name = 0;
            utility = 0;
        }

        public Item(int _name, float _utility)
        {
            Name = _name;
            utility = _utility;
        }

        public Item(Item i)
        {
            this.Name = i.Name;
            this.utility = i.utility;
        }
    }
}
