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
    public class BAL_DienNuoc
    {
        DataAccessLayer db;

        public BAL_DienNuoc() { 
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }

        public bool ThemDienNuoc(int maPhongTro, DateTime thoiDiem, int soDienHienTai, int soNuocHienTai,ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemDienNuoc", CommandType.StoredProcedure, ref err,
                new SqlParameter("@Maphongtro", maPhongTro),
                new SqlParameter("@Thoidiem", thoiDiem),
                new SqlParameter("@Sodienhientai", soDienHienTai),
                new SqlParameter("@Sonuochientai", soNuocHienTai));
        }
        public DataSet ShowAllDienNuoc()
        {
            return db.ExecuteQueryDataSet("select  * from DienNuoc", CommandType.Text, null);
        }
        public DataSet ShowDienNuoc(int maPhongTro, DateTime? thoiDiem = null)
        {
            if (thoiDiem == null)
            {
                return db.ExecuteQueryDataSet("proc_ShowDienNuoc", CommandType.StoredProcedure,
                    new SqlParameter("@Maphongtro", maPhongTro));
            }
            else
            {
                return db.ExecuteQueryDataSet("proc_ShowDienNuoc", CommandType.StoredProcedure,
                    new SqlParameter("@Maphongtro", maPhongTro),
                    new SqlParameter("@Thoidiem", thoiDiem));
            }
        }

        public bool CapNhatDienNuoc(int maPhongTro, DateTime thoiDiem, int soDienHienTai, int soNuocHienTai, int soDienCu, int soNuocCu, ref string err)
        {
            return db.MyExecuteNonQuery("proc_CapNhatDienNuoc", CommandType.StoredProcedure, ref err,
                new SqlParameter("@Maphongtro", maPhongTro),
                new SqlParameter("@Thoidiem", thoiDiem),
                new SqlParameter("@Sodienhientai", soDienHienTai),
                new SqlParameter("@Sonuochientai", soNuocHienTai),
                new SqlParameter("@Sodiencu", soDienCu),
                new SqlParameter("@Sonuoccu", soNuocCu));
        }

        public bool XoaDienNuoc(int maPhongTro, DateTime thoiDiem, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaDienNuoc", CommandType.StoredProcedure, ref err,
                new SqlParameter("@Maphongtro", maPhongTro),
                new SqlParameter("@Thoidiem", thoiDiem));
        }
        public DateTime LayThoiDiemTiepTheo(int maPhong)
        {
            DateTime time = DateTime.Now;
            DataTable table_time = db.ExecuteQueryDataSet("proc_LayThoiDiemTiepTheo", CommandType.StoredProcedure,
               new SqlParameter("Maphong", maPhong)).Tables[0];
            time = Convert.ToDateTime(table_time.Rows[0][0]);
            return time;
        }
        public int LayTienDien(DateTime thoiDiem, int maPhong)
        {
            int tienDien = 0;
            DataTable table_result = db.ExecuteQueryDataSet("select dbo.func_LayTienDienCuaPhong(@thoiDiem,@maPhong)", CommandType.Text,new SqlParameter("thoiDiem",thoiDiem),
               new SqlParameter("Maphong", maPhong)).Tables[0];
            tienDien = Convert.ToInt32(table_result.Rows[0][0]);
            return tienDien;
        }
        public int LayTienNuoc(DateTime thoiDiem, int maPhong)
        {
            int tienNuoc = 0;
            DataTable table_result = db.ExecuteQueryDataSet("select dbo.func_LayTienNuocCuaPhong(@thoiDiem,@maPhong)", CommandType.Text, new SqlParameter("thoiDiem", thoiDiem),
               new SqlParameter("Maphong", maPhong)).Tables[0];
            tienNuoc = Convert.ToInt32(table_result.Rows[0][0]);
            return tienNuoc;
        }

        public DataSet TimKiemTheoMaPhong(int maPT)
        {
            return db.ExecuteQueryDataSet("proc_ShowAllDienNuocTheoMaPhongTro",CommandType.StoredProcedure,new SqlParameter("@maPT",maPT));
        }
    }
}
