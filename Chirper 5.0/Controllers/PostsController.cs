using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Chirper_5._0.Models;
using Microsoft.AspNet.Identity;
using WebGrease.Css.Extensions;

namespace Chirper_5._0.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Posts
        public ActionResult Index(string userId)
        {
            if (String.IsNullOrEmpty(userId))
                userId = User.Identity.GetUserId();

           ViewBag.AreTheyMine = userId == User.Identity.GetUserId();
           return View(db.Posts.Where(post => post.Author.Id.Equals(userId)).ToList());

        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Text")] Post post)
        {
            if (ModelState.IsValid)
            {
                post.CreateDateTime = DateTime.Now;
                post.Author = db.Users.Find(User.Identity.GetUserId());
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(post);
        }

        public ActionResult Repost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (post.Author.Id.Equals(User.Identity.GetUserId()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                var rePost = new Post()
                {
                    RepostFrom = post,
                    Author = db.Users.Find(User.Identity.GetUserId()),
                    Text = post.Text,
                    CreateDateTime = DateTime.Now
                };
                
                db.Posts.Add(rePost);
                db.SaveChanges();
                return RedirectToAction("Index","Feed");
            }
            return RedirectToAction("Index", "Feed");
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            if (!post.Author.Id.Equals(User.Identity.GetUserId()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Text")] Post post)
        {
            var editPost = db.Posts.Find(post.Id);
            if (editPost == null)
            {
                return HttpNotFound();
            }
            if (!editPost.Author.Id.Equals(User.Identity.GetUserId()))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (ModelState.IsValid)
            {
                editPost.LastEditedDateTime = DateTime.Now;
                editPost.Text = post.Text;
                db.Entry(editPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Feed");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post deletePost = db.Posts.Find(id);

            db.Posts.Where(post => post.RepostFrom.Id.Equals(id)).ForEach(rePost => db.Posts.Remove(rePost));
            db.Posts.Remove(deletePost);
            db.SaveChanges();
            return RedirectToAction("Index", "Feed");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
