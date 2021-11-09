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
        // danh sách chứa tên item và độ hữu ích của item trong giao dịch
        private List<Itemset> content;
        private float tu;

        public int ID { get => id; set => id = value; }
        public List<Itemset> Content { get => content; set => content = value; }
        public float TU { get => tu; set => tu = value; }

        public Transaction(int _id)
        {
            ID = _id;
            Content = new List<Itemset>();
            TU = 0;
        }

        public Transaction(Transaction T)
        {
            ID = T.ID;
            Content = T.Content;
            TU = T.TU;
        }

        /// <summary>
        ///     Thêm 1 item vào giao dịch.
        /// </summary>
        /// <param name="item"> item </param>
        /// <param name="utility"> độ hữu ích của item trong giao dịch </param>
        /// <returns> 
        ///     Nếu item đã có trong giao dịch: không được thêm và trả về false.
        ///     Ngược lại, thêm item và trả về true.
        /// </returns>
        public bool AddItem(int item, float utility)
        {
            // nếu item đã có trong giao dịch: không được thêm
            for(int i = 0; i < Content.Count; i++)
            {
                if (Content[i].Name[0] == item)
                    return false;
            }
            // else: thêm item và cộng utility vào TU
            Content.Add(new Itemset(item, utility));
            TU += utility;
            return true;
        }

        /// <summary>
        ///     Xóa item trong giao dịch.
        /// </summary>
        /// <param name="item"> item </param>
        /// <returns>
        ///     Nếu có item trong giao dịch: xóa item và trả về true.
        ///     Ngược lại trả về false.
        /// </returns>
        public bool DeleteItem(int item)
        {
            // nếu item đã có trong giao dịch: xóa item và trừ đi utility của item khỏi TU
            for(int i = 0; i < Content.Count; i++)
            {
                if (Content[i].Name[0] == item)
                {
                    TU -= Content[i].utility;
                    Content.RemoveAt(i);
                    return true;
                }
            }
            // else
            return false;
        }

        /// <summary>
        ///     Thay đổi độ hữu ích của item trong giao dịch.
        /// </summary>
        /// <param name="item"> item </param>
        /// <param name="newUtility"> độ hữu ích mới cần cập nhật </param>
        public bool ChangeUtility(int item, float newUtility)
        {
            // nếu item đã có trong giao dịch: cập nhật utility và TU
            for (int i = 0; i < Content.Count; i++)
            {
                if(Content[i].Name[0] == item)
                {
                    TU -= Content[i].utility;
                    Content[i].utility = newUtility;
                    TU += newUtility;
                    return true;
                }
            }
            // else
            return false;
        }
    }
}
