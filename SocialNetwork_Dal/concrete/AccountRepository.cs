using SocialNetwork_Dal.Abstract;
using SocialNetwork_Dal.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialNetwork_Dal.concrete
{
    public class AccountRepository : IAccountRepository
    {

        DbClass db = new DbClass();
        //private SqlConnection con;
        //string constr = ConfigurationManager.ConnectionStrings["dbConn"].ToString();

        public AccountRepository()
        {
            
        }
        //Verify if login is correct or not!
       
        public User LoginUser(UserLoginCheck user)
        {

            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Email", user.Email));
            sqlParameters.Add(new SqlParameter("@Password", user.Password));

            User user1 = null;

            DataTable dt = db.execGetProc("spLoginUser", sqlParameters);

            if(dt.Rows.Count > 0)
            {
                user1 = new User();
                DataRow r = dt.Rows[0];
                user1.Id = Convert.ToInt32(r["UserId"]);
                user1.FirstName = r["FirstName"].ToString();
                user1.LastName = r["LastName"].ToString();
                user1.UserName = r["UserName"].ToString();
                user1.Email = r["Email"].ToString();
                user1.Phone = r["Phone"].ToString();
               

            }
            return user1;
        }


        //Registering new User

        public bool RegisterUser(User user)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@FirstName", user.FirstName));
            sqlParameters.Add(new SqlParameter("@LastName", user.LastName));
            sqlParameters.Add(new SqlParameter("@Username", user.UserName));
            sqlParameters.Add(new SqlParameter("@Email", user.Email));
            sqlParameters.Add(new SqlParameter("@Phone", user.Phone));
            sqlParameters.Add(new SqlParameter("@Password", user.Password));

            bool added = db.execInsertProc("spRegisterUser", sqlParameters);
            return added;
            throw new NotImplementedException();
        }


        //check if email exst
        public bool EmailExists(string Email)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            sqlParameters.Add(new SqlParameter("@Email", Email));

           DataTable dt = db.execGetProc("spCheckEmailExist", sqlParameters);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;

        }

    }
}
