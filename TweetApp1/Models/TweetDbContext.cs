using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TweetApp1.Models
{
    public class TweetDbContext :DbContext
    {
        public TweetDbContext()
        {
        }

        public TweetDbContext(DbContextOptions<TweetDbContext> options)
            : base(options)
        {

        }
        

        
        public DbSet<User> User { get; set; }

       
        public DbSet<Tweet> Tweets { get; set; }

        
        public DbSet<Comment> Comments { get; set; }
      

    }
}
