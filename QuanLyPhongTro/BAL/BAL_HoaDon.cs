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
    public class BAL_HoaDon
    {
        DataAccessLayer db;

        public BAL_HoaDon() { 
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }

        public bool ThemHoaDon(DateTime? thoidiem, int maPT, ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemHoaDon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@ThoiDiem", thoidiem),
                new SqlParameter("@MaPhong", maPT),
                new SqlParameter("@Tiendien", DBNull.Value),
                new SqlParameter("@Tiennuoc", DBNull.Value),
                new SqlParameter("@Trangthai", DBNull.Value),
                new SqlParameter("@TienDV", DBNull.Value),
                new SqlParameter("@Tienphong", DBNull.Value));
        }

        public DataSet ShowHoaDon(DateTime? thoidiem, int maPT)
        {
            return db.ExecuteQueryDataSet("proc_ShowHoaDon", CommandType.StoredProcedure,
                new SqlParameter("@PK_ThoiDiem", thoidiem),
                new SqlParameter("@PK_MaPhong", maPT));
        }

        public bool CapNhatHoaDon(DateTime? thoidiem, int maPT, int tienDien, int tienNuoc, byte trangThai, int tienDV, int tienPhong, ref string err)
        {
            return db.MyExecuteNonQuery("proc_CapNhatHoaDon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@ThoiDiem", thoidiem),
                new SqlParameter("@MaPhong", maPT),
                new SqlParameter("@Tiendien", tienDien),
                new SqlParameter("@Tiennuoc", tienNuoc),
                new SqlParameter("@Trangthai", trangThai),
                new SqlParameter("@TienDV", tienDV),
                new SqlParameter("@Tienphong", tienPhong));
        }

        public bool XoaHoaDon(DateTime? thoidiem, int maPT, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaHoaDon", CommandType.StoredProcedure, ref err,
                new SqlParameter("@PK_ThoiDiem", thoidiem),
                new SqlParameter("@PK_MaPhong", maPT));
        }

        public DataSet ShowDanhSachHoaDon()
        {
            return db.ExecuteQueryDataSet("select * from HoaDon", CommandType.Text, null);
        }
        public DataSet timKiemHoaDonTheoMaPT(int maPT)
        {
            return db.ExecuteQueryDataSet("proc_ShowAllHoaDonTheoMaPhongTro", CommandType.StoredProcedure,new SqlParameter("@maPT", maPT));
        }

        public DataSet ThongKeDoanhThu(DateTime thoidiem)
        {
            return db.ExecuteQueryDataSet("proc_ThongKeChiPhi", CommandType.StoredProcedure, new SqlParameter("@thoidiem", thoidiem));
        }
    }
}
