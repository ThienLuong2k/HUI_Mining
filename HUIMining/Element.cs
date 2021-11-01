using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUIMining
{
    public class Element
    {
        private int tid;
        private float iutil;
        private float reutil;

        public int Tid { get => tid; set => tid = value; }
        public float Iutil { get => iutil; set => iutil = value; }
        public float Reutil { get => reutil; set => reutil = value; }

        public Element()
        {
            Tid = 0;
            Iutil = Reutil = 0;
        }

        public Element(int _tid, float _iutil, float _reutil)
        {
            Tid = _tid;
            Iutil = _iutil;
            Reutil = _reutil;
        }

        public Element(Element e)
        {
            this.Tid = e.Tid;
            this.Iutil = e.Iutil;
            this.Reutil = e.Reutil;
        }
    }
}
