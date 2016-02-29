using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr.Runtime.Misc;
using WebGrease.Css.Extensions;

namespace Chirper_5._0.Models
{
    public class Feed
    {
        public ApplicationUser User;

        public List<Post> Posts; 
        public Feed(ApplicationUser user)
        {
            User = user;
            Posts = new List<Post>();
            user.Subscriptions.ForEach(userSub => userSub.Posts.ForEach(post => Posts.Add(post)));
            user.Posts.ForEach(post => Posts.Add(post));
            Posts = Posts.OrderByDescending(post => post.CreateDateTime).ToList();
        }
    }
}
