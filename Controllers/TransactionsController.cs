using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Easy_Games.Models;

namespace Easy_Games.Controllers
{
    public class TransactionsController : Controller
    {
        private MyContextClass db = new MyContextClass();

        // GET: Transactions


        public ActionResult Index()
        {
            var transaction = db.Transaction.Include(t => t.Client).Include(t => t.TransactionType);
            return View(transaction.ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            ViewBag.ClientID = new SelectList(db.Client, "ClientID", "Name");
            ViewBag.TransactionTypeID = new SelectList(db.TransactionType, "TransactionTypeID", "TransactionTypeName");
            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionID,Amount,TransactionTypeID,ClientID,Comment")] Transaction transaction)
        {
            Client cl = new Client();

            if (ModelState.IsValid)
            {
                
                var bal = (from i in db.Client
                           where i.ClientID == transaction.ClientID
                           select i).Single();


                try
                {
                    if (transaction.Amount < 0 && (bal.ClientBalance+transaction.Amount>0))
                    {
                        bal.ClientBalance = bal.ClientBalance + transaction.Amount;


                    }

                    else if (transaction.Amount > 0)
                    {
                        bal.ClientBalance = bal.ClientBalance + transaction.Amount;
                    }
                   

                    else
                    {
                        ViewBag.Message = "Insufficient Funds";
                        return RedirectToAction("Create");
                       
                    }
                }
                catch (Exception ex)
                {

                    ModelState.Clear();
                    ViewBag.Message = $"Insufficient Funds{ex.Message}";

                    
                }
               

                db.Entry(bal).State = EntityState.Modified;     //update client table
                db.Transaction.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ClientID = new SelectList(db.Client, "ClientID", "Name", transaction.ClientID);
            ViewBag.TransactionTypeID = new SelectList(db.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            return View(transaction);
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }

            ViewBag.ClientID = new SelectList(db.Client, "ClientID", "Name", transaction.ClientID);
            ViewBag.TransactionTypeID = new SelectList(db.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionID,Amount,TransactionTypeID,ClientID,Comment")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
               
                db.Entry(transaction).State = EntityState.Modified;
                //transaction.Amount = Convert.ToDecimal(transaction.Amount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ClientID = new SelectList(db.Client, "ClientID", "Name", transaction.ClientID);
            ViewBag.TransactionTypeID = new SelectList(db.TransactionType, "TransactionTypeID", "TransactionTypeName", transaction.TransactionTypeID);
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(long? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(long id)
        {
            Transaction transaction = db.Transaction.Find(id);
            db.Transaction.Remove(transaction);
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
