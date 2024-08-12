using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Security.Policy;

namespace BAL
{
    public class BAL_LoaiNoiThat
    {
        DataAccessLayer db;

        public BAL_LoaiNoiThat()
        {
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }

        public bool ThemLoaiNoiThat(int maLoaiNoiThat, string tenLoaiNoiThat, ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemLoaiNoiThat", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLoaiNoiThat", maLoaiNoiThat),
                new SqlParameter("@TenLoaiNoiThat", tenLoaiNoiThat));
        }

        public DataSet ShowLoaiNoiThat(int maLoaiNoiThat)
        {
            return db.ExecuteQueryDataSet("proc_ShowLoaiNoiThat", CommandType.StoredProcedure,
                new SqlParameter("@PK_MaLoaiNoiThat", maLoaiNoiThat));
        }

        public bool CapNhatLoaiNoiThat(int maLoaiNoiThat, string tenLoaiNoiThat, ref string err)
        {
            return db.MyExecuteNonQuery("proc_CapNhatLoaiNoiThat", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLoaiNoiThat", maLoaiNoiThat),
                new SqlParameter("@TenLoaiNoiThat", tenLoaiNoiThat));
        }

        public bool XoaLoaiNoiThat(int maLoaiNoiThat, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaLoaiNoiThat", CommandType.StoredProcedure, ref err,
                new SqlParameter("@PK_MaLoaiNoiThat", maLoaiNoiThat));
        }
        public DataSet ShowDanhSachLoaiNT()
        {
            return db.ExecuteQueryDataSet("select * from LoaiNoiThat", CommandType.Text, null);
        }
        public DataSet LayMaLoaiNoiThat()
        {
            return db.ExecuteQueryDataSet("select Maloainoithat from LoaiNoiThat", CommandType.Text, null);
        }
        
        public int layMaxMaNoiThat()
        {
            int maxMaLoaiNoiThat = -1;
            DataSet maPhong = db.ExecuteQueryDataSet("proc_LayMaxMaLoaiNoiThat", CommandType.StoredProcedure, null);
            maxMaLoaiNoiThat = Convert.ToInt32(maPhong.Tables[0].Rows[0][0]);
            return maxMaLoaiNoiThat;
        }

        public DataSet TimKiemTheoMaLNT(int maLoaiNoiThat)
        {
            return db.ExecuteQueryDataSet("proc_ShowAllTheoMaLoaiNoiThat", CommandType.StoredProcedure,new SqlParameter("@maLNT",maLoaiNoiThat));
        }
    }
}
