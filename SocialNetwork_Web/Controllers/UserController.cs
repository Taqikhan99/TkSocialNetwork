using SocialNetwork_Dal.Abstract;
using SocialNetwork_Dal.concrete;
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
        public ActionResult CreatePost(string text,string imgPath)
        {

            return View();
        }
    }
}