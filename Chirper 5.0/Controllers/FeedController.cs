using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chirper_5._0.Models;
using Microsoft.AspNet.Identity;

namespace Chirper_5._0.Controllers
{
    [Authorize]
    public class FeedController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Feed
        public ActionResult Index()
        {
            var feed = new Feed(db.Users.Find(User.Identity.GetUserId()));
            return View(feed);
        }
    }
}