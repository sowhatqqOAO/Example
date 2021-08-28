using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Example2.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult List()
        {
            return View();
        }
    }
}