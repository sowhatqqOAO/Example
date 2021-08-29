using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DAL;
using Example2.Helpers;
using Example2.Model;
using Example2.ViewModel;
using PagedList;

namespace Example2.Controllers
{
    public class MemberController : Controller
    {
        #region 初始化
        private int ipageSize = 10;//預設每頁的最大顯示筆數
        cCustom cCustom = new cCustom();
        #endregion

        #region List
        public ActionResult List(int? page)
        {
            DataTable dt = cCustom.fnGetCustom(null);
            var pageNumeber = page ?? 1;
            List<VMCustomer> lVMCustomer = new List<VMCustomer>();
            if (dt != null && dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    VMCustomer oVMCustomer = new VMCustomer();
                    oVMCustomer.CustomerID = dr["CustomerID"].ToString();
                    oVMCustomer.CompanyName = dr["CompanyName"].ToString();
                    oVMCustomer.ContactName = dr["ContactName"].ToString();
                    oVMCustomer.ContactTitle = dr["ContactTitle"].ToString();
                    oVMCustomer.Address = dr["Address"].ToString();
                    oVMCustomer.City = dr["City"].ToString();
                    oVMCustomer.Region = dr["Region"].ToString();
                    oVMCustomer.PostalCode = dr["PostalCode"].ToString();
                    oVMCustomer.Country = dr["Country"].ToString();
                    oVMCustomer.Phone = dr["Phone"].ToString();
                    oVMCustomer.Fax = dr["Fax"].ToString();
                    lVMCustomer.Add(oVMCustomer);
                }
            }
            return View(lVMCustomer.ToPagedList(pageNumeber, ipageSize));
        }
        #endregion

        #region Details
        [HttpPost]
        [cMultiButton(m_sName = "Details", m_sArgument1 = "sModel", m_sArgument2 = "sKey")]
        public ActionResult Details(string sModel, string sKey)
        {
            #region 移除不需要參數
            ControllerContext.RouteData.Values.Remove("sModel");
            ControllerContext.RouteData.Values.Remove("sKey");
            #endregion
            ViewBag.Model = sModel;
            ViewBag.Key = sKey;
            DataTable dt = new DataTable();
            #region Return View 參數(先定義避免是NullV，View會出錯)
            VMCustomer oVMCustomer = new VMCustomer();
            #endregion
            if (string.IsNullOrEmpty(sModel) || (sModel == "Edit" && string.IsNullOrEmpty(sKey)))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);//遺失重要參數->網頁錯誤
            }
            else
            {
                switch (sModel)
                {
                    case "New":
                        break;
                    case "Edit":
                        Dictionary<string, object> dWhere = new Dictionary<string, object>() { };
                        dWhere.Add("CustomerID", sKey);
                        dt = cCustom.fnGetCustom(dWhere);
                        if (dt != null && dt.Rows.Count > 0)
                        {
                            oVMCustomer.CustomerID = dt.Rows[0]["CustomerID"].ToString();
                            oVMCustomer.CompanyName = dt.Rows[0]["CompanyName"].ToString();
                            oVMCustomer.ContactName = dt.Rows[0]["ContactName"].ToString();
                            oVMCustomer.ContactTitle = dt.Rows[0]["ContactTitle"].ToString();
                            oVMCustomer.Address = dt.Rows[0]["Address"].ToString();
                            oVMCustomer.City = dt.Rows[0]["City"].ToString();
                            oVMCustomer.Region = dt.Rows[0]["Region"].ToString();
                            oVMCustomer.PostalCode = dt.Rows[0]["PostalCode"].ToString();
                            oVMCustomer.Country = dt.Rows[0]["Country"].ToString();
                            oVMCustomer.Phone = dt.Rows[0]["Phone"].ToString();
                            oVMCustomer.Fax = dt.Rows[0]["Fax"].ToString();
                        }
                        break;
                }

            }
            return View(oVMCustomer);
        }
        #region Details存檔

        /// <summary>
        /// Details儲存事件
        /// </summary>
        /// <param name="oVMCustomer">資料庫Model</param>
        /// <param name="sModel">編輯模式通常：New | Edit</param>
        /// <param name="sPk">Key</param>
        /// <returns>View</returns>
        [HttpPost]
        [cMultiButton(m_sName = "Save", m_sArgument1 = "sModel", m_sArgument2 = "sPk")]
        [ValidateAntiForgeryToken]
        public ActionResult Details(VMCustomer oVMCustomer, string sModel, string sPk)
        {
            bool bAction = false;
            ViewBag.Model = sModel;
            ViewBag.Key = sPk;
            #region 移除不需要參數
            ControllerContext.RouteData.Values.Remove("sModel");
            ControllerContext.RouteData.Values.Remove("sPk");
            #endregion

            #region Return View 參數(先定義避免是NullV，View會出錯)
            if (oVMCustomer == null)
            {
                oVMCustomer = new VMCustomer();
            }
            #endregion

            if (ModelState.IsValid)
            {
                int iDBControl = 0;
                Dictionary<string, object> dParame = new Dictionary<string, object>();
                dParame.Add("CompanyName", oVMCustomer.CompanyName);
                dParame.Add("ContactName", oVMCustomer.ContactName);
                dParame.Add("ContactTitle", oVMCustomer.ContactTitle);
                dParame.Add("Address", oVMCustomer.Address);
                dParame.Add("City", oVMCustomer.City);
                dParame.Add("Region", oVMCustomer.Region);
                dParame.Add("PostalCode", oVMCustomer.PostalCode);
                dParame.Add("Country", oVMCustomer.Country);
                dParame.Add("Phone", oVMCustomer.Phone);
                dParame.Add("Fax", oVMCustomer.Fax);

                switch (sModel)
                {
                    case "New":
                        dParame.Add("CustomerID", oVMCustomer.CustomerID);
                        iDBControl = cCustom.fnInsertCustom(dParame);
                        break;
                    case "Edit":
                        Dictionary<string, object> dWhere = new Dictionary<string, object>();
                        dWhere.Add("CustomerID", oVMCustomer.CustomerID);
                        iDBControl = cCustom.fnUpdateCustom(dParame, dWhere);
                        break;
                }

                if (iDBControl <= 0)
                {
                    this.TempData["AlertError"] = "資料庫更新失敗";
                }
                else
                {
                    bAction = true;

                    switch (sModel)
                    {
                        case "New":
                            this.TempData["AlertInfo"] = "新增完成";
                            break;
                        case "Edit":
                            this.TempData["AlertInfo"] = "更新完成";
                            break;
                    }
                }
            }
            else
            {
                #region 將所有錯誤訊息列出
                foreach (string sValue in ModelState.Keys)
                {
                    if (ModelState[sValue].Errors.Count > 0)
                    {
                        this.TempData["AlertError"] += ModelState[sValue].Errors[0].ErrorMessage + "<br/>";
                    }
                }
                #endregion
            }

            if (bAction)
            {
                //成功導頁
                return this.RedirectToAction("List", ControllerContext.RouteData.Values);
            }
            else
            {
                //失敗停留
                return this.View(oVMCustomer);
            }
        }
        #endregion

        #endregion

        #region Delete
        /// <summary>
        /// 刪除資料
        /// </summary>
        /// <param name="sPk">Key</param>
        /// <returns>View</returns>
        [HttpPost]
        [cMultiButton(m_sName = "Delete", m_sArgument1 = "sPk")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(string sPk)
        {
            #region 移除不需要參數
            ControllerContext.RouteData.Values.Remove("sPk");
            #endregion

            if (string.IsNullOrEmpty(sPk))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            { 
                Dictionary<string, object> dWhere = new Dictionary<string, object>();
                dWhere.Add("CustomerID", sPk);
                cCustom.fnDeleteCustom(dWhere);
            }
            return RedirectToAction("List", "Member");
        }
        #endregion
    }
}