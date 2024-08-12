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
    public class BAL_LoaiPhongTro
    {
        DataAccessLayer db;

        public BAL_LoaiPhongTro()
        {
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }

        public bool ThemLoaiPhongTro(int maLoaiPhong, int dienTich, int giaTien, int tienCoc, ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemLoaiPhong", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLoaiPhong", maLoaiPhong),
                new SqlParameter("@DienTich", dienTich),
                new SqlParameter("@GiaTien", giaTien),
                new SqlParameter("@TienCoc", tienCoc));
        }

        public DataSet ShowLoaiPhongTro(int maLoaiPhong)
        {
            return db.ExecuteQueryDataSet("proc_ShowLoaiPhong", CommandType.StoredProcedure,
                new SqlParameter("@PK_MaLoaiPhong", maLoaiPhong));
        }
        public DataSet ShowAllLoaiPhongTro()
        {
            return db.ExecuteQueryDataSet("Select * from LoaiPhongTro", CommandType.Text,
                null);
        }

        public bool CapNhatLoaiPhongTro(int maLoaiPhong, int dienTich, int giaTien, int tienCoc, ref string err)
        {
            return db.MyExecuteNonQuery("proc_CapNhatLoaiPhong", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaLoaiPhong", maLoaiPhong),
                new SqlParameter("@DienTich", dienTich),
                new SqlParameter("@GiaTien", giaTien),
                new SqlParameter("@TienCoc", tienCoc));
        }

        public bool XoaLoaiPhongTro(int maLoaiPhong, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaLoaiPhong", CommandType.StoredProcedure, ref err,
                new SqlParameter("@Maloaiphong", maLoaiPhong));
        }
        public DataSet LayMaLoaiPhong()
        {
            return db.ExecuteQueryDataSet("Select Maloaiphong from LoaiPhongTro", CommandType.Text,
                null);
        }
        public int layMaxMaloaiphong()
        {
            int maxMaloaiphong = -1;
            DataSet maLoaiPhong = db.ExecuteQueryDataSet("proc_LayMaxMaLoaiPhongTro", CommandType.StoredProcedure, null);
            maxMaloaiphong = maLoaiPhong.Tables[0].Rows[0][0] is DBNull ? 0 : Convert.ToInt32(maLoaiPhong.Tables[0].Rows[0][0]);
            return maxMaloaiphong;
        }
        public DataSet TimKiemTheoMaLP(int maLP)
        {
            return db.ExecuteQueryDataSet("proc_ShowAllTheoMaLoaiPhong", CommandType.StoredProcedure,new SqlParameter("@maLP",maLP));
        }
    }
}
