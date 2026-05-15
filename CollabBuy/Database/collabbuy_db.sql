-- ========================================================================================
-- FILE INISIALISASI DATABASE COLLABBUY
-- RDBMS: PostgreSQL
-- Deskripsi: File ini berisi pembuatan tabel, data dummy, trigger, procedure, dan view.
-- ========================================================================================
-- ========================================================================================
-- FILE INISIALISASI DATABASE COLLABBUY (VERSI MASTER PRODUK & MULTI-ITEM PO)
-- RDBMS: PostgreSQL
-- ========================================================================================

DROP SCHEMA public CASCADE;
CREATE SCHEMA public;

-- =========================================================
-- BAGIAN 1: DATA DEFINITION LANGUAGE (DDL) - PEMBUATAN TABEL
-- =========================================================

CREATE TABLE users (
    id_user SERIAL PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    nomor_telepon VARCHAR(20),
    email VARCHAR(100) UNIQUE,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    peran VARCHAR(20) DEFAULT 'User', 
    is_diblokir BOOLEAN DEFAULT FALSE
);

CREATE TABLE verifications (
    id_verifikasi SERIAL PRIMARY KEY,
    id_user INTEGER REFERENCES users(id_user) ON DELETE CASCADE,
    nim VARCHAR(20) UNIQUE NOT NULL,
    nama_toko VARCHAR(100) NOT NULL,
    bukti_ktm VARCHAR(255) NOT NULL,
    tahun_masuk INTEGER NOT NULL,
    is_verifikasi BOOLEAN DEFAULT FALSE
);

CREATE TABLE categories (
    id_kategori SERIAL PRIMARY KEY,
    nama_kategori VARCHAR(100) NOT NULL
);

CREATE TABLE preorders (
    id_po SERIAL PRIMARY KEY,
    id_penjual INTEGER REFERENCES users(id_user) ON DELETE CASCADE,
    judul_po VARCHAR(150) NOT NULL,
    jenis_po VARCHAR(50) NOT NULL, 
    info_rekening VARCHAR(255) NOT NULL,
    batas_waktu TIMESTAMP NOT NULL,
    is_aktif BOOLEAN DEFAULT TRUE
);

-- TABEL PRODUCTS SEKARANG PUNYA ID_PENJUAL (GUDANG) DAN ID_PO (LAPAK)
CREATE TABLE products (
    id_produk SERIAL PRIMARY KEY,
    id_penjual INTEGER REFERENCES users(id_user) ON DELETE CASCADE,
    id_po INTEGER REFERENCES preorders(id_po) ON DELETE SET NULL, -- Bisa NULL jika sedang di gudang
    id_kategori INTEGER REFERENCES categories(id_kategori) ON DELETE SET NULL,
    nama_produk VARCHAR(150) NOT NULL,
    deskripsi TEXT,
    harga_dasar INTEGER NOT NULL,
    harga_diskon INTEGER,
    target_kuota INTEGER,
    min_order INTEGER DEFAULT 1,
    foto_produk VARCHAR(255)
);

CREATE TABLE transactions (
    id_transaksi SERIAL PRIMARY KEY,
    id_koordinator INTEGER REFERENCES users(id_user) ON DELETE CASCADE,
    tanggal_transaksi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    total_bayar_grup INTEGER NOT NULL,
    status_pesanan VARCHAR(50) DEFAULT 'Menunggu', 
    bukti_bayar VARCHAR(255),
    is_valid BOOLEAN DEFAULT FALSE
);

CREATE TABLE transaction_details (
    id_detail SERIAL PRIMARY KEY,
    id_transaksi INTEGER REFERENCES transactions(id_transaksi) ON DELETE CASCADE,
    id_produk INTEGER REFERENCES products(id_produk) ON DELETE RESTRICT,
    nama_penitip VARCHAR(100) NOT NULL,
    jumlah_pesanan INTEGER NOT NULL,
    catatan VARCHAR(255),
    selisih_refund INTEGER DEFAULT 0
);

CREATE TABLE complaints (
    id_aduan SERIAL PRIMARY KEY,
    id_user INTEGER REFERENCES users(id_user) ON DELETE CASCADE,
    subjek VARCHAR(150) NOT NULL,
    deskripsi TEXT NOT NULL,
    tanggal TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_selesai BOOLEAN DEFAULT FALSE,
    balasan TEXT
);

CREATE TABLE reviews (
    id_ulasan SERIAL PRIMARY KEY,
    id_produk INTEGER REFERENCES products(id_produk) ON DELETE CASCADE,
    id_user INTEGER REFERENCES users(id_user) ON DELETE CASCADE,
    rating INTEGER CHECK (rating >= 1 AND rating <= 5),
    komentar TEXT,
    tanggal_ulasan TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    balasan_penjual TEXT
);


-- =========================================================
-- BAGIAN 2: DATA MANIPULATION LANGUAGE (DML) - DATA DUMMY
-- =========================================================

INSERT INTO users (nama, nomor_telepon, email, username, password, peran, is_diblokir) VALUES
('Rangga Saputra', '081234567890', 'rangga@unej.ac.id', 'rangga_admin', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'Admin', FALSE),
('Nabila Maharani', '081298765432', 'nabila.bem@unej.ac.id', 'nabila_bem', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User', FALSE),
('Daffa Arya', '085612349876', 'daffa.himatif@unej.ac.id', 'daffa_himatif', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User', FALSE);

INSERT INTO verifications (id_user, nim, nama_toko, bukti_ktm, tahun_masuk, is_verifikasi) VALUES
(1, '2301010101', 'Danus BPM Fasilkom', 'Uploads/KTM/20260514_rangga_ktm.jpg', 2023, TRUE),
(2, '2301010102', 'BEM Store UNEJ', 'Uploads/KTM/20260514_nabila_ktm.jpg', 2023, TRUE),
(3, '2401010103', 'HIMATIF Merch', 'Uploads/KTM/20260514_daffa_ktm.jpg', 2024, TRUE);

INSERT INTO categories (nama_kategori) VALUES ('Pakaian'), ('Aksesoris'), ('Peralatan Kuliah');

INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif) VALUES
(2, 'PO Jaket & Kaos BEM Batch 1', 'Biasa', 'BCA 1234567890 a.n Nabila (Bendahara BEM)', '2026-05-25 23:59:00', TRUE),
(3, 'Danus HIMATIF Gotong Royong', 'Gotong Royong', 'Mandiri 0987654321 a.n Daffa (Danus HIMATIF)', '2026-06-10 23:59:00', TRUE),
(1, 'BPM Peduli Merchandise', 'Biasa', 'Gopay 081234567890 a.n Rangga Saputra', '2026-05-20 23:59:00', TRUE);

-- Data Dummy Produk diisi dengan id_penjual (Pemilik) dan id_po (PO aktifnya)
INSERT INTO products (id_penjual, id_po, id_kategori, nama_produk, deskripsi, harga_dasar, harga_diskon, target_kuota, min_order, foto_produk) VALUES
(2, 1, 1, 'Kaos Polo BEM Eksklusif', 'Kaos polo premium berbahan cotton combed 30s dengan bordir logo BEM.', 85000, NULL, NULL, 1, 'Uploads/Products/kaos_bem.jpg'),
(3, 2, 2, 'Ganci Akrilik HIMATIF', 'Gantungan kunci akrilik custom desain maskot HIMATIF.', 15000, 12000, 50, 2, 'Uploads/Products/ganci_himatif.jpg'),
(1, 3, 3, 'Tumblr Aesthetic BPM', 'Tumblr stainless steel 500ml dengan desain estetik logo BPM.', 45000, NULL, NULL, 1, 'Uploads/Products/tumblr_bpm.jpg');

INSERT INTO transactions (id_koordinator, tanggal_transaksi, total_bayar_grup, status_pesanan, bukti_bayar, is_valid) VALUES
(2, '2026-05-15 10:30:00', 30000, 'Diproses', 'Uploads/Payments/resi_nabila.jpg', TRUE),
(3, '2026-05-16 14:15:00', 45000, 'Menunggu', 'Uploads/Payments/resi_daffa.jpg', FALSE),
(1, '2026-05-17 09:00:00', 170000, 'Selesai', 'Uploads/Payments/resi_rangga.jpg', TRUE);

INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan, selisih_refund) VALUES
(1, 2, 'Caca (Kelas A)', 2, 'Desain warna biru ya', 6000), 
(2, 3, 'Kevin (Kelas B)', 1, 'Warna hitam', 0),            
(3, 1, 'Tiara (Kelas C)', 2, 'Ukuran L dan M', 0);        

INSERT INTO complaints (id_user, subjek, deskripsi, tanggal, is_selesai, balasan) VALUES
(3, 'Validasi Bukti Transfer Lama', 'Halo min, bukti bayar saya ke toko BEM belum divalidasi.', '2026-05-18 08:00:00', TRUE, 'Sudah diinfokan ke pihak BEM.'),
(2, 'Aplikasi Error saat Checkout', 'Tabel detail teman yang nitip mereset.', '2026-05-19 10:20:00', FALSE, NULL);

INSERT INTO reviews (id_produk, id_user, rating, komentar, tanggal_ulasan, balasan_penjual) VALUES
(1, 1, 5, 'Bahan kaosnya sangat adem dan jahitannya rapi!', '2026-05-18 16:30:00', 'Terima kasih banyak atas pesanannya!');


-- =========================================================
-- BAGIAN 3: ADVANCED DB OBJECTS (VIEW, FUNCTION, PROCEDURE, TRIGGER)
-- =========================================================

-- View Katalog (Melihat produk yang punya ID PO aktif)
CREATE OR REPLACE VIEW vw_katalog_aktif AS
SELECT 
    p.id_produk,
    po.judul_po,
    kat.nama_kategori,
    p.nama_produk,
    p.harga_dasar,
    p.harga_diskon,
    po.batas_waktu,
    po.info_rekening
FROM products p
JOIN preorders po ON p.id_po = po.id_po
JOIN categories kat ON p.id_kategori = kat.id_kategori
WHERE po.is_aktif = TRUE AND po.batas_waktu >= CURRENT_TIMESTAMP;

-- Function Cek Harga
CREATE OR REPLACE FUNCTION cek_harga_saat_ini(p_id_produk INT) 
RETURNS INT AS $$
DECLARE
    v_jenis_po VARCHAR;
    v_harga_dasar INT;
    v_harga_diskon INT;
    v_target_kuota INT;
    v_total_dipesan INT;
BEGIN
    SELECT po.jenis_po, p.harga_dasar, p.harga_diskon, p.target_kuota 
    INTO v_jenis_po, v_harga_dasar, v_harga_diskon, v_target_kuota
    FROM products p JOIN preorders po ON p.id_po = po.id_po WHERE p.id_produk = p_id_produk;

    SELECT COALESCE(SUM(jumlah_pesanan), 0) INTO v_total_dipesan 
    FROM transaction_details WHERE id_produk = p_id_produk;

    IF v_jenis_po = 'Gotong Royong' AND v_total_dipesan >= v_target_kuota THEN
        RETURN v_harga_diskon;
    ELSE
        RETURN v_harga_dasar;
    END IF;
END;
$$ LANGUAGE plpgsql;

-- Procedure Checkout
CREATE OR REPLACE PROCEDURE proses_checkout(
    p_id_koordinator INT,
    p_total_bayar INT,
    p_id_produk INT,
    p_nama_penitip VARCHAR,
    p_jumlah INT,
    p_catatan VARCHAR
)
LANGUAGE plpgsql
AS $$
DECLARE
    v_id_transaksi INT;
BEGIN
    INSERT INTO transactions (id_koordinator, total_bayar_grup, status_pesanan)
    VALUES (p_id_koordinator, p_total_bayar, 'Menunggu')
    RETURNING id_transaksi INTO v_id_transaksi;

    INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan)
    VALUES (v_id_transaksi, p_id_produk, p_nama_penitip, p_jumlah, p_catatan);
EXCEPTION
    WHEN OTHERS THEN
        RAISE NOTICE 'Terjadi kesalahan saat checkout: %', SQLERRM;
        ROLLBACK;
END;
$$;

-- Trigger Refund
CREATE OR REPLACE FUNCTION trg_hitung_refund_gotong_royong()
RETURNS TRIGGER AS $$
DECLARE
    v_target INT;
    v_harga_normal INT;
    v_harga_diskon INT;
    v_total_sekarang INT;
    v_jenis VARCHAR;
BEGIN
    SELECT p.target_kuota, p.harga_dasar, p.harga_diskon, po.jenis_po 
    INTO v_target, v_harga_normal, v_harga_diskon, v_jenis
    FROM products p JOIN preorders po ON p.id_po = po.id_po 
    WHERE p.id_produk = NEW.id_produk;

    IF v_jenis = 'Gotong Royong' THEN
        SELECT SUM(jumlah_pesanan) INTO v_total_sekarang 
        FROM transaction_details WHERE id_produk = NEW.id_produk;

        IF v_total_sekarang >= v_target THEN
            UPDATE transaction_details 
            SET selisih_refund = (v_harga_normal - v_harga_diskon) * jumlah_pesanan
            WHERE id_produk = NEW.id_produk;
        END IF;
    END IF;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER t_after_insert_detail
AFTER INSERT ON transaction_details
FOR EACH ROW
EXECUTE FUNCTION trg_hitung_refund_gotong_royong();

-- =========================================================
-- BAGIAN 4: KUMPULAN KUERI ANALITIK 
-- (Jangan dieksekusi bersamaan dengan DDL/DML, 
--  kueri ini digunakan di dalam program C# atau dicoba satu per satu di pgAdmin)
-- =========================================================

/*
-- 1. GROUP BY (Melihat barang terjual tiap produk)
SELECT p.nama_produk, SUM(td.jumlah_pesanan) AS total_terjual
FROM transaction_details td
JOIN products p ON td.id_produk = p.id_produk
GROUP BY p.nama_produk ORDER BY total_terjual DESC;

-- 2a. CUBE (Kombinasi silang Kategori & Jenis PO)
SELECT kat.nama_kategori, po.jenis_po, SUM(td.jumlah_pesanan) AS total_barang_terjual
FROM transaction_details td
JOIN products p ON td.id_produk = p.id_produk
JOIN preorders po ON p.id_po = po.id_po
JOIN categories kat ON p.id_kategori = kat.id_kategori
GROUP BY CUBE (kat.nama_kategori, po.jenis_po);

-- 2b. ROLL UP (Hierarki Waktu: Tahun -> Bulan)
SELECT EXTRACT(YEAR FROM t.tanggal_transaksi) AS tahun, EXTRACT(MONTH FROM t.tanggal_transaksi) AS bulan, SUM(t.total_bayar_grup) AS omzet_kotor
FROM transactions t WHERE t.status_pesanan = 'Selesai'
GROUP BY ROLLUP (EXTRACT(YEAR FROM t.tanggal_transaksi), EXTRACT(MONTH FROM t.tanggal_transaksi));

-- 2c. GROUPING SETS (Total per Penjual & per Kategori dalam 1 tabel)
SELECT u.nama AS nama_penjual, kat.nama_kategori, SUM(td.jumlah_pesanan) AS unit_terjual
FROM transaction_details td
JOIN transactions t ON td.id_transaksi = t.id_transaksi
JOIN products p ON td.id_produk = p.id_produk
JOIN categories kat ON p.id_kategori = kat.id_kategori
JOIN preorders po ON p.id_po = po.id_po
JOIN users u ON po.id_penjual = u.id_user
GROUP BY GROUPING SETS ((u.nama), (kat.nama_kategori));

-- 3. SUBQUERY (Produk yang kuotanya sisa <= 5)
SELECT nama_produk, target_kuota FROM products p
WHERE p.target_kuota IS NOT NULL AND (
    p.target_kuota - (SELECT COALESCE(SUM(jumlah_pesanan), 0) FROM transaction_details td WHERE td.id_produk = p.id_produk)
) <= 5;

-- 4a. UNION (Gabungan transaksi berjalan dan selesai)
SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Diproses'
UNION
SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Selesai';

-- 4b. INTERSECT (Penjual yang juga jajan sebagai pembeli)
SELECT id_user FROM verifications WHERE is_verifikasi = TRUE
INTERSECT
SELECT id_koordinator FROM transactions;

-- 4c. EXCEPT (User yang belum pernah transaksi)
SELECT id_user, nama FROM users
EXCEPT
SELECT u.id_user, u.nama FROM users u JOIN transactions t ON u.id_user = t.id_koordinator;
*/