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
    public class BAL_DichVu
    {
        DataAccessLayer db;

        public BAL_DichVu() { 
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }

        public bool ThemDichVu(int maDV, string tenDV, int chiPhi, ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemDichVu", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaDichVu", maDV),
                new SqlParameter("@TenDichVu", tenDV),
                new SqlParameter("@ChiPhi", chiPhi));
        }
        public DataSet ShowAllDichVu()
        {
            return db.ExecuteQueryDataSet("select * from DichVu", CommandType.Text, null);
        }
        public DataSet ShowDichVu(int maDV)
        {
            return db.ExecuteQueryDataSet("proc_ShowDichVu", CommandType.StoredProcedure,
                new SqlParameter("@PK_MaDichVu", maDV));
        }

        public bool CapNhatDichVu(int maDV, string tenDV, int chiPhi, ref string err)
        {
            return db.MyExecuteNonQuery("proc_CapNhatDichVu", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaDichVu", maDV),
                new SqlParameter("@TenDichVu", tenDV),
                new SqlParameter("@ChiPhi", chiPhi));
        }

        public bool XoaDichVu(int maDV, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaDichVu", CommandType.StoredProcedure, ref err,
                new SqlParameter("@PK_MaDichVu", maDV));
        }
        public DataSet LayMaDichVu()
        {
            return db.ExecuteQueryDataSet("select Madichvu from DichVu order by Madichvu", CommandType.Text,
                null);
        }
        public int laymaxMaDichVu()
        {
            int maxMaDichVu = -1;
            DataSet maDichVu = db.ExecuteQueryDataSet("proc_LayMaxMaDichVu", CommandType.StoredProcedure, null);
            maxMaDichVu = maDichVu.Tables[0].Rows[0][0] is DBNull ? 0 : Convert.ToInt32(maDichVu.Tables[0].Rows[0][0]);
            return maxMaDichVu;
        }
        public DataSet DanhSachDichVuDangTrangBiChoTro(int maPhongTro)
        {
            return db.ExecuteQueryDataSet("select * from View_Ds_DichVuDangTrangBi where Maphongtro = @Maphongtro", CommandType.Text, new SqlParameter("@Maphongtro", maPhongTro));
        }
        
        public DataSet TimKiemTheoMaDV(int maDV)
        {
            return db.ExecuteQueryDataSet("proc_ShowAllDichVuTheoMaDichVu", CommandType.StoredProcedure, new SqlParameter("@maDV",maDV));
        }
    }
}
