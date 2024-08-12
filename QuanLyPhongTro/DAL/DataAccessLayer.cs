using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataAccessLayer
    {

       // string connStr = "Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLyPhongTro;Integrated Security=True";
        SqlConnection conn = null;
        SqlCommand cmd = null;
        SqlDataAdapter da = null;
        public static string err ;
        public DataAccessLayer(string nameDataBase,string username,string password)
        {
            connectToDataBase(nameDataBase,username,password);
            cmd = conn.CreateCommand();
        }
        public bool connectToDataBase(string nameDataBase, string username, string password)
        {
            err = "";
            string connStr = "";
            try
            {
                if(nameDataBase == "QuanLyPhongTro")
                    connStr = $"Data Source=.\\SQLEXPRESS;Initial Catalog=QuanLyPhongTro;User ID={username};Password={password};Encrypt=False";
                else
                {
                    connStr = $"Data Source=.\\SQLEXPRESS; Initial Catalog ={nameDataBase}; Integrated Security = True; Encrypt = False";
                }
                conn = new SqlConnection(connStr);
                conn.Open();
                conn.Close();
                return true;
            }
            catch (Exception ex)
            {
                err = ex.Message;
                return false;
            }
        }
        public DataSet ExecuteQueryDataSet(string strSQL, CommandType ct, params SqlParameter[] p)
        {
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            cmd.Parameters.Clear();
            if (p != null && p.Length > 0)
            {
                foreach (SqlParameter parameter in p)
                {
                    cmd.Parameters.Add(parameter);
                }
            }

            da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);
            return ds;
        }

        public bool MyExecuteNonQuery(string strSQL, CommandType ct, ref string error, params SqlParameter[] param)
        {
            bool flag = false;
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            conn.Open();
            cmd.Parameters.Clear();
            cmd.CommandText = strSQL;
            cmd.CommandType = ct;
            foreach (SqlParameter p in param)
            {
                cmd.Parameters.Add(p);
            }

            try
            {
                cmd.ExecuteNonQuery();
                flag = true;
            }
            catch (SqlException ex)
            {
                error = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return flag;
        }
    }
}
