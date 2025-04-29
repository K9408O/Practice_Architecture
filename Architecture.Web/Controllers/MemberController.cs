using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace Architecture.Web.Controllers
{
    public class MemberController : Controller
    {
        // GET: Member
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Search()
        {
            return View();
        }


    }
}