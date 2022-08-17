using System;
using System.ComponentModel.DataAnnotations;

namespace TweetAPP1.Models
{

    public class UserComments
    {

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string Comments { get; set; }

        public string Imagename { get; set; }


        public DateTime Date { get; set; }


    }
}
