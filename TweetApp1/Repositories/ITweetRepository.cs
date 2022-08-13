using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp1.Models;
using TweetAPP1.Models;

namespace TweetApp1.Repositories
{
    public interface ITweetRepository
    {
        Task<int> Register(User users);

       
        
        Task<User> Login(string Username, string password);

        
        Task<List<TweetAPP1.Models.UserTweets>> GetAllTweets();

        
        Task<List<UserTweets>> GetTweetsByUser(string username);

        
        Task<IList<RegisteredUser>> GetAllUsers();

        
        Task<int> PostTweet(Tweet tweet);

         
        Task<bool> UpdatePassword(string emailId, string oldpassword, string newPassword);
 
        Task<bool> ForgotPassword(string emailId, string password);

        
        Task<User> ValidateEmailId(string emailId);

        
        Task<User> ValidateName(string loginId);
 
        Task<int> Likes(int tweetid,bool flag);

        
        Task<List<UserComments>> GetComments(string username, int tweetid);

       
        Task<int> Comments(string comment, string username, string userName, int tweetid, DateTime date);

       
        Task<int> DeleteTweet(string username, int tweetid);

        Task<int> DeleteUser(string username);

        Task<int> DeleteComment(int tweetid);


        Task<User> GetUserProfile(string username);
    }
}
