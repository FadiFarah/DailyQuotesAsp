using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using QuoteWebApp.Models;
using Microsoft.AspNet.Identity;
using MongoDB.Bson;
using MongoDB.Driver;
using Web.Mongodb.Helper;

namespace QuoteWebApp.Controllers
{
    public class QuotesController : Controller
    {


        // GET: Quotes
        [Authorize]
        public ActionResult UserQuotes()
        {
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();
            List<Quote> results = new List<Quote>();
            foreach (var quote in quoteList)
            {
                if (quote.UserId == User.Identity.GetUserId().ToString())
                {
                    results.Add(quote);
                }
            }
            return View(results);
        }
        [Authorize]
        public ActionResult Index()
        {
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();
            List<Quote> results = new List<Quote>();
            foreach (var quote in quoteList)
            {
                results.Add(quote);
            }
            return View(results);
        }
        [Authorize]
        public ActionResult ReadMore(Quote quote)
        {
            return View(quote);
        }
        // GET: Quotes/Details/5
        [Authorize]
        public ActionResult Details(string id)
        {
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();
            Quote result = new Quote();
            foreach (var quote in quoteList)
            {
                if (quote.Id == id)
                {
                    result = quote;
                    return View(result);
                }
            }
            return HttpNotFound();
        }

        // GET: Quotes/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Quotes/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Quote quote)
        {
            string userId = User.Identity.GetUserId().ToString();
            quote.UserId = userId;
            quote.Id = Guid.NewGuid().ToString();
            quote.UserName = User.Identity.GetUserName().ToString();
            MongoDbManager dbManager = MongoDbManager.Instance;
            dbManager.QuoteCollection.InsertOne(quote);
            return RedirectToAction("Index");
        }



        // GET: Quotes/Edit/5
        [Authorize]
        public ActionResult Edit(string id)
        {
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();

            MongoDbManager dbManager = MongoDbManager.Instance;

            Quote result = new Quote();
            foreach (var quote in quoteList)
            {
                if (quote.Id == id)
                {
                    result = quote;
                    return View(result);
                }
            }
            return HttpNotFound();
        }

        // POST: Quotes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Edit(Quote newQuote)
        {

            MongoDbManager dbManager = MongoDbManager.Instance;
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();

            Quote result = new Quote();
            foreach (var quote in quoteList)
            {
                if (quote.Id == newQuote.Id)
                {
                    newQuote.Id = quote.Id;
                    newQuote.UserId = quote.UserId;
                    newQuote.UserName = quote.UserName;
                    BsonDocument oldQ = new BsonDocument(quote.ToBsonDocument());
                    BsonDocument newQ = new BsonDocument(newQuote.ToBsonDocument());

                    dbManager.QuoteCollection.FindOneAndUpdate(oldQ,newQ);
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();            
        }

        // GET: Quotes/Delete/5
        [Authorize]
        public ActionResult Delete(string id)
        {
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();
            Quote result = new Quote();
            foreach (var quote in quoteList)
            {
                if (quote.Id == id)
                {
                    result = quote;
                    return View(result);
                }
            }
            return HttpNotFound();
        }

        // POST: Quotes/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            MongoDbManager dbManager = MongoDbManager.Instance;
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();

            Quote result = new Quote();
            foreach (var quote in quoteList)
            {
                if (quote.Id == id)
                {
                    BsonDocument b = new BsonDocument(quote.ToBsonDocument());
                    
                    dbManager.QuoteCollection.DeleteOne(b);
                    result = quote;
                    return RedirectToAction("Index");
                }
            }
            return HttpNotFound();
        }
    }
}
