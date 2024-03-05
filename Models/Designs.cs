using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Cloud22010446_Dut4life.Models
{

	public class Designs
	{
        public int? DesignID { get; set; }

        public string DesignName { get; set; }

        public double Price { get; set; }
        public string Url { get; set; }
       
        public string Description { get; set; }
        public int Quantity { get; set; } // New property for Quantity


        SqlCommand cmd;
        SqlDataAdapter da;
        DataSet ds;

        public static SqlConnection connect()
        {
            string connection = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection con = new SqlConnection(connection);
            if (con.State == ConnectionState.Open)
            {
                con.Close();

            }
            else
            {
                con.Open();
            }


            return con;

        }

        public bool DMLOpperation(string query)
        {
            cmd = new SqlCommand(query, connect());
            int x = cmd.ExecuteNonQuery();
            if (x == 1)
            {
                return true;
            }
            else
            {
                return false;
            }


        }

        public DataTable SelactAll(string query)
        {
            da = new SqlDataAdapter(query, connect());
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}