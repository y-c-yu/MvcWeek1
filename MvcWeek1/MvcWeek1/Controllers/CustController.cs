using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ClosedXML.Excel;
using MvcWeek1.Models;
using Newtonsoft.Json;

namespace MvcWeek1.Controllers
{
    public class CustController : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶資料Repository repo;

        public CustController()
        {
            repo = RepositoryHelper.Get客戶資料Repository();

            ViewBag.CategoryList = from item in repo.Get客戶分類List()
                                   select new SelectListItem { Text = item, Value = item };
        }
        // GET: Cust
        public ActionResult Index(string selector, string sortOrder)
        {
            ViewBag.CustomerCategory = selector;
            ViewBag.客戶名稱SortParam = string.IsNullOrWhiteSpace(sortOrder) ? "客戶名稱_desc" : "";
            ViewBag.統一編號SortParam = sortOrder == "統一編號" ? "統一編號_desc" : "統一編號";

            IQueryable <客戶資料> c1;
            if (String.IsNullOrWhiteSpace(selector) || selector=="All")
                c1 = repo.All();
            else   
                c1 = repo.Get客戶資料ListBy客戶分類(selector);

            switch (sortOrder)
            {
                case "客戶名稱_desc":
                    c1 = c1.OrderByDescending(s => s.客戶名稱);
                    break;
                case "統一編號":
                    c1 = c1.OrderBy(s => s.統一編號);
                    break;
                case "統一編號_desc":
                    c1 = c1.OrderByDescending(s => s.統一編號);
                    break;
                default:
                    c1 = c1.OrderBy(s => s.客戶名稱);
                    break;
            }

            return View(c1);
        }

        // GET: Cust/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // GET: Cust/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cust/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶分類,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶資料);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            return View(客戶資料);
        }

        // GET: Cust/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Cust/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶分類,客戶名稱,統一編號,電話,傳真,地址,Email")] 客戶資料 客戶資料)
        {
            if (ModelState.IsValid)
            {
                repo.UnitOfWork.Context.Entry(客戶資料).State = EntityState.Modified;
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            return View(客戶資料);
        }

        // GET: Cust/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶資料 客戶資料 = repo.Find(id.Value);
            if (客戶資料 == null)
            {
                return HttpNotFound();
            }
            return View(客戶資料);
        }

        // POST: Cust/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶資料 客戶資料 = repo.Find(id);
            repo.Delete(客戶資料);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public FileResult Export()
        {
            var data = from p in repo.All()
                       select new ViewModels.Customer()
                       {
                           Id = p.Id,
                           客戶名稱 = p.客戶名稱,
                           統一編號 = p.統一編號,
                           電話 = p.電話,
                           傳真 = p.傳真,
                           地址 = p.地址,
                           Email = p.Email,
                           IsDeleted = p.IsDeleted,
                           客戶分類 = p.客戶分類
                       };

            DataTable dt = Common.ConvertTo<ViewModels.Customer>(data.ToList());

            using (var wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "客戶資料.xlsx");
                }
            }
        }

        [HttpPost]
        public JsonResult Export2()
        {
            var data = from p in repo.All()
                       select new ViewModels.Customer()
                       {
                           Id = p.Id,
                           客戶名稱 = p.客戶名稱,
                           統一編號 = p.統一編號,
                           電話 = p.電話,
                           傳真 = p.傳真,
                           地址 = p.地址,
                           Email = p.Email,
                           IsDeleted = p.IsDeleted,
                           客戶分類 = p.客戶分類
                       };

            return Json(data);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repo.UnitOfWork.Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
