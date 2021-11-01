using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUIMining
{
    public class UtilityList
    {
        private List<int> id; // name of item/itemset
        private List<Element> elements;
        private float sumIutil;
        private float sumReutil;

        public List<Element> Elements { get => elements; set => elements = value; }
        public float SumIutil { get => sumIutil; set => sumIutil = value; }
        public float SumReutil { get => sumReutil; set => sumReutil = value; }
        public List<int> ID { get => id; set => id = value; }

        public UtilityList()
        {
            ID = new List<int>();
            Elements = new List<Element>();
            SumIutil = 0;
            SumReutil = 0;
        }

        public UtilityList(List<int> id)
        {
            ID = id;
            Elements = new List<Element>();
            SumIutil = SumReutil = 0;
        }

        public UtilityList(int id)
        {
            ID = new List<int>(1) { id };
            Elements = new List<Element>();
            SumIutil = SumReutil = 0;
        }

        public UtilityList(List<int> id, List<Element> _elements, float _iutils, float _reutils)
        {
            ID = id;
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
