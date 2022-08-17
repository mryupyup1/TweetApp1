using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TweetApp1.Models;
using Microsoft.EntityFrameworkCore;
using TweetAPP1.Models;

namespace TweetApp1.Repositories
{
   public class TweetRepository : ITweetRepository
    {
        private readonly TweetDbContext dbcontext;

       
        public TweetRepository(TweetDbContext context)
        {
            this.dbcontext = context;
        }

         
        public async Task<int> Comments(string comment, string username, string name, int tweetid, DateTime date)
        {
            Comment comments = new Comment();
            int results = 0;
            var result = await this.dbcontext.Tweets.Where(s =>s.Username == username && s.Id == tweetid).FirstOrDefaultAsync();
            if (result != null)
            {
                comments.TweetId = result.Id;
                comments.Username = username;
                comments.FirstName = name;
                comments.Comments = comment;
                comments.Date = date;
                this.dbcontext.Add(comments);
                results = await this.dbcontext.SaveChangesAsync();
            }

            return results;
        }

        public async Task<int> DeleteComment(int tweetid)
        {
            var result =  this.dbcontext.Comments.Where(s => s.TweetId == tweetid);
            this.dbcontext.Comments.RemoveRange(result);
            await this.dbcontext.SaveChangesAsync();
            return 1;
        }

     

        public async Task<int> DeleteTweet(string username, int tweetid)
        {
            var result = await this.dbcontext.Tweets.Where(s => s.Username == username && s.Id == tweetid).FirstOrDefaultAsync();
            this.dbcontext.Remove(result);
            var response = await this.dbcontext.SaveChangesAsync();
            return response;
        }

        public async Task<int> DeleteUser(string username)
        {
            var userResult = await this.dbcontext.User.Where(s => s.Username == username).FirstOrDefaultAsync();
            this.dbcontext.Remove(userResult);

            var tweetResult = await this.dbcontext.Tweets.Where(s => s.Username == username).ToListAsync(); 
            if (tweetResult != null)
            {
                foreach (var tweet in tweetResult)
                {
                this.dbcontext.Remove(tweet);
                }
            }

            var commentResult = await this.dbcontext.Comments.Where(s => s.FirstName == username).ToListAsync();
            if(commentResult != null)
            {
                foreach (var comment in commentResult)
                {
                    this.dbcontext.Remove(comment);
                }
               
            }

          
            var response = await this.dbcontext.SaveChangesAsync();
            return response;
        }



        public async Task<bool> ForgotPassword(string emailId, string password)
        {
            var result = await this.dbcontext.User.Where(s => s.EmailId == emailId).FirstOrDefaultAsync();
            if (result != null)
            {
                result.Password = password;
                this.dbcontext.Update(result);
                var response = this.dbcontext.SaveChanges();
                if (response > 0)
                {
                    return true;
                }
            }

            return false;
        }

         
        public async Task<List<UserTweets>> GetAllTweets()
        {
            var result = await (from tweet in this.dbcontext.Tweets join user in this.dbcontext.User on tweet.Username equals user.Username select new UserTweets {tweetId =tweet.Id, UserName = user.Username, Tweets = tweet.Tweets, Imagename = user.ImageName, TweetDate = tweet.TweetDate, FirstName = user.FirstName, LastName = user.LastName, Likes = tweet.Likes }).ToListAsync();
            return result;
        }

        
        public async Task<IList<RegisteredUser>> GetAllUsers()
        {
            var result = await this.dbcontext.User.Select(p => new RegisteredUser
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Username = p.Username,
                ImageName = p.ImageName,
            }).ToListAsync();
            return result;
        }

        public async Task<List<UserComments>> GetComments(string username, int tweetid)
        {
            var result = await this.dbcontext.Tweets.Where(s => s.Username == username && s.Id == tweetid).FirstOrDefaultAsync();
            var result1 = await (from commentss in this.dbcontext.Comments join users in this.dbcontext.User on username equals users.Username where commentss.TweetId == result.Id select new UserComments { Username = commentss.Username,FirstName = commentss.FirstName, Comments = commentss.Comments, Imagename = users.ImageName, Date = commentss.Date }).ToListAsync();
            return result1;
        }

         
        public async Task<List<UserTweets>> GetTweetsByUser(string username)
        {
            var users = await this.dbcontext.User.FirstOrDefaultAsync(e => e.Username == username);
            var result = await (from tweet in this.dbcontext.Tweets join user in this.dbcontext.User on tweet.Username equals user.Username where tweet.Username == users.Username select new UserTweets {tweetId = tweet.Id, UserName = user.Username, Tweets = tweet.Tweets, Imagename = user.ImageName, TweetDate = tweet.TweetDate, FirstName = user.FirstName, LastName = user.LastName, Likes = tweet.Likes }).ToListAsync();
            return result;
        }

         
        public async Task<User> GetUserProfile(string username)
        {
            var result = await this.dbcontext.User.Where(s => s.Username == username).FirstOrDefaultAsync();
            return result;
        }

         
        public async Task<int> Likes(int tweetid,bool flag)
        {
            var result = await this.dbcontext.Tweets.Where(s => s.Id == tweetid).FirstOrDefaultAsync();
            if (flag)
            {
              result.Likes++;
            }
            else 
            {
                result.Likes--;
            }
            this.dbcontext.Tweets.Update(result);
            await this.dbcontext.SaveChangesAsync();
            return result.Likes;
        }
         
        public async Task<User> Login(string username, string password)
        {
            User user = await this.dbcontext.User.FirstOrDefaultAsync(e => e.Username == username && e.Password == password);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public async Task<int> PostTweet(Tweet tweet)
        {
            
            this.dbcontext.Tweets.Add(tweet);
            var result = await this.dbcontext.SaveChangesAsync();
            return result;
        }

        public async Task<int> Register(User users)
        {
            this.dbcontext.User.Add(users);
            var result = await this.dbcontext.SaveChangesAsync();
            return result;
        }

        
        public async Task<bool> UpdatePassword(string emailId, string oldpassword, string newPassword)
        {
            var update = await this.dbcontext.User.Where(x => x.EmailId == emailId && x.Password == oldpassword).FirstOrDefaultAsync();
            if (update != null)
            {
                update.Password = newPassword;
                this.dbcontext.User.Update(update);
                var result = await this.dbcontext.SaveChangesAsync();
                if (result > 0)
                {
                    return true;
                }
            }

            return false;
        }

        
        public async Task<User> ValidateEmailId(string emailId)
        {
            var user = await this.dbcontext.User.FirstOrDefaultAsync(e => e.EmailId == emailId);
            return user;
        }

        
        public async Task<User> ValidateName(string loginId)
        {
            var user = await this.dbcontext.User.FirstOrDefaultAsync(e => e.Username == loginId);
            return user;
        }

         

         
        

         
    }
}
