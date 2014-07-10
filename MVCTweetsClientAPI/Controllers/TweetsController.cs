using MVCTweetsClientAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MVCTweetsClientAPI.Controllers
{
    public class TweetsController : ApiController
    {
   

        public List<TwitterModel> GetTweets()
        {

            List<TwitterModel> lstTweets = new List<TwitterModel>();

            // New Code added for Twitter API 1.1
          
                var twitter = new Twitter(ConfigurationManager.AppSettings["OauthConsumerKey"],
                                                                ConfigurationManager.AppSettings["OauthConsumerKeySecret"],
                                                                ConfigurationManager.AppSettings["OauthAccessToken"],
                                                                ConfigurationManager.AppSettings["OauthAccessTokenSecret"]);

                var response = twitter.GetTweets(100);
                dynamic timeline = System.Web.Helpers.Json.Decode(response);

                foreach (var tweet in timeline)
                {

                    TwitterModel tModel = new TwitterModel();
                    tModel.TweetID = tweet.id.ToString();
                    tModel.AuthorName = tweet.user.name;
                    tModel.AuthorUrl = tweet.user.url;
                    tModel.Content = tweet.Text;
                    string publishedDate = tweet.created_at;
                    publishedDate = publishedDate.Substring(0, 19);
                    tModel.Published = DateTime.ParseExact(publishedDate, "ddd MMM dd HH:mm:ss", null);
                    tModel.ProfileImage = tweet.user.profile_image_url;

                    //Statistics Fields
                    tModel.RetweetCount = tweet.retweet_count;
                    tModel.FollowersCount = tweet.User.followers_count;
                    tModel.UserMentionsCount = tweet.entities.user_mentions.Length;


                    lstTweets.Add(tModel);
                    new MongoDBEntities().SaveTweet(tModel);

                }
            
            return lstTweets;
        }

        public List<TwitterModel> GetTopRetweet()
        {
            return new MongoDBEntities().GetTopRetweet(5);
        }
        public List<TwitterModel> GetTopFollowers()
        {
            return new MongoDBEntities().GetTopFollowers(5);
        }
        public List<TwitterModel> GetTopMentions()
        {
            return new MongoDBEntities().GetTopMentions(5);
        }
    }
}
