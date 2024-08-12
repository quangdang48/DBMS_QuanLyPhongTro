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
    public class BAL_HopDong
    {
        DataAccessLayer db;

        public BAL_HopDong() { 
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }

        public bool ThemHopDong(int nguoithue, int maPT, DateTime dangky,ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemHopDong", CommandType.StoredProcedure, ref err,
                 new SqlParameter("@Manguoithue", nguoithue),
                 new SqlParameter("@Maphongtro", maPT),
                 new SqlParameter("@Ngaytao", dangky) );
        }

        public DataSet ShowHopDong(int nguoithue, int maPT)
        {
            return db.ExecuteQueryDataSet("proc_ShowHopDong", CommandType.StoredProcedure,
                new SqlParameter("@Manguoithue", nguoithue),
                new SqlParameter("@Maphongtro", maPT));
        }

        public bool CapNhatHopDong(int nguoithue, int maPT, DateTime dangky, DateTime? ketthuc, ref string err)
        {
            SqlParameter ngaydondi;
            if (ketthuc == null)
            {
                ngaydondi = new SqlParameter("@Ngaykt", DBNull.Value);
            }
            else
            {
                ngaydondi = new SqlParameter("@Ngaykt", ketthuc);
            }
            return db.MyExecuteNonQuery("proc_CapNhatHopDong", CommandType.StoredProcedure, ref err,
                 new SqlParameter("@Manguoithue", nguoithue),
                 new SqlParameter("@Maphongtro", maPT),
                 new SqlParameter("@Ngaytao", dangky),
                 ngaydondi);
        }

        public bool XoaHopDong(int nguoithue, int maPT, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaHopDong", CommandType.StoredProcedure, ref err,
                new SqlParameter("@Manguoithue", nguoithue),
                new SqlParameter("@Maphongtro", maPT));
        }
        public DataSet ShowDanhSachHopDong()
        {
            return db.ExecuteQueryDataSet("select * from HopDong", CommandType.Text, null);
        }

        public DataSet TimKiemTheoMaNTT(int maNTT)
        {
            return db.ExecuteQueryDataSet($"proc_ShowAllHopDongTheoMaNguoiThue", CommandType.StoredProcedure,new SqlParameter("@maNTT",maNTT));
        }
    }
}
