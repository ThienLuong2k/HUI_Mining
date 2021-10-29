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
        private List<int> content;
        private int tu;

        public int ID { get => id; set => id = value; }
        public List<int> Content { get => content; set => content = value; }
        public int TU { get => tu; set => tu = value; }

        public Transaction()
        {
            ID = 0;
            Content = new List<int>();
            TU = 0;
        }

        public Transaction(int _id, List<int> _content, int _tu)
        {
            ID = _id;
            Content = _content;
            TU = _tu;
        }
    }
}
