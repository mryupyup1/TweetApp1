using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace TweetApp1.Models
{
    public class Tweet
    {
       [Key]
        public int Id { get; set; }

         

        public string Username { get; set; }

        public string Tweets { get; set; }

        public string FirstName { get; set; }

        public DateTime TweetDate { get; set; }


        public int Likes { get; set; }

        public IList<Comment> coments { get; set; }
    }
}
