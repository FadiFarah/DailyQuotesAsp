using QuoteWebApp.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Mongodb.Helper
{
    public class MongoDbManager
    {
       
        private static object sync = new object();
        private static MongoDbManager instance;
        private MongoClient Server; //MongoServer
        public IMongoCollection<Quote> QuoteCollection; //Collection in db that we already created in DB
        private MongoDbManager()
        {
            var connectionString = "mongodb://127.0.0.1:27017"; //connection string from our local PC.
            Server = new MongoClient(connectionString);
            var database = Server.GetDatabase("db"); //Get the database by name
            //get collection by name 'quotes' and map it to QuoteCollection
            QuoteCollection = database.GetCollection<Quote>("quotes");
        }
        public static MongoDbManager Instance
        {
            get
            {
                lock (sync)
                {
                    if (instance == null)
                    {
                        instance = new MongoDbManager();
                    }
                }
                    return instance;
            }
            private set { }
        }

    }
    
}