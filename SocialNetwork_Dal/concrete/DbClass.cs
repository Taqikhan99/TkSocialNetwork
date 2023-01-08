using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SocialNetwork_Dal.concrete
{
    public class DbClass
    {
        string constr;
        SqlConnection con;
        public DbClass()
        {
            constr = ConfigurationManager.ConnectionStrings["dbConn"].ToString();
            con = new SqlConnection(constr);
        }

        //procedure call for insert
        public bool execInsertProc(string procname, List<SqlParameter> sqlParameters)
        {
                int i = 0;

                using (SqlCommand cmd = new SqlCommand(procname, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    if (sqlParameters != null)
                    {
                        foreach (SqlParameter p in sqlParameters)
                        {
                            cmd.Parameters.Add(p);
                        }
                    }

                    con.Open();
                    i = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (i >= 1)
                {
                    return true;
                }
                return false;
           


        }

        //make generic function to call get procedures
        public DataTable execGetProc(string procname, List<SqlParameter> parameters = null)
        {

            DataTable dt = new DataTable();
            using (SqlCommand cmd = new SqlCommand(procname, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;

                if (parameters != null)
                {
                    foreach (SqlParameter p in parameters)
                    {
                        cmd.Parameters.Add(p);
                    }
                }

                SqlDataAdapter adapter = new SqlDataAdapter(cmd);

                con.Open();
                adapter.Fill(dt);
                con.Close();


            }

            return dt;

        }

    }
}
