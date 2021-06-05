using QuoteWebApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Mongodb.Helper;

namespace QuoteWebApp.Controllers
{
    public class QuotesWebAPIController : ApiController
    {
        [HttpGet]
        public IEnumerable<Quote> GetQuotes()
        {
            var quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToArray();
            return quoteList;
        }
    }
}
