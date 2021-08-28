using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Example2.Helpers
{
    /// <summary>
    /// MVC Controller參數傳遞
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class cMultiButtonAttribute : ActionNameSelectorAttribute
    {
        /// <summary>
        /// 執行的ActionName
        /// </summary>
        public string m_sName { get; set; }

        /// <summary>
        /// 參數1的ID
        /// </summary>
        public string m_sArgument1 { get; set; }

        /// <summary>
        /// 參數2的ID
        /// </summary>
        public string m_sArgument2 { get; set; }

        /// <summary>
        /// 參數3的ID
        /// </summary>
        public string m_sArgument3 { get; set; }

        /// <summary>
        /// 參數4的ID
        /// </summary>
        public string m_sArgument4 { get; set; }

        /// <summary>
        /// 參數5的ID
        /// </summary>
        public string m_sArgument5 { get; set; }

        /// <summary>
        /// 取得Post有效參數
        /// </summary>
        /// <param name="oControllerContext">ControllerContext物件</param>
        /// <param name="sActionName">Post參數名稱</param>
        /// <param name="oMethodInfo">MethodInfo物件</param>
        /// <returns>bool</returns>
        public override bool IsValidName(ControllerContext oControllerContext, string sActionName, MethodInfo oMethodInfo)
        {
            string[] aKeys = oControllerContext.HttpContext.Request.Params.AllKeys;
            string sKey = aKeys.FirstOrDefault(this.KeyStartsWithButtonName);

            bool bKeyIsValid = IsValid(sKey);

            if (bKeyIsValid)
            {
                string[] aParts = sKey.Split(":".ToCharArray());
                string sP1 = aParts.Length < 2 ? null : aParts[1];
                string sP2 = aParts.Length < 3 ? null : aParts[2];
                string sP3 = aParts.Length < 4 ? null : aParts[3];
                string sP4 = aParts.Length < 5 ? null : aParts[4];
                string sP5 = aParts.Length < 6 ? null : aParts[5];

                this._fnUpdateValueProviderIn(oControllerContext, sP1, sP2, sP3, sP4, sP5);
            }

            return bKeyIsValid;
        }

        /// <summary>
        /// 驗證Key是否有效
        /// </summary>
        /// <param name="sKey">Post參數key</param>
        /// <returns>bool</returns>
        private static bool IsValid(string sKey)
        {
            return sKey != null;
        }

        /// <summary>
        /// 將Post有效參數存放在RouteData Values
        /// </summary>
        /// <param name="oControllerContext">ControllerContext物件</param>
        /// <param name="sValue1">Post參數1</param>
        /// <param name="sValue2">Post參數2</param>
        /// <param name="sValue3">Post參數3</param>
        /// <param name="sValue4">Post參數4</param>
        /// <param name="sValue5">Post參數5</param>
        private void _fnUpdateValueProviderIn(ControllerContext oControllerContext, string sValue1, string sValue2, string sValue3, string sValue4, string sValue5)
        {
            if (string.IsNullOrEmpty(this.m_sArgument1))
            {
                return;
            }
            else
            {
                oControllerContext.RouteData.Values[this.m_sArgument1] = sValue1;
            }

            if (string.IsNullOrEmpty(this.m_sArgument2))
            {
                return;
            }
            else
            {
                oControllerContext.RouteData.Values[this.m_sArgument2] = sValue2;
            }

            if (string.IsNullOrEmpty(this.m_sArgument3))
            {
                return;
            }
            else
            {
                oControllerContext.RouteData.Values[this.m_sArgument3] = sValue3;
            }

            if (string.IsNullOrEmpty(this.m_sArgument4))
            {
                return;
            }
            else
            {
                oControllerContext.RouteData.Values[this.m_sArgument4] = sValue4;
            }

            if (string.IsNullOrEmpty(this.m_sArgument5))
            {
                return;
            }
            else
            {
                oControllerContext.RouteData.Values[this.m_sArgument5] = sValue5;
            }
        }

        /// <summary>
        /// 判斷這個字串執行個體的開頭是否符合指定的字串。
        /// </summary>
        /// <param name="sKey">Post參數key</param>
        /// <returns>bool</returns>
        private bool KeyStartsWithButtonName(string sKey)
        {
            return sKey.StartsWith(this.m_sName, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}