using DAL;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL
{
    public class BAL_Account
    {
        DataAccessLayer db;

        public BAL_Account()
        {
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }
        public DataSet LayThongTinTaiKhoang(string username)
        {
            return db.ExecuteQueryDataSet("select * from Account where username = @Username", CommandType.Text, new SqlParameter("@Username", username));
        }
        public DataSet  LayTatCaTaiKhoang()
        {
            return db.ExecuteQueryDataSet(@"select * from Account where roles = 'user'", CommandType.Text);
        }
        public bool ThemTaiKhoangPhongTro(string username,string password,int maPhong,ref string err ) 
        {
            return db.MyExecuteNonQuery("proc_ThemTaiKhoang", CommandType.StoredProcedure,ref err,new SqlParameter("@username",username),new SqlParameter("@pass_word",password),new SqlParameter("@ma_phong",maPhong));
        }
        public bool XoaTaiKhoan(string username, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaTaiKhoangPhong", CommandType.StoredProcedure, ref err, new SqlParameter("@username", username));
        }
        public bool CapNhatTaiKhoan(string username, string password, ref string err)
        {
            return db.MyExecuteNonQuery("proc_CapNhatTaiKhoang", CommandType.StoredProcedure, ref err, new SqlParameter("@username", username), new SqlParameter("@password", password));
        }

    }
}
