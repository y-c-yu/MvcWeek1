using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcWeek1.Models;

namespace MvcWeek1.Controllers
{
    public class ContactController : Controller
    {
        //private 客戶資料Entities db = new 客戶資料Entities();
        客戶聯絡人Repository repo;
        客戶資料Repository repoCust;

        public ContactController()
        {
            repo = RepositoryHelper.Get客戶聯絡人Repository();
            repoCust = RepositoryHelper.Get客戶資料Repository(repo.UnitOfWork);
        }

        // GET: Contact
        public ActionResult Index(string search, string sortOrder)
        {
            ViewBag.CustomerTitle = search;
            ViewBag.職稱SortParam = string.IsNullOrWhiteSpace(sortOrder) ? "職稱_desc" : "";
            ViewBag.姓名SortParam = sortOrder == "姓名" ? "姓名_desc" : "姓名";
            ViewBag.EmailSortParam = sortOrder == "Email" ? "Email_desc" : "Email";
            ViewBag.手機SortParam = sortOrder == "手機" ? "手機_desc" : "手機";
            ViewBag.電話SortParam = sortOrder == "電話" ? "電話_desc" : "電話";
            ViewBag.客戶名稱SortParam = sortOrder == "客戶名稱" ? "客戶名稱_desc" : "客戶名稱";

            IQueryable<客戶聯絡人> c1;
            if (string.IsNullOrWhiteSpace(search))
                c1 = repo.All().Include(客 => 客.客戶資料);
            else
                c1 = repo.Get客戶聯絡人ListBy職稱(search).Include(p => p.客戶資料);

            switch (sortOrder)
            {
                case "職稱_desc":
                    c1 = c1.OrderByDescending(s => s.職稱);
                    break;
                case "姓名":
                    c1 = c1.OrderBy(s => s.姓名);
                    break;
                case "姓名_desc":
                    c1 = c1.OrderByDescending(s => s.姓名);
                    break;
                case "Email":
                    c1 = c1.OrderBy(s => s.Email);
                    break;
                case "Email_desc":
                    c1 = c1.OrderByDescending(s => s.Email);
                    break;
                case "手機":
                    c1 = c1.OrderBy(s => s.手機);
                    break;
                case "手機_desc":
                    c1 = c1.OrderByDescending(s => s.手機);
                    break;
                case "電話":
                    c1 = c1.OrderBy(s => s.電話);
                    break;
                case "電話_desc":
                    c1 = c1.OrderByDescending(s => s.電話);
                    break;
                case "客戶名稱":
                    c1 = c1.OrderBy(s => s.客戶資料.客戶名稱);
                    break;
                case "客戶名稱_desc":
                    c1 = c1.OrderByDescending(s => s.客戶資料.客戶名稱);
                    break;
                default:
                    c1 = c1.OrderBy(s => s.職稱);
                    break;
            }

            return View(c1);
        }

        // GET: Contact/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // GET: Contact/Create
        public ActionResult Create()
        {
            ViewBag.客戶Id = new SelectList(repoCust.All(), "Id", "客戶名稱");
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.Add(客戶聯絡人);
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }

            ViewBag.客戶Id = new SelectList(repoCust.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            ViewBag.客戶Id = new SelectList(repoCust.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,客戶Id,職稱,姓名,Email,手機,電話")] 客戶聯絡人 客戶聯絡人)
        {
            if (ModelState.IsValid)
            {
                repo.UnitOfWork.Context.Entry(客戶聯絡人).State = EntityState.Modified;
                repo.UnitOfWork.Commit();
                return RedirectToAction("Index");
            }
            ViewBag.客戶Id = new SelectList(repoCust.All(), "Id", "客戶名稱", 客戶聯絡人.客戶Id);
            return View(客戶聯絡人);
        }

        // GET: Contact/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            客戶聯絡人 客戶聯絡人 = repo.Find(id.Value);
            if (客戶聯絡人 == null)
            {
                return HttpNotFound();
            }
            return View(客戶聯絡人);
        }

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            客戶聯絡人 客戶聯絡人 = repo.Find(id);
            repo.Delete(客戶聯絡人);
            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
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
