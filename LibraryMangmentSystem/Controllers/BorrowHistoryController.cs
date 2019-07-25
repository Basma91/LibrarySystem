using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibraryMangmentSystem.Models;

namespace LibraryMangmentSystem.Controllers
{
    public class BorrowHistoryController : Controller
    {
        private LibraryEntities db = new LibraryEntities();

        // GET: BorrowHistory/Create
        public ActionResult Create(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var book = db.Book.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }

            var borrowHistory = new BorrowHistory { BookId = book.BookId, BorrowDate = DateTime.Now, Book = book };
            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "CustomerName");
            return View(borrowHistory);
        }

        // POST: BorrowHistory/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BorrowHistoryId,BookId,CustomerId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                db.BorrowHistory.Add(borrowHistory);
                db.SaveChanges();
                return RedirectToAction("Index", "Book");
            }

            ViewBag.CustomerId = new SelectList(db.Customer, "CustomerId", "Name", borrowHistory.CustomerId);
            borrowHistory.Book = db.Book.Find(borrowHistory.BookId);
            return View(borrowHistory);
        }

        // GET: BorrowHistory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BorrowHistory borrowHistory = db.BorrowHistory
                .Include(b => b.Book)
                .Include(c => c.Customer)
                .Where(b => b.BookId == id && b.ReturnDate == null)
                .FirstOrDefault();
            if (borrowHistory == null)
            {
                return HttpNotFound();
            }
            return View(borrowHistory);
        }

        // POST: BorrowHistory/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BorrowHistoryId,BookId,CustomerId,BorrowDate,ReturnDate")] BorrowHistory borrowHistory)
        {
            if (ModelState.IsValid)
            {
                var borrowHistoryItem = db.BorrowHistory.Include(i => i.Book)
                    .FirstOrDefault(i => i.BorrowHistoryId == borrowHistory.BorrowHistoryId);
                if (borrowHistoryItem == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                borrowHistoryItem.ReturnDate = DateTime.Now;
                db.SaveChanges();
                return RedirectToAction("Index", "Book");
            }
            return View(borrowHistory);
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