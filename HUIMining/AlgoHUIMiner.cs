using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HUIMining
{
    public class AlgoHUIMiner
    {
        private StreamWriter writer;
        // 
        private const int BUFFER_SIZE = 200;
        private int[] itemsetBuffer;
        public int huiCount;
        private XuLy xuly;
        
        public AlgoHUIMiner()
        {
            xuly = new XuLy();
        }

        /// <summary>
        ///     Đọc dữ liệu từ file txt
        /// </summary>
        /// <param name="filename"> đường dẫn file txt </param>
        /// <param name="cItems"> số lượng item trong CSDL </param>
        /// <param name="cTransactions"> số lượng giao dịch </param>
        /// <returns> nếu đọc DL thành công: true và có số lượng item, số lượng giao dịch</returns>
        public bool Input(string filename, out int cItems, out int cTransactions)
        {
            return xuly.NhapDL(filename, out cItems, out cTransactions);
        }

        /// <summary>
        ///     Tìm các tập hữu ích cao dùng thuật toán HUI-Miner.
        /// </summary>
        /// <param name="file_input"> đường dẫn file dữ liệu </param>
        /// <param name="minutil"> minutil </param>
        public void RunAlgoHuiminer(string file_input, string file_output, int minutil)
        {
            // khởi tạo writer
            writer = new StreamWriter(file_output, false);
            // khởi tạo buffer
            itemsetBuffer = new int[BUFFER_SIZE];
            // list chứa Utility List của các item i
            // thỏa điều kiện TWU(i) >= minutil
            List<UtilityList> ListUL = new List<UtilityList>();
            // lưu Utility List của mỗi item
            Dictionary<int, UtilityList> ListULofItem = new Dictionary<int, UtilityList>();
            Dictionary<int, float> list_item = xuly.ListItem;
            // với mỗi item
            foreach (int item in list_item.Keys)
            {
                // nếu item thỏa điều kiện cắt tỉa (TWU >= minutil)
                if (list_item[item] >= minutil)
                {
                    // khởi tạo UL của item (UL rỗng)
                    UtilityList ul = new UtilityList(item);
                    ListULofItem.Add(item, ul);
                    // thêm vào list trước, điền từng dòng vào sau
                    ListUL.Add(ul);
                }
            }
            // sắp xếp các Utility List dựa vào TWU của item
            ListUL.Sort(delegate (UtilityList ul1, UtilityList ul2) {
                return xuly.CompareItems(ul1.ID, ul2.ID);
            });
            // quét CSDL để điền từng dòng vào Utility List của từng item
            // thỏa điều kiện TWU >= minutil
            string[] db = File.ReadAllLines(file_input);
            int tid = 0;
            // với mỗi dòng trong file CSDL
            for (int line = 0; line < db.Length; line++)
            {
                // Nếu dòng này không phải giao dịch
                if (String.IsNullOrEmpty(db[line]) || db[line][0] == '#'
                    || db[line][0] == '%' || db[line][0] == '@')
                {
                    continue;
                }
                // else: 
                tid++;
                // Mỗi giao dịch gồm 3 phần
                string[] tc = db[line].Split(':');
                // Phần 1: danh sách item xuất hiện trong giao dịch, cách nhau bởi khoảng trắng
                string[] items = tc[0].Split(' ');
                // Phần 2: giá trị TWU của giao dịch (không lấy ở đây)
                // Phần 3: độ hữu ích của các item trong giao dịch
                string[] utilityValues = tc[2].Split(' ');

                // Thực hiện chỉnh sửa giao dịch
                float reu = 0; // remaining utility
                               // Dùng list chứa các item để biểu diễn 1 giao dịch được chỉnh sửa
                List<Item> revisedTc = new List<Item>();
                // với mỗi item trong giao dịch
                for (int j = 0; j < items.Length; j++)
                {
                    // chuyển các giá trị về số
                    int item = int.Parse(items[j]);
                    float utility = float.Parse(utilityValues[j]);
                    // nếu item có TWU >= minutil
                    if (list_item[item] >= minutil)
                    {
                        // Thêm item vào giao dịch được chỉnh sửa
                        Item it = new Item(item, utility);
                        revisedTc.Add(it);
                        reu += utility;
                    }
                }
                // sắp xếp lại các item tăng dần theo TWU
                revisedTc.Sort(delegate (Item i1, Item i2)
                {
                    return xuly.CompareItems(i1.Name, i2.Name);
                });

                // với mỗi item trong giao dịch được chỉnh sửa
                for (int i = 0; i < revisedTc.Count; i++)
                {
                    // lấy tên và utility của item đang xét
                    int item_x = revisedTc[i].Name;
                    float x_utility = revisedTc[i].utility;
                    reu -= x_utility;
                    // Lấy Utility List của item
                    UtilityList ULofItem = ListULofItem[item_x];
                    // tạo Element của item ứng với giao dịch
                    Element e = new Element(tid, x_utility, reu);
                    // thêm Element vừa tạo vào UL của item
                    ULofItem.AddElement(e);

                    // *** Do ULofItem, ListULofItem và ListUL là các biến tham chiếu
                    // *** nên thêm vào ULofItem thì listUL cũng được thêm tương tự
                }
            }
            huiCount = 0;
            HUI_Miner(itemsetBuffer, 0, null, ListUL, minutil);
            writer.Close();
        }

        /// <summary>
        ///     Hàm biểu diễn thuật toán HUI-Miner.
        /// </summary>
        /// <param name="pUL"> Utility List của item/itemset P </param>
        /// <param name="ULs"> Utility List của các mở rộng của P </param>
        /// <param name="minutil"> minutil </param>
        private void HUI_Miner(int[] prefix, int prefixLength, UtilityList pUL, List<UtilityList> ULs, int minutil)
        {
            // với mỗi Utility List của Px trong ULs
            for (int i = 0; i < ULs.Count; i++)
            {
                UtilityList X = ULs[i];
                // Nếu Px là HUI: xuất ra file
                if (X.SumIutil >= minutil)
                {
                    WriteOut(prefix, prefixLength, X.ID, X.SumIutil);
                }
                // Nếu Utility List có thể kết hợp
                if (X.SumIutil + X.SumReutil >= minutil)
                {
                    // tạo list chứa UL của các mở rộng của Px
                    List<UtilityList> exULs = new List<UtilityList>();

                    // với mỗi Py nằm phía sau Px trong danh sách ULs,
                    // thực hiện kết hợp UL của Py này với UL của Px
                    // và đưa vào list trên
                    for (int j = i + 1; j < ULs.Count; j++)
                    {
                        UtilityList Y = ULs[j];
                        exULs.Add(xuly.Construct(pUL, X, Y));
                    }
                    // tạo prefix mới
                    itemsetBuffer[prefixLength] = X.ID;
                    // dùng đệ quy gọi lại thuật toán
                    HUI_Miner(itemsetBuffer, prefixLength + 1, X, exULs, minutil);
                }
            }
        }

        private void WriteOut(int[] prefix, int prefixLength, int item, float utility)
        {
            huiCount++;
            for (int iPrefix = 0; iPrefix < prefixLength; iPrefix++)
            {
                writer.Write(prefix[iPrefix] + " ");
            }
            writer.WriteLine(item + " #UTIL: " + utility);
        }

        public void Clear()
        {
            xuly.Refresh();
        }
    }
}
