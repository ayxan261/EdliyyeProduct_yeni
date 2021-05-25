using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EdliyyeProduct.Models.Entity;
using PagedList;
using PagedList.Mvc;


namespace EdliyyeProduct.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        EdliyyeEntityEntities db = new EdliyyeEntityEntities();
        public ActionResult Index(string ara)
        {
            //var model = db.Possts.ToList();
            return View(db.Possts.Where(x => x.Title.Contains(ara) || ara == null).ToList());
        }

        public ActionResult CommentPage(int id)
        {
            var model = db.Possts.Find(id);
            var cmp = db.Comments.Where(x => x.PostId == model.Id).ToList();
            return View(cmp);
        }

        public ActionResult NewPost()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewPost(Posst p)
        {
            var m = db.Possts.FirstOrDefault(x => x.Title == p.Title);
            if (m == null)
            {
                db.Possts.Add(p);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            else 
            {
                ViewBag.message = "This record already existed";
                return View();
            }
            
        }

        public ActionResult NewComment()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewComment(Comment c)
        {
            var m = db.Comments.FirstOrDefault(x => x.Name == c.Name);
            if (m == null)
            {
                db.Comments.Add(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.message = "This record already existed";
                return View();
            }

        }
    }
}