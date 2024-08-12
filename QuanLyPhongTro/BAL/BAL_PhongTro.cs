using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.ComponentModel.Design;
using static System.Net.Mime.MediaTypeNames;

namespace BAL
{
    public class BAL_PhongTro
    {
        DataAccessLayer db = null;
        public BAL_PhongTro()
        {
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }

        public DataSet ShowDanhSachPhong()
        {
            return db.ExecuteQueryDataSet("select * from PhongTro",
                CommandType.Text, null);
        }
        public DataSet ShowPhongTro(int maPhong)
        {
            return db.ExecuteQueryDataSet("proc_ShowPhongTro",
                CommandType.StoredProcedure,
                new SqlParameter("@MaPhong", maPhong));
        }

        public bool ThemPhongTro(int maPhong, int maLoaiPhong, ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemPhongTro",
                CommandType.StoredProcedure,
                ref err,
                new SqlParameter("@MaPhong", maPhong),
                new SqlParameter("@MaLoaiPhong", maLoaiPhong)
                );
        }

        public bool CapNhatPhongTro(int maPhong, int maLoaiPhong, bool trangThai, ref string err)
        {
            return db.MyExecuteNonQuery("proc_CapNhatPhongTro",
                CommandType.StoredProcedure,
                ref err, new SqlParameter("@MaPhong", maPhong),
                new SqlParameter("@MaLoaiPhong", maLoaiPhong),
                new SqlParameter("@TrangThai", trangThai));
        }
        public bool XoaPhongTro(int maPhong, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaPhongTro",
                CommandType.StoredProcedure,
                ref err, new SqlParameter("@MaPhong", maPhong));
        }
        public DataSet LayMaPhongTro()
        {
            return db.ExecuteQueryDataSet("select Maphongtro from PhongTro",CommandType.Text,null);
        }
        public int layMaxMaphong()
        {
            int maxMaphong = -1;
            DataSet maPhong =  db.ExecuteQueryDataSet("proc_LayMaxMaPhong", CommandType.StoredProcedure, null);
            maxMaphong = maPhong.Tables[0].Rows[0][0] is DBNull ? 0 : Convert.ToInt32(maPhong.Tables[0].Rows[0][0]);
            return maxMaphong;
        }
        public DataSet ShowPhongTroDangTrong()
        {
            return db.ExecuteQueryDataSet("select * from View_TroChuaThue", CommandType.Text, null);
        }
        public DataSet ShowPhongTroDangTrongTheoMa(int maPhongTro)
        {
            return db.ExecuteQueryDataSet("select * from View_TroChuaThue where maPhongTro = @Maphongtro", CommandType.Text,new SqlParameter("@Maphongtro",maPhongTro));
        }
        public DataSet LayMaPhongTroDangTrong()
        {
            return db.ExecuteQueryDataSet("select Maphongtro from View_TroChuaThue", CommandType.Text,null);
        }
        public bool kiemTraTrangThaiPhongTro(int maPhongTro, DateTime thoiDiem)
        {
            DataTable table_result = db.ExecuteQueryDataSet("select dbo.func_KiemTraPhongTroCoDangDuocThue(@thoiDiem,@maPhong)", CommandType.Text,new SqlParameter("@thoiDiem",thoiDiem),new SqlParameter("@maPhong",maPhongTro)).Tables[0];
            bool check = Convert.ToBoolean(table_result.Rows[0][0]);
            return check;
        }
        public int layTienPhong(int maPhongTro)
        {
            int giaTien = 0;
            DataTable table_result =  db.ExecuteQueryDataSet("select LoaiPhongTro.Giatien from PhongTro inner join LoaiPhongTro on PhongTro.Maloaiphongtro = LoaiPhongTro.Maloaiphong where Maphongtro = @maPhong", CommandType.Text, new SqlParameter("@maPhong", maPhongTro)).Tables[0];
            giaTien = Convert.ToInt32(table_result.Rows[0][0]);
            return giaTien;
        }
        public DataSet LayChiTietPhongTro(int maPhongTro)
        {
            return db.ExecuteQueryDataSet("select * from View_ChiTietPhongTro where Maphongtro = @Maphongtro", CommandType.Text, new SqlParameter("@Maphongtro",maPhongTro));
        }

        public DataSet TimKiemTheoMaPhong(int maPhong)
        {
            return db.ExecuteQueryDataSet($"proc_ShowAllTheoMaPhong", CommandType.StoredProcedure,new SqlParameter("@maPhong",maPhong));
        }
    }
}
