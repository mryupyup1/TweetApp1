using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using TweetApp1.Service;
using TweetApp1.Models;
using Microsoft.AspNetCore.Authorization;
using TweetApp1.Sender;

namespace TweetApp1.Controllers
{
    [Route("api/v1.0/tweets/")]
    [ApiController]
    public class TweetAppController : Controller
    {
        private readonly ITweetAppService tweetAppService;
        private readonly IConfiguration configuration;
        private ILogger<TweetAppController> logger;
        private readonly IMessageSender _messageSender;

        
        public TweetAppController(ITweetAppService tweetAppService, ILogger<TweetAppController> logger, IConfiguration configuration, IMessageSender messageSender)
        {
            this.tweetAppService = tweetAppService;
            this.configuration = configuration;
            this.logger = logger;
            this._messageSender = messageSender;
        }


        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.Register(user);
                message = "User registered successfully!";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while registering user");
                throw;
            }
        }


        [HttpGet]
        [Route("login/{username},{password}")]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                Token token = null;
                var result = await this.tweetAppService.UserLogin(username, password);
                if (result != null)
                {
                    token = new Token() { UserId = result.UserId, Username = result.Username,Firstname= result.FirstName, Tokens = this.GenerateJwtToken(username), Message = "Successfully login" };
                }
                else
                {
                    token = new Token() { Tokens = null, Message = "UnSuccess Login" };
                }

                _messageSender.Publish(token.Message);
                return this.Ok(token);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while login user");
                throw;
            }
        }

       
        [HttpPost]
        [Route("tweet")]
        public async Task<IActionResult> Tweet( Tweet tweet)
        {
            var message = "";
            try
            {
                tweet.TweetDate = DateTime.Now;
                var result = await this.tweetAppService.PostTweet(tweet);
                message = "Tweet Posted";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while posting user tweet");
                throw;
            }
        }

        
        [HttpDelete]
        [Route("tweetdelete/{username},{tweetid}")]
        public async Task<IActionResult> DeleteTweet(string username, int tweetid)
        {
            var message="";
            try
            {
                var result = await this.tweetAppService.DeleteTweet(username, tweetid);
                result = await this.tweetAppService.DeleteComment(tweetid);
                message = "Tweet deleted";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while Deleteing user tweet");
                throw;
            }
        }

        [HttpDelete]
        [Route("user/{username}")]
        public async Task<IActionResult> DeleteUser(string username)
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.DeleteUser(username);
                message = "User deleted";
                _messageSender.Publish(message);
                return this.Ok(result); 

            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while fetching user");
                throw;
            }
        }

        [HttpGet]
        [Route("users/all")]
        public async Task<IActionResult> GetAllUsers()
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.GetAllUsers();
                message = "All user data fetched";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while retrieving users");
                throw;
            }
        }

       
        [HttpGet]
        [Route("user/search/{username}")]
        public async Task<IActionResult> GetTweetsByUser(string username)
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.GetTweetsByUser(username);
                message = "User Tweet fetched";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while fetching tweets by user");
                throw;
            }
        }

      

        
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllTweets()
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.GetAllTweets();
                message = "All tweets fetched";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while fetching user tweets");
                throw;
            }
        }

       
        [HttpGet]
        [Route("allcomments/{username},{tweetid}")]
        public async Task<IActionResult> GetAllComments(string username, int tweetid)
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.GetComments(username, tweetid);
                message = "Comments fetched";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while fetching user comments");
                throw;
            }
        }

       
        [HttpGet]
        [Route("user/{username}")]
        public async Task<User> GetUserProfile(string username)
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.GetUserProfile(username);
                message = "User profile fetched";
                _messageSender.Publish(message);
                return result;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while fetching user");
                throw;
            }
        }

       
        [HttpPut]
        [Route("update/{emailId},{oldpassword},{newpassword}")]
        public async Task<IActionResult> UpdatePassword(string emailId, string oldpassword, string newPassword)
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.UpdatePassword(emailId, oldpassword, newPassword);
                message = "Password updated";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex) { this.logger.LogError(ex, $"Error occured while updating user password"); throw;}
        } 

       
        [HttpPut]
        [Route("forgot/{emailId},{password}")]
        public async Task<IActionResult> ForgotPassword(string emailId, string password)
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.ForgotPassword(emailId, password);
                message = "Password updated";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex) { this.logger.LogError(ex, $"Error occured while reseting user password"); throw; }
        }

       
        [HttpPost]
        [Route("reply/{comment},{username},{Name},{tweetid}")]
        public async Task<IActionResult> PostComment(string comment, string username, string Name, int tweetid)
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.Comments(comment, username, Name, tweetid);
                message = "Comment posted";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while posting user comment");
                throw;
            }
        }

       
        [HttpPut]
        [Route("likes/{tweetid},{flag}")]
        public async Task<IActionResult> PutLikes(int tweetid,bool flag)
        {
            var message = "";
            try
            {
                var result = await this.tweetAppService.Likes(tweetid,flag);
                message = "Tweet Liked";
                _messageSender.Publish(message);
                return this.Ok(result);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, $"Error occured while getting user like");
                throw;
            }
        }





        private string GenerateJwtToken(string emailId)
        {
            
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            //recommended is 5 min
            
            var token = new JwtSecurityToken(
                this.configuration["Jwt:Issuer"],
                this.configuration["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
