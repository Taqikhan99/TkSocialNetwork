using SocialNetwork_Dal.Abstract;
using SocialNetwork_Dal.concrete;
using SocialNetwork_Dal.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SocialNetwork_Web.Controllers
{
    public class AccountsController : Controller
    {
        // GET: Accounts
        //public ActionResult Index()
        //{
        //    return View();
        //}

        private IAccountRepository _accountRepo;

        public AccountsController()
        {
            this._accountRepo = new AccountRepository();

        }

        //Login method
        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(UserLoginCheck check)
        {
            try
            {   
                if (ModelState.IsValid)
                {
                    User u = _accountRepo.LoginUser(check);
                    if(u != null)
                    {
                        TempData["lmessage"] = "Login Successful!";
                        FormsAuthentication.SetAuthCookie(u.Email, false);

                        //set usr in tempdata and pass to the homepage
                        Session["loggedinUser"] = u;

                        return RedirectToAction("Index", "User");
                    }
                    
                }
                return View();
            }
            catch(Exception ex)
            {

                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage");
            }

            
        }


        //signup method
        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //first check if email exists
                    if (!_accountRepo.EmailExists(user.Email))
                    {
                        bool success = _accountRepo.RegisterUser(user);
                        if (success)
                        {
                            TempData["smessage"] = "Registration Successful";
                            return RedirectToAction("Login");
                        }
                    }

                    
                    else
                    {
                        TempData["message"] = "Email Already exists!";
                        return View();
                    }

                }
            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage");
            }

            return View();
        }

        //signout
        public ActionResult Signout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Login");
        }


        //Error Page
        public ActionResult ErrorPage()
        {
            return View();
        }

    }
}