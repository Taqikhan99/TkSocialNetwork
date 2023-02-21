﻿using Newtonsoft.Json;
using SocialNetwork_Dal.Abstract;
using SocialNetwork_Dal.concrete;
using SocialNetwork_Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;

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
            ViewBag.updProf = TempData["updProfMess"];
            ViewBag.postcreated = TempData["postmessage"];

            return View();
        }

        //View UserProfile
        [Authorize]
        public ActionResult UserProfile(int Id)
        {
            try
            {
                User user=userRepo.GetProfileData(Id);
                var cuser = Session["loggedinUser"] as User;
                if(cuser.Id != Id)
                {
                    int status=userRepo.GetFriendReqStatus(Id, cuser.Id);
                    ViewBag.frStatus=status;


                }


                return View(user);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Accounts");
            }

            
        }


       



        [HttpPost]
        public ActionResult UserProfile(User user)
        {

            try
            {
                bool updated = userRepo.UpdateProfile(user);
                if (updated)
                {
                    TempData["updProfMess"] = "Profile Updated Success";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Accounts");
            }

            return Json("Ok",JsonRequestBehavior.AllowGet);
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
                return RedirectToAction("ErrorPage", "Accounts");
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

                        TempData["postmessage"] = "Post Created Successfully";
                        return RedirectToAction("Index");
                    }

                }
                TempData["message"] = "Post Not Created";
                return RedirectToAction("ErrorPage", "Accounts");


            }
            catch(Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage","Accounts");
            }


            
        }



        //list people page
        public ActionResult PeoplesPage()
        {
            try
            {
                var cuser = Session["loggedinUser"] as User;
                //will pass the current user id to make get reamining users from user table
                List<User> users = userRepo.GetOtherUsers(cuser.Id);

                 return View(users);
            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Accounts");
            }
            

        }




        [HttpPost]
        public ActionResult SendRequest(int toId)
        {
            try
            {
                var cuser = Session["loggedinUser"] as User;

                bool send = userRepo.SendRequest(cuser.Id, toId);
                
                if(send)
                {
                    return Json(new {message="Send Req Success",code=1},JsonRequestBehavior.AllowGet);
                }
                return Json(new { message = "Send Req Failed", code = 2 }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                TempData["message"] = ex.Message;
                return RedirectToAction("ErrorPage", "Account");
            }
        }

    }
}