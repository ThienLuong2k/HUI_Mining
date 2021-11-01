using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUIMining
{
    public class Transaction
    {
        private int id;
        private List<Itemset> content;
        private float tu;

        public int ID { get => id; set => id = value; }
        public List<Itemset> Content { get => content; set => content = value; }
        public float TU { get => tu; set => tu = value; }

        public Transaction()
        {
            ID = 0;
            Content = new List<Itemset>();
            TU = 0;
        }

        public Transaction(int _id, List<Itemset> _content, float _tu)
        {
            ID = _id;
            Content = _content;
            TU = _tu;
        }

        public Transaction(Transaction T)
        {
            ID = T.ID;
            Content = T.Content;
            TU = T.TU;
        }
    }
}
