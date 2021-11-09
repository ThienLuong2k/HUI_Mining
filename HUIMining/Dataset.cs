using System.Collections;

namespace HUIMining
{
    public class Dataset
    {
        private ArrayList data;

        public ArrayList Data { get => data; set => data = value; }

        public Dataset()
        {
            Data = new ArrayList();
        }

        /// <summary>
        ///     Thêm một giao dịch T. <br/><br/>
        ///     Trước khi gọi phương thức này, phải đảm bảo ID của T không trùng với ID
        ///     của các giao dịch đã thêm trước đó. Giao dịch sau khi thêm vào chỉ có thể
        ///     xóa, không được sửa.
        /// </summary>
        /// <param name="T"> giao dịch T</param>
        public void AddTransaction(Transaction T)
        {
            // kiểm tra trùng ID
            //for(int i = 0; i < Data.Count; i++)
            //{
            //    // nếu phần tử thứ i là Transaction
            //    if(Data[i].GetType() == typeof(Transaction))
            //    {
            //        if (((Transaction)Data[i]).ID == T.ID)
            //            return;
            //    }
            //}
            Data.Add(T);
        }

        /// <summary>
        ///     Xóa 1 giao dịch khi biết trước ID.
        /// </summary>
        /// <param name="id"> ID </param>
        public void DeleteTransaction(int id)
        {
            // kiểm tra trùng ID
            for (int i = 0; i < Data.Count; i++)
            {
                // nếu phần tử thứ i là Transaction
                //if (Data[i].GetType() == typeof(Transaction))
                //{
                    if (((Transaction)Data[i]).ID == id)
                    {
                        Data.RemoveAt(i);
                        return;
                    }
                //}
            }
        }
    }
}
