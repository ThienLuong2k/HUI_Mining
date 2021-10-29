using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUIMining
{
    public class UtilityList
    {
        private List<Element> elements;
        private int sumIutil;
        private int sumReutil;

        public List<Element> Elements { get => elements; set => elements = value; }

        public int SumIutil { get => sumIutil; set => sumIutil = value; }

        public int SumReutil { get => sumReutil; set => sumReutil = value; }

        public UtilityList()
        {
            Elements = new List<Element>();
            SumIutil = 0;
            SumReutil = 0;
        }

        public UtilityList(List<Element> _elements, int _iutils, int _reutils)
        {
            Elements = _elements;
            SumIutil = _iutils;
            SumReutil = _reutils;
        }

        public void AddElement(Element e)
        {
            Elements.Add(e);
            SumIutil += e.Iutil;
            SumReutil += e.Reutil;
        }
    }
}
