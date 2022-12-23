using SocialNetwork_Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SocialNetwork_Web.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //Login method
        public ActionResult Login()
        {

            return View();
        }

        //signup method
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(UserSignup user)
        {
            try
            {
                if (ModelState.IsValid)
                {

                }
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage");
            }

            return View();
        }




        //Error Page
        public ActionResult ErrorPage()
        {
            return View();
        }

    }
}