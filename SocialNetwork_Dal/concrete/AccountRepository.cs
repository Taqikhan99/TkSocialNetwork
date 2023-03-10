using Newtonsoft.Json;
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
                user1.UserName = r["UserName"].ToString();
                user1.Email = r["Email"].ToString();
                user1.Phone = r["Phone"].ToString();
                user1.ProfilePicPath = r["ProfilePic"].ToString();
               

            }
            return user1;
        }


        //Registering new User

        public bool RegisterUser(User user)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>();
            
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

        //get country options
       

        public string Countryoptions()
        {
            string query = $"select CountryId,Countryname from CountryTb";
            string jsnDatatable = "";

            DataTable dt = db.execQuery(query);
            if (dt.Rows.Count > 0)
            {
                jsnDatatable = JsonConvert.SerializeObject(dt);
                return jsnDatatable;

            }
            else
            {
                return null;
            }
            throw new NotImplementedException();
        }

        public string CityOptions(int id)
        {
            string query = $"select CityId,CityName from CityTb where CountryId={id}";
            string jsnDatatable = "";

            DataTable dt = db.execQuery(query);
            if (dt.Rows.Count > 0)
            {
                jsnDatatable = JsonConvert.SerializeObject(dt);
                return jsnDatatable;

            }
            else
            {
                return null;
            }
        }


        

    }
}
