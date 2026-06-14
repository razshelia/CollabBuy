-- ============================================================
--  COLLABBUY DATABASE  —  FILE SQL TERSTRUKTUR PER MATERI
--  Dibuat dari repository CollabBuy-master
--  PASSWORD LOGIN DUMMY: password123
-- ============================================================


-- ============================================================
-- SECTION 0 : INISIALISASI SCHEMA
-- ============================================================
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;


-- ============================================================
-- SECTION 1 : DDL — CREATE TABLE & INDEX
-- ============================================================

CREATE TABLE users (
    id_user         SERIAL PRIMARY KEY,
    nama            VARCHAR(100) NOT NULL,
    nomor_telepon   VARCHAR(20)  NOT NULL,
    email           VARCHAR(100) UNIQUE,
    username        VARCHAR(50)  UNIQUE NOT NULL,
    password        VARCHAR(255) NOT NULL,
    peran           VARCHAR(20)  DEFAULT 'User',
    is_diblokir     BOOLEAN      DEFAULT FALSE,
    alasan_blokir   TEXT         DEFAULT ''
);

CREATE TABLE verifications (
    id_verifikasi   SERIAL PRIMARY KEY,
    id_user         INTEGER REFERENCES users (id_user) ON DELETE CASCADE UNIQUE NOT NULL,
    nim             VARCHAR(20)  UNIQUE NOT NULL,
    nama_toko       VARCHAR(100) NOT NULL,
    bukti_ktm       BYTEA        NOT NULL,
    tahun_masuk     INTEGER      NOT NULL,
    is_verifikasi   BOOLEAN      DEFAULT FALSE
);

CREATE TABLE categories (
    id_kategori     SERIAL PRIMARY KEY,
    nama_kategori   VARCHAR(100) NOT NULL,
    is_deleted      BOOLEAN      NOT NULL DEFAULT FALSE
);

CREATE TABLE preorders (
    id_po           SERIAL PRIMARY KEY,
    id_penjual      INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    judul_po        VARCHAR(150) NOT NULL,
    jenis_po        VARCHAR(50)  NOT NULL,
    info_rekening   VARCHAR(255) NOT NULL,
    batas_waktu     TIMESTAMP    NOT NULL,
    is_aktif        BOOLEAN      DEFAULT TRUE,
    is_deleted      BOOLEAN      NOT NULL DEFAULT FALSE
);

CREATE TABLE products (
    id_produk       SERIAL PRIMARY KEY,
    id_penjual      INTEGER REFERENCES users (id_user)      ON DELETE CASCADE,
    id_po           INTEGER REFERENCES preorders (id_po)    ON DELETE SET NULL,
    id_kategori     INTEGER REFERENCES categories (id_kategori) ON DELETE RESTRICT NOT NULL,
    nama_produk     VARCHAR(150) NOT NULL,
    deskripsi       TEXT,
    harga_dasar     INTEGER NOT NULL,
    harga_diskon    INTEGER,
    target_kuota    INTEGER,
    min_order       INTEGER DEFAULT 1,
    foto_produk     BYTEA,
    is_deleted      BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE transactions (
    id_transaksi        SERIAL PRIMARY KEY,
    id_koordinator      INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    tanggal_transaksi   TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status_pesanan      VARCHAR(50) DEFAULT 'Menunggu',
    bukti_bayar         BYTEA,
    is_valid            BOOLEAN DEFAULT FALSE
);

CREATE TABLE transaction_details (
    id_detail               SERIAL PRIMARY KEY,
    id_transaksi            INTEGER REFERENCES transactions (id_transaksi) ON DELETE CASCADE,
    id_produk               INTEGER REFERENCES products (id_produk) ON DELETE RESTRICT,
    id_po_saat_beli         INTEGER REFERENCES preorders (id_po),
    nama_produk_snapshot    VARCHAR(150) NOT NULL,
    nama_penitip            VARCHAR(100) NOT NULL,
    jumlah_pesanan          INTEGER NOT NULL,
    catatan                 VARCHAR(255),
    harga_satuan_saat_beli  INTEGER NOT NULL DEFAULT 0,
    harga_diskon_saat_beli  INTEGER,
    selisih_refund          INTEGER DEFAULT 0,
    CONSTRAINT uq_detail_transaksi_produk_penitip
        UNIQUE (id_transaksi, id_produk, nama_penitip)
);

CREATE TABLE complaints (
    id_aduan    SERIAL PRIMARY KEY,
    id_user     INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    subjek      VARCHAR(150) NOT NULL,
    deskripsi   TEXT         NOT NULL,
    tanggal     TIMESTAMP    DEFAULT CURRENT_TIMESTAMP,
    is_selesai  BOOLEAN      DEFAULT FALSE,
    balasan     TEXT
);

CREATE TABLE reviews (
    id_ulasan       SERIAL PRIMARY KEY,
    id_produk       INTEGER REFERENCES products (id_produk) ON DELETE CASCADE,
    id_user         INTEGER REFERENCES users (id_user)      ON DELETE CASCADE,
    rating          INTEGER CHECK (rating >= 1 AND rating <= 5),
    komentar        TEXT,
    tanggal_ulasan  TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    balasan_penjual TEXT
);

CREATE TABLE activity_logs (
    id_log      SERIAL PRIMARY KEY,
    id_user     INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    aktivitas   VARCHAR(255) NOT NULL,
    waktu_akses TIMESTAMP    DEFAULT CURRENT_TIMESTAMP
);

-- INDEX
CREATE INDEX idx_products_id_penjual         ON products(id_penjual);
CREATE INDEX idx_products_id_po              ON products(id_po);
CREATE INDEX idx_products_id_kategori        ON products(id_kategori);
CREATE INDEX idx_td_id_transaksi             ON transaction_details(id_transaksi);
CREATE INDEX idx_td_id_produk                ON transaction_details(id_produk);
CREATE INDEX idx_transactions_id_koord       ON transactions(id_koordinator);
CREATE INDEX idx_preorders_id_penjual        ON preorders(id_penjual);
CREATE INDEX idx_reviews_id_produk           ON reviews(id_produk);
CREATE INDEX idx_complaints_id_user          ON complaints(id_user);
CREATE INDEX idx_transactions_status_pesanan ON transactions(status_pesanan);
CREATE INDEX idx_activity_logs_id_user       ON activity_logs(id_user);


-- ============================================================
-- SECTION 2 : TRIGGER
--   (Trigger function + CREATE TRIGGER harus dibuat
--    SEBELUM INSERT data agar berjalan dengan benar)
-- ============================================================

-- Helper function: kembalikan harga saat ini berdasarkan kuota GR
CREATE OR REPLACE FUNCTION cek_harga_saat_ini(p_id_produk INT) RETURNS INT AS $$
DECLARE
    v_id_po         INT;
    v_jenis_po      VARCHAR;
    v_harga_dasar   INT;
    v_harga_diskon  INT;
    v_target_kuota  INT;
    v_total_dipesan INT;
BEGIN
    SELECT id_po, harga_dasar, harga_diskon, target_kuota
    INTO v_id_po, v_harga_dasar, v_harga_diskon, v_target_kuota
    FROM products WHERE id_produk = p_id_produk FOR UPDATE;

    IF v_id_po IS NULL THEN RETURN v_harga_dasar; END IF;

    SELECT jenis_po INTO v_jenis_po
    FROM preorders WHERE id_po = v_id_po AND is_deleted = FALSE;

    SELECT COALESCE(SUM(jumlah_pesanan), 0) INTO v_total_dipesan
    FROM transaction_details WHERE id_produk = p_id_produk;

    IF v_jenis_po = 'Gotong Royong'
       AND v_total_dipesan >= COALESCE(v_target_kuota, 999999) THEN
        RETURN COALESCE(v_harga_diskon, v_harga_dasar);
    ELSE
        RETURN v_harga_dasar;
    END IF;
END;
$$ LANGUAGE plpgsql;

-- ── TRIGGER 1 ─────────────────────────────────────────────
-- Cegah produk masuk ke PO milik penjual lain
CREATE OR REPLACE FUNCTION cek_kepemilikan_po() RETURNS TRIGGER AS $$
DECLARE
    v_pemilik_po INTEGER;
BEGIN
    IF NEW.id_po IS NOT NULL THEN
        SELECT id_penjual INTO v_pemilik_po
        FROM preorders WHERE id_po = NEW.id_po;
        IF NEW.id_penjual != v_pemilik_po THEN
            RAISE EXCEPTION 'ID Penjual produk tidak cocok dengan pemilik PO!';
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_cek_kepemilikan_po
    BEFORE INSERT OR UPDATE ON products
    FOR EACH ROW EXECUTE FUNCTION cek_kepemilikan_po();

-- ── TRIGGER 2 ─────────────────────────────────────────────
-- Otomatis isi snapshot nama produk, harga, dan ID PO saat beli
CREATE OR REPLACE FUNCTION trg_set_harga_otomatis() RETURNS TRIGGER AS $$
DECLARE
    v_nama_produk  VARCHAR;
    v_harga_diskon INT;
    v_id_po        INT;
BEGIN
    SELECT nama_produk, harga_diskon, id_po
    INTO v_nama_produk, v_harga_diskon, v_id_po
    FROM products WHERE id_produk = NEW.id_produk;

    NEW.nama_produk_snapshot   := v_nama_produk;
    NEW.harga_satuan_saat_beli := cek_harga_saat_ini(NEW.id_produk);
    NEW.harga_diskon_saat_beli := v_harga_diskon;
    NEW.id_po_saat_beli        := v_id_po;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER t_before_insert_detail
    BEFORE INSERT ON transaction_details
    FOR EACH ROW EXECUTE FUNCTION trg_set_harga_otomatis();

-- ── TRIGGER 3 ─────────────────────────────────────────────
-- Jalan setiap ada transaksi baru ATAU preorder baru dibuka
-- Sehingga setiap aktivitas di app otomatis "menyapu" PO yang sudah expired
CREATE OR REPLACE FUNCTION fn_nonaktifkan_po_expired()
RETURNS TRIGGER AS $$
BEGIN
    UPDATE preorders
    SET is_aktif = FALSE
    WHERE batas_waktu < NOW()
      AND is_aktif = TRUE;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER trg_auto_tutup_po_expired
AFTER INSERT ON transactions
FOR EACH STATEMENT
EXECUTE FUNCTION fn_nonaktifkan_po_expired();

-- SESUDAH (diperbaiki — hapus UPDATE):
CREATE TRIGGER trg_auto_tutup_po_on_new_po
AFTER INSERT ON preorders
FOR EACH STATEMENT
EXECUTE FUNCTION fn_nonaktifkan_po_expired();

-- ── TRIGGER 4 ─────────────────────────────────────────────
-- Hitung refund Gotong Royong otomatis saat kuota terpenuhi
CREATE OR REPLACE FUNCTION trg_hitung_refund_gotong_royong() RETURNS TRIGGER AS $$
DECLARE
    v_id_po          INT;
    v_target         INT;
    v_harga_diskon   INT;
    v_total_sekarang INT;
    v_jenis          VARCHAR;
BEGIN
    SELECT id_po INTO v_id_po FROM products WHERE id_produk = NEW.id_produk;
    IF v_id_po IS NULL THEN RETURN NEW; END IF;

    SELECT p.target_kuota, p.harga_diskon, po.jenis_po
    INTO v_target, v_harga_diskon, v_jenis
    FROM products p JOIN preorders po ON p.id_po = po.id_po
    WHERE p.id_produk = NEW.id_produk;

    IF v_jenis = 'Gotong Royong' AND v_harga_diskon IS NOT NULL THEN
        SELECT SUM(jumlah_pesanan) INTO v_total_sekarang
        FROM transaction_details
        WHERE id_produk = NEW.id_produk AND id_po_saat_beli = v_id_po;

        IF v_total_sekarang >= v_target THEN
            UPDATE transaction_details
            SET selisih_refund = (harga_satuan_saat_beli - v_harga_diskon) * jumlah_pesanan
            WHERE id_produk       = NEW.id_produk
              AND id_po_saat_beli = v_id_po
              AND harga_satuan_saat_beli > v_harga_diskon
              AND selisih_refund  = 0;
        END IF;
    END IF;
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER t_after_insert_detail
    AFTER INSERT ON transaction_details
    FOR EACH ROW EXECUTE FUNCTION trg_hitung_refund_gotong_royong();


-- ============================================================
-- SECTION 3 : INSERT DATA (DML — Dummy Data)
--   Dijalankan SETELAH trigger terdefinisi
-- ============================================================

INSERT INTO users (nama, nomor_telepon, email, username, password, peran) VALUES
('Rangga Saputra',     '081200000001', 'admin@unej.ac.id',  'admin',  'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'Admin'),
('Nabila BEM',         '081200000002', 'nabila@unej.ac.id', 'nabila', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Daffa HMIF',         '081200000003', 'daffa@unej.ac.id',  'daffa',  'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Budi UKM Olahraga',  '081200000004', 'budi@unej.ac.id',   'budi',   'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Siti Kopma',         '081200000005', 'siti@unej.ac.id',   'siti',   'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Tiara (Buyer)',       '081200000006', 'tiara@unej.ac.id',  'tiara',  'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Kevin (Buyer)',       '081200000007', 'kevin@unej.ac.id',  'kevin',  'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Reza (Buyer)',        '081200000008', 'reza@unej.ac.id',   'reza',   'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Andi (Buyer)',        '081200000009', 'andi@unej.ac.id',   'andi',   'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Maya (Buyer)',        '081200000010', 'maya@unej.ac.id',   'maya',   'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User');

INSERT INTO verifications (id_user, nim, nama_toko, bukti_ktm, tahun_masuk, is_verifikasi) VALUES
(2, '23010101', 'BEM Store UNEJ',  '\x', 2023, TRUE),
(3, '24010102', 'HIMATIF Merch',   '\x', 2024, TRUE),
(4, '22010103', 'UKM Sport Danus', '\x', 2022, TRUE),
(5, '23010104', 'Kopma Jember',    '\x', 2023, TRUE);

INSERT INTO categories (nama_kategori) VALUES
('Pakaian & Konveksi'),
('Aksesoris & Souvenir'),
('Makanan & Minuman'),
('Peralatan Kuliah'),
('Merchandise Event');

INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif) VALUES
(2, 'PO PDH BEM Batch 1',          'Biasa',         'BCA 12345 a.n Nabila',    '2026-05-10 23:59:00',                   FALSE),
(2, 'PO Makanan Rapat BEM',         'Biasa',         'BCA 12345 a.n Nabila',    CURRENT_TIMESTAMP + INTERVAL '30 days',  TRUE),
(3, 'Danus HIMATIF Gotong Royong',  'Gotong Royong', 'Mandiri 98765 a.n Daffa', CURRENT_TIMESTAMP + INTERVAL '15 days',  TRUE),
(3, 'PO Lanyard Fasilkom',          'Biasa',         'Mandiri 98765 a.n Daffa', CURRENT_TIMESTAMP + INTERVAL '45 days',  TRUE),
(4, 'PO Jersey UKM Olahraga',       'Gotong Royong', 'BRI 112233 a.n Budi',     CURRENT_TIMESTAMP + INTERVAL '20 days',  TRUE),
(5, 'PO Binder Kuliah Kopma',       'Biasa',         'BNI 445566 a.n Siti',     '2026-05-15 23:59:00',                   FALSE);

INSERT INTO products
    (id_penjual, id_po, id_kategori, nama_produk, deskripsi, harga_dasar, harga_diskon, target_kuota, min_order, foto_produk)
VALUES
(2, 1,    1, 'PDH BEM Pengurus',          'Bahan nagata drill.',           120000,  NULL,  NULL,  1, '\x'),
(2, 1,    1, 'Kaos Panitia',              'Cotton combed 30s.',             65000,  NULL,  NULL,  1, '\x'),
(2, 2,    3, 'Nasi Kotak Ayam Geprek',    'Level pedas 1-5.',               15000,  NULL,  NULL,  5, '\x'),
(2, 2,    3, 'Es Teh Manis Jumbo',        'Es teh ukuran 1 liter.',          5000,  NULL,  NULL,  2, '\x'),
(2, 2,    3, 'Risol Mayo BEM',            'Isi smoked beef, keju, telur.',   4000,  NULL,  NULL, 10, '\x'),
(3, 3,    2, 'Ganci Akrilik Maskot',      'Akrilik tebal 3mm.',             15000, 10000,    50,  1, '\x'),
(3, 3,    2, 'Stiker Pack Hacker',        'Isi 10 stiker coding.',          12000,  8000,    30,  2, '\x'),
(3, 4,    2, 'Lanyard Eksklusif UNEJ',    'Desain terbaru.',                25000,  NULL,  NULL,  1, '\x'),
(3, 4,    4, 'Flashdisk Custom Logo',     '32GB Sandisk.',                  85000,  NULL,  NULL,  1, '\x'),
(3, NULL, 1, 'Tote Bag Ngoding (Gudang)', 'Produk belum masuk PO.',         35000,  NULL,  NULL,  1, '\x'),
(4, 5,    1, 'Jersey Futsal Set',         'Bahan dryfit.',                 110000, 95000,    24, 12, '\x'),
(4, 5,    1, 'Jersey Voli Set',           'Bahan milano.',                 115000,100000,    20, 10, '\x'),
(4, 5,    2, 'Botol Minum Sport',         'Kapasitas 1L.',                  45000, 35000,    50,  1, '\x'),
(4, NULL, 2, 'Handuk Kecil Gym',          'Bahan katun.',                   20000,  NULL,  NULL,  1, '\x'),
(5, 6,    4, 'Binder Aesthetic',          'Include kertas.',                35000,  NULL,  NULL,  1, '\x'),
(5, 6,    4, 'Notebook Spiral',           'Polos 100 lbr.',                 15000,  NULL,  NULL,  1, '\x'),
(5, 6,    4, 'Pulpen Gel Set',            'Isi 5 pcs.',                     12000,  NULL,  NULL,  2, '\x'),
(5, NULL, 3, 'Kopi Botol Kopma',          'Kopi susu gula aren.',           12000,  NULL,  NULL,  3, '\x'),
(5, NULL, 3, 'Keripik Singkong',          'Rasa balado.',                    8000,  NULL,  NULL,  5, '\x'),
(5, NULL, 5, 'Mug Wisuda',               'Custom foto.',                    25000,  NULL,  NULL,  1, '\x');

-- INSERT TRANSAKSI & DETAIL
-- Batch 1: PO BEM Batch 1 (sudah tutup — trigger waktu di-disable sementara)
INSERT INTO transactions (id_koordinator, tanggal_transaksi, status_pesanan, bukti_bayar, is_valid) VALUES
(6, '2026-05-01 10:00:00', 'Selesai', '\x', TRUE),
(6, '2026-05-02 11:30:00', 'Selesai', '\x', TRUE),
(6, '2026-05-05 14:00:00', 'Selesai', '\x', TRUE);

ALTER TABLE transaction_details DISABLE TRIGGER t_before_insert_detail;

INSERT INTO transaction_details
    (id_transaksi, id_produk, id_po_saat_beli, nama_produk_snapshot,
     nama_penitip, jumlah_pesanan, catatan, harga_satuan_saat_beli, harga_diskon_saat_beli)
VALUES
    (1, 1, 1, 'PDH BEM Pengurus', 'Tiara', 1, 'Ukuran M',      120000, NULL),
    (1, 1, 1, 'PDH BEM Pengurus', 'Siska', 2, 'Ukuran L',      120000, NULL),
    (1, 2, 1, 'Kaos Panitia',     'Kevin', 1, 'Warna Hitam',    65000, NULL);

ALTER TABLE transaction_details ENABLE TRIGGER t_before_insert_detail;

-- Batch 2: PO Gotong Royong HIMATIF (masih aktif — trigger menyala)
INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES
(2, 6, 'Grup Kelas A', 20, 'Ganci Maskot'),
(2, 7, 'Grup Kelas A', 15, 'Stiker');

-- PO Lanyard Fasilkom
INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES
(3, 8, 'Tiara', 2, 'Lanyard Merah'),
(3, 8, 'Bagas', 3, 'Lanyard Biru');

-- Batch 3: transaksi campuran status
INSERT INTO transactions (id_koordinator, tanggal_transaksi, status_pesanan, bukti_bayar, is_valid) VALUES
(7, '2026-05-08 09:00:00', 'Diproses', '\x', TRUE),
(7, '2026-05-12 10:15:00', 'Menunggu', '\x', FALSE),
(8, '2026-05-15 16:45:00', 'Diproses', '\x', TRUE),
(8, '2026-05-16 12:00:00', 'Selesai',  '\x', TRUE);

INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES
(4, 11, 'Tim Futsal Fasilkom', 15, 'Size campur, list nyusul'),
(4, 13, 'Kevin',                5, 'Botol warna hitam');

INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES
(5, 3, 'Panitia Konsumsi', 25, 'Ayam geprek pedas sedang');

INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES
(6, 4, 'Reza & Kawan Kost', 10, 'Es teh manis'),
(6, 5, 'Reza',              20, 'Risol mayo anget');

-- Batch 4: PO Binder Kopma (sudah tutup — trigger waktu di-disable sementara)
ALTER TABLE transaction_details DISABLE TRIGGER t_before_insert_detail;

INSERT INTO transaction_details
    (id_transaksi, id_produk, id_po_saat_beli, nama_produk_snapshot,
     nama_penitip, jumlah_pesanan, catatan, harga_satuan_saat_beli, harga_diskon_saat_beli)
VALUES
    (7, 15, 6, 'Binder Aesthetic', 'Reza',         1, 'Binder B5', 35000, NULL),
    (7, 16, 6, 'Notebook Spiral',  'Adiknya Reza', 2, 'Notebook',  15000, NULL);

ALTER TABLE transaction_details ENABLE TRIGGER t_before_insert_detail;

-- Batch 5: transaksi lanjutan
INSERT INTO transactions (id_koordinator, tanggal_transaksi, status_pesanan, bukti_bayar, is_valid) VALUES
(9,  '2026-05-17 08:30:00', 'Selesai',  '\x', TRUE),
(9,  '2026-05-18 13:20:00', 'Diproses', '\x', TRUE),
(10, '2026-05-18 19:10:00', 'Menunggu', '\x', FALSE),
(10, '2026-05-19 10:05:00', 'Selesai',  '\x', TRUE),
(10, '2026-05-19 14:30:00', 'Menunggu', '\x', FALSE);

INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES
(8,  6,  'Andi',           5,  'Ganci'),
(8,  9,  'Andi',           1,  'Flashdisk 32GB'),
(9,  12, 'Tim Voli Putri', 12, 'Jersey set cewek'),
(10, 3,  'Maya',           2,  'Ayam geprek'),
(10, 4,  'Sari',           2,  'Es Teh'),
(11, 8,  'Grup Maba B',    10, 'Lanyard UNEJ'),
(12, 13, 'Maya',           3,  'Botol minum merah');

-- Reviews & Complaints
INSERT INTO reviews (id_produk, id_user, rating, komentar, tanggal_ulasan, balasan_penjual) VALUES
(1,  6,  5, 'Jahitannya rapi banget, worth it!',                   '2026-05-03 10:00:00', 'Terima kasih banyak, Tiara!'),
(6,  6,  4, 'Akriliknya lumayan tebal, tapi agak lama sampainya.', '2026-05-04 12:00:00', 'Maaf ya atas keterlambatannya.'),
(15, 8,  5, 'Bindernya aesthetic parah!',                          '2026-05-17 09:00:00', NULL),
(8,  10, 5, 'Desain lanyardnya keren!',                            '2026-05-19 11:00:00', NULL);

INSERT INTO complaints (id_user, subjek, deskripsi, tanggal, is_selesai, balasan) VALUES
(7,  'Admin BEM Slow Respon',  'Halo, validasi resi saya untuk ayam geprek belum diproses.',     '2026-05-13 08:00:00', TRUE,  'Sudah dibantu follow up ke admin konsumsi BEM.'),
(9,  'Fitur Keranjang Ngebug', 'Waktu saya nambah list titipan, kadang layarnya ngestuck.',      '2026-05-18 14:00:00', FALSE, NULL),
(10, 'Salah Input Nominal',    'Min, saya salah transfer lebih 10 ribu ke BEM. Bisa di-refund?', '2026-05-19 15:30:00', FALSE, NULL);

INSERT INTO activity_logs (id_user, aktivitas, waktu_akses) VALUES
(1, 'Berhasil login ke sistem (Dashboard Admin)',         '2026-05-20 08:00:00'),
(1, 'Mengunduh Laporan PDF (LPJ Danus PO BEM)',           '2026-05-20 08:30:00'),
(2, 'Menambahkan produk baru ke PO BEM Batch 1',          '2026-05-01 10:15:00'),
(7, 'Mengunduh Kuitansi Transaksi #1 (Format PDF)',       '2026-05-12 10:20:00'),
(8, 'Berhasil melakukan checkout keranjang belanja',      '2026-05-15 16:46:00'),
(1, 'Memblokir akun pengguna akibat manipulasi pesanan',  '2026-05-21 14:00:00');


-- ============================================================
-- SECTION 4 : VIEW
-- ============================================================

CREATE OR REPLACE VIEW vw_lpj_danus_per_po AS
SELECT
    po.id_po,
    po.id_penjual,
    po.judul_po,
    po.jenis_po,
    po.batas_waktu,
    p.id_produk,
    p.nama_produk,
    p.harga_dasar,
    p.harga_diskon,
    COALESCE(agg.total_barang_terjual,    0) AS total_barang_terjual,
    COALESCE(agg.omzet_kotor,            0) AS omzet_kotor,
    COALESCE(agg.total_refund_dicairkan, 0) AS total_refund_dicairkan,
    COALESCE(agg.omzet_bersih_lpj,       0) AS omzet_bersih_lpj,
    CASE
        WHEN po.is_aktif = TRUE AND po.batas_waktu >= NOW() THEN 'Sedang Berjalan'
        WHEN po.is_aktif = TRUE AND po.batas_waktu  < NOW() THEN 'Batas Waktu Habis'
        ELSE 'Ditutup'
    END AS status_po
FROM preorders po
JOIN products p ON po.id_po = p.id_po
LEFT JOIN (
    SELECT
        td.id_produk,
        SUM(td.jumlah_pesanan)                                                          AS total_barang_terjual,
        SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli)                              AS omzet_kotor,
        SUM(COALESCE(td.selisih_refund, 0))                                             AS total_refund_dicairkan,
        SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)) AS omzet_bersih_lpj
    FROM transaction_details td
    JOIN transactions t ON td.id_transaksi = t.id_transaksi
    WHERE t.status_pesanan = 'Selesai'
    GROUP BY td.id_produk
) agg ON p.id_produk = agg.id_produk
WHERE po.is_deleted = FALSE;

-- ── Dipanggil: SELECT * FROM vw_lpj_danus_per_po;
-- SELECT * FROM vw_lpj_danus_per_po;

CREATE OR REPLACE VIEW vw_lpj_pesanan_aktif_per_po AS
SELECT
    p.id_produk,
    SUM(td.jumlah_pesanan) AS unit_pending
FROM transaction_details td
JOIN transactions t ON td.id_transaksi = t.id_transaksi
JOIN products p ON td.id_produk = p.id_produk
WHERE t.status_pesanan != 'Selesai'
GROUP BY p.id_produk;

-- ── Dipanggil: SELECT * FROM vw_lpj_pesanan_aktif_per_po;
-- SELECT * FROM vw_lpj_pesanan_aktif_per_po;

CREATE OR REPLACE VIEW vw_log_aktivitas AS
SELECT
    al.id_log,
    al.id_user,
    u.nama  AS pelaku,
    u.peran,
    al.aktivitas,
    al.waktu_akses
FROM activity_logs al
JOIN users u ON al.id_user = u.id_user;

-- ── Dipanggil: SELECT * FROM vw_log_aktivitas ORDER BY waktu_akses DESC;
-- SELECT * FROM vw_log_aktivitas ORDER BY waktu_akses DESC;

CREATE OR REPLACE VIEW vw_detail_pesanan_pembeli AS
SELECT
    t.id_transaksi,
    TO_CHAR(t.tanggal_transaksi, 'DD Mon YYYY, HH24:MI') AS tanggal_transaksi,
    t.status_pesanan,
    t.bukti_bayar,
    COALESCE(td.nama_produk_snapshot, '-')               AS nama_produk,
    td.nama_penitip,
    td.jumlah_pesanan                                    AS jumlah,
    td.harga_satuan_saat_beli                            AS harga_satuan,
    (td.jumlah_pesanan * td.harga_satuan_saat_beli)      AS subtotal,
    COALESCE(td.catatan, '-')                            AS catatan,
    COALESCE(td.selisih_refund, 0)                       AS selisih_refund
FROM transactions t
JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
ORDER BY t.id_transaksi, td.nama_penitip, td.nama_produk_snapshot;

-- ── Dipanggil: SELECT * FROM vw_detail_pesanan_pembeli;
-- SELECT * FROM vw_detail_pesanan_pembeli;

CREATE OR REPLACE VIEW vw_detail_pesanan_penjual AS
SELECT
    t.id_transaksi,
    u.nama                                               AS nama_pembeli,
    u.nomor_telepon                                      AS nomor_telepon,
    TO_CHAR(t.tanggal_transaksi, 'DD Mon YYYY, HH24:MI') AS tanggal_transaksi,
    t.status_pesanan,
    t.bukti_bayar,
    p.id_penjual,
    td.id_produk,
    COALESCE(td.nama_produk_snapshot, '-')               AS nama_produk,
    td.nama_penitip,
    td.jumlah_pesanan                                    AS jumlah,
    td.harga_satuan_saat_beli                            AS harga_satuan,
    (td.jumlah_pesanan * td.harga_satuan_saat_beli)      AS subtotal,
    COALESCE(td.catatan, '-')                            AS catatan,
    COALESCE(td.selisih_refund, 0)                       AS selisih_refund
FROM transactions t
JOIN users u ON t.id_koordinator = u.id_user
JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
JOIN products p ON td.id_produk = p.id_produk;

-- ── Dipanggil: SELECT * FROM vw_detail_pesanan_penjual WHERE id_penjual = 2;
-- SELECT * FROM vw_detail_pesanan_penjual WHERE id_penjual = 2;

CREATE OR REPLACE VIEW vw_pesanan_masuk_penjual AS
SELECT
    t.id_transaksi,
    u.nama                                                                    AS nama_pembeli,
    u.nomor_telepon                                                           AS nomor_telepon,
    t.tanggal_transaksi,
    t.status_pesanan,
    p.id_penjual,
    COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0)           AS total_harga_lapak
FROM transactions t
JOIN users u ON t.id_koordinator = u.id_user
JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
JOIN products p ON td.id_produk = p.id_produk
GROUP BY t.id_transaksi, u.nama, u.nomor_telepon, t.tanggal_transaksi, t.status_pesanan, p.id_penjual;

-- ── Dipanggil: SELECT * FROM vw_pesanan_masuk_penjual WHERE id_penjual = 3;
-- SELECT * FROM vw_pesanan_masuk_penjual WHERE id_penjual = 3;

CREATE OR REPLACE VIEW vw_semua_user AS
SELECT
    u.id_user,
    u.nama,
    u.username,
    COALESCE(u.email, '-')         AS email,
    COALESCE(u.nomor_telepon, '-') AS nomor_telepon,
    u.peran,
    CASE WHEN u.is_diblokir = TRUE THEN 'Diblokir' ELSE 'Aktif' END AS status_akun
FROM users u
ORDER BY u.peran, u.nama;

-- ── Dipanggil: SELECT * FROM vw_semua_user;
-- SELECT * FROM vw_semua_user;

CREATE OR REPLACE VIEW vw_ulasan_penjual AS
SELECT
    r.id_ulasan,
    p.id_penjual,
    p.nama_produk,
    u.nama           AS nama_pembeli,
    r.rating,
    r.komentar,
    r.tanggal_ulasan,
    r.balasan_penjual
FROM reviews r
JOIN products p ON r.id_produk = p.id_produk
JOIN users   u ON r.id_user   = u.id_user
ORDER BY r.tanggal_ulasan DESC;

-- ── Dipanggil: SELECT * FROM vw_ulasan_penjual WHERE id_penjual = 2;
-- SELECT * FROM vw_ulasan_penjual WHERE id_penjual = 2;

CREATE OR REPLACE VIEW vw_aduan_pending AS
SELECT
    c.id_aduan,
    c.id_user,
    u.nama  AS nama_pelapor,
    c.subjek,
    c.deskripsi,
    c.tanggal
FROM complaints c
JOIN users u ON c.id_user = u.id_user
WHERE c.is_selesai = FALSE
ORDER BY c.tanggal ASC;

-- ── Dipanggil: SELECT * FROM vw_aduan_pending;
-- SELECT * FROM vw_aduan_pending;

CREATE OR REPLACE VIEW vw_activity_log AS
SELECT
    al.id_log,
    u.nama      AS pelaku,
    u.peran,
    al.aktivitas,
    al.waktu_akses
FROM activity_logs al
JOIN users u ON al.id_user = u.id_user
ORDER BY al.waktu_akses DESC;

-- ── Dipanggil: SELECT * FROM vw_activity_log LIMIT 10;
-- SELECT * FROM vw_activity_log LIMIT 10;

CREATE OR REPLACE VIEW vw_verifikasi_pending AS
SELECT
    v.id_user,
    u.nama      AS nama_owner,
    v.nim,
    v.nama_toko,
    v.tahun_masuk,
    v.bukti_ktm
FROM verifications v
JOIN users u ON v.id_user = u.id_user
WHERE v.is_verifikasi = FALSE
ORDER BY v.id_verifikasi ASC;

-- ── Dipanggil: SELECT * FROM vw_verifikasi_pending;
-- SELECT * FROM vw_verifikasi_pending;

CREATE OR REPLACE VIEW vw_produk_per_penjual AS
SELECT
    p.id_produk,
    p.id_penjual,
    p.id_po,
    p.id_kategori,
    p.nama_produk,
    p.deskripsi,
    p.harga_dasar,
    p.harga_diskon,
    p.target_kuota,
    p.min_order,
    p.foto_produk,
    k.nama_kategori,
    COALESCE(po.judul_po, '-')     AS judul_po,
    COALESCE(po.jenis_po, 'Biasa') AS jenis_po,
    CASE WHEN p.id_po IS NULL THEN FALSE ELSE TRUE END AS in_sesi_po
FROM products p
JOIN categories k ON p.id_kategori = k.id_kategori
LEFT JOIN preorders po ON p.id_po = po.id_po
WHERE p.is_deleted = FALSE
ORDER BY p.id_produk DESC;

-- ── Dipanggil: SELECT * FROM vw_produk_per_penjual WHERE id_penjual = 4;
-- SELECT * FROM vw_produk_per_penjual WHERE id_penjual = 4;

CREATE OR REPLACE VIEW vw_leaderboard_penjual AS
SELECT
    u.nama AS nama_penjual,
    COALESCE(SUM(
        (td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)
    ), 0) AS total_omzet_bersih,
    CASE
        WHEN COALESCE(SUM(
            (td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)
        ), 0) >= 500000 THEN 'Seller Sultan'
        WHEN COALESCE(SUM(
            (td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)
        ), 0) >= 100000 THEN 'Seller Menengah'
        ELSE 'Seller Newbie'
    END AS tier_penjual
FROM transaction_details td
JOIN products p ON td.id_produk = p.id_produk
JOIN users    u ON p.id_penjual = u.id_user
JOIN transactions t ON td.id_transaksi = t.id_transaksi
WHERE t.status_pesanan = 'Selesai'
GROUP BY u.nama
ORDER BY total_omzet_bersih DESC;

-- ── Dipanggil: SELECT * FROM vw_leaderboard_penjual;
-- SELECT * FROM vw_leaderboard_penjual;

CREATE OR REPLACE VIEW vw_produk_hampir_penuh AS
SELECT
    p.id_produk,
    p.nama_produk,
    po.judul_po,
    p.harga_dasar,
    p.target_kuota,
    COALESCE(SUM(td.jumlah_pesanan), 0)                  AS terisi,
    p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0) AS sisa_kuota,
    p.foto_produk
FROM products p
JOIN preorders po ON p.id_po = po.id_po
LEFT JOIN transaction_details td ON p.id_produk = td.id_produk
WHERE po.is_aktif    = TRUE
  AND po.is_deleted  = FALSE
  AND po.batas_waktu >= CURRENT_TIMESTAMP
  AND p.target_kuota IS NOT NULL
  AND p.is_deleted   = FALSE
GROUP BY p.id_produk, p.nama_produk, po.judul_po, p.harga_dasar, p.target_kuota, p.foto_produk
HAVING (p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0)) <= 10
   AND (p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0)) >  0
ORDER BY sisa_kuota ASC;

-- ── Dipanggil: SELECT * FROM vw_produk_hampir_penuh;
-- SELECT * FROM vw_produk_hampir_penuh;

CREATE OR REPLACE VIEW vw_katalog_produk AS
SELECT
    p.id_produk,
    p.id_penjual,
    p.id_po,
    p.nama_produk,
    kat.nama_kategori,
    po.judul_po,
    p.harga_dasar,
    p.harga_diskon,
    po.batas_waktu,
    p.foto_produk,
    COALESCE(v.nama_toko, u.nama)                         AS nama_toko,
    po.jenis_po,
    p.target_kuota,
    CASE WHEN p.id_po IS NULL THEN FALSE ELSE TRUE END     AS in_sesi_po,
    COALESCE((
        SELECT SUM(td.jumlah_pesanan)
        FROM transaction_details td
        JOIN transactions t ON td.id_transaksi = t.id_transaksi
        WHERE td.id_produk = p.id_produk
          AND t.status_pesanan NOT IN ('Batal', 'Gagal')
    ), 0)                                                  AS terpesan
FROM products p
LEFT JOIN preorders   po  ON p.id_po       = po.id_po
LEFT JOIN categories  kat ON p.id_kategori = kat.id_kategori
LEFT JOIN users       u   ON p.id_penjual  = u.id_user
LEFT JOIN verifications v ON p.id_penjual  = v.id_user
WHERE p.is_deleted = FALSE;

-- ── Dipanggil: SELECT * FROM vw_katalog_produk;
-- SELECT * FROM vw_katalog_produk;

CREATE OR REPLACE VIEW vw_transaksi_lengkap AS
SELECT
    t.id_transaksi,
    t.id_koordinator,
    t.tanggal_transaksi,
    t.status_pesanan,
    t.is_valid,
    t.bukti_bayar,
    COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0) AS total_tagihan,
    COALESCE(SUM(COALESCE(td.selisih_refund, 0)), 0)                AS total_cashback
FROM transactions t
LEFT JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
GROUP BY t.id_transaksi, t.id_koordinator, t.tanggal_transaksi,
         t.status_pesanan, t.is_valid, t.bukti_bayar;

-- ── Dipanggil: SELECT * FROM vw_transaksi_lengkap ORDER BY id_transaksi;
-- SELECT * FROM vw_transaksi_lengkap ORDER BY id_transaksi;


-- ============================================================
-- SECTION 5 : FUNCTION (Statement Function / Table Function)
-- ============================================================

-- Statistik ringkas dashboard penjual
CREATE OR REPLACE FUNCTION fn_statistik_dashboard_penjual(p_id_penjual INT)
RETURNS TABLE (
    total_produk_master BIGINT,
    total_po_aktif      BIGINT,
    total_omzet_kotor   BIGINT
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        (SELECT COUNT(id_produk) FROM products  WHERE id_penjual = p_id_penjual),
        (SELECT COUNT(id_po)     FROM preorders WHERE id_penjual = p_id_penjual AND is_aktif = TRUE AND is_deleted = FALSE),
        (SELECT COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0)
         FROM transaction_details td
         JOIN products p ON td.id_produk = p.id_produk
         WHERE p.id_penjual = p_id_penjual);
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_statistik_dashboard_penjual(2);

-- Ringkasan penjualan: total pendapatan & total pesanan selesai
CREATE OR REPLACE FUNCTION fn_ringkasan_penjualan(p_id_penjual INT)
RETURNS TABLE (total_pendapatan BIGINT, total_pesanan BIGINT) AS $$
BEGIN
    RETURN QUERY
    SELECT
        COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0) AS total_pendapatan,
        COUNT(DISTINCT t.id_transaksi)                                   AS total_pesanan
    FROM transactions t
    JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
    JOIN products p ON td.id_produk = p.id_produk
    WHERE p.id_penjual    = p_id_penjual
      AND t.status_pesanan = 'Selesai';
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_ringkasan_penjualan(3);

-- Ringkasan pesanan aktif (belum Selesai): jumlah & estimasi nilai
CREATE OR REPLACE FUNCTION fn_ringkasan_pesanan_aktif(p_id_penjual INT)
RETURNS TABLE (total_pesanan_aktif BIGINT, total_nilai_aktif BIGINT) AS $$
BEGIN
    RETURN QUERY
    SELECT
        COUNT(DISTINCT t.id_transaksi)                                   AS total_pesanan_aktif,
        COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0) AS total_nilai_aktif
    FROM transactions t
    JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
    JOIN products p ON td.id_produk = p.id_produk
    WHERE p.id_penjual     = p_id_penjual
      AND t.status_pesanan NOT IN ('Selesai', 'Dibatalkan');
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_ringkasan_pesanan_aktif(3);

-- Riwayat transaksi selesai per pembeli
CREATE OR REPLACE FUNCTION fn_riwayat_cuan_penjual(p_id_penjual INT)
RETURNS TABLE (
    nama_pembeli    TEXT,
    tanggal_pesanan TIMESTAMP,
    total_harga     BIGINT
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        u.nama::TEXT                                                         AS nama_pembeli,
        t.tanggal_transaksi                                                  AS tanggal_pesanan,
        COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0)      AS total_harga
    FROM transactions t
    JOIN users u ON t.id_koordinator = u.id_user
    JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
    JOIN products p ON td.id_produk = p.id_produk
    WHERE p.id_penjual    = p_id_penjual
      AND t.status_pesanan = 'Selesai'
    GROUP BY t.id_transaksi, u.nama, t.tanggal_transaksi
    ORDER BY t.tanggal_transaksi DESC;
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_riwayat_cuan_penjual(2);

-- Data lengkap user berdasarkan ID
CREATE OR REPLACE FUNCTION fn_get_user_lengkap_by_id(p_id_user INT)
RETURNS TABLE (
    id_user INT, nama TEXT, nomor_telepon TEXT, email TEXT,
    username TEXT, password TEXT, peran TEXT, is_diblokir BOOLEAN,
    nim TEXT, nama_toko TEXT, tahun_masuk INT, is_verifikasi BOOLEAN, bukti_ktm BYTEA
) AS $$
BEGIN
    RETURN QUERY
    SELECT u.id_user, u.nama::TEXT, u.nomor_telepon::TEXT, u.email::TEXT,
           u.username::TEXT, u.password::TEXT, u.peran::TEXT, u.is_diblokir,
           v.nim::TEXT, v.nama_toko::TEXT, v.tahun_masuk, v.is_verifikasi, v.bukti_ktm
    FROM users u
    LEFT JOIN verifications v ON u.id_user = v.id_user
    WHERE u.id_user = p_id_user;
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_get_user_lengkap_by_id(2);

-- Data lengkap user berdasarkan username
CREATE OR REPLACE FUNCTION fn_get_user_lengkap_by_username(p_username TEXT)
RETURNS TABLE (
    id_user INT, nama TEXT, nomor_telepon TEXT, email TEXT,
    username TEXT, password TEXT, peran TEXT, is_diblokir BOOLEAN,
    nim TEXT, nama_toko TEXT, tahun_masuk INT, is_verifikasi BOOLEAN, bukti_ktm BYTEA
) AS $$
BEGIN
    RETURN QUERY
    SELECT u.id_user, u.nama::TEXT, u.nomor_telepon::TEXT, u.email::TEXT,
           u.username::TEXT, u.password::TEXT, u.peran::TEXT, u.is_diblokir,
           v.nim::TEXT, v.nama_toko::TEXT, v.tahun_masuk, v.is_verifikasi, v.bukti_ktm
    FROM users u
    LEFT JOIN verifications v ON u.id_user = v.id_user
    WHERE u.username = p_username;
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_get_user_lengkap_by_username('nabila');

-- Sesi PO aktif dengan filter keyword
CREATE OR REPLACE FUNCTION fn_sesi_po_aktif(p_keyword TEXT)
RETURNS TABLE (
    id_po         INT,
    nama_sesi     TEXT,
    jenis_po      TEXT,
    nama_toko     TEXT,
    jumlah_produk BIGINT,
    harga_min     BIGINT,
    harga_max     BIGINT,
    deadline      TIMESTAMP,
    is_aktif      BOOLEAN
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        po.id_po,
        po.judul_po::TEXT         AS nama_sesi,
        po.jenis_po::TEXT,
        v.nama_toko::TEXT,
        COUNT(p.id_produk)        AS jumlah_produk,
        COALESCE(MIN(p.harga_dasar), 0)::BIGINT AS harga_min,
        COALESCE(MAX(p.harga_dasar), 0)::BIGINT AS harga_max,
        po.batas_waktu            AS deadline,
        po.is_aktif
    FROM preorders po
    JOIN verifications v ON po.id_penjual = v.id_user
    LEFT JOIN products p ON po.id_po = p.id_po AND p.is_deleted = FALSE
    WHERE po.is_aktif    = TRUE
      AND po.is_deleted  = FALSE
      AND po.batas_waktu >= CURRENT_TIMESTAMP
      AND (po.judul_po   ILIKE '%' || p_keyword || '%'
           OR v.nama_toko ILIKE '%' || p_keyword || '%')
    GROUP BY po.id_po, po.judul_po, po.jenis_po,
             v.nama_toko, po.batas_waktu, po.is_aktif
    ORDER BY po.batas_waktu ASC;
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_sesi_po_aktif('');

-- Daftar PO milik penjual (opsional filter hanya aktif)
CREATE OR REPLACE FUNCTION fn_po_by_penjual(p_id_penjual INT, p_aktif_saja BOOLEAN DEFAULT FALSE)
RETURNS TABLE (
    id_po         INT,
    judul_po      TEXT,
    jenis_po      TEXT,
    info_rekening TEXT,
    batas_waktu   TIMESTAMP,
    is_aktif      BOOLEAN
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        po.id_po,
        po.judul_po::TEXT,
        po.jenis_po::TEXT,
        po.info_rekening::TEXT,
        po.batas_waktu,
        po.is_aktif
    FROM preorders po
    WHERE po.id_penjual = p_id_penjual
      AND po.is_deleted = FALSE
      AND (NOT p_aktif_saja
           OR (po.is_aktif = TRUE AND po.batas_waktu > NOW()))
    ORDER BY
        CASE WHEN p_aktif_saja     THEN po.batas_waktu END ASC,
        CASE WHEN NOT p_aktif_saja THEN po.batas_waktu END DESC;
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_po_by_penjual(2);
-- SELECT * FROM fn_po_by_penjual(3, TRUE);

-- Produk yang bisa diulas oleh pembeli tertentu
CREATE OR REPLACE FUNCTION fn_produk_bisa_diulas(p_id_user INT)
RETURNS TABLE (id_produk INT, nama_produk TEXT) AS $$
BEGIN
    RETURN QUERY
    SELECT DISTINCT p.id_produk, p.nama_produk::TEXT
    FROM transaction_details td
    JOIN transactions t ON td.id_transaksi = t.id_transaksi
    JOIN products     p ON td.id_produk    = p.id_produk
    WHERE t.id_koordinator = p_id_user
      AND t.status_pesanan IN ('Diproses', 'Selesai');
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_produk_bisa_diulas(6);

-- Riwayat aduan seorang user
CREATE OR REPLACE FUNCTION fn_riwayat_aduan_user(p_id_user INT)
RETURNS TABLE (
    subjek     TEXT,
    deskripsi  TEXT,
    tanggal    TIMESTAMP,
    is_selesai BOOLEAN,
    balasan    TEXT
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        c.subjek::TEXT,
        c.deskripsi::TEXT,
        c.tanggal,
        c.is_selesai,
        c.balasan::TEXT
    FROM complaints c
    WHERE c.id_user = p_id_user
    ORDER BY c.tanggal DESC;
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_riwayat_aduan_user(7);

-- Nama toko berdasarkan ID produk (scalar function)
CREATE OR REPLACE FUNCTION fn_nama_toko_by_produk(p_id_produk INT)
RETURNS TEXT AS $$
    SELECT COALESCE(v.nama_toko, u.nama)
    FROM products p
    JOIN users u ON p.id_penjual = u.id_user
    LEFT JOIN verifications v ON p.id_penjual = v.id_user
    WHERE p.id_produk = p_id_produk AND p.is_deleted = FALSE
    LIMIT 1;
$$ LANGUAGE sql;

-- ── Dipanggil:
-- SELECT fn_nama_toko_by_produk(6);

-- Seluruh detail transaksi seorang pembeli
CREATE OR REPLACE FUNCTION fn_transaksi_lengkap_pembeli(p_id_pembeli INT)
RETURNS TABLE (
    id_transaksi           INT,
    id_koordinator         INT,
    tanggal_transaksi      TIMESTAMP,
    status_pesanan         TEXT,
    is_valid               BOOLEAN,
    bukti_bayar            BYTEA,
    id_produk              INT,
    nama_penitip           TEXT,
    jumlah_pesanan         INT,
    catatan                TEXT,
    nama_produk_snapshot   TEXT,
    harga_satuan_saat_beli INT,
    harga_diskon_saat_beli INT,
    selisih_refund         INT
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        t.id_transaksi,
        t.id_koordinator,
        t.tanggal_transaksi,
        t.status_pesanan::TEXT,
        t.is_valid,
        t.bukti_bayar,
        td.id_produk,
        td.nama_penitip::TEXT,
        td.jumlah_pesanan,
        td.catatan::TEXT,
        td.nama_produk_snapshot::TEXT,
        td.harga_satuan_saat_beli,
        td.harga_diskon_saat_beli,
        COALESCE(td.selisih_refund, 0)
    FROM transactions t
    LEFT JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
    WHERE t.id_koordinator = p_id_pembeli
    ORDER BY t.tanggal_transaksi DESC, td.nama_penitip, td.nama_produk_snapshot;
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_transaksi_lengkap_pembeli(6);

-- Detail transaksi berdasarkan ID transaksi
CREATE OR REPLACE FUNCTION fn_transaksi_by_id(p_id_transaksi INT)
RETURNS TABLE (
    id_transaksi           INT,
    id_koordinator         INT,
    tanggal_transaksi      TIMESTAMP,
    status_pesanan         TEXT,
    is_valid               BOOLEAN,
    bukti_bayar            BYTEA,
    id_produk              INT,
    nama_penitip           TEXT,
    jumlah_pesanan         INT,
    catatan                TEXT,
    nama_produk_snapshot   TEXT,
    harga_satuan_saat_beli INT,
    harga_diskon_saat_beli INT,
    selisih_refund         INT
) AS $$
BEGIN
    RETURN QUERY
    SELECT
        t.id_transaksi,
        t.id_koordinator,
        t.tanggal_transaksi,
        t.status_pesanan::TEXT,
        t.is_valid,
        t.bukti_bayar,
        td.id_produk,
        td.nama_penitip::TEXT,
        td.jumlah_pesanan,
        td.catatan::TEXT,
        td.nama_produk_snapshot::TEXT,
        td.harga_satuan_saat_beli,
        td.harga_diskon_saat_beli,
        COALESCE(td.selisih_refund, 0)
    FROM transactions t
    LEFT JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
    WHERE t.id_transaksi = p_id_transaksi;
END;
$$ LANGUAGE plpgsql;

-- ── Dipanggil:
-- SELECT * FROM fn_transaksi_by_id(2);


-- ============================================================
-- SECTION 6 : STORED PROCEDURE
-- ============================================================

-- SP 1: Tindak penjual nakal (blokir + tutup aduan)
CREATE OR REPLACE PROCEDURE sp_tindak_penjual_nakal(
    p_id_aduan   INT,
    p_id_penjual INT,
    p_balasan    TEXT
)
LANGUAGE plpgsql AS $$
BEGIN
    IF NOT EXISTS (SELECT 1 FROM complaints WHERE id_aduan = p_id_aduan) THEN
        RAISE EXCEPTION 'Aduan dengan id_aduan=% tidak ditemukan.', p_id_aduan;
    END IF;
    IF NOT EXISTS (SELECT 1 FROM users WHERE id_user = p_id_penjual) THEN
        RAISE EXCEPTION 'Pengguna dengan id_user=% tidak ditemukan.', p_id_penjual;
    END IF;

    UPDATE complaints
    SET is_selesai = TRUE, balasan = p_balasan
    WHERE id_aduan = p_id_aduan;

    UPDATE users
    SET is_diblokir = TRUE
    WHERE id_user = p_id_penjual;
END;
$$;

-- ── Cara memanggil SP (tidak perlu di-run untuk setup, hanya referensi):
/*
CALL sp_tindak_penjual_nakal(2, 3, 'Akun diblokir karena pelanggaran berulang.');
*/

-- SP 2: Hitung ulang cashback Gotong Royong secara manual
CREATE OR REPLACE PROCEDURE sp_recalculate_cashback_gr(
    p_id_produk    INT,
    p_id_po        INT,
    p_harga_dasar  BIGINT,
    p_harga_diskon BIGINT,
    OUT p_sukses   BOOLEAN,
    OUT p_pesan    TEXT
)
LANGUAGE plpgsql AS $$
DECLARE
    v_total_terpesan INT;
    v_target_kuota   INT;
    v_selisih        BIGINT;
    v_affected       INT;
BEGIN
    SELECT target_kuota INTO v_target_kuota FROM products WHERE id_produk = p_id_produk;

    SELECT COALESCE(SUM(td.jumlah_pesanan), 0) INTO v_total_terpesan
    FROM transaction_details td
    JOIN transactions t ON td.id_transaksi = t.id_transaksi
    WHERE td.id_produk       = p_id_produk
      AND td.id_po_saat_beli = p_id_po
      AND t.status_pesanan   NOT IN ('Dibatalkan', 'Batal', 'Gagal');

    IF v_total_terpesan < v_target_kuota THEN
        p_sukses := FALSE;
        p_pesan  := 'Kuota belum terpenuhi (' || v_total_terpesan || '/' || v_target_kuota || ')';
        RETURN;
    END IF;

    v_selisih := p_harga_dasar - p_harga_diskon;
    IF v_selisih <= 0 THEN
        p_sukses := FALSE;
        p_pesan  := 'Selisih cashback tidak valid.';
        RETURN;
    END IF;

    UPDATE transaction_details td
    SET selisih_refund = td.jumlah_pesanan * v_selisih
    FROM transactions t
    WHERE td.id_transaksi    = t.id_transaksi
      AND td.id_produk       = p_id_produk
      AND td.id_po_saat_beli = p_id_po
      AND td.selisih_refund  = 0
      AND t.status_pesanan   NOT IN ('Dibatalkan', 'Batal', 'Gagal');

    GET DIAGNOSTICS v_affected = ROW_COUNT;
    p_sukses := TRUE;
    p_pesan  := 'Cashback diupdate untuk ' || v_affected || ' baris titipan.';
END;
$$;

-- ── Cara memanggil SP (tidak perlu di-run untuk setup, hanya referensi):
/*
CALL sp_recalculate_cashback_gr(6, 3, 15000, 10000, NULL, NULL);
*/


-- ============================================================
-- SECTION 7 : TRANSACTION
--   Implementasi nyata ada di C# (TransactionRepository.cs).
--   Blok di bawah ini adalah contoh padanan SQL-murni
--   yang tidak perlu dijalankan di pgAdmin.
-- ============================================================

/*
-- Contoh simulasi transaksi SQL (BEGIN … COMMIT / ROLLBACK)
-- Ini TIDAK dijalankan karena flow sesungguhnya dikerjakan
-- oleh aplikasi C# menggunakan NpgsqlTransaction.

BEGIN;

    -- 1. Buat header transaksi
    INSERT INTO transactions (id_koordinator, bukti_bayar, status_pesanan)
    VALUES (6, '\x', 'Menunggu')
    RETURNING id_transaksi;
    -- Misal id_transaksi = 99

    -- 2. Insert item pertama (trigger otomatis isi snapshot & harga)
    INSERT INTO transaction_details
        (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan)
    VALUES (99, 8, 'Budi', 2, 'Lanyard biru');
    -- Trigger t_before_insert_detail  → isi nama_produk_snapshot, harga_satuan_saat_beli
    -- Trigger trg_cek_waktu_po        → tolak jika PO sudah tutup
    -- Trigger t_after_insert_detail   → hitung refund GR jika kuota terpenuhi

    -- 3. Insert item kedua
    INSERT INTO transaction_details
        (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan)
    VALUES (99, 9, 'Budi', 1, 'Flashdisk 32GB');

COMMIT;
-- Jika ada error di salah satu step → ROLLBACK otomatis

-- Panduan C# (dari TransactionRepository.cs):
--   using var conn  = new NpgsqlConnection(_connStr);
--   await conn.OpenAsync();
--   using var dbTx  = await conn.BeginTransactionAsync();
--   try {
--       var cmdHeader = new NpgsqlCommand(@"
--           INSERT INTO transactions (id_koordinator, bukti_bayar, status_pesanan)
--           VALUES (@koord, @bukti, 'Menunggu') RETURNING id_transaksi", conn, dbTx);
--       int idTransaksi = (int)await cmdHeader.ExecuteScalarAsync();
--
--       foreach (var item in daftarItem) {
--           var cmdDetail = new NpgsqlCommand(@"
--               INSERT INTO transaction_details
--                   (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan)
--               VALUES (@trx, @produk, @penitip, @jumlah, @catatan)", conn, dbTx);
--           await cmdDetail.ExecuteNonQueryAsync();
--       }
--       await dbTx.CommitAsync();
--   } catch {
--       await dbTx.RollbackAsync();
--       throw;
--   }
*/


-- ============================================================
-- SECTION 8 : GROUP BY — CUBE, ROLLUP, GROUPING SETS
-- ============================================================

-- 8a. GROUP BY biasa: total barang terjual per produk
SELECT
    td.nama_produk_snapshot AS nama_produk,
    SUM(td.jumlah_pesanan)  AS total_terjual
FROM transaction_details td
GROUP BY td.nama_produk_snapshot
ORDER BY total_terjual DESC;

-- 8b. CUBE: kombinasi silang Kategori × Jenis PO
--     (menghasilkan subtotal tiap kombinasi + grand total)
SELECT
    COALESCE(kat.nama_kategori, 'Semua Kategori')    AS kategori,
    COALESCE(po.jenis_po, 'Tanpa PO / Semua Jenis') AS jenis_po,
    SUM(td.jumlah_pesanan)                           AS total_barang_terjual
FROM transaction_details td
JOIN      products    p   ON td.id_produk  = p.id_produk
LEFT JOIN preorders   po  ON p.id_po       = po.id_po AND po.is_deleted = FALSE
LEFT JOIN categories  kat ON p.id_kategori = kat.id_kategori
GROUP BY CUBE (kat.nama_kategori, po.jenis_po);

-- 8c. ROLLUP: hierarki waktu → omzet per bulan → per tahun → grand total
SELECT
    EXTRACT(YEAR  FROM t.tanggal_transaksi)                                                   AS tahun,
    EXTRACT(MONTH FROM t.tanggal_transaksi)                                                   AS bulan,
    SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli)                                        AS omzet_kotor,
    SUM(td.selisih_refund)                                                                    AS total_refund,
    SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0))    AS omzet_bersih
FROM transactions t
JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
WHERE t.status_pesanan = 'Selesai'
GROUP BY ROLLUP (
    EXTRACT(YEAR  FROM t.tanggal_transaksi),
    EXTRACT(MONTH FROM t.tanggal_transaksi)
);

-- 8d. GROUPING SETS: rekap per penjual dan per kategori dalam satu query
SELECT
    u.nama            AS nama_penjual,
    kat.nama_kategori AS nama_kategori,
    SUM(td.jumlah_pesanan) AS unit_terjual
FROM transaction_details td
JOIN transactions  t   ON td.id_transaksi = t.id_transaksi
JOIN products      p   ON td.id_produk    = p.id_produk
JOIN categories    kat ON p.id_kategori   = kat.id_kategori
JOIN users         u   ON p.id_penjual    = u.id_user
GROUP BY GROUPING SETS ((u.nama), (kat.nama_kategori));


-- ============================================================
-- SECTION 9 : SUBQUERY
-- ============================================================

-- 9a. Status ketersediaan kuota (subquery via LEFT JOIN + agregasi)
SELECT
    p.nama_produk,
    p.target_kuota,
    COALESCE(SUM(td.jumlah_pesanan), 0) AS barang_terpesan,
    CASE
        WHEN (p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0)) <= 0
            THEN 'Target Terpenuhi / Habis'
        WHEN (p.target_kuota - COALESCE(SUM(td.jumlah_pesanan), 0)) <= 10
            THEN 'Sisa Kuota Kritis (Peringatan!)'
        ELSE 'Kuota Masih Aman'
    END AS status_ketersediaan
FROM products p
LEFT JOIN transaction_details td ON p.id_produk = td.id_produk
WHERE p.target_kuota IS NOT NULL
  AND p.is_deleted   = FALSE
GROUP BY p.id_produk, p.nama_produk, p.target_kuota;

-- 9b. Deteksi produk dengan sisa kuota <= 5 (correlated subquery)
SELECT nama_produk, target_kuota
FROM products p
WHERE p.target_kuota IS NOT NULL
  AND p.is_deleted   = FALSE
  AND (
        p.target_kuota - (
            SELECT COALESCE(SUM(jumlah_pesanan), 0)
            FROM transaction_details td
            WHERE td.id_produk = p.id_produk
        )
      ) <= 5;

-- 9c. Klasifikasi tier penjual berdasarkan omzet bersih (subquery tersirat via GROUP BY + CASE)
SELECT
    u.nama AS nama_penjual,
    SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)) AS total_omzet_bersih,
    CASE
        WHEN SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)) >= 500000
            THEN 'Seller Sultan (Top Tier)'
        WHEN SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)) >= 100000
            THEN 'Seller Menengah (Mid Tier)'
        ELSE 'Seller Pemula (Newbie)'
    END AS tier_penjual
FROM transaction_details td
JOIN products p ON td.id_produk = p.id_produk
JOIN users    u ON p.id_penjual = u.id_user
GROUP BY u.nama
ORDER BY total_omzet_bersih DESC;

-- 9d. Subquery dalam vw_katalog_produk — jumlah terpesan per produk
--     (contoh scalar correlated subquery dalam SELECT)
SELECT
    p.id_produk,
    p.nama_produk,
    COALESCE((
        SELECT SUM(td.jumlah_pesanan)
        FROM transaction_details td
        JOIN transactions t ON td.id_transaksi = t.id_transaksi
        WHERE td.id_produk = p.id_produk
          AND t.status_pesanan NOT IN ('Batal', 'Gagal')
    ), 0) AS total_terpesan
FROM products p
WHERE p.is_deleted = FALSE;


-- ============================================================
-- SECTION 10 : TEORI HIMPUNAN (UNION / UNION ALL / INTERSECT / EXCEPT)
-- ============================================================

-- 10a. UNION: transaksi berstatus 'Diproses' dan 'Selesai' (duplikat dihilangkan)
SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Diproses'
UNION
SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Selesai';

-- 10b. UNION ALL: sama seperti di atas tetapi duplikat TIDAK dihilangkan
SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Diproses'
UNION ALL
SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Selesai';

-- 10c. INTERSECT: penjual terverifikasi yang sekaligus pernah menjadi koordinator transaksi
SELECT id_user, nama FROM users
WHERE id_user IN (SELECT id_user FROM verifications WHERE is_verifikasi = TRUE)
INTERSECT
SELECT u.id_user, u.nama FROM users u
JOIN transactions t ON u.id_user = t.id_koordinator;

-- 10d. EXCEPT: user yang BELUM pernah melakukan transaksi (pengguna pasif)
SELECT id_user, nama FROM users
EXCEPT
SELECT u.id_user, u.nama FROM users u
JOIN transactions t ON u.id_user = t.id_koordinator;

-- 10e. Advanced Logging — 5 aktivitas terbaru (memanfaatkan view)
SELECT
    pelaku,
    aktivitas,
    waktu_akses
FROM vw_log_aktivitas
ORDER BY waktu_akses DESC
LIMIT 5;