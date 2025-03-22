# BTL
# Nhớ đổi lại mã kết nối sql !!




create database BTL_QuanLyQuanNet;

create table KHACHHANG (
	Taikhoan char(20) primary key,
	Matkhau char(20),
	Sodu int default 0
);

create table LICHSU (
	Thoigian datetime default getdate(),
	Taikhoan char(20),
	Mota nchar(30)
);

create table TK_ThuNhap(
	Thoigian date DEFAULT getdate(),
	Mota nchar(30),
	Sotien int
);

create table QuanLyThoiGian (
	Somay nchar(20) primary key,
	Taikhoan char(20),
	Loai nchar(20),
	Thoigianconlai nchar(20),
	Sodu int default 0,
	foreign key (Taikhoan) references KHACHHANG(Taikhoan),
);
