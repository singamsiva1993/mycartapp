using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MyCartApp.DAL
{
    public class DbCore
    {
        private string constring;

        public DbCore()
        {
            constring = ConfigurationManager.ConnectionStrings["mycart"].ToString();
        }

        public int CoreOps(string proc, SqlParameter[] param)
        {
            int res = 0;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(proc, con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (param != null)
                    {
                        cmd.Parameters.AddRange(param);
                    }
                    res = cmd.ExecuteNonQuery();

                }
                con.Close();
            }
            return res;
        }
        public DataTable GetTable(string proc, SqlParameter[] param)
        {
            DataTable dt = null;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(proc, con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(param);
                    SqlDataAdapter dab = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    dab.Fill(dt);
                }
                con.Close();
            }
            return dt;
        }
        public DataTable GetTable(string proc)
        {
            DataTable dt = null;
            using (SqlConnection con = new SqlConnection(constring))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand(proc, con))
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter dab = new SqlDataAdapter(cmd);
                    dt = new DataTable();
                    dab.Fill(dt);
                }
                con.Close();
            }
            return dt;
        }
    }
}