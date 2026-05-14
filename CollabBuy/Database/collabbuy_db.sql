-- ========================================================================================
-- 1. RESET SCHEMA
-- ========================================================================================
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;

-- ========================================================================================
-- 2. DDL: PEMBUATAN TABEL (Sesuai Kebutuhan Baru)
-- ========================================================================================

CREATE TABLE users (
    id_user SERIAL PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    nama VARCHAR(100) NOT NULL,
    email VARCHAR(100) UNIQUE,
    nomor_telepon VARCHAR(20),
    peran VARCHAR(20) CHECK (peran IN ('User', 'Admin')),
    fakultas VARCHAR(100),
    prodi VARCHAR(100),
    is_diblokir BOOLEAN DEFAULT FALSE
);

CREATE TABLE verifications (
    id_verifikasi SERIAL PRIMARY KEY,
    id_user INT REFERENCES users(id_user) ON DELETE CASCADE,
    nama_toko VARCHAR(100) NOT NULL,
    nim VARCHAR(20) NOT NULL,
    tahun_masuk INT NOT NULL, 
    bukti_mahasiswa TEXT NOT NULL,
    is_verifikasi BOOLEAN DEFAULT FALSE
);

CREATE TABLE categories (
    id_kategori SERIAL PRIMARY KEY,
    nama_kategori VARCHAR(50) UNIQUE NOT NULL,
    deskripsi TEXT
);

CREATE TABLE products (
    id_produk SERIAL PRIMARY KEY,
    id_seller INT REFERENCES users(id_user) ON DELETE CASCADE,
    id_kategori INT REFERENCES categories(id_kategori),
    nama_produk VARCHAR(150) NOT NULL,
    stok_produk INT NOT NULL,
    foto_produk TEXT,
    is_aktif BOOLEAN DEFAULT TRUE
);

CREATE TABLE preorders (
    id_po SERIAL PRIMARY KEY,
    id_produk INT NOT NULL REFERENCES products(id_produk) ON DELETE CASCADE,
    jenis_po VARCHAR(20) NOT NULL CHECK (jenis_po IN ('Biasa', 'GotongRoyong')),
    harga_dasar DECIMAL(12,2) NOT NULL,
    harga_diskon DECIMAL(12,2),
    target_kuota INT,
    kuota_terkini INT DEFAULT 0,
    batas_waktu TIMESTAMP NOT NULL,
    is_aktif BOOLEAN DEFAULT TRUE
);

CREATE TABLE checkouts (
    id_checkout SERIAL PRIMARY KEY,
    id_user_coordinator INT REFERENCES users(id_user),
    id_po INT REFERENCES preorders(id_po),
    tanggal_pemesanan TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    jumlah_pesanan INT NOT NULL,
    total_bayar_awal DECIMAL(12,2) NOT NULL,
    kembalian DECIMAL(12,2) DEFAULT 0,
    metode_pembayaran VARCHAR(50) DEFAULT 'Transfer',
    status_pesanan VARCHAR(20) DEFAULT 'Menunggu' 
        CHECK (status_pesanan IN ('Menunggu', 'Diproses', 'Tersedia', 'Selesai', 'Dibatalkan')),
    bukti_pembayaran TEXT,
    is_valid BOOLEAN DEFAULT FALSE
);

CREATE TABLE complaints (
    id_aduan SERIAL PRIMARY KEY,
    id_user INT REFERENCES users(id_user),
    subjek VARCHAR(100),
    pesan TEXT,
    status_aduan VARCHAR(20) DEFAULT 'Terbuka'
);

-- ========================================================================================
-- IMPLEMENTASI 10 MATERI ADVANCED POSTGRESQL
-- ========================================================================================

-- [7. FUNCTION] Menghitung selisih kembalian per transaksi
CREATE OR REPLACE FUNCTION fn_hitung_refund(p_id_po INT, p_jumlah_pesanan INT)
RETURNS DECIMAL AS $$
DECLARE
    v_harga_dasar DECIMAL;
    v_harga_diskon DECIMAL;
BEGIN
    SELECT harga_dasar, harga_diskon INTO v_harga_dasar, v_harga_diskon 
    FROM preorders WHERE id_po = p_id_po;
    
    RETURN (v_harga_dasar - v_harga_diskon) * p_jumlah_pesanan;
END;
$$ LANGUAGE plpgsql;


-- [10. TRIGGER] & [6. STATEMENT] Update Kembalian Otomatis Saat Target Gotong Royong Tercapai
CREATE OR REPLACE FUNCTION trg_func_proses_diskon_masal()
RETURNS TRIGGER AS $$
BEGIN
    IF NEW.kuota_terkini > (SELECT stok_produk FROM products WHERE id_produk = NEW.id_produk) THEN
        RAISE EXCEPTION 'Stok fisik produk tidak mencukupi untuk PO ini!';
    END IF;

    IF NEW.kuota_terkini >= NEW.target_kuota AND NEW.jenis_po = 'GotongRoyong' THEN
        UPDATE checkouts 
        SET kembalian = fn_hitung_refund(NEW.id_po, jumlah_pesanan)
        WHERE id_po = NEW.id_po AND status_pesanan != 'Dibatalkan';
    END IF;
    
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_auto_refund
AFTER UPDATE ON preorders
FOR EACH ROW EXECUTE FUNCTION trg_func_proses_diskon_masal();


-- [8. STORE PROCEDURE] & [9. TRANSACTION] Proses Pesanan
CREATE OR REPLACE PROCEDURE sp_buat_transaksi(
    p_id_user INT, p_id_po INT, p_qty INT, p_bukti TEXT
)
LANGUAGE plpgsql AS $$
DECLARE
    v_harga_awal DECIMAL;
BEGIN
    SELECT harga_dasar INTO v_harga_awal FROM preorders WHERE id_po = p_id_po;

    INSERT INTO checkouts (id_user_coordinator, id_po, jumlah_pesanan, total_bayar_awal, bukti_pembayaran)
    VALUES (p_id_user, p_id_po, p_qty, (v_harga_awal * p_qty), p_bukti);

    UPDATE preorders SET kuota_terkini = kuota_terkini + p_qty WHERE id_po = p_id_po;

    COMMIT;
END;
$$;


-- [5. VIEW] Katalog Produk
CREATE OR REPLACE VIEW vw_katalog_produk AS
SELECT 
    po.id_po, p.nama_produk, c.nama_kategori, v.nama_toko AS penjual,
    po.jenis_po, po.harga_dasar, po.harga_diskon, po.target_kuota, po.kuota_terkini, p.foto_produk
FROM preorders po
JOIN products p ON po.id_produk = p.id_produk
JOIN categories c ON p.id_kategori = c.id_kategori
JOIN verifications v ON p.id_seller = v.id_user;


-- [1. GROUP BY] Total barang terjual tiap produk
CREATE OR REPLACE VIEW vw_rekap_terjual AS
SELECT p.nama_produk, SUM(c.jumlah_pesanan) as total_qty
FROM checkouts c
JOIN preorders po ON c.id_po = po.id_po
JOIN products p ON po.id_produk = p.id_produk
GROUP BY p.nama_produk;


-- [3. SUBQUERY] Produk hampir habis
CREATE OR REPLACE VIEW vw_po_limit AS
SELECT nama_produk FROM products 
WHERE id_produk IN (SELECT id_produk FROM preorders WHERE target_kuota - kuota_terkini <= 5);


-- [4. TEORI HIMPUNAN]
CREATE OR REPLACE VIEW vw_semua_status AS
SELECT id_checkout, 'AKTIF' as status FROM checkouts WHERE status_pesanan = 'Diproses'
UNION
SELECT id_checkout, 'DONE' as status FROM checkouts WHERE status_pesanan = 'Selesai';

CREATE OR REPLACE VIEW vw_user_aktif_dua_sisi AS
SELECT id_user FROM verifications
INTERSECT
SELECT id_user_coordinator FROM checkouts;

CREATE OR REPLACE VIEW vw_user_pasif AS
SELECT id_user FROM users WHERE peran = 'User'
EXCEPT
SELECT id_user_coordinator FROM checkouts;


-- ========================================================================================
-- SEEDING DATA
-- ========================================================================================

-- ✅ FIX: Hash password disesuaikan dengan salt di PasswordHelper.cs ("DanusFasilkomUnej2026")
-- Password admin : admin123  → hash SHA256(admin123 + DanusFasilkomUnej2026)
-- Password users : user123   → hash SHA256(user123  + DanusFasilkomUnej2026)
--
-- Cara verifikasi hash:
--   python3 -c "import hashlib; print(hashlib.sha256(('admin123'+'DanusFasilkomUnej2026').encode()).hexdigest())"
--   Hasil: 51a066721aa7233320257149b5b310900d9cb1f8477d895d22816887ddff5fab
--
--   python3 -c "import hashlib; print(hashlib.sha256(('user123'+'DanusFasilkomUnej2026').encode()).hexdigest())"
--   Hasil: 5f5857d16cd0989b30a9899f1281b674ef30fa343cbacebdd5b004bba297d9ce

INSERT INTO users (username, password, nama, peran) 
VALUES ('admin', '51a066721aa7233320257149b5b310900d9cb1f8477d895d22816887ddff5fab', 'Super Admin CollabBuy', 'Admin');
-- Login: username=admin | password=admin123

INSERT INTO users (username, password, nama, email, nomor_telepon, peran, fakultas, prodi) VALUES 
('danus_bem',    '5f5857d16cd0989b30a9899f1281b674ef30fa343cbacebdd5b004bba297d9ce', 'Danus BEM Fasilkom',   'bem@unej.ac.id',  '081234567890', 'User', 'Ilmu Komputer', 'Teknologi Informasi'),
('danus_himatif','5f5857d16cd0989b30a9899f1281b674ef30fa343cbacebdd5b004bba297d9ce', 'Danus HIMATIF',        'hima@unej.ac.id', '081345678901', 'User', 'Ilmu Komputer', 'Informatika'),
('maba_ti_24',   '5f5857d16cd0989b30a9899f1281b674ef30fa343cbacebdd5b004bba297d9ce', 'Reza Mahasiswa TI',    'reza@unej.ac.id', '085755556666', 'User', 'Ilmu Komputer', 'Teknologi Informasi');
-- Login semua user: password=user123

INSERT INTO verifications (id_user, nama_toko, nim, tahun_masuk, bukti_mahasiswa, is_verifikasi) VALUES 
(2, 'Merch BEM Fasilkom', '2402401', 2023, 'Images/KTM/ktm_bem.jpg', TRUE),
(3, 'Danus HIMATIF Store', '2402402', 2024, 'Images/KTM/ktm_himatif.jpg', TRUE);

INSERT INTO categories (nama_kategori, deskripsi) VALUES 
('Aksesoris', 'Ganci, Stiker'), ('Pakaian', 'Kaos, Jaket'), ('Botol', 'Tumblr Minum');

INSERT INTO products (id_seller, id_kategori, nama_produk, stok_produk, foto_produk) VALUES 
(2, 2, 'Kaos Fasilkom Unity', 100, 'Images/Products/kaos.png'),
(2, 3, 'Tumblr BEM Fasilkom', 50, 'Images/Products/tumblr.png'),
(3, 1, 'Ganci HIMATIF', 200, 'Images/Products/ganci.png');

INSERT INTO preorders (id_produk, jenis_po, harga_dasar, harga_diskon, target_kuota, kuota_terkini, batas_waktu) VALUES 
(1, 'GotongRoyong', 100000, 85000, 10, 0, '2026-12-01 23:59:00');

CALL sp_buat_transaksi(4, 1, 2, 'Images/Bukti/bayar1.jpg');