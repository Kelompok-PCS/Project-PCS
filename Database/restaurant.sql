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
DROP TABLE paket_menu CASCADE CONSTRAINTS;


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
  email varchar(100) NOT NULL,
  alamat varchar(100) NOT NULL,
  no_hp varchar(13) NOT NULL,
  kota varchar(50) NOT NULL,
  kecematan varchar(50) NOT NULL,
  kode_pos NUMBER NOT NULL,
  point NUMBER NOT NULL,
  saldo NUMBER NOT NULL,
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
  id_member varchar(10) NOT NULL CONSTRAINTS fk_members REFERENCES members(id_member),
  keterangan varchar(255) NOT NULL
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
  gambar_paket varchar(50) NOT NULL,
  id_kategori varchar(10) NOT NULL CONSTRAINTS FK_KATEGORI1 REFERENCES KATEGORI(ID_KATEGORI),
  status NUMBER NOT NULL
);

CREATE TABLE paket_menu (
  id_paket varchar(10) NOT NULL,
  id_menu varchar(10) NOT NULL
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



insert into members values('M000000001','Fendy Sugiarto','FendyGanteng','fendygantengsekaleh@gmail.com','Jalan Ngagel Madya 70-77','1234567890','K0001','D0001',123123,0,0,'1');
insert into members values('M000000002','San Widodo','SanKing','sanking@gmail.com','Jalan Ngagel Madya 70-77','1234567890123','K0002','D0001',423421,0,0,'1');
insert into members values('M000000003','Yongki Tanu','YoTa','yongkikun@gmail.com','Jalan Ngagel Madya 70-77','1234567890123','K0001','D0001',523123,0,0,'1');
insert into members values('M000000000','DEFAULT','DEFAULT','DEFAULT','DEFAULT','555000555000','DEFAULT','DEFAULT',555555,0,0,'1');

Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT001','Nasi','Makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT002','Cap Cay','Makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT003','Bakmi','Makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT004','Mihun','Makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT005','Signature Menu','Makanan','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT006','Kopi','Minuman','1');
Insert into KATEGORI (ID_KATEGORI,NAMA_KATEGORI,JENIS_KATEGORI,STATUS_KATEGORI) values ('KAT007','Minuman','Minuman','1');

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


INSERT INTO menu VALUES('MEN001', 'Nasi Goreng Jawa', 20000, 'Image/Nasgor.jpg', 'Dengan Bumbu Jawa', 'KAT002', 1);
INSERT INTO menu VALUES('MEN002', 'Ayam goreng', 5000, 'Image/Aygor.jpg', 'Dengan tambahan rempah-rempah', 'KAT001', 1);
INSERT INTO menu VALUES('MEN003', 'Iga Bakar', 20000, 'Image/Igbak.jpg', 'Iga daging sapi yang berkualitas', 'KAT006', 1);
INSERT INTO menu VALUES('MEN004', 'Cumi Goreng', 30000, 'Image/Cumgor.jpg', 'Bergizi, nikmat, dan krispi', 'KAT001', 1);
INSERT INTO menu VALUES('MEN005', 'Mie Goreng', 15000, 'Image/Migor.jpg', 'Dengan kelezatan nikmat', 'KAT003', 1);
INSERT INTO menu VALUES('MEN006', 'Ayam Geprek', 12000, 'Image/Aygep.jpg', 'Geprek, dengan tingkat kepedasan yang menggugah lidah', 'KAT005', 1);
INSERT INTO menu VALUES('MEN007', 'Es Teh  Manis', 3000, 'Image/Steh.jpg', 'Dingin', 'KAT007', 1);
INSERT INTO menu VALUES('MEN008', 'Es Lemon Tea', 5000, 'Image/Lteh.jpg', 'Jeruk Lemon', 'KAT007', 1);
INSERT INTO menu VALUES('MEN009', 'Es Mega Mendung', 8000, 'Image/Megmen.jpg', 'Soda', 'KAT005', 1);
INSERT INTO menu VALUES('MEN010', 'Kopi Luwak', 8000, 'Image/kopi.jpg', 'Luwak asli', 'KAT007', 1);
INSERT INTO menu VALUES('MEN011', 'Jus Alpukat', 20000, 'Image/Jusalpukat.jpg', 'Alpukat terpercaya', 'KAT004', 1);
INSERT INTO menu VALUES('MEN012', 'Bubur Ayam', 15000, 'Image/Bubur.jpg', 'Lembut', 'KAT004', 1);

INSERT INTO paket VALUES('PK001', 'Steak', 50000, 'Image/beef-steak.jpg', 'KAT006', 1);
INSERT INTO paket VALUES('PK002', 'Bubur', 10000, 'Image/pkt-b.jpg', 'KAT004', 1);
INSERT INTO paket VALUES('PK003', 'Siang', 20000, 'Image/nasi-ayam-hemat.jpg', 'KAT001', 1);
INSERT INTO paket VALUES('PK004', 'Agep Murmer', 15000, 'Image/aybak.jpg', 'KAT005', 1);
INSERT INTO paket VALUES('PK005', 'Namikun', 25000, 'Image/pkt-nasi-kuning-ayam-goreng-suwir.jpg', 'KAT003', 1);
INSERT INTO paket VALUES('PK006', 'Mie-Aygep', 22000, 'Image/mie.jpg', 'KAT005', 1);
INSERT INTO paket VALUES('PK007', 'Ayam Kremes', 25000, 'Image/nasi-kotak-ayam-kremes.jpg', 'KAT005', 1);
INSERT INTO paket VALUES('PK008', 'Nasgor', 12000, 'Image/nasgor2.jpg', 'KAT002', 1);

INSERT INTO PAKET_MENU VALUES ('PK001', 'MEN003');
INSERT INTO PAKET_MENU VALUES ('PK001', 'MEN008');
INSERT INTO PAKET_MENU VALUES ('PK002', 'MEN011');
INSERT INTO PAKET_MENU VALUES ('PK002', 'MEN012');
INSERT INTO PAKET_MENU VALUES ('PK003', 'MEN002');
INSERT INTO PAKET_MENU VALUES ('PK003', 'MEN007');
INSERT INTO PAKET_MENU VALUES ('PK004', 'MEN006');
INSERT INTO PAKET_MENU VALUES ('PK004', 'MEN008');
INSERT INTO PAKET_MENU VALUES ('PK005', 'MEN005');
INSERT INTO PAKET_MENU VALUES ('PK005', 'MEN009');
INSERT INTO PAKET_MENU VALUES ('PK006', 'MEN005');
INSERT INTO PAKET_MENU VALUES ('PK006', 'MEN006');
INSERT INTO PAKET_MENU VALUES ('PK007', 'MEN002');
INSERT INTO PAKET_MENU VALUES ('PK007', 'MEN008');
INSERT INTO PAKET_MENU VALUES ('PK008', 'MEN001');
INSERT INTO PAKET_MENU VALUES ('PK008', 'MEN007');
INSERT INTO PAKET_MENU VALUES ('PK009', 'MEN002');
INSERT INTO PAKET_MENU VALUES ('PK009', 'MEN003');
INSERT INTO PAKET_MENU VALUES ('PK009', 'MEN005');
INSERT INTO PAKET_MENU VALUES ('PK009', 'MEN011');
INSERT INTO PAKET_MENU VALUES ('PK009', 'MEN012');
INSERT INTO PAKET_MENU VALUES ('PK010', 'MEN013');
INSERT INTO PAKET_MENU VALUES ('PK010', 'MEN014');
INSERT INTO PAKET_MENU VALUES ('PK011', 'MEN009');
INSERT INTO PAKET_MENU VALUES ('PK011', 'MEN012');
INSERT INTO PAKET_MENU VALUES ('PK012', 'MEN012');
INSERT INTO PAKET_MENU VALUES ('PK012', 'MEN014');
INSERT INTO PAKET_MENU VALUES ('PK013', 'MEN010');
INSERT INTO PAKET_MENU VALUES ('PK013', 'MEN014');

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