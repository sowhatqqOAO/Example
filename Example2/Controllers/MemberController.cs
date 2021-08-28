using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using DAL;
using Example2.Model;
using PagedList;

namespace Example2.Controllers
{
    public class MemberController : Controller
    {
        private int ipageSize = 10;//預設每頁的最大顯示筆數
        cCustom cCustom = new cCustom();
        public ActionResult List(int? page)
        {
            DataTable dt = cCustom.fnGetCustom(null);
            var pageNumeber = page ?? 1;
            List<Customers> lCustomers = new List<Customers>();
			if (dt != null && dt.Rows.Count > 0)
			{
				foreach (DataRow dr in dt.Rows)
				{
                    Customers oCustomers = new Customers();
                    oCustomers.CustomerID = dr["CustomerID"].ToString();
                    oCustomers.CompanyName = dr["CompanyName"].ToString();
                    oCustomers.ContactName = dr["ContactName"].ToString();
                    oCustomers.ContactTitle = dr["ContactTitle"].ToString();
                    oCustomers.Address = dr["Address"].ToString();
                    oCustomers.City = dr["City"].ToString();
                    oCustomers.Region = dr["Region"].ToString();
                    oCustomers.PostalCode = dr["PostalCode"].ToString();
                    oCustomers.Country = dr["Country"].ToString();
                    oCustomers.Phone = dr["Phone"].ToString();
                    oCustomers.Fax = dr["Fax"].ToString();
                    lCustomers.Add(oCustomers);
                }
			}
            return View(lCustomers.ToPagedList(pageNumeber, ipageSize));
        }
	}
}