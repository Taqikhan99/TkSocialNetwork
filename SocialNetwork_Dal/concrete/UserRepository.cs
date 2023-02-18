﻿using Newtonsoft.Json;
using SocialNetwork_Dal.Abstract;
using SocialNetwork_Dal.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SocialNetwork_Dal.concrete
{
    public class UserRepository : IUserRepository
    {
        DbClass db = new DbClass();
        public User GetUserData(string email)
        {
            //DataTable dt = db.execQuery($"Select UserId,FirstName,LastName,Email,Phone from UserTb where Email={email}");

            throw new NotImplementedException();
        }


        #region Creating New Post Method

        //method to create a post
        public string CreatePost(Post post, int posterId)
        {
            string extension = Path.GetExtension(post.PostImage.FileName);

            //check if extension is jpg, jpeg or png
            if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
            {


                string filename = HttpContext.Current.User.Identity.Name + "_" + Path.GetFileName(post.PostImage.FileName) + Path.GetExtension(post.PostImage.FileName);

                post.PostImagePath = "~/Content/PostImages/" + filename;

                if (post.PostImage != null)
                {
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/PostImages/"), filename);
                    //string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/profilepics/"), HttpContext.Current.User.Identity.Name + "_" + Path.GetFileName((teacher.UserPic.FileName))+ Path.GetExtension(teacher.UserPic.FileName));
                    post.PostImage.SaveAs(path);
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                    
                    sqlParameters.Add(new SqlParameter("@posttext", post.PostText));
                    sqlParameters.Add(new SqlParameter("@postImage", post.PostImagePath));
                    sqlParameters.Add(new SqlParameter("@postedby", posterId));
                   
                   

                    bool updated = db.execInsertProc("spCreatePost", sqlParameters);

                    if (updated)
                    {
                        return "OK";
                    }

                }


            }
                throw new NotImplementedException();
        }
        #endregion


        #region Visiting UserProfile
        public User GetProfileData(int id)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@userid", id));

            User user = new User();
            DataTable dt = db.execGetProc("spGetUserProfile", sqlParameters);

            //u.UserId, u.Username,u.Email,u.CreatedAt,up.DateOfBirth,up.ProfilePic,up.Gender,up.city

            if (dt.Rows.Count>0) {
            
                DataRow r = dt.Rows[0];
                user.Id = Convert.ToInt32(r["UserId"]);
                
                user.UserName = r["UserName"].ToString();
                user.Email = r["Email"].ToString();
                user.CreatedAt =  Convert.ToDateTime(r["CreatedAt"]);
                user.Dob = Convert.ToDateTime( r["DateOfBirth"]);
                user.ProfilePicPath = r["ProfilePic"].ToString();
                user.Gender = r["Gender"].ToString();
                user.City = r["City"].ToString() ;
                user.Country = r["Country"].ToString() ;
                user.Password = r["Password"].ToString();
                user.Phone = r["Phone"].ToString();


            }
            return user;

        }
        #endregion


        #region Updating Profile
        public bool UpdateProfile(User user)
        {

            // first save the imag path
            string extension = Path.GetExtension(user.ProfilePic.FileName);

            //check if extension is jpg, jpeg or png
            if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".jpeg") || extension.ToLower().Equals(".png"))
            {


                string filename = user.UserName + "_" + Path.GetFileName(user.ProfilePic.FileName) + Path.GetExtension(user.ProfilePic.FileName);

                user.ProfilePicPath = "~/Content/ProfileImages/" + filename;

                if (user.ProfilePic != null)
                {
                    string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/ProfileImages/"), filename);
                    //string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Content/images/profilepics/"), HttpContext.Current.User.Identity.Name + "_" + Path.GetFileName((teacher.UserPic.FileName))+ Path.GetExtension(teacher.UserPic.FileName));
                    user.ProfilePic.SaveAs(path);
                    List<SqlParameter> sqlParameters = new List<SqlParameter>();
                }
            }


            bool updated = false;
            if (user != null)
            {
                List<SqlParameter> sqlParameters = new List<SqlParameter>();
                sqlParameters.Add(new SqlParameter("@userid", user.Id));
                sqlParameters.Add(new SqlParameter("@username", user.UserName));
                sqlParameters.Add(new SqlParameter("@password", user.Password));
                sqlParameters.Add(new SqlParameter("@email", user.Email));
                sqlParameters.Add(new SqlParameter("@profilepicpath", user.ProfilePicPath));
                sqlParameters.Add(new SqlParameter("@city", user.City));
                sqlParameters.Add(new SqlParameter("@gender", user.Gender));
                //sqlParameters.Add(new SqlParameter("@Username", s.Username));
                //sqlParameters.Add(new SqlParameter("@DepartId", s.DepartId));
                //sqlParameters.Add(new SqlParameter("@Password", s.Password));

                updated = db.execInsertProc("spUpdateProfile", sqlParameters);



            }
            return updated;
        }

        #endregion



        #region Display All Users on Peoples Page

        public List<User> GetOtherUsers(int id)
        {
            List<User> users = new List<User>();
            string query = $"Select * from UsersTb where UserId != {id}";

            DataTable dt = db.execQuery(query);
            if(dt.Rows.Count > 0)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    User user = new User();
                    user.Id = Convert.ToInt32(dr["UserId"]);
                    user.UserName= dr["UserName"].ToString();
                    user.Email = dr["email"].ToString();

                    users.Add(user);
                }
            }

            return users;
            throw new NotImplementedException();
        }

        #endregion

    }
}
