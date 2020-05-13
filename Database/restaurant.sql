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
DROP TABLE jabatan CASCADE CONSTRAINTS;
DROP TABLE meja CASCADE CONSTRAINTS;

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

INSERT INTO pegawai VALUES('PEG001','Farhan','JAB00001','farhan@gmail.com','555000555000','PEG001555000555000','1');

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
  point NUMBER NOT NULL,
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
  harga_menu number NOT NULL,
  gambar varchar(200) NOT NULL,
  deskripsi varchar(200) NOT NULL,
  id_kategori varchar(10) NOT NULL CONSTRAINTS fk_kategori REFERENCES kategori(id_kategori),
  status NUMBER NOT NULL
);

CREATE TABLE hjual (
  id_hjual varchar(14) NOT NULL CONSTRAINTS pk_hjual PRIMARY KEY,
  tanggal_transaksi date NOT NULL,
  total NUMBER NOT NULL,
  jenis_pemesanan varchar(10) NOT NULL,
  id_pegawai varchar(10) NOT NULL CONSTRAINTS fk_pegawai REFERENCES pegawai(id_pegawai),
  id_member varchar(10) NOT NULL CONSTRAINTS fk_members REFERENCES members(id_member)
);

CREATE TABLE djual (
  id_djual varchar(20) NOT NULL CONSTRAINTS pk_djual PRIMARY KEY,
  id_menu varchar(10) NOT NULL CONSTRAINTS fk_menu REFERENCES MENU(ID_MENU),
  harga NUMBER NOT NULL,
  jumlah NUMBER NOT NULL,
  subtotal NUMBER NOT NULL,
  id_hjual varchar(14) NOT NULL CONSTRAINTS fk_hjual REFERENCES hjual(id_hjual)
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
  harga_paket number NOT NULL,
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

CREATE TABLE jabatan (
  id_jabatan varchar(10) NOT NULL,
  nama_jabatan varchar(50) NOT NULL
) ;

CREATE TABLE meja (
  id_meja number(11) NOT NULL,
  baris number(11) NOT NULL,
  kolom number(11) NOT NULL,
  status varchar(3) NOT NULL
);

INSERT INTO meja VALUES (1, 1, 1, '2'); 
INSERT INTO meja VALUES (2, 2, 1, '2'); 
INSERT INTO meja VALUES (3, 5, 1, '2'); 
INSERT INTO meja VALUES (4, 6, 1, '2'); 
INSERT INTO meja VALUES (5, 1, 4, '1'); 
INSERT INTO meja VALUES (6, 2, 4, '1'); 
INSERT INTO meja VALUES (7, 5, 4, '1'); 
INSERT INTO meja VALUES (8, 6, 4, '1'); 
INSERT INTO meja VALUES (9, 1, 2, '2'); 
INSERT INTO meja VALUES (10, 3, 2, '2');
INSERT INTO meja VALUES (11, 4, 2, '2');
INSERT INTO meja VALUES (12, 6, 2, '2');
INSERT INTO meja VALUES (13, 1, 3, '2');
INSERT INTO meja VALUES (14, 3, 3, '2');
INSERT INTO meja VALUES (15, 4, 3, '2');
INSERT INTO meja VALUES (16, 6, 3, '1');

insert into members values('M000000001','Fendy Sugiarto','FendyGanteng','12345678','fendygantengsekaleh@gmail.com','Jalan Ngagel Madya 70-77',1234567890,'K0001','D0001',123123,'1');
insert into members values('M000000002','San Widodo','SanKing','12345678','sanking@gmail.com','Jalan Ngagel Madya 70-77',1234567890123,'K0002','D0001',423421,'1');
insert into members values('M000000003','Yongki Tanu','YoTa','12345678','yongkikun@gmail.com','Jalan Ngagel Madya 70-77',1234567890123,'K0001','D0001',523123,'1');
insert into members values('M000000000','DEFAULT','DEFAULT','DEFAULT','DEFAULT','DEFAULT',555000555000,'DEFAULT','DEFAULT',555555,'1');

Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT001','Nasi','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT002','Cap Cay','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT003','Bakmi','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT004','Mihun','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT005','Signature Menu','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT006','Kopi','minuman','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT007','Minuman','minuman','1');

INSERT INTO daerah VALUES ('D0001', 'Jawa Timur');
INSERT INTO daerah VALUES ('D0002', 'Jawa Barat');
INSERT INTO daerah VALUES ('D0003', 'Jawa Tengah');

INSERT INTO kota VALUES('K0001', 'Surabaya', 'D0001');
INSERT INTO kota VALUES('K0002', 'Malang', 'D0001');
INSERT INTO kota VALUES('K0003', 'Probolinggo', 'D0001');
INSERT INTO kota VALUES('K0004', 'Batu', 'D0001');
INSERT INTO kota VALUES('K0005', 'Malang', 'D0001');
INSERT INTO kota VALUES('K0006', 'Bandung', 'D0002');
INSERT INTO kota VALUES('K0007', 'Bekasi', 'D0002');
INSERT INTO kota VALUES('K0008', 'Cirebon', 'D0002');
INSERT INTO kota VALUES('K0009', 'Indramayu', 'D0002');
INSERT INTO kota VALUES('K0010', 'Depok', 'D0002');
INSERT INTO kota VALUES('K0011', 'Magelang', 'D0003');
INSERT INTO kota VALUES('K0012', 'Pekalongan', 'D0003');
INSERT INTO kota VALUES('K0013', 'Salatiga', 'D0003');
INSERT INTO kota VALUES('K0014', 'Semarang', 'D0003');
INSERT INTO kota VALUES('K0015', 'Surakarta', 'D0003');

INSERT INTO jabatan VALUES ('JAB00001', 'Waiter');
INSERT INTO jabatan VALUES ('JAB00002', 'Host');

insert into promo values('PRO001','Promo Ramadhan1',20000,to_date('2012-06-05', 'YYYY-MM-DD'),to_date('2012-06-05', 'YYYY-MM-DD'),'a',1);
insert into promo values('PRO002','Promo Ramadhan2',20000,to_date('2012-06-05', 'YYYY-MM-DD'),to_date('2012-06-05', 'YYYY-MM-DD'),'a',1);
insert into promo values('PRO003','Promo Ramadhan3',20000,to_date('2012-06-05', 'YYYY-MM-DD'),to_date('2012-06-05', 'YYYY-MM-DD'),'a',1);
insert into promo values('PRO004','Promo Ramadhan4',20000,to_date('2012-06-05', 'YYYY-MM-DD'),to_date('2012-06-05', 'YYYY-MM-DD'),'a',1);
insert into promo values('PRO005','Promo Ramadhan5',20000,to_date('2012-06-05', 'YYYY-MM-DD'),to_date('2012-06-05', 'YYYY-MM-DD'),'a',1);

INSERT INTO paket  VALUES('PK001', 'Steak', 50000, 'Image/beef-steak.jpg', 'KAT006', 1);
INSERT INTO paket VALUES('PK002', 'Bubur', 10000, 'Image/pkt-b.jpg', 'KAT004', 1);
INSERT INTO paket VALUES('PK003', 'Siang', 20000, 'Image/nasi-ayam-hemat.jpg', 'KAT001', 1);
INSERT INTO paket VALUES('PK004', 'Agep Murmer', 15000, 'Image/aybak.jpg', 'KAT005', 1);
INSERT INTO paket VALUES('PK005', 'Namikun', 25000, 'Image/pkt-nasi-kuning-ayam-goreng-suwir.jpg', 'KAT003', 1);
INSERT INTO paket VALUES('PK006', 'Mie-Aygep', 22000, 'Image/mie.jpg', 'KAT005', 1);
INSERT INTO paket VALUES('PK007', 'Ayam Kremes', 25000, 'Image/nasi-kotak-ayam-kremes.jpg', 'KAT005', 1);
INSERT INTO paket VALUES('PK008', 'Nasgor', 12000, 'Image/nasgor2.jpg', 'KAT002', 1);

Insert into DAERAH (KODE_DAERAH,NAMA_DAERAH) values ('D0001','Jawa Timur');
Insert into DAERAH (KODE_DAERAH,NAMA_DAERAH) values ('D0002','Jawa Barat');
Insert into DAERAH (KODE_DAERAH,NAMA_DAERAH) values ('D0003','Jawa Tengah');

Insert into DJUAL (ID_DJUAL,ID_MENU,HARGA,JUMLAH,SUBTOTAL,ID_HJUAL) values ('HJ20200512003_01','MEN002','20000','2','40000','HJ20200512003');
Insert into DJUAL (ID_DJUAL,ID_MENU,HARGA,JUMLAH,SUBTOTAL,ID_HJUAL) values ('HJ20200512001_01','MEN002','20000','3','60000','HJ20200512001');
Insert into DJUAL (ID_DJUAL,ID_MENU,HARGA,JUMLAH,SUBTOTAL,ID_HJUAL) values ('HJ20200512001_02','MEN001','10000','2','20000','HJ20200512001');
Insert into DJUAL (ID_DJUAL,ID_MENU,HARGA,JUMLAH,SUBTOTAL,ID_HJUAL) values ('HJ20200512002_01','MEN001','10000','2','20000','HJ20200512002');

Insert into HJUAL (ID_HJUAL,TANGGAL_TRANSAKSI,TOTAL,JENIS_PEMESANAN,ID_PEGAWAI,ID_MEMBER) values ('HJ20200512003',to_date('12-05-2020','DD-MM-RRRR'),'40000','Take Away','PEG001','M000000000');
Insert into HJUAL (ID_HJUAL,TANGGAL_TRANSAKSI,TOTAL,JENIS_PEMESANAN,ID_PEGAWAI,ID_MEMBER) values ('HJ20200512001',to_date('12-05-2020','DD-MM-RRRR'),'80000','Take Away','PEG001','M000000000');
Insert into HJUAL (ID_HJUAL,TANGGAL_TRANSAKSI,TOTAL,JENIS_PEMESANAN,ID_PEGAWAI,ID_MEMBER) values ('HJ20200512002',to_date('12-05-2020','DD-MM-RRRR'),'20000','Dine In','PEG001','M000000000');

Insert into JABATAN (ID_JABATAN,NAMA_JABATAN) values ('JAB00001','Waiter');
Insert into JABATAN (ID_JABATAN,NAMA_JABATAN) values ('JAB00002','Host');

Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT001','Nasi','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT002','Cap Cay','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT003','Bakmi','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT004','Mihun','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT005','Signature Menu','makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT006','Kopi','minuman','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT007','Minuman','minuman','1');

Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0001','Surabaya','D0001');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0002','Malang','D0001');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0003','Probolinggo','D0001');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0004','Batu','D0001');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0005','Malang','D0001');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0006','Bandung','D0002');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0007','Bekasi','D0002');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0008','Cirebon','D0002');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0009','Indramayu','D0002');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0010','Depok','D0002');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0011','Magelang','D0003');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0012','Pekalongan','D0003');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0013','Salatiga','D0003');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0014','Semarang','D0003');
Insert into KOTA (KODE_KOTA,NAMA_KOTA,KODE_DAERAH) values ('K0015','Surakarta','D0003');

Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('1','1','1','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('2','2','1','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('3','5','1','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('4','6','1','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('5','1','4','1');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('6','2','4','1');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('7','5','4','1');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('8','6','4','1');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('9','1','2','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('10','3','2','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('11','4','2','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('12','6','2','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('13','1','3','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('14','3','3','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('15','4','3','2');
Insert into MEJA (ID_MEJA,BARIS,KOLOM,STATUS) values ('16','6','3','1');

Insert into MEMBERS (ID_MEMBER,FULLNAME,USERNAME,PASSWORD,EMAIL,ALAMAT,NO_HP,KOTA,KECEMATAN,KODE_POS,STATUS) values ('M000000001','Fendy Sugiarto','FendyGanteng','12345678','fendygantengsekaleh@gmail.com','Jalan Ngagel Madya 70-77','1234567890','K0001','D0001','123123','1');
Insert into MEMBERS (ID_MEMBER,FULLNAME,USERNAME,PASSWORD,EMAIL,ALAMAT,NO_HP,KOTA,KECEMATAN,KODE_POS,STATUS) values ('M000000002','San Widodo','SanKing','12345678','sanking@gmail.com','Jalan Ngagel Madya 70-77','1234567890123','K0002','D0001','423421','1');
Insert into MEMBERS (ID_MEMBER,FULLNAME,USERNAME,PASSWORD,EMAIL,ALAMAT,NO_HP,KOTA,KECEMATAN,KODE_POS,STATUS) values ('M000000003','Yongki Tanu','YoTa','12345678','yongkikun@gmail.com','Jalan Ngagel Madya 70-77','1234567890123','K0001','D0001','523123','1');
Insert into MEMBERS (ID_MEMBER,FULLNAME,USERNAME,PASSWORD,EMAIL,ALAMAT,NO_HP,KOTA,KECEMATAN,KODE_POS,STATUS) values ('M000000000','DEFAULT','DEFAULT','DEFAULT','DEFAULT','DEFAULT','555000555000','DEFAULT','DEFAULT','555555','1');

Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN001','Nasi Goreng','10000','temp gambar','nasi goreng enak','KAT001','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN002','Sushi Goreng','20000','temp gambar','sushi yang dimasak','KAT005','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN007','Strawberry Sensation','25000','temp gambar','milk, sprite, lime, strawberry syrup','KAT007','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN008','Orange Blush','25000','temp gambar','milk, sprite, lime, orange syrup','KAT007','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN009','Container Blue','25000','temp gambar','pepsi blue strawberry syrup','KAT007','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN010','Bakmie Goreng Seafood','35454','temp gambar','seafood fried noodle','KAT003','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN011','Bakmie Goreng / Kuah','29000','temp gambar','fried noodle','KAT003','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN012','Bakmie Goreng Casiew','35000','temp gambar','Casiew Fried Noodle','KAT003','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN013','Bakmie Goreng Bebek','35000','temp gambar','Roasted Duck Fried Noodle','KAT003','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN003','Espresso','5000','temp gambar','espresso delicate foam 30 ml','KAT006','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN004','Marocchino','6000','temp gambar','cocoa powder milk espresso','KAT006','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN005','Americano','5000','temp gambar','espresso water 150ml','KAT006','1');
Insert into MENU (ID_MENU,NAMA_MENU,HARGA_MENU,GAMBAR,DESKRIPSI,ID_KATEGORI,STATUS) values ('MEN006','Antoccino','0','temp gambar','espresso milk 150ml','KAT006','1');

Insert into PEGAWAI (ID_PEGAWAI,NAMA,JABATAN,EMAIL,NOHP,PASSWORD,STATUS) values ('PEG001','Farhan','JAB00001','farhan@gmail.com','555000555000','PEG001555000555000','1');

Insert into PROMO (ID_PROMO,NAMA_PROMO,HARGA_PROMO,PERIODE_AWAL,PERIODE_AKHIR,GAMBAR_PROMO,STATUS_PROMO) values ('PRO001','Promo Ramadhan1','20000',to_date('05-06-2012','DD-MM-RRRR'),to_date('05-06-2012','DD-MM-RRRR'),'a','1');
Insert into PROMO (ID_PROMO,NAMA_PROMO,HARGA_PROMO,PERIODE_AWAL,PERIODE_AKHIR,GAMBAR_PROMO,STATUS_PROMO) values ('PRO002','Promo Ramadhan2','20000',to_date('05-06-2012','DD-MM-RRRR'),to_date('05-06-2012','DD-MM-RRRR'),'a','1');
Insert into PROMO (ID_PROMO,NAMA_PROMO,HARGA_PROMO,PERIODE_AWAL,PERIODE_AKHIR,GAMBAR_PROMO,STATUS_PROMO) values ('PRO003','Promo Ramadhan3','20000',to_date('05-06-2012','DD-MM-RRRR'),to_date('05-06-2012','DD-MM-RRRR'),'a','1');
Insert into PROMO (ID_PROMO,NAMA_PROMO,HARGA_PROMO,PERIODE_AWAL,PERIODE_AKHIR,GAMBAR_PROMO,STATUS_PROMO) values ('PRO004','Promo Ramadhan4','20000',to_date('05-06-2012','DD-MM-RRRR'),to_date('05-06-2012','DD-MM-RRRR'),'a','1');
Insert into PROMO (ID_PROMO,NAMA_PROMO,HARGA_PROMO,PERIODE_AWAL,PERIODE_AKHIR,GAMBAR_PROMO,STATUS_PROMO) values ('PRO005','Promo Ramadhan5','20000',to_date('05-06-2012','DD-MM-RRRR'),to_date('05-06-2012','DD-MM-RRRR'),'a','1');


create or replace function autogen_IDpegawai return varchar2
is
    tampung varchar2(10);
begin
    select 'PEG'||lpad(nvl(max(substr(id_pegawai,-3,3)),'0')+1,3,0) into tampung from pegawai;
    return tampung;
end;
/

create or replace procedure proc_Insert_pegawai(kode in varchar2,nama in varchar2,jabatan in varchar2,email in varchar2,nohp in varchar2,password in varchar2)
is
begin
insert into pegawai values(kode,nama,jabatan,email,nohp,password,'1');
end;
/

CREATE OR REPLACE TRIGGER checkMenu
BEFORE INSERT ON menu 
FOR EACH ROW
DECLARE
BEGIN
    :NEW.nama_menu := INITCAP(:NEW.nama_menu);
END;
/

commit;