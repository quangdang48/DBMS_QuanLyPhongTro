using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BAL
{
    public class BAL_ChiTietDichVu
    {
        DataAccessLayer db;

        public BAL_ChiTietDichVu() { 
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username,BAL_ConnectDatabase.password);
        }

        public bool ThemCTDV(int maPT, int maDV, DateTime dangky,ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemChiTietDichVu", CommandType.StoredProcedure, ref err,
                new SqlParameter("@Maphongtro", maPT),
                new SqlParameter("@Madichvu", maDV),
                new SqlParameter("@Thoidiemdangky", dangky)
                 );
        }
        public DataSet ShowAllChiTietDichVu()
        {
            return db.ExecuteQueryDataSet("select * from ChiTietDichVu", CommandType.Text, null);
        }

        public DataSet ShowChiTietDichVu(int maPT, int maDV, DateTime? dangky)
        {
            return db.ExecuteQueryDataSet("proc_ShowChiTietDichVu", CommandType.StoredProcedure,
                new SqlParameter("@Maphongtro", maPT),
                new SqlParameter("@Madichvu", maDV),
                new SqlParameter("@Thoidiemdangky", dangky));
        }

        public bool CapNhatChiTietDichVu(int maPT, int maDV, DateTime dangky, DateTime? ngungdung, ref string err)
        {
            SqlParameter nullPara;
            if(ngungdung == null)
            {
                nullPara = new SqlParameter("@Thoidiemngungsudung", DBNull.Value);
            }
            else
            {
                nullPara = new SqlParameter("@Thoidiemngungsudung", ngungdung);
            }
            return db.MyExecuteNonQuery("proc_CapNhatChiTietDichVu", CommandType.StoredProcedure, ref err,
                new SqlParameter("@Maphongtro", maPT),
                new SqlParameter("@Madichvu", maDV),
                new SqlParameter("@Thoidiemdangky", dangky),
                nullPara);
        }

        public bool XoaChiTietDichVu(int maPT, int maDV, DateTime? dangky, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaChiTietDichVu", CommandType.StoredProcedure, ref err,
                new SqlParameter("@Maphongtro", maPT),
                new SqlParameter("@Madichvu", maDV),
                new SqlParameter("@Thoidiemdangky", dangky));
        }
        public int layTongTienDichVu(DateTime thoiDiem, int maPhong)
        {
            DataTable result = db.ExecuteQueryDataSet("select dbo.func_TinhTongTienDichVu(@thoiDiem,@maPhong)", CommandType.Text,
               new SqlParameter("@thoiDiem", thoiDiem), new SqlParameter("@maPhong", maPhong)).Tables[0];
            int tongTien = 0;
            tongTien = Convert.ToInt32(result.Rows[0][0]);
            return tongTien;
        }
        public DataSet TimKiemTheoMaPT(int maPT)
        {
            return db.ExecuteQueryDataSet($"proc_ShowAllChiTietDichVuTheoMaPhongTro", CommandType.StoredProcedure,new SqlParameter("@maPT",maPT));
        }
    }
}
