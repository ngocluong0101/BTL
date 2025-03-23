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

--drop table KHACHHANG;
--select * from KHACHHANG;
--update QuanLyThoiGian set Sodu = Sodu + 15000 where Taikhoan = 'aaaa123';



--select * from LICHSU;

--drop table LICHSU;


--select * from TK_ThuNhap;


--select * from QuanLyThoiGian;
--drop table QuanLyThoiGian;
--insert into TK_ThuNhap (Mota, Sotien) values ('haha', 15000);
--drop table TK_ThuNhap;

--select * from KHACHHANG;

--SELECT * FROM TK_ThuNhap WHERE Thoigian BETWEEN '2025/03/10' AND '2025/03/18';

--insert into QuanLyThoiGian values 
--	(N'Máy 13', 'bbbb123', N'Gaming', '03:47', 30000),
--	(N'Máy 09', 'aaaa123', N'Chuyên nghiệp', '02:20', 16000),
--	(N'Máy 08', 'dddd123', N'Gaming', '02:34', 20000),
--	(N'Máy 14', 'dinhan123', N'Thi đấu', '01:50', 0),
--	(N'Máy 06', 'eeee123', N'Tiêu chuẩn', '01:13', 15000),
--	(N'Máy 12', 'ffff123', N'Tiêu chuẩn', '03:34', 0),
--	(N'Máy 07', 'luong123', N'Chuyên nghiệp', '02:15', 0),
--	(N'Máy 10', 'malinh123', N'Gaming', '04:47', 0),
--	(N'Máy 04', 'player1', N'Thi đấu', '02:10', 0),
--	(N'Máy 11', 'player2', N'Gaming', '01:13', 0),
--	(N'Máy 17', 'player3', N'Chuyên nghiệp', '03:01', 0),
--	(N'Máy 15', 'player4', N'Thi đấu', '01:16', 0),
--	(N'Máy 02', 'player5', N'Gaming', '02:14', 0),
--	(N'Máy 05', 'player6', N'Tiêu chuẩn', '01:13', 0);
