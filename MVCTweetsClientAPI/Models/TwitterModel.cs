using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace MVCTweetsClientAPI.Models
{
    public class TwitterModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId _Id { get; set; }

        public string TweetID { get; set; }
        public string Content { get; set; }
        public string AuthorName { get; set; }
        public string AuthorUrl { get; set; }
        public string ProfileImage { get; set; }
        public DateTime? Published { get; set; }
        public int RetweetCount { get; set; }
        public int FollowersCount { get; set; }
        public int UserMentionsCount { get; set; }
    }
}