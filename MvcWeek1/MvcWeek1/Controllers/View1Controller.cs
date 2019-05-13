using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWeek1.Models;

namespace MvcWeek1.Controllers
{
    public class View1Controller : Controller
    {
        vw_客戶對應資訊數量統計Repository repo;

        public View1Controller()
        {
            repo = RepositoryHelper.Getvw_客戶對應資訊數量統計Repository();
        }

        // GET: View1
        public ActionResult Index()
        {
            return View(repo.All());
        }
    }
}