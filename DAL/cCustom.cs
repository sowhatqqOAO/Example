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
                con.Open();//開啟資料庫讀取
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
                con.Close(); //關閉資料庫 
                #endregion
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

        #region Insert
        public int fnInsertCustom(Dictionary<string, object> dParmater)
        {
            int iReturn = 0;
            if (dParmater != null && dParmater.Count > 0)
            {
                SqlConnection con = new SqlConnection(sCon);
                try
                {
                    using (SqlCommand command = con.CreateCommand())
                    {
                        con.Open();
                        List<string> lColum = new List<string>();
                        string sSql = @"Insert into Customers "; 
                        if (dParmater != null && dParmater.Count > 0)
                        {
                            foreach (KeyValuePair<string, object> kvp in dParmater)
                            {
                                lColum.Add(kvp.Key);
                                command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                            }
                        }
                        sSql = sSql + "(" + string.Join(",", lColum) + ")";
                        sSql = sSql+ " Values " + "(@" + string.Join(",@", lColum) + ")";
                        command.CommandText = sSql;
                        iReturn = Convert.ToInt32(command.ExecuteNonQuery());
                        con.Close();
                    }

                }
                catch (Exception e)
                {

                }
                finally
                {
                    con.Dispose();
                    con.Close(); //關閉資料庫連線
                }
            }
            return iReturn;
        }
        #endregion

        #region Update
        public int fnUpdateCustom(Dictionary<string, object> dParmater,Dictionary<string, object> dWhere)
        {
            int iReturn = 0;
            if (dParmater != null && dParmater.Count > 0)
            {
                SqlConnection con = new SqlConnection(sCon);
                try
                {
                    using (SqlCommand command = con.CreateCommand())
                    {
                        con.Open();
                        List<string> lColum = new List<string>();
                        List<string> lWhere = new List<string>();
                        string sSql = @"Update Customers SET ";
                        if (dParmater != null && dParmater.Count > 0)
                        {
                            foreach (KeyValuePair<string, object> kvp in dParmater)
                            {
                                lColum.Add(kvp.Key +"=@" + kvp.Key);
                                command.Parameters.AddWithValue("@" + kvp.Key, kvp.Value);
                            }
                            sSql = sSql + string.Join(",", lColum);
                        }
                        
                        if (dWhere != null && dWhere.Count > 0)
                        {
                            foreach (KeyValuePair<string, object> kvpWhere in dWhere)
                            {
                                lWhere.Add(kvpWhere.Key + "=@" + kvpWhere.Key);
                                command.Parameters.AddWithValue("@" + kvpWhere.Key, kvpWhere.Value);
                            }
                            sSql = sSql +" Where "+ string.Join(" and ", lWhere);
                        }
                        
                        command.CommandText = sSql;
                        iReturn = Convert.ToInt32(command.ExecuteNonQuery());
                        con.Close();
                    }

                }
                catch (Exception e)
                {

                }
                finally
                {
                    con.Dispose();
                    con.Close(); //關閉資料庫連線
                }
            }
            return iReturn;
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