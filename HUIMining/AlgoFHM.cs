using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HUIMining
{
    public class AlgoFHM
    {
        // key 1: item 1, key 2: item 2, value: twu(item 1, item 2)
        private Dictionary<int, Dictionary<int, float>> EUCS;
        // Stream writer
        private StreamWriter writer;
        // 
        private const int BUFFER_SIZE = 200;
        private int[] itemsetBuffer;
        public int huiCount;
        //
        private XuLy xuly;

        public AlgoFHM()
        {
            xuly = new XuLy();
            EUCS = new Dictionary<int, Dictionary<int, float>>();
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
        ///     Tìm các tập hữu ích cao dùng thuật toán FHM
        /// </summary>
        /// <param name="file_input"> đường dẫn file dữ liệu </param>
        /// <param name="minutil"> minutil </param>
        public void RunAlgoFhm(string file_input, string file_output, int minutil)
        {
            // khởi tạo writer
            writer = new StreamWriter(file_output, false);
            // khởi tạo Buffer
            itemsetBuffer = new int[BUFFER_SIZE];
            // list chứa Utility List của các item i
            // thỏa điều kiện TWU(i) >= minutil
            List<UtilityList> ListUL = new List<UtilityList>();
            // lưu Utility List của mỗi item
            Dictionary<int, UtilityList> ListULofItem = new Dictionary<int, UtilityList>();

            // với mỗi item
            foreach (int item in xuly.ListItem.Keys)
            {
                // nếu item thỏa điều kiện cắt tỉa (TWU >= minutil)
                if (xuly.ListItem[item] >= minutil)
                {
                    // khởi tạo UL của item (UL rỗng)
                    UtilityList ul = new UtilityList(item);
                    ListULofItem.Add(item, ul);
                    // thêm vào list trước, điền từng dòng vào sau
                    ListUL.Add(ul);
                }
            }
            // sắp xếp các Utility List dựa vào TWU của item
            ListUL.Sort(delegate (UtilityList ul1, UtilityList ul2)
            {
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
                // else: lấy các phần thích hợp trong dòng này
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
                float newTU = 0; // TU của giao dịch sau khi chỉnh sửa
                // Dùng list chứa các item để biểu diễn 1 giao dịch được chỉnh sửa
                List<Item> revisedTc = new List<Item>();
                // với mỗi item torng giao dịch
                for (int j = 0; j < items.Length; j++)
                {
                    // chuyển các giá trị về số
                    int item = int.Parse(items[j]);
                    float utility = float.Parse(utilityValues[j]);
                    // nếu item có TWU >= minutil
                    if (xuly.ListItem[item] >= minutil)
                    {
                        // Thêm item vào giao dịch được chỉnh sửa
                        Item it = new Item(item, utility);
                        revisedTc.Add(it);
                        reu += it.utility;
                        // cộng độ hữu ích của item trong giao dịch
                        // vào newTU
                        newTU += it.utility;
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
                    //thêm Element vừa tạo vào UL của item
                    ULofItem.AddElement(e);

                    // *** Do ULofItem, ListULofItem và ListUL là các biến tham chiếu
                    // *** nên thêm vào ULofItem thì listUL cũng được thêm tương tự

                    // ================= xây dựng EUCS
                    // khai báo danh sách chứa các cặp {y, TWU(x,y)}
                    Dictionary<int, float> eucsItem;
                    // Nếu chưa có item x trong EUCS: thêm item x và danh sách item y sau x
                    if (!EUCS.ContainsKey(item_x))
                    {
                        eucsItem = new Dictionary<int, float>();
                        EUCS.Add(item_x, eucsItem);
                    }
                    // với mỗi item y sau x trong giao dịch được chỉnh sửa
                    for (int j = i + 1; j < revisedTc.Count; j++)
                    {
                        int item_y = revisedTc[j].Name;
                        // nếu tồn tại (x, y, c) trong EUCS
                        if (EUCS[item_x].ContainsKey(item_y))
                        {
                            // cộng newTU vào TWU(x,y) hiện tại
                            EUCS[item_x][item_y] += newTU;
                        }
                        else
                        {
                            // thêm item y và newTU vào danh sách item sau x
                            (EUCS[item_x]).Add(item_y, newTU);
                        }
                    }
                    // ================= kết thúc xây dựng EUCS
                }
            }
            huiCount = 0;
            FHM(itemsetBuffer, 0, null, ListUL, minutil);
            writer.Close();
        }

        /// <summary>
        ///     Hàm biểu diễn thuật toán FHM.
        /// </summary>
        /// <param name="pUL"> Utility List của item/itemset P </param>
        /// <param name="ULs"> Utility List của các mở rộng của P </param>
        /// <param name="minutil"> minutil </param>
        private void FHM(int[] prefix, int prefixLength, UtilityList pUL, List<UtilityList> ULs, int minutil)
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

                    // với mỗi Py nằm phía sau Px trong danh sách ULs, nếu thỏa điều kiện,
                    // thực hiện kết hợp UL của Py này với UL của Px
                    // và đưa vào list trên
                    for (int j = i + 1; j < ULs.Count; j++)
                    {
                        UtilityList Y = ULs[j];
                        // =================== Sử dụng EUCS
                        // Nếu TWU(x,y) < minutil: bỏ qua bước kết hợp Utility List
                        if (EUCS.ContainsKey(X.ID))
                        {
                            Dictionary<int, float> mapTwuf = EUCS[X.ID];
                            if (mapTwuf.ContainsKey(Y.ID))
                            {
                                float twuf = mapTwuf[Y.ID];
                                if (twuf < minutil)
                                    continue;
                            }
                        }
                        UtilityList temp = xuly.Construct(pUL, X, Y);
                        if (temp != null)
                        {
                            exULs.Add(temp);
                        }

                    }
                    //
                    itemsetBuffer[prefixLength] = X.ID;
                    // dùng đệ quy gọi lại thuật toán
                    FHM(itemsetBuffer, prefixLength + 1, X, exULs, minutil);
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
            EUCS.Clear();
            xuly.Refresh();
        }
    }
}
