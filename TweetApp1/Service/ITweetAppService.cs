using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp1.Models;
using TweetAPP1.Models;

namespace TweetApp1.Service
{
    public interface ITweetAppService
    {
        Task<string> Register(User users);

        
        Task<User> UserLogin(string Username, string password);

        
        Task<List<UserTweets>> GetAllTweets();
 
        Task<List<UserTweets>> GetTweetsByUser(string username);

        
        Task<IList<RegisteredUser>> GetAllUsers();

         
        Task<string> PostTweet(Tweet tweet);

         
        Task<string> UpdatePassword(string emailId, string oldpassword, string newPassword);

        
        Task<string> ForgotPassword(string emailId, string password);

       
        Task<int> Likes(int tweetid,bool flag);
 
        Task<List<UserComments>> GetComments(string username, int tweetid);

         
        Task<int> Comments(string comment, string username, string userName, int tweetid);

       
        Task<string> DeleteTweet(string username, int tweetid);
        Task<string> DeleteUser(string username);

        Task<string> DeleteComment(int tweetid);


        Task<User> GetUserProfile(string username);

    }
}

