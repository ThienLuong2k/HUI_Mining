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
        // key 1: item 1, key 2: item 2, value: twu(item 1, item 2)
        private Dictionary<int, Dictionary<int, float>> EUCS;
        // Stream writer
        private StreamWriter writer;
        // 
        private const int BUFFER_SIZE = 200;
        private int[] itemsetBuffer;
        //
        public int HuiCount;

        public XuLy()
        {
            ListItem = new Dictionary<int, float>();
            EUCS = new Dictionary<int, Dictionary<int, float>>();
            HuiCount = 0;
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
                
                countTc = db.Length;
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
        private UtilityList Construct(UtilityList P, UtilityList Px, UtilityList Py)
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
        private int CompareItems(int item1, int item2)
        {
            float compare = ListItem[item2] - ListItem[item1];
            return (compare == 0) ? item2 - item1 : (int)compare;
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

            // với mỗi item
            foreach (int item in ListItem.Keys)
            {
                // nếu item thỏa điều kiện cắt tỉa (TWU >= minutil)
                if (ListItem[item] >= minutil)
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
                return CompareItems(ul1.ID, ul2.ID);
            });
            // quét CSDL để điền từng dòng vào Utility List của từng item
            // thỏa điều kiện TWU >= minutil
            string[] db = File.ReadAllLines(file_input);
            // với mỗi dòng trong file CSDL
            for (int tid = 0; tid < db.Length; tid++)
            {
                // Nếu dòng này không phải giao dịch
                if (String.IsNullOrEmpty(db[tid]) || db[tid][0] == '#'
                    || db[tid][0] == '%' || db[tid][0] == '@')
                {
                    continue;
                }
                // else: 
                // Mỗi giao dịch gồm 3 phần
                string[] tc = db[tid].Split(':');
                // Phần 1: danh sách item xuất hiện trong giao dịch, cách nhau bởi khoảng trắng
                string[] items = tc[0].Split(' ');
                // Phần 2: giá trị TWU của giao dịch (không lấy ở đây)
                // Phần 3: độ hữu ích của các item trong giao dịch
                string[] utilityValues = tc[2].Split(' ');

                // Thực hiện chỉnh sửa giao dịch
                float reu = 0; // remaining utility
                                // Dùng list chứa các item để biểu diễn 1 giao dịch được chỉnh sửa
                List<Itemset> revisedTc = new List<Itemset>();
                // với mỗi item trong giao dịch
                for (int j = 0; j < items.Length; j++)
                {
                    // chuyển các giá trị về số
                    int item = int.Parse(items[j]);
                    float utility = float.Parse(utilityValues[j]);
                    // nếu item có TWU >= minutil
                    if (ListItem[item] >= minutil)
                    {
                        // Thêm item vào giao dịch được chỉnh sửa
                        Itemset set = new Itemset(item, utility);
                        revisedTc.Add(set);
                        reu += utility;
                    }
                }
                // sắp xếp lại các item tăng dần theo TWU
                revisedTc.Sort(delegate (Itemset set1, Itemset set2)
                {
                    return CompareItems(set1.Name, set2.Name);
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
                    Element e = new Element(tid + 1, x_utility, reu);
                    // thêm Element vừa tạo vào UL của item
                    ULofItem.AddElement(e);

                    // *** Do ULofItem, ListULofItem và ListUL là các biến tham chiếu
                    // *** nên thêm vào ULofItem thì listUL cũng được thêm tương tự
                }
            }
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
                        exULs.Add(Construct(pUL, X, Y));
                    }
                    // tạo prefix mới
                    itemsetBuffer[prefixLength] = X.ID;
                    // dùng đệ quy gọi lại thuật toán
                    HUI_Miner(itemsetBuffer, prefixLength + 1, X, exULs, minutil);
                }
            }
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
            foreach (int item in ListItem.Keys)
            {
                // nếu item thỏa điều kiện cắt tỉa (TWU >= minutil)
                if (ListItem[item] >= minutil)
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
                return CompareItems(ul1.ID, ul2.ID);
            });
            // quét CSDL để điền từng dòng vào Utility List của từng item
            // thỏa điều kiện TWU >= minutil
            string[] db = File.ReadAllLines(file_input);
            // với mỗi dòng trong file CSDL
            for (int tid = 0; tid < db.Length; tid++)
            {
                // Nếu dòng này không phải giao dịch
                if (String.IsNullOrEmpty(db[tid]) || db[tid][0] == '#'
                    || db[tid][0] == '%' || db[tid][0] == '@')
                {
                    continue;
                }
                // else: lấy các phần thích hợp trong dòng này
                // Mỗi giao dịch gồm 3 phần
                string[] tc = db[tid].Split(':');
                // Phần 1: danh sách item xuất hiện trong giao dịch, cách nhau bởi khoảng trắng
                string[] items = tc[0].Split(' ');
                // Phần 2: giá trị TWU của giao dịch (không lấy ở đây)
                // Phần 3: độ hữu ích của các item trong giao dịch
                string[] utilityValues = tc[2].Split(' ');

                // Thực hiện chỉnh sửa giao dịch
                float reu = 0; // remaining utility
                float newTU = 0; // TU của giao dịch sau khi chỉnh sửa
                // Dùng list chứa các item để biểu diễn 1 giao dịch được chỉnh sửa
                List<Itemset> revisedTc = new List<Itemset>();
                // với mỗi item torng giao dịch
                for (int j = 0; j < items.Length; j++)
                {
                    // chuyển các giá trị về số
                    int item = int.Parse(items[j]);
                    int utility = int.Parse(utilityValues[j]);
                    // nếu item có TWU >= minutil
                    if (ListItem[item] >= minutil)
                    {
                        // Thêm item vào giao dịch được chỉnh sửa
                        Itemset set = new Itemset(item, utility);
                        revisedTc.Add(set);
                        reu += set.utility;
                        // cộng độ hữu ích của item trong giao dịch
                        // vào newTU
                        newTU += set.utility;
                    }
                }
                // sắp xếp lại các item tăng dần theo TWU
                revisedTc.Sort(delegate (Itemset set1, Itemset set2)
                {
                    return CompareItems(set1.Name, set2.Name);
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
                    Element e = new Element(tid + 1, x_utility, reu);
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
                        UtilityList temp = Construct(pUL, X, Y);
                        if(temp != null) {
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
            HuiCount++;
            for (int iPrefix = 0; iPrefix < prefixLength; iPrefix++)
            {
                writer.Write(prefix[iPrefix] + " ");
            }
            writer.WriteLine(item + " #UTIL: " + utility);
        }

        /// <summary>
        ///     Loại bỏ toàn bộ item trong List item và EUCS
        /// </summary>
        public void Refresh()
        {
            ListItem.Clear();
            EUCS.Clear();
            HuiCount = 0;
        }
    }
}
