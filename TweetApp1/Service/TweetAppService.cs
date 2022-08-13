using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TweetApp1.Models;
using TweetApp1.Repositories;
using TweetAPP1.Models;

namespace TweetApp1.Service
{
    public class TweetAppService : ITweetAppService
    {
        private readonly ITweetRepository tweetRepository;
        private ILogger<TweetAppService> logger;

       
        public TweetAppService(ITweetRepository tweetRepository, ILogger<TweetAppService> logger)
        {
            this.tweetRepository = tweetRepository;
            this.logger = logger;
        }

         
        public async Task<int> Comments(string comment, string username, string userName, int tweetid)
        {
            try
            {
                DateTime date = DateTime.Now;
                var result = await this.tweetRepository.Comments(comment, username, userName, tweetid, date);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while posting comment");
                throw;
            }
        }

        
        public async Task<string> ForgotPassword(string emailId, string password)
        {
            try
            {
                string message = string.Empty;
                if (password != null)
                {
                    password = this.EncryptPassword(password);
                }

                var result = await this.tweetRepository.ForgotPassword(emailId, password);
                if (result)
                {
                    message = "\"Changed Password\"";
                }
                else
                {
                    message = "Failed";
                }

                return message;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while reseting password");
                throw;
            }
        }

        
        public async Task<List<UserTweets>> GetAllTweets()
        {
            try
            {
                var result = await this.tweetRepository.GetAllTweets();
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving all tweets");
                throw;
            }
        }



        
        public async Task<IList<RegisteredUser>> GetAllUsers()
        {
            try
            {
                var result = await this.tweetRepository.GetAllUsers();
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving all registered users");
                throw;
            }
        }

        
        public async Task<List<UserTweets>> GetTweetsByUser(string username)
        {
            try
            {
                var result = await this.tweetRepository.GetTweetsByUser(username);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving tweets by user");
                throw;
            }
        }

         
        public async Task<User> GetUserProfile(string username)
        {
            try
            {
                var result = await this.tweetRepository.GetUserProfile(username);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving user");
                throw;
            }
        }

       
        public async Task<int> Likes(int tweetid,bool flag)
        {
            try
            {
                var result = await this.tweetRepository.Likes(tweetid,flag);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while getting likes");
                throw;
            }
        }

       
        public async Task<string> PostTweet(Tweet tweet)
        {
            try
            {
                string message = string.Empty;
                var result = await this.tweetRepository.PostTweet(tweet);
                if (result > 0)
                {
                    message = "\"Posted\"";
                }
                else
                {
                    message = "Error occured";
                }

                return message;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while posting tweets");
                throw;
            }
        }

        
        public async Task<string> UpdatePassword(string emailId, string oldpassword, string newPassword)
        {
            try
            {
                string message = string.Empty;
                if (newPassword != null && oldpassword != null)
                {
                    newPassword = this.EncryptPassword(newPassword);
                    oldpassword = this.EncryptPassword(oldpassword);
                }

                var result = await this.tweetRepository.UpdatePassword(emailId, oldpassword, newPassword);
                if (result)
                {
                    message = "\"Updated Successfully\"";
                }
                else
                {
                    message = "Update Failed";
                }

                return message;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while updating password");
                throw;
            }
        }

        
        public async Task<User> UserLogin(string emailId, string password)
        {
            try
            {
                if (password != null)
                {
                    password = this.EncryptPassword(password);
                }

                var result = await this.tweetRepository.Login(emailId, password);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while logging in");
                throw;
            }
        }

        
        public async Task<string> Register(User users)
        {
            try
            {
                if (users != null)
                {
                    string message = string.Empty;
                    var validate = await this.tweetRepository.ValidateEmailId(users.EmailId);
                    var uservalidate = await this.tweetRepository.ValidateName(users.Username);
                    if (validate == null && uservalidate == null)
                    {
                        users.Password = this.EncryptPassword(users.Password);
                        var result = await this.tweetRepository.Register(users);
                        if (result > 0)
                        {
                            message = "Successfully registerd";
                        }
                        else
                        {
                            message = "Registration failed";
                        }
                    }
                    else
                    {
                        if (validate != null)
                        {
                            message = "EmailId is already used";
                        }
                        else
                        {
                            message = "Username is already taken";
                        }
                    }

                    return message;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while registering");
                throw;
            }
        }

        public async Task<List<UserComments>> GetComments(string username, int tweetid)
        {
            try
            {
                var result = await this.tweetRepository.GetComments(username, tweetid);
                if (result != null)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while comments");
                throw;
            }
        }

        public async Task<string> DeleteTweet(string username, int tweetid)
        {
            try
            {
                string message = string.Empty;
                var result = await this.tweetRepository.DeleteTweet(username, tweetid);
                if (result > 0)
                {
                    return message = "\"Deleted\"";
                }
                else
                {
                    return message = "\"Failed to Delete\"";
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while comments");
                throw;
            }
        }

        public async Task<string> DeleteUser(string username)
        {
            try
            {
                string message = string.Empty;
                var result = await this.tweetRepository.DeleteUser(username);
                if (result > 0)
                {
                    return message = "\"Deleted\"";
                }
                else
                {
                    return message = "\"Failed to Delete\"";
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while comments");
                throw;
            }
        }

        private string EncryptPassword(string password)
        {
            string message = string.Empty;
            byte[] encode = new byte[password.Length];
            encode = Encoding.UTF8.GetBytes(password);
            message = Convert.ToBase64String(encode);
            return message;
        }

        public async Task<string> DeleteComment(int tweetid)
        {
            try
            {
                string message = string.Empty;
                var result = await this.tweetRepository.DeleteComment(tweetid);
                if (result > 0)
                {
                    return message = "\"comment Deleted\"";
                }
                else
                {
                    return message = "\"Failed to Delete comment\"";
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while comments");
                throw;
            }
        }
    }
}
