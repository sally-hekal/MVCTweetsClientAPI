using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MVCTweetsClientAPI.Models
{
    public class MongoDBEntities : DbContext
    {

        public MongoDBEntities() : base("name=MongoConnection") { }
        static MongoServer server = MongoServer.Create(ConfigurationManager.ConnectionStrings["MongoConnection"].ConnectionString.ToString());
        MongoDatabase database = server.GetDatabase("MVCMongo");
        List<TwitterModel> tweets;
        public List<TwitterModel> Tweets
        {
            get
            {
                var collection = database.GetCollection<TwitterModel>("Tweet");
                return collection.FindAllAs<TwitterModel>().ToList();
            }
            set { tweets = value; }
        }
        public void SaveTweet(TwitterModel colleciton)
        {
            try
            {
                MongoCollection<TwitterModel> MCollection = database.GetCollection<TwitterModel>("Tweets");
                BsonDocument doc = new BsonDocument { 
                    {"TweetID",colleciton.TweetID},
                    {"AuthorName",colleciton.AuthorName},
                     {"AuthorUrl",colleciton.AuthorUrl},
                    {"Content",colleciton.Content},
                     {"ProfileImage",colleciton.ProfileImage},
                    {"Published",colleciton.Published},
                      {"FollowersCount",colleciton.FollowersCount},
                    {"RetweetCount",colleciton.RetweetCount},
                      {"UserMentionsCount",colleciton.UserMentionsCount}

                };


                MCollection.Insert(doc);

            }
            catch (Exception ex) { }
        }

        public List<TwitterModel> GetTopFollowers(int number)
        {

            var collection = database.GetCollection<TwitterModel>("Tweets");
            return collection.FindAllAs<TwitterModel>().SetSortOrder(SortBy.Descending("FollowersCount")).SetLimit(number).ToList();
        }
        public List<TwitterModel> GetTopRetweet(int number)
        {

            var collection = database.GetCollection<TwitterModel>("Tweets");
            return collection.FindAllAs<TwitterModel>().SetSortOrder(SortBy.Descending("RetweetCount")).SetLimit(number).ToList();
        }
        public List<TwitterModel> GetTopMentions(int number)
        {

            var collection = database.GetCollection<TwitterModel>("Tweets");
            return collection.FindAllAs<TwitterModel>().SetSortOrder(SortBy.Descending("UserMentionsCount")).SetLimit(number).ToList();
        }

    }
}