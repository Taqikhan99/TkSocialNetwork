using SocialNetwork_Dal.Abstract;
using SocialNetwork_Dal.concrete;
using SocialNetwork_Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork_Web.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        private IUserRepository userRepo;

        public UserController()
        {
            userRepo = new UserRepository();
        }
        [Authorize]
        public ActionResult Index()
        {
            

            ViewBag.cuser = Session["loggedinUser"];

            return View();
        }

        //View UserProfile
        [Authorize]
        public ActionResult MyProfile()
        {
            

            return View();
        }



        //create post
        [HttpPost]
        public ActionResult CreatePost(Post post)
        {
            try
            {
                var currentUser = (SocialNetwork_Dal.Entities.User)Session["loggedinUser"];
                if(currentUser != null)
                {
                    string message = userRepo.CreatePost(post, currentUser.Id);
                }

                
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage","Account");
            }

            if (ModelState.IsValid)
            {


                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false,JsonRequestBehavior.AllowGet);
            }

            
        }
    }
}