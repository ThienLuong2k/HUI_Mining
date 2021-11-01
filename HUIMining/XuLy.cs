using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace HUIMining
{
    public class XuLy
    {
        public Dictionary<int, int> ListItem; // item, TWU

        public XuLy()
        {
            ListItem = new Dictionary<int, int>();
        }

        /// <summary>
        ///     Đọc dữ liệu từ file txt, lưu lại số lượng item khác nhau
        ///     và số lượng giao dịch, tính TWU của từng item
        /// </summary>
        /// <param name="filename"> đường dẫn file txt </param>
        /// <param name="countItems"> số lượng item trong CSDL </param>
        /// <param name="countTc"> số lượng giao dịch </param>
        /// <returns> nếu đọc DL thành công: true và có số lượng item, số lượng giao dịch</returns>
        public bool NhapDuLieu(string filename, out int countItems, out int countTc)
        {
            try
            {
                string[] db = File.ReadAllLines(filename);
                for(int i = 0; i < db.Length; i++)
                {
                    // if the line is a comment, is empty
                    // or is a kind of metadata
                    if(String.IsNullOrEmpty(db[i]) || db[i][0] == '#' 
                        || db[i][0] == '%' || db[i][0] == '@')
                    {
                        continue;
                    }
                    // split the transaction
                    string[] tc = db[i].Split(':');
                    // the first part is the list of items
                    string[] items = tc[0].Split(' ');
                    // the second part is transaction utility
                    int tu = int.Parse(tc[1]);
                    // for each item, add TU to its TWU
                    for(int j = 0; j < items.Length; j++)
                    {
                        int item = int.Parse(items[j]);
                        if (ListItem.ContainsKey(item))
                        {
                            ListItem[item] += tu;
                        }
                        else
                        {
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
        ///     Hàm thực thi thuật toán HUI-Miner
        /// </summary>
        /// <param name="pUL"> Utility List của item/itemset P </param>
        /// <param name="ULs"> Utility List của các mở rộng của P </param>
        /// <param name="minutil"> minutil </param>
        /// <returns> Các itemset là HUI </returns>
        private List<Itemset> HUI_Miner(UtilityList pUL, List<UtilityList> ULs, int minutil)
        {
            List<Itemset> result = new List<Itemset>();
            // for each Utility List of itemset in ULs
            for(int i = 0; i < ULs.Count; i++)
            {
                UtilityList X = ULs[i];
                if (X.SumIutil >= minutil)
                    result.Add(new Itemset(X.ID, X.SumIutil));
                // SumIutil: utility of X in database
                if(X.SumIutil + X.SumReutil >= minutil)
                {
                    // this list will contain the Utility List of pX extensions
                    List<UtilityList> exULs = new List<UtilityList>();
                    for(int j = i + 1; j < ULs.Count; j++)
                    {
                        UtilityList Y = ULs[j];
                        exULs.Add(Construct(pUL, X, Y));
                    }
                    result.AddRange(HUI_Miner(X, exULs, minutil));
                }
            }
            return result;
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
            UtilityList pxyUL = new UtilityList();
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
            List<int> pxID = Px.ID;
            List<int> pyID = Py.ID;
            pxyUL.ID.AddRange(pxID);
            if (P == null)
            {
                // pxyUL.ID = Px.ID + Py.ID
                pxyUL.ID.AddRange(pyID);
            }
            else
            {
                // pxyUL.ID = Px.ID + (xóa tiền tố P của Py.ID)
                pyID.RemoveRange(0, P.ID.Count);
                pxyUL.ID.AddRange(pyID);
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
            while(left <= right)
            {
                int mid = (left + right) / 2;
                if(eList[mid].Tid < tid)
                {
                    left = mid + 1;
                }
                else if(eList[mid].Tid > tid)
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
        ///     Chạy thuật toán 1: HUI-Miner hoặc 2: FHM
        /// </summary>
        /// <param name="algo"> chọn thuật toán 1 hoặc 2 </param>
        /// <param name="filename"> đường dẫn file dữ liệu </param>
        /// <param name="minutil"> minutil </param>
        /// <returns> Danh sách itemset là HUI </returns>
        public List<Itemset> RunAlgo(int algo, string filename, int minutil)
        {
            // create a list to store Utility List of items with TWU >= minutil
            List<UtilityList> ListUL = new List<UtilityList>();
            // store the Utility List for each item
            Dictionary<int, UtilityList> ListULofItem = new Dictionary<int, UtilityList>();

            // for each item
            foreach(int item in ListItem.Keys)
            {
                // if the item is promising (TWU >= minutil)
                if(ListItem[item] >= minutil)
                {
                    // create an empty Utility List that we will fill later
                    UtilityList ul = new UtilityList(item);
                    ListULofItem.Add(item, ul);
                    // add the item to list of high TWU items
                    ListUL.Add(ul);
                }
            }
            // sort the list of high TWU items in ascending order
            ListUL.Sort(delegate (UtilityList ul1, UtilityList ul2)
            {
                return CompareItems(ul1.ID[0], ul1.ID[0]);
            });
            // database pass to construct the Utility List of 1-itemsets
            // having TWU >= minutil (promising items)
            try
            {
                string[] db = File.ReadAllLines(filename);
                for (int tid = 0; tid < db.Length; tid++)
                {
                    // if the line is a comment, is empty
                    // or is a kind of metadata
                    if (String.IsNullOrEmpty(db[tid]) || db[tid][0] == '#'
                        || db[tid][0] == '%' || db[tid][0] == '@')
                    {
                        continue;
                    }
                    // split the transaction
                    string[] tc = db[tid].Split(':');
                    // get the list of items
                    string[] items = tc[0].Split(' ');
                    // List<int> list_item = new List<int>();

                    // get the list of utility values corresponding to each item
                    // for that transaction
                    string[] utilityValues = tc[2].Split(' ');
                    // copy the transaction into lists but without items with TWU < minutil
                    // (revise the transaction)
                    float reu = 0; // remaining utility
                    List<Itemset> revisedTc = new List<Itemset>();
                    // for each item
                    for(int j = 0; j < items.Length; j++)
                    {
                        int item = int.Parse(items[j]);
                        int utility = int.Parse(utilityValues[j]);
                        // if the item has enough utility
                        if(ListItem[item] >= minutil)
                        {
                            // add it
                            Itemset set = new Itemset(item, utility);
                            revisedTc.Add(set);
                            reu += set.utility;
                        }
                    }
                    revisedTc.Sort(delegate (Itemset set1, Itemset set2)
                    {
                        return CompareItems(set1.Name[0], set2.Name[0]);
                    });
                    // for each item left in the transaction
                    foreach(Itemset iset in revisedTc)
                    {
                        // subtract the utility of this item from the remaining utility
                        reu = reu - iset.utility;
                        // get the utility list of this item

                        UtilityList ULofItem = ListULofItem[iset.Name[0]];

                        // add a new element to the UL of this item corresponding to this transaction
                        Element e = new Element(tid, iset.utility, reu);

                        ULofItem.AddElement(e);
                        //foreach(UtilityList ul in ListUL)
                        //{
                        //    if (ul.ID == iset.Name)
                        //        ul.Elements.Add(e);
                        //}
                    }
                }
            }
            catch
            {
                // the text file cant be open or some exception
                return null;
            }

            if(algo == 1)
            {
                return HUI_Miner(null, ListUL, minutil);
            }
            else
            {
                // EUCS = Build_EUCS();
                // return FHM(null, ListUL, minutil, EUCS);
                return null;
            }
        }

        /// <summary>
        ///     So sánh 2 item
        /// </summary>
        /// <param name="item1"> item 1 </param>
        /// <param name="item2"> item 2 </param>
        /// <returns> 1 con số cho biết item 1 lớn hay nhỏ hơn item 2 </returns>
        private int CompareItems(int item1, int item2)
        {
            int compare = ListItem[item1] - ListItem[item2];
            // if the same, use the lexical order (thứ tự từ điển) otherwise use the TWU
            return (compare == 0) ? item1 - item2 : compare;
        }

        public void Refresh()
        {
            ListItem.Clear();
        }
    }
}
