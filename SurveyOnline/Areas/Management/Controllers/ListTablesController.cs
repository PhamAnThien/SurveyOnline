using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SurveyOnline.Models;

namespace SurveyOnline.Areas.Management.Controllers
{
    public class ListTablesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Management/ListTables
        public async Task<ActionResult> Index()
        {
            var listTables = db.ListTables.Where(l => l.ParentID == null);
            return View(await listTables.ToListAsync());
        }

        // GET: Management/ListTables/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListTable listTable = await db.ListTables.FindAsync(id);
            if (listTable == null)
            {
                return HttpNotFound();
            }
            return View(listTable);
        }

        // GET: Management/ListTables/Create
        public ActionResult Create(int? parentID, string returnUrl)
        {
            List<ListTable> selectListItems = new List<ListTable>();

            if (parentID == null)
            {
                 selectListItems.Add(new ListTable() { Name = "" });
            }
            else
             selectListItems = db.ListTables.Where(l => l.ID == parentID).ToList();
            ViewBag.returnUrl = returnUrl;
            ViewBag.ParentID = new SelectList(selectListItems, "ID", "Name");
            return View();
        }

        // POST: Management/ListTables/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Name,ParentID")] ListTable listTable, string returnUrl)
        {
            if (listTable.ParentID != null)
                if (listTable.ParentID == 0)
                {
                    listTable.ParentID = null;
                }
            if (ModelState.IsValid)
            {
                db.ListTables.Add(listTable);
                await db.SaveChangesAsync();
                if (string.IsNullOrEmpty(returnUrl))
                {
                    return RedirectToAction("Index");
                }
                return Redirect(returnUrl);
            }

            ViewBag.ParentID = new SelectList(db.ListTables, "ID", "Name", listTable.ParentID);
            return View(listTable);
        }

        // GET: Management/ListTables/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListTable listTable = await db.ListTables.FindAsync(id);
            if (listTable == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentID = new SelectList(db.ListTables, "ID", "Name", listTable.ParentID);
            return View(listTable);
        }

        // POST: Management/ListTables/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Name,ParentID")] ListTable listTable)
        {
            if (ModelState.IsValid)
            {
                db.Entry(listTable).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentID = new SelectList(db.ListTables, "ID", "Name", listTable.ParentID);
            return View(listTable);
        }

        // GET: Management/ListTables/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ListTable listTable = await db.ListTables.FindAsync(id);
            if (listTable == null)
            {
                return HttpNotFound();
            }
            return View(listTable);
        }

        // POST: Management/ListTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ListTable listTable = await db.ListTables.FindAsync(id);
            db.ListTables.Remove(listTable);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
