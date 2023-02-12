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
                user.Dob =  Convert.ToDateTime(r["DateOfBirth"]);
                user.ProfilePicPath = r["ProfilePic"].ToString();
                user.Gender = r["Gender"].ToString();
                user.City = r["City"].ToString() ;
                user.Password = r["Password"].ToString();
                user.Phone = r["Phone"].ToString();


            }
            return user;

        }
    }
}
