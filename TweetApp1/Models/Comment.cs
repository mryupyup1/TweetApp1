using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TweetApp1.Models
{
    public class Comment
    {
        [Key]
        
        public int Id { get; set; }
 
        public int TweetId { get; set; }

         
        public string Username { get; set; }

        public string FirstName { get; set; }

        
        public string Comments { get; set; }

       
        public DateTime Date { get; set; }

        
    }
}
