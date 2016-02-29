using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Chirper_5._0.Models;
using Microsoft.AspNet.Identity;

namespace Chirper_5._0.Controllers
{
    public class MySubscribtionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: MySubscribtions
        public ActionResult Index()
        {
            return View(new MySubscribtionList(db.Users.Find(User.Identity.GetUserId())));
        }
    }
}