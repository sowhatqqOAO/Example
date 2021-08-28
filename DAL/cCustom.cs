using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace DAL
{
    public class cCustom
    {
        private string sCon = ConfigurationManager.ConnectionStrings["DefaultConnectionString"].ToString();//連線字串
        #region 取得List內容
        /// <summary>
        /// 取得List內容
        /// </summary>
        /// <param name="dWhere">自動加入Where條件</param>
        /// <returns>搜尋結果的DataTable</returns>
        public DataTable fnGetCustom(Dictionary<string, object> dWhere)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                string sSql = @"select * from Customers ";
                if (dWhere != null && dWhere.Count > 0)
                {
                    sSql += "where 1=1";
                }
                con.Open();//開啟資料庫讀取
                SqlCommand command = new SqlCommand(sSql, con);
                if (dWhere != null && dWhere.Count > 0)
                {
                    foreach (KeyValuePair<string, object> kvp in dWhere) 
                    {
                        command.Parameters.AddWithValue("@"+ kvp.Key, kvp.Value);
                    }
                }
              
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        dt.Load(reader);
                    }
                    reader.Close();//關閉讀取資料庫資料的元件
                }
                con.Close(); //關閉資料庫
            }
            catch (Exception e)
            {

            }
            finally 
            {
                con.Dispose();
                con.Close();
            }

            return dt;

        }
        #endregion
    }
}
