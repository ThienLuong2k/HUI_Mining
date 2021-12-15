using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUIMining
{
    public class XuLy
    {
        public Dictionary<int, float> ListItem; // item, TWU

        public XuLy()
        {
            ListItem = new Dictionary<int, float>();
        }

        /// <summary>
        ///     Đọc dữ liệu
        /// </summary>
        /// <param name="filename"> đường dẫn file txt </param>
        /// <param name="countItems"> số lượng item trong CSDL </param>
        /// <param name="countTc"> số lượng giao dịch </param>
        /// <returns> nếu đọc DL thành công: true và có số lượng item, số lượng giao dịch</returns>
        public bool NhapDL(string filename, out int countItems, out int countTc)
        {
            try
            {
                countTc = 0;
                string[] db = File.ReadAllLines(filename);
                for(int i = 0; i < db.Length; i++)
                {
                    // Một vài file dữ liệu sẽ có các dòng rỗng, hoặc các dòng
                    // chú thích hay metadata thì bắt đầu bằng một trong các ký tự '#','%','@'
                    if(String.IsNullOrEmpty(db[i]) || db[i][0] == '#' 
                        || db[i][0] == '%' || db[i][0] == '@')
                    {
                        continue;
                    }
                    // else:
                    countTc++;
                    // Mỗi giao dịch gồm 3 phần
                    string[] tc = db[i].Split(':');
                    // Phần 1: danh sách item xuất hiện trong giao dịch, cách nhau bởi khoảng trắng
                    string[] items = tc[0].Split(' ');
                    // Phần 2: TU của giao dịch
                    float tu = float.Parse(tc[1]);
                    // Phần 3: độ hữu ích của các item trong giao dịch (không lấy ở đây)

                    // với mỗi item, cộng TU vào TWU
                    for(int j = 0; j < items.Length; j++)
                    {
                        int item = int.Parse(items[j]);
                        // nếu item đã có trong danh sách
                        if (ListItem.ContainsKey(item))
                        {
                            ListItem[item] += tu;
                        }
                        else
                        {
                            // thêm item vào danh sách, TWU = 0 + TU của giao dịch
                            ListItem.Add(item, tu);
                        }
                    }
                }
                countItems = ListItem.Count;
                return true;
            }
            catch
            {
                countItems = countTc = 0;
                return false;
            }
        }


        /// <summary>
        ///     Kết hợp Utility List của Px và Py.
        /// </summary>
        /// <param name="P"> UL của itemset P </param>
        /// <param name="Px"> UL của itemset Px </param>
        /// <param name="Py"> UL của itemset Py </param>
        /// <returns> UL của itemset Pxy. </returns>
        public UtilityList Construct(UtilityList P, UtilityList Px, UtilityList Py)
        {
            UtilityList pxyUL = new UtilityList(Py.ID);
            foreach(Element Ex in Px.Elements)
            {
                // find element Ey in Py with Tid = Ex.Tid
                Element Ey = FindElementWithTid(Py, Ex.Tid);
                if(Ey == null)
                {
                    continue;
                }
                // if the prefix P is null
                if(P == null)
                {
                    Element Exy = new Element(Ex.Tid, Ex.Iutil + Ey.Iutil, Ey.Reutil);
                    pxyUL.AddElement(Exy);
                }
                else
                {
                    // find the element in the Utility List of P with the same Tid
                    Element e = FindElementWithTid(P, Ex.Tid);
                    if(e != null)
                    {
                        Element Exy = new Element(Ex.Tid, Ex.Iutil + Ey.Iutil - e.Iutil, Ex.Reutil);
                        pxyUL.AddElement(Exy);
                    }
                }
            }
            return pxyUL;
        }

        /// <summary>
        ///     Tìm element trong Utility List với tid, dùng thuật tìm kiếm nhị phân.
        /// </summary>
        /// <param name="ul"> Utility List cần tìm element </param>
        /// <param name="tid"> Tid của element cần tìm </param>
        /// <returns> 1 element nếu tìm thấy, hoặc null </returns>
        private Element FindElementWithTid(UtilityList ul, int tid)
        {
            List<Element> eList = ul.Elements;
            int left = 0;
            int right = eList.Count - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                if (eList[mid].Tid < tid)
                {
                    left = mid + 1;
                }
                else if (eList[mid].Tid > tid)
                {
                    right = mid - 1;
                }
                else
                {
                    return eList[mid];
                }
            }
            return null;
        }

        /// <summary>
        ///     Hàm cung cấp 1 số nguyên C để so sánh 2 item
        /// </summary>
        /// <param name="item1"> item 1 </param>
        /// <param name="item2"> item 2 </param>
        /// <returns>
        ///     C = TWU(item 2) - TWU(item 1) <br/>
        ///     Nếu C = 0: 2 item bằng nhau -> dùng thứ tự từ điển
        /// </returns>
        public int CompareItems(int item1, int item2)
        {
            float compare = ListItem[item2] - ListItem[item1];
            return (compare == 0) ? item2 - item1 : (int)compare;
        }

        /// <summary>
        ///     Loại bỏ toàn bộ item trong List item
        /// </summary>
        public void Refresh()
        {
            ListItem.Clear();
        }
    }
}
