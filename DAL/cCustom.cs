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

        #region 取得Customer內容
        /// <summary>
        /// 取得Customer內容
        /// </summary>
        /// <param name="dWhere">自動加入Where條件</param>
        /// <returns>搜尋結果的DataTable</returns>
        public DataTable fnGetCustom(Dictionary<string, object> dWhere)
        {
            DataTable dt = new DataTable();
            SqlConnection con = new SqlConnection(sCon);
            try
            {
                #region 抓取資料
                string sSql = @"select * from Customers ";
                if (dWhere != null && dWhere.Count > 0)
                {
                    sSql += "where 1=1 ";
                    foreach (KeyValuePair<string, object> kvp in dWhere)
                    {
                        sSql += " and " + kvp.Key + "=@" + kvp.Key;
                    }
                }
                con.Open();//開啟資料庫讀取
                SqlCommand command = new SqlCommand(sSql, con);
                if (dWhere != null && dWhere.Count > 0)
                {
                    foreach (KeyValuePair<string, object> kvp in dWhere)
                    {
                        command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                    }
                }
                
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            dt.Load(reader);
                        }
                        reader.Close();//關閉讀取資料庫資料的元件
                    }
                }
                #endregion
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

        #region Delete
        public bool fnDeleteCustom(Dictionary<string, object> dWhere)
        {
            bool bDelete = false;
            SqlConnection con = new SqlConnection(sCon);
            string sSql = @"delete from Customers ";
            try
            {

                if (dWhere != null && dWhere.Count > 0)
                {
                    sSql += "where 1=1";
                    foreach (KeyValuePair<string, object> kvp in dWhere)
                    {
                        sSql += " and " + kvp.Key + "=@" + kvp.Key;
                    }
                }
                con.Open(); //打開資料庫連線
                SqlCommand command = new SqlCommand(sSql, con);
                if (dWhere != null && dWhere.Count > 0)
                {
                    foreach (KeyValuePair<string, object> kvp in dWhere)
                    {
                        command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                    }
                }
                command.ExecuteNonQuery(); //執行sql語法
                con.Close(); //關閉資料庫連線
                bDelete = true;
            }
            catch (Exception e)
            {

            }
            finally 
            {
                con.Dispose();
                con.Close(); //關閉資料庫連線
            }

            return bDelete;
        }
        #endregion
    }
}