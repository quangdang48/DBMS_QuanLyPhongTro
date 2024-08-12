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
    public class BAL_NoiThat
    {
        DataAccessLayer db;

        public BAL_NoiThat()
        {
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }

        public bool ThemNoiThat(int maNoiThat, string tenNoiThat, int maLoaiNoiThat, int giaTien, string nhaSanXuat, ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemNoiThat", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaNoiThat", maNoiThat),
                new SqlParameter("@TenNoiThat", tenNoiThat),
                new SqlParameter("@MaPhongTro", DBNull.Value),
                new SqlParameter("@MaLoaiNoiThat", maLoaiNoiThat),
                new SqlParameter("@GiaTien", giaTien),
                new SqlParameter("@NhaSanXuat", nhaSanXuat),
                new SqlParameter("@TrangThai", DBNull.Value));
        }

        public DataSet ShowNoiThat(int maNoiThat)
        {
            return db.ExecuteQueryDataSet("proc_ShowNoiThat", CommandType.StoredProcedure,
                new SqlParameter("@PK_MaNoiThat", maNoiThat));
        }
        public DataSet ShowAllNoiThat()
        {
            return db.ExecuteQueryDataSet("select * from NoiThat", CommandType.Text,
                null);
        }

        public bool CapNhatNoiThat(int maNoiThat, string tenNoiThat, int maPhongTro, int maLoaiNoiThat, int giaTien, string nhaSanXuat, bool trangThai, ref string err)
        {
            SqlParameter maphongtro;
            if (maPhongTro == -1)
            {
                maphongtro = new SqlParameter("@MaPhongTro", DBNull.Value);
            }
            else
            {
                maphongtro = new SqlParameter("@MaPhongTro", maPhongTro);
            }
            return db.MyExecuteNonQuery("proc_CapNhatNoiThat", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaNoiThat", maNoiThat),
                new SqlParameter("@TenNoiThat", tenNoiThat),
                maphongtro,
                new SqlParameter("@MaLoaiNoiThat", maLoaiNoiThat),
                new SqlParameter("@GiaTien", giaTien),
                new SqlParameter("@NhaSanXuat", nhaSanXuat),
                new SqlParameter("@TrangThai", trangThai));
        }

        public bool XoaNoiThat(int maNoiThat, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaNoiThat", CommandType.StoredProcedure, ref err,
                new SqlParameter("@PK_MaNoiThat", maNoiThat));
        }
        public int layMaxMaNoiThat()
        {
            int maxMaNoiThat = -1;
            DataSet maPhong = db.ExecuteQueryDataSet("proc_LayMaxMaNoiThat", CommandType.StoredProcedure, null);
            maxMaNoiThat = maPhong.Tables[0].Rows[0][0] is DBNull ? 0 : Convert.ToInt32(maPhong.Tables[0].Rows[0][0]);
            return maxMaNoiThat;
        }
        public DataSet ShowAllNoiThatTrangBiChoTro(int maPhongTro)
        {
            return db.ExecuteQueryDataSet("select* from View_XemNoiThatDangDuocTrangBiChoTro where Maphongtro = @Maphongtro", CommandType.Text,
                new SqlParameter("@Maphongtro",maPhongTro));
        }
        public DataSet TimKiemTheoMaNT(int maNT)
        {
            return db.ExecuteQueryDataSet("proc_ShowAllTheoMaNoiThat", CommandType.StoredProcedure,new SqlParameter("@maNT",maNT));
        }
    }
}
