DROP TABLE pegawai CASCADE CONSTRAINTS;
DROP TABLE members CASCADE CONSTRAINTS;
DROP TABLE kategori CASCADE CONSTRAINTS;
DROP TABLE menu CASCADE CONSTRAINTS;
DROP TABLE hjual CASCADE CONSTRAINTS;
DROP TABLE djual CASCADE CONSTRAINTS;
DROP TABLE promo CASCADE CONSTRAINTS;
DROP TABLE paket CASCADE CONSTRAINTS;
DROP TABLE promo_menu CASCADE CONSTRAINTS;
DROP TABLE daerah CASCADE CONSTRAINTS;
DROP TABLE kota CASCADE CONSTRAINTS;
PURGE RECYCLEBIN;

CREATE TABLE pegawai (
  id_pegawai varchar(10) CONSTRAINTS pk_pegawai PRIMARY KEY,
  nama varchar(25) NOT NULL,
  jabatan varchar(25) NOT NULL,
  email varchar(100) NOT NULL,
  nohp varchar(100) NOT NULL,
  password varchar(100) NOT NULL,
  status NUMBER NOT NULL
);

CREATE TABLE members (
  id_member varchar(10) NOT NULL CONSTRAINTS pk_member PRIMARY KEY,
  fullname varchar(50) NOT NULL,
  username varchar(50) NOT NULL,
  password varchar(50) NOT NULL,
  email varchar(100) NOT NULL,
  alamat varchar(100) NOT NULL,
  no_hp NUMBER NOT NULL,
  kota varchar(50) NOT NULL,
  kecematan varchar(50) NOT NULL,
  kode_pos NUMBER NOT NULL,
  status varchar(1) NOT NULL
);

CREATE TABLE kategori (
  id_kategori varchar(10) NOT NULL CONSTRAINTS pk_kategori PRIMARY KEY,
  nama_kategori varchar(50) NOT NULL,
  jenis_kategori varchar(50) NOT NULL,
  status_kategori NUMBER NOT NULL
);

CREATE TABLE menu (
  id_menu varchar(10) NOT NULL CONSTRAINTS pk_menu PRIMARY KEY,
  nama_menu varchar(50) NOT NULL,
  harga_menu varchar(10) NOT NULL,
  gambar varchar(200) NOT NULL,
  deskripsi varchar(200) NOT NULL,
  id_kategori varchar(10) NOT NULL CONSTRAINTS fk_kategori REFERENCES kategori(id_kategori),
  status NUMBER NOT NULL
);

CREATE TABLE hjual (
  id_hjual varchar(10) NOT NULL CONSTRAINTS pk_hjual PRIMARY KEY,
  tanggal_transaksi date NOT NULL,
  total NUMBER NOT NULL,
  jenis_pemesanan varchar(10) NOT NULL,
  id_pegawai varchar(10) NOT NULL CONSTRAINTS fk_pegawai REFERENCES pegawai(id_pegawai),
  id_member varchar(10) NOT NULL CONSTRAINTS fk_members REFERENCES members(id_member)
);

CREATE TABLE djual (
  id_djual varchar(10) NOT NULL CONSTRAINTS pk_djual PRIMARY KEY,
  id_menu varchar(10) NOT NULL CONSTRAINTS fk_menu REFERENCES MENU(ID_MENU),
  harga NUMBER NOT NULL,
  jumlah NUMBER NOT NULL,
  subtotal NUMBER NOT NULL,
  id_hjual varchar(10) NOT NULL CONSTRAINTS fk_hjual REFERENCES hjual(id_hjual)
);

CREATE TABLE promo (
  id_promo varchar(10) NOT NULL CONSTRAINTS pk_promo PRIMARY KEY,
  nama_promo varchar(50) NOT NULL,
  harga_promo NUMBER NOT NULL,
  periode_awal date NOT NULL,
  periode_akhir date NOT NULL,
  gambar_promo varchar(200) NOT NULL,
  status_promo NUMBER NOT NULL
);

CREATE TABLE paket (
  id_paket varchar(10) NOT NULL CONSTRAINTS PK_paket PRIMARY KEY,
  nama_paket varchar(50) NOT NULL,
  harga_paket NUMBER NOT NULL,
  id_kategori varchar(10) NOT NULL CONSTRAINTS FK_KATEGORI1 REFERENCES KATEGORI(ID_KATEGORI),
  id_promo varchar(10) NOT NULL CONSTRAINTS fk_PROMO REFERENCES promo(id_promo),
  status NUMBER NOT NULL
);

CREATE TABLE promo_menu (
  id_menu varchar(10) NOT NULL CONSTRAINTS fk_menu1 REFERENCES MENU(ID_MENU),
  id_promo varchar(10) NOT NULL CONSTRAINTS fk_PROMO1 REFERENCES promo(id_promo)
);

CREATE TABLE daerah (
  kode_daerah varchar(100) NOT NULL CONSTRAINTS pk_daerah PRIMARY KEY,
  nama_daerah varchar(100) NOT NULL
);

CREATE TABLE kota (
  kode_kota varchar(100) NOT NULL CONSTRAINTS pk_kota PRIMARY KEY,
  nama_kota varchar(100) NOT NULL,
  kode_daerah varchar(100) NOT NULL CONSTRAINTS fk_daerah REFERENCES daerah(kode_daerah)
);

COMMIT;
insert into members values('M000000001','Fendy Sugiarto','FendyGanteng','12345678','fendygantengsekaleh@gmail.com','Jalan Ngagel Madya 70-77',1234567890,'Surabaya','Ngagel',123123,'1');
insert into members values('M000000002','San Widodo','SanKing','12345678','sanking@gmail.com','Jalan Ngagel Madya 70-77',1234567890123,'Sidoarjo','Ngagel',423421,'1');
insert into members values('M000000003','Yongki Tanu','YoTa','12345678','yongkikun@gmail.com','Jalan Ngagel Madya 70-77',1234567890123,'Surabaya','Ngagel',523123,'1');

Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT001','Nasi','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT002','Cap Cay','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT003','Bakmi','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT004','Mihun','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT005','Signature Menu','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT006','Kopi','minuman','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT007','Minuman','minuman','1');

Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN001','Nasi Capjai','12000','temp gambar','uenakkk','KAT002','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN002','Susi Goreng','19000','temp gambar','Goreng terusss','KAT005','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN003','test','10000','temp gambar','asdasdad','KAT004','0');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN004','test kedua','30000','temp gambar','asdadasdad','KAT006','0');
commit;