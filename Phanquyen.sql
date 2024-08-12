--Tạo login admin
use master
go
create login ChuTro
with password = '123456',
DEFAULT_DATABASE = QuanLyPhongTro,
check_expiration=off,
check_policy=off;
go
use QuanLyPhongTro
go 
CREATE USER ChuTro FOR LOGIN ChuTro;
go
ALTER SERVER ROLE sysadmin ADD MEMBER ChuTro
go

--Tạo role cho người thuê
Use QuanLyPhongTro
go
CREATE ROLE NguoiThue
go
--------Gán các quyền trên các bảng cho role NguoiThue---------
GRANT SELECT ON View_Ds_DichVuDangTrangBi TO NguoiThue
GRANT SELECT ON View_Ds_NguoiThueTro_HienTai  TO NguoiThue
GRANT SELECT ON View_XemNoiThatDangDuocTrangBiChoTro TO NguoiThue
GRANT SELECT ON View_ChiTietPhongTro TO NguoiThue
grant select on Account to NguoiThue
-- Gán quyền thực thi trên các procedure, function cho role Staff
grant execute on dbo.proc_ShowAllDienNuocTheoMaPhongTro to NguoiThue
grant execute on dbo.proc_ShowAllHoaDonTheoMaPhongTro to NguoiThue
grant execute on dbo.proc_ShowDienNuoc to NguoiThue
grant execute on proc_ShowHoaDon to NguoiThue
go
--Tạo user
use master
go
create login room1
with password = '123456',
DEFAULT_DATABASE = QuanLyPhongTro,
check_expiration=off,
check_policy=off;
go 
use QuanLyPhongTro
create user room1 for login room1
go 
alter role NguoiThue add member room1
