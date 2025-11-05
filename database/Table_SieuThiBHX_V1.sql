create database SieuThiBHX_V1
go
use SieuThiBHX_V1
go
set dateformat dmy;
go

--------------------------CREATE TABLE------------------------
-- 1. Các bảng không phụ thuộc
CREATE TABLE LoaiHang (
	maLoaiHang VARCHAR(30)NOT NULL,
	tenLoaiHang NVARCHAR(100),
	PRIMARY KEY(maLoaiHang)
);


CREATE TABLE TaiKhoan (
	MaTaiKhoan VARCHAR(30)NOT NULL,
	TenTaiKhoan VARCHAR(100),
	MatKhau VARCHAR(100),
	Quyen INT,
	PRIMARY KEY(MaTaiKhoan)
);

CREATE TABLE NhaCungCap (
	MaNhaCungCap VARCHAR(30)NOT NULL,
	TenNhaCungCap NVARCHAR(100),
	SoDienThoai VARCHAR(30),
	DiaChi NVARCHAR(100),
	PRIMARY KEY(MaNhaCungCap)
);

CREATE TABLE KhuyenMai (
	MaKhuyenMai VARCHAR(30)NOT NULL,
	TenKhuyenMai NVARCHAR(100),
	GiaTri FLOAT,
	PRIMARY KEY(MaKhuyenMai)
);

CREATE TABLE ChiNhanh (
	MaChiNhanh VARCHAR(30)NOT NULL,
	TenChiNhanh NVARCHAR(100),
	DiaChi NVARCHAR(100),
	SoDienThoai VARCHAR(10),
	PRIMARY KEY(MaChiNhanh)
);

-- 2. Các bảng có phụ thuộc
CREATE TABLE SanPham (
	maSanPham VARCHAR(30)NOT NULL,
	tenSanPham NVARCHAR(100),
	donViTinh NVARCHAR(100),
	donGia FLOAT,
	ngaySanXuat DATETIME,
	hanSuDung DATETIME,
	anhSanPham varbinary(MAX),
	maLoaiHang VARCHAR(30),
	maNhaCungCap VARCHAR(30),
	maKhuyenMai VARCHAR(30) NULL,  
	PRIMARY KEY(maSanPham),
	FOREIGN KEY (maLoaiHang) REFERENCES LoaiHang(maLoaiHang),
	FOREIGN KEY (maNhaCungCap) REFERENCES NhaCungCap(maNhaCungCap),
	FOREIGN KEY (maKhuyenMai) REFERENCES KhuyenMai(maKhuyenMai)
);


CREATE TABLE KhachHang (
    maKhachHang VARCHAR(30) NOT NULL,
    tenKhachHang NVARCHAR(100),
    soDienThoai NVARCHAR(10),
    diaChi NVARCHAR(100),
    diem FLOAT,
	capBac NVARCHAR(50),
    PRIMARY KEY(maKhachHang)
);


CREATE TABLE ChucVu (
	maChucVu VARCHAR(30)NOT NULL,
	tenChucVu NVARCHAR(100),
	PRIMARY KEY(maChucVu)
);

CREATE TABLE NhanVien (
	maNhanVien VARCHAR(30) NOT NULL,
	tenNhanVien NVARCHAR(100),
	soDienThoai VARCHAR(10),
	diaChi NVARCHAR(100),
	LoaiNhanVien NVARCHAR(100),
	maChiNhanh VARCHAR(30),
	maChucVu VARCHAR(30),
	maTaiKhoan VARCHAR(30),
	PRIMARY KEY(maNhanVien),
	FOREIGN KEY (maChiNhanh) REFERENCES ChiNhanh(maChiNhanh),
	FOREIGN KEY (maChucVu) REFERENCES ChucVu(maChucVu),
	FOREIGN KEY (maTaiKhoan) REFERENCES TaiKhoan(maTaiKhoan)
);


CREATE TABLE CaLam (
	maCaLam VARCHAR(30)NOT NULL,
	tenCaLam NVARCHAR(100),
	gioBatDau VARCHAR(20),
	gioKetThuc VARCHAR(20),
	PRIMARY KEY(maCaLam)
);

CREATE TABLE LichLam (
	maLichLam VARCHAR(30) NOT NULL,
	ngayLam DATETIME NULL,
	maNhanVien VARCHAR(30) NULL,
	maCaLam VARCHAR(30) NULL,
	PRIMARY KEY(maLichLam),
	FOREIGN KEY (maNhanVien) REFERENCES NhanVien(maNhanVien),
	FOREIGN KEY (maCaLam) REFERENCES CaLam(maCaLam)
);

CREATE TABLE BangLuong (
	MaBangLuong VARCHAR(30)NOT NULL,
	ThangNam DATETIME NULL,
	TongGioCong FLOAT NULL,
	Luong FLOAT NULL,
	maNhanVien VARCHAR(30),
	PRIMARY KEY(MaBangLuong),
	FOREIGN KEY (maNhanVien) REFERENCES NhanVien(maNhanVien)
);

CREATE TABLE ChiTietBangLuong (
	MaChiTietBangLuong VARCHAR(30)NOT NULL,
	SoGioCongThucTe FLOAT NULL,
	NgayLam DATETIME NULL,
	maBangLuong VARCHAR(30),
	maLichLam VARCHAR(30),
	PRIMARY KEY(MaChiTietBangLuong),
	FOREIGN KEY (maBangLuong) REFERENCES BangLuong(maBangLuong),
	FOREIGN KEY (maLichLam) REFERENCES LichLam(maLichLam)
);

CREATE TABLE HoaDon (
	maHD VARCHAR(30)NOT NULL,
	ngayLapHD DATETIME,
	gioLapHD DATETIME,
	tongTien FLOAT,
	thanhTien FLOAT,
	phuongThucThanhToan NVARCHAR(50),
	maKhachHang VARCHAR(30),
	maNhanVien VARCHAR(30),
	PRIMARY KEY(maHD),
	FOREIGN KEY (maKhachHang) REFERENCES KhachHang(maKhachHang),
	FOREIGN KEY (maNhanVien) REFERENCES NhanVien(maNhanVien)
);

CREATE TABLE ChiTietHoaDon (
	id INT IDENTITY(1,1) NOT NULL,
	soLuong INT,
	maHoaDon VARCHAR(30),
	maSanPham VARCHAR(30),
	PRIMARY KEY(id),
	FOREIGN KEY (maHoaDon) REFERENCES HoaDon(maHD),
	FOREIGN KEY (maSanPham) REFERENCES SanPham(maSanPham)
);

CREATE TABLE PhieuNhap (
	MaPhieuNhap VARCHAR(30)NOT NULL,
	NgayNhap DATETIME,
	ThanhTien FLOAT,
	maNhanVien VARCHAR(30),
	PRIMARY KEY(MaPhieuNhap),
	FOREIGN KEY (maNhanVien) REFERENCES NhanVien(maNhanVien)
);
ALTER TABLE PhieuNhap
ADD MaChiNhanh VARCHAR(30);

ALTER TABLE PhieuNhap
ADD CONSTRAINT FK_PhieuNhap_ChiNhanh
FOREIGN KEY (MaChiNhanh) REFERENCES ChiNhanh(MaChiNhanh);


CREATE TABLE ChiTietPhieuNhap (
	id INT IDENTITY(1,1) NOT NULL,
	SoLuong INT,
	DonGia FLOAT,
	maPhieuNhap VARCHAR(30),
	maSanPham VARCHAR(30),
	PRIMARY KEY(id),
	FOREIGN KEY (maPhieuNhap) REFERENCES PhieuNhap(maPhieuNhap),
	FOREIGN KEY (maSanPham) REFERENCES SanPham(maSanPham)
);

CREATE TABLE KhoHang (
	id INT IDENTITY(1,1) NOT NULL,
	soLuong INT,
	maSanPham VARCHAR(30),
	idChiTietPhieuNhap INT,
	maChiNhanh VARCHAR(30),
	PRIMARY KEY(id),
	FOREIGN KEY (maChiNhanh) REFERENCES ChiNhanh(MaChiNhanh),
	FOREIGN KEY (maSanPham) REFERENCES SanPham(maSanPham),
	FOREIGN KEY (idChiTietPhieuNhap) REFERENCES ChiTietPhieuNhap(id)
);