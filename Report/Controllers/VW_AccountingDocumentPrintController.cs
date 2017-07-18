using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Report.Models;

namespace Report.Controllers
{
    public class VW_AccountingDocumentPrintController : Controller
    {
        private DecaFinancialEntities db = new DecaFinancialEntities();

        // GET: VW_AccountingDocumentPrint
        public ActionResult Index()
        {
            return View(db.VW_AccountingDocumentPrint.ToList());
        }

        // GET: VW_AccountingDocumentPrint/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VW_AccountingDocumentPrint vW_AccountingDocumentPrint = db.VW_AccountingDocumentPrint.Find(id);
            if (vW_AccountingDocumentPrint == null)
            {
                return HttpNotFound();
            }
            return View(vW_AccountingDocumentPrint);
        }

        // GET: VW_AccountingDocumentPrint/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VW_AccountingDocumentPrint/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Description,Debtor,Creditor,DocumentDescription,DocumentDate,TemporaryDocumentNumber,PermanantDocumentNumber,IsPermenant,IsConfirmed,AccountingDocumentId,DetailedAccountCode,DetailedAccountName,CertainAccountCode,CertainAccountName,TotalAccountCode,TotalAccountName,DocumentType")] VW_AccountingDocumentPrint vW_AccountingDocumentPrint)
        {
            if (ModelState.IsValid)
            {
                db.VW_AccountingDocumentPrint.Add(vW_AccountingDocumentPrint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vW_AccountingDocumentPrint);
        }

        // GET: VW_AccountingDocumentPrint/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VW_AccountingDocumentPrint vW_AccountingDocumentPrint = db.VW_AccountingDocumentPrint.Find(id);
            if (vW_AccountingDocumentPrint == null)
            {
                return HttpNotFound();
            }
            return View(vW_AccountingDocumentPrint);
        }

        // POST: VW_AccountingDocumentPrint/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Description,Debtor,Creditor,DocumentDescription,DocumentDate,TemporaryDocumentNumber,PermanantDocumentNumber,IsPermenant,IsConfirmed,AccountingDocumentId,DetailedAccountCode,DetailedAccountName,CertainAccountCode,CertainAccountName,TotalAccountCode,TotalAccountName,DocumentType")] VW_AccountingDocumentPrint vW_AccountingDocumentPrint)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vW_AccountingDocumentPrint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vW_AccountingDocumentPrint);
        }

        // GET: VW_AccountingDocumentPrint/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VW_AccountingDocumentPrint vW_AccountingDocumentPrint = db.VW_AccountingDocumentPrint.Find(id);
            if (vW_AccountingDocumentPrint == null)
            {
                return HttpNotFound();
            }
            return View(vW_AccountingDocumentPrint);
        }

        // POST: VW_AccountingDocumentPrint/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VW_AccountingDocumentPrint vW_AccountingDocumentPrint = db.VW_AccountingDocumentPrint.Find(id);
            db.VW_AccountingDocumentPrint.Remove(vW_AccountingDocumentPrint);
            db.SaveChanges();
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
