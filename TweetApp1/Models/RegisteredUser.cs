using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace TweetAPP1.Models
{
   
    public class RegisteredUser
    {
         
        public string FirstName { get; set; }

        
        public string LastName { get; set; }

        public string ImageName { get; set; }

        public string Username { get; set; }
    }
}
