using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp1.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        
        [Required]
        public string FirstName { get; set; }

         
        public string LastName { get; set; }

        
        [Required]
        public string ContactNumber { get; set; }

         
        [Required]
        public string Username { get; set; }
 
        [Required]
        public string EmailId { get; set; }

        
        [Required]
        public string Password { get; set; }

        public string ImageName { get; set; }

        public IList<Tweet> Tweet { get; set; }




    }
}
