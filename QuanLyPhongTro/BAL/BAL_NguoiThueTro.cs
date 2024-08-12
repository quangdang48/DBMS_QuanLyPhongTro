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
    public class BAL_NguoiThueTro
    {
        DataAccessLayer db;

        public BAL_NguoiThueTro()
        {
            db = new DataAccessLayer(BAL_ConnectDatabase.nameDatabase, BAL_ConnectDatabase.username, BAL_ConnectDatabase.password);
        }



        public bool ThemNguoiThueTro(int maNguoiThue, DateTime? ngayVaoO, string ho, string tenLot, string ten, string soCCCD, string queQuan, string soDT, string ngheNghiep, ref string err)
        {
            return db.MyExecuteNonQuery("proc_ThemNguoiThueTro", CommandType.StoredProcedure, ref err,
                new SqlParameter("@MaNguoiThue", maNguoiThue),
                new SqlParameter("@NgayVao", ngayVaoO),
                new SqlParameter("@Ngaydi", DBNull.Value),
                new SqlParameter("@Ho", ho),
                new SqlParameter("@TenLot", tenLot),
                new SqlParameter("@Ten", ten),
                new SqlParameter("@SoCCCD", soCCCD),
                new SqlParameter("@QueQuan", queQuan),
                new SqlParameter("@SoDT", soDT),
                new SqlParameter("@job", ngheNghiep));
        }

        public DataSet ShowDanhSachNguoiThueTro()
        {
            return db.ExecuteQueryDataSet("select * from NguoiThueTro", CommandType.Text);
        }

        public DataSet ShowNguoiThueTro(int maNguoiThue)
        {
            return db.ExecuteQueryDataSet("proc_ShowNguoiThueTro", CommandType.StoredProcedure,
                new SqlParameter("@PK_MaNguoiThue", maNguoiThue));
        }
        public DataSet ShowNguoiThueTroHienTai()
        {
            return db.ExecuteQueryDataSet("select * from View_Ds_NguoiThueTro_HienTai", CommandType.Text,
                null);
        }

        public bool CapNhatNguoiThueTro(int maNguoiThue, DateTime? ngayVaoO, DateTime? ngayDonDi, string ho, string tenLot, string ten, string soCCCD, string queQuan, string soDT, string ngheNghiep, ref string err)
        {
            SqlParameter ngayDonDiPara;
            if(ngayDonDi == null)
            {
                ngayDonDiPara = new SqlParameter("@Ngaydi", DBNull.Value);
            }
            else
            {
                ngayDonDiPara = new SqlParameter("@Ngaydi", ngayDonDi);
            }
            return db.MyExecuteNonQuery("proc_CapNhatNguoiThueTro", CommandType.StoredProcedure, ref err,
                 new SqlParameter("@MaNguoiThue", maNguoiThue),
                 new SqlParameter("@NgayVao", ngayVaoO),
                 ngayDonDiPara,
                 new SqlParameter("@Ho", ho),
                 new SqlParameter("@TenLot", tenLot),
                 new SqlParameter("@Ten", ten),
                 new SqlParameter("@SoCCCD", soCCCD),
                 new SqlParameter("@QueQuan", queQuan),
                 new SqlParameter("@SoDT", soDT),
                 new SqlParameter("@job", ngheNghiep));
        }

        public bool XoaNguoiThueTro(int maNguoiThue, ref string err)
        {
            return db.MyExecuteNonQuery("proc_XoaNguoiThueTro", CommandType.StoredProcedure, ref err,
                new SqlParameter("@Manguoithue", maNguoiThue));
        }
        public DataSet LayMaNguoiThue()
        {
            return db.ExecuteQueryDataSet("Select Manguoithue from NguoiThueTro order by Manguoithue", CommandType.Text, null);
        }
        public int layMaxMaNguoiThue()
        {
            int maxMaNguoiThue = -1;
            DataSet maNguoiThue = db.ExecuteQueryDataSet("proc_LayMaxMaNguoiThue", CommandType.StoredProcedure, null);
            maxMaNguoiThue = Convert.ToInt32(maNguoiThue.Tables[0].Rows[0][0]);
            return maxMaNguoiThue;
        }
        public DataSet ShowNguoiThueHienTaiTheoMa(int maPhongTro)
        {
            return db.ExecuteQueryDataSet("select * from View_Ds_NguoiThueTro_HienTai where Maphongtro=@maPhongTro", CommandType.Text, new SqlParameter("@maPhongTro", maPhongTro));
        }
        public DataSet LayMaNguoiThueHienTai()
        {
            return db.ExecuteQueryDataSet("Select distinct Maphongtro from View_Ds_NguoiThueTro_HienTai order by Maphongtro", CommandType.Text, null);
        }
        public DataSet ShowNguoiThueHienTaiTheoPhong(int maPhong)
        {
            return db.ExecuteQueryDataSet("Select * from View_Ds_NguoiThueTro_HienTai where Maphongtro = @Maphongtro", CommandType.Text, new SqlParameter("@Maphongtro", maPhong));
        }
        public DataSet TimKiemTheoMaNTT(int maNTT)
        {
            return db.ExecuteQueryDataSet($"proc_ShowAllTheoMaNguoiThueTro", CommandType.StoredProcedure, new SqlParameter("@maNTT", maNTT));
        }

    }
}