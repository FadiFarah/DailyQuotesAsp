using QuoteWebApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Web.Mongodb.Helper;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.Web.Http.Description;

namespace QuoteWebApp.Controllers
{
    public class QuotesWebAPIController : ApiController
    {
        [HttpGet]
        public List<Quote> GetQuotes()
        {
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();
            return quoteList;
        }

        [HttpPost]
        [ResponseType(typeof(Quote))]
        public void PostQuote(Quote quote)
        {
            quote.Id = Guid.NewGuid().ToString();
            quote.UserId= Guid.NewGuid().ToString();
            MongoDbManager dbManager = MongoDbManager.Instance;
            dbManager.QuoteCollection.InsertOne(quote);
        }

        [HttpPut]
        [ResponseType(typeof(Quote))]
        public void PutQuote(Quote quote)
        {

            MongoDbManager dbManager = MongoDbManager.Instance;
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();

            Quote result = new Quote();
            foreach (var q in quoteList)
            {
                if (q.Id == quote.Id)
                {
                    //BsonDocument b = new BsonDocument(q.ToBsonDocument());

                    dbManager.QuoteCollection.ReplaceOne(q.ToBsonDocument(), quote);
                }
            }
        }
        [HttpDelete]
        public void DeleteQuote(string id)
        {
            MongoDbManager dbManager = MongoDbManager.Instance;
            List<Quote> quoteList = MongoDbManager.Instance.QuoteCollection.AsQueryable().ToList();

            Quote result = new Quote();
            foreach (var quote in quoteList)
            {
                if (quote.Id == id)
                {
                    //BsonDocument b = new BsonDocument(quote.ToBsonDocument());

                    dbManager.QuoteCollection.DeleteOne(quote.ToBsonDocument());
                }
            }
        }
    }
}
