using SocialNetwork_Dal.Abstract;
using SocialNetwork_Dal.concrete;
using SocialNetwork_Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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
            TempData["loginmessage"] = TempData["lmessage"];

            return View();
        }

        //View UserProfile
        [Authorize]
        public ActionResult UserProfile(int Id)
        {
            try
            {
                User user=userRepo.GetProfileData(Id);
                return View(user);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }

            return View();
        }

        [HttpPost]
        public ActionResult UserProfile(User user)
        {
            return View();
        }

        //upload profile pic
        [HttpPost]
        public ActionResult UpdateProfile(HttpPostedFileBase file)
        {
            try
            {


                return Json("Ok", JsonRequestBehavior.AllowGet);
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }


        //create post
        [Authorize]
        [HttpPost]
        public ActionResult CreatePost(Post post)
        {
            try
            {
                var currentUser = (SocialNetwork_Dal.Entities.User)Session["loggedinUser"];
                if(currentUser != null)
                {
                    string message = userRepo.CreatePost(post, currentUser.Id);

                    //check if post created
                    if (message == "OK") {
                    
                        ViewBag.message = "Post Created Successfully";
                        return RedirectToAction("Index");
                    }

                }
                TempData["message"] = "Post Not Created";
                return RedirectToAction("ErrorPage", "Account");


            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage","Account");
            }


            
        }


        //list people page
        public ActionResult PeoplesPage()
        {
            return View();

        }


    }
}