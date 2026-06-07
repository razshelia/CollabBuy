-- INISIALISASI DATABASE COLLABBUY
-- Dibuat pada: 23 Mei 2026 (Diperbarui dengan perbaikan Snapshot PO)
-- PASSWORD LOGIN: password123

DROP SCHEMA public CASCADE;
CREATE SCHEMA public;

-- BAGIAN 1: DDL (DATA DEFINITION LANGUAGE)
CREATE TABLE users (
    id_user SERIAL PRIMARY KEY,
    nama VARCHAR(100) NOT NULL,
    nomor_telepon VARCHAR(20) NOT NULL,
    email VARCHAR(100) UNIQUE,
    username VARCHAR(50) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    peran VARCHAR(20) DEFAULT 'User',
    is_diblokir BOOLEAN DEFAULT FALSE
);

CREATE TABLE verifications (
    id_verifikasi SERIAL PRIMARY KEY,
    id_user INTEGER REFERENCES users (id_user) ON DELETE CASCADE UNIQUE NOT NULL,
    nim VARCHAR(20) UNIQUE NOT NULL,
    nama_toko VARCHAR(100) NOT NULL,
    bukti_ktm BYTEA NOT NULL, 
    tahun_masuk INTEGER NOT NULL,
    is_verifikasi BOOLEAN DEFAULT FALSE
);

CREATE TABLE categories (
    id_kategori SERIAL PRIMARY KEY,
    nama_kategori VARCHAR(100) NOT NULL
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE preorders (
    id_po SERIAL PRIMARY KEY,
    id_penjual INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    judul_po VARCHAR(150) NOT NULL,
    jenis_po VARCHAR(50) NOT NULL,
    info_rekening VARCHAR(255) NOT NULL,
    batas_waktu TIMESTAMP NOT NULL,
    is_aktif BOOLEAN DEFAULT TRUE,
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE products (
    id_produk SERIAL PRIMARY KEY,
    id_penjual INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    id_po INTEGER REFERENCES preorders (id_po) ON DELETE SET NULL,
    id_kategori INTEGER REFERENCES categories (id_kategori) ON DELETE RESTRICT NOT NULL,
    nama_produk VARCHAR(150) NOT NULL,
    deskripsi TEXT,
    harga_dasar INTEGER NOT NULL,
    harga_diskon INTEGER,
    target_kuota INTEGER,
    min_order INTEGER DEFAULT 1,
    foto_produk BYTEA,
    is_deleted BOOLEAN NOT NULL DEFAULT FALSE
);

CREATE TABLE transactions (
    id_transaksi SERIAL PRIMARY KEY,
    id_koordinator INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    tanggal_transaksi TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    status_pesanan VARCHAR(50) DEFAULT 'Menunggu',
    bukti_bayar BYTEA,
    is_valid BOOLEAN DEFAULT FALSE
);

CREATE TABLE transaction_details (
    id_detail SERIAL PRIMARY KEY,
    id_transaksi INTEGER REFERENCES transactions (id_transaksi) ON DELETE CASCADE,
    id_produk INTEGER REFERENCES products (id_produk) ON DELETE RESTRICT,
    id_po_saat_beli INTEGER REFERENCES preorders(id_po), 
    nama_produk_snapshot VARCHAR(150) NOT NULL,
    nama_penitip VARCHAR(100) NOT NULL,
    jumlah_pesanan INTEGER NOT NULL,
    catatan VARCHAR(255),
    harga_satuan_saat_beli INTEGER NOT NULL DEFAULT 0,
    harga_diskon_saat_beli INTEGER,
    selisih_refund INTEGER DEFAULT 0,
    CONSTRAINT uq_detail_transaksi_produk_penitip 
        UNIQUE (id_transaksi, id_produk, nama_penitip)
);

CREATE TABLE complaints (
    id_aduan SERIAL PRIMARY KEY,
    id_user INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    subjek VARCHAR(150) NOT NULL,
    deskripsi TEXT NOT NULL,
    tanggal TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    is_selesai BOOLEAN DEFAULT FALSE,
    balasan TEXT
);

CREATE TABLE reviews (
    id_ulasan SERIAL PRIMARY KEY,
    id_produk INTEGER REFERENCES products (id_produk) ON DELETE CASCADE,
    id_user INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    rating INTEGER CHECK (rating >= 1 AND rating <= 5),
    komentar TEXT,
    tanggal_ulasan TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    balasan_penjual TEXT
);

CREATE TABLE activity_logs (
    id_log SERIAL PRIMARY KEY,
    id_user INTEGER REFERENCES users (id_user) ON DELETE CASCADE,
    aktivitas VARCHAR(255) NOT NULL,
    waktu_akses TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE INDEX idx_products_id_penjual   ON products(id_penjual);
CREATE INDEX idx_products_id_po        ON products(id_po);
CREATE INDEX idx_products_id_kategori  ON products(id_kategori);
CREATE INDEX idx_td_id_transaksi       ON transaction_details(id_transaksi);
CREATE INDEX idx_td_id_produk          ON transaction_details(id_produk);
CREATE INDEX idx_transactions_id_koord ON transactions(id_koordinator);
CREATE INDEX idx_preorders_id_penjual  ON preorders(id_penjual);
CREATE INDEX idx_reviews_id_produk     ON reviews(id_produk);
CREATE INDEX idx_complaints_id_user    ON complaints(id_user);
CREATE INDEX idx_transactions_status_pesanan ON transactions(status_pesanan);
CREATE INDEX idx_activity_logs_id_user ON activity_logs(id_user);

-- BAGIAN 2: DATA MANIPULATION LANGUAGE

INSERT INTO users (nama, nomor_telepon, email, username, password, peran) VALUES
('Rangga Saputra', '081200000001', 'admin@unej.ac.id', 'admin', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'Admin'),
('Nabila BEM', '081200000002', 'nabila@unej.ac.id', 'nabila', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Daffa HMIF', '081200000003', 'daffa@unej.ac.id', 'daffa', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Budi UKM Olahraga', '081200000004', 'budi@unej.ac.id', 'budi', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Siti Kopma', '081200000005', 'siti@unej.ac.id', 'siti', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Tiara (Buyer)', '081200000006', 'tiara@unej.ac.id', 'tiara', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Kevin (Buyer)', '081200000007', 'kevin@unej.ac.id', 'kevin', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Reza (Buyer)', '081200000008', 'reza@unej.ac.id', 'reza', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Andi (Buyer)', '081200000009', 'andi@unej.ac.id', 'andi', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User'),
('Maya (Buyer)', '081200000010', 'maya@unej.ac.id', 'maya', 'ef92b778bafe771e89245b89ecbc08a44a4e166c06659911881f383d4473e94f', 'User');

INSERT INTO verifications (id_user, nim, nama_toko, bukti_ktm, tahun_masuk, is_verifikasi) VALUES
(2, '23010101', 'BEM Store UNEJ', '\x', 2023, TRUE),
(3, '24010102', 'HIMATIF Merch',  '\x', 2024, TRUE),
(4, '22010103', 'UKM Sport Danus','\x', 2022, TRUE),
(5, '23010104', 'Kopma Jember',   '\x', 2023, TRUE);

INSERT INTO categories (nama_kategori) VALUES
('Pakaian & Konveksi'), 
('Aksesoris & Souvenir'), 
('Makanan & Minuman'),
('Peralatan Kuliah'),
('Merchandise Event');

INSERT INTO preorders (id_penjual, judul_po, jenis_po, info_rekening, batas_waktu, is_aktif) VALUES
(2, 'PO PDH BEM Batch 1',         'Biasa',         'BCA 12345 a.n Nabila',    '2026-05-10 23:59:00', FALSE), 
(2, 'PO Makanan Rapat BEM',        'Biasa',         'BCA 12345 a.n Nabila',    CURRENT_TIMESTAMP + INTERVAL '30 days', TRUE),
(3, 'Danus HIMATIF Gotong Royong', 'Gotong Royong', 'Mandiri 98765 a.n Daffa', CURRENT_TIMESTAMP + INTERVAL '15 days', TRUE),
(3, 'PO Lanyard Fasilkom',         'Biasa',         'Mandiri 98765 a.n Daffa', CURRENT_TIMESTAMP + INTERVAL '45 days', TRUE),
(4, 'PO Jersey UKM Olahraga',      'Gotong Royong', 'BRI 112233 a.n Budi',     CURRENT_TIMESTAMP + INTERVAL '20 days', TRUE),
(5, 'PO Binder Kuliah Kopma',      'Biasa',         'BNI 445566 a.n Siti',     '2026-05-15 23:59:00', FALSE);

INSERT INTO products (id_penjual, id_po, id_kategori, nama_produk, deskripsi, harga_dasar, harga_diskon, target_kuota, min_order, foto_produk) VALUES
(2, 1, 1, 'PDH BEM Pengurus',            'Bahan nagata drill.',            120000, NULL,   NULL,  1, '\x'),
(2, 1, 1, 'Kaos Panitia',                'Cotton combed 30s.',             65000, NULL,   NULL,  1, '\x'),
(2, 2, 3, 'Nasi Kotak Ayam Geprek',      'Level pedas 1-5.',               15000, NULL,   NULL,  5, '\x'),
(2, 2, 3, 'Es Teh Manis Jumbo',          'Es teh ukuran 1 liter.',          5000, NULL,   NULL,  2, '\x'),
(2, 2, 3, 'Risol Mayo BEM',              'Isi smoked beef, keju, telur.',   4000, NULL,   NULL, 10, '\x'),
(3, 3, 2, 'Ganci Akrilik Maskot',        'Akrilik tebal 3mm.',             15000, 10000,    50,  1, '\x'),
(3, 3, 2, 'Stiker Pack Hacker',          'Isi 10 stiker coding.',         12000,  8000,    30,  2, '\x'),
(3, 4, 2, 'Lanyard Eksklusif UNEJ',      'Desain terbaru.',               25000, NULL,   NULL,  1, '\x'),
(3, 4, 4, 'Flashdisk Custom Logo',       '32GB Sandisk.',                 85000, NULL,   NULL,  1, '\x'),
(3, NULL, 1, 'Tote Bag Ngoding (Gudang)','Produk belum masuk PO.',        35000, NULL,   NULL,  1, '\x'),
(4, 5, 1, 'Jersey Futsal Set',          'Bahan dryfit.',                110000, 95000,    24, 12, '\x'),
(4, 5, 1, 'Jersey Voli Set',            'Bahan milano.',                115000,100000,    20, 10, '\x'),
(4, 5, 2, 'Botol Minum Sport',          'Kapasitas 1L.',                 45000, 35000,    50,  1, '\x'),
(4, NULL, 2, 'Handuk Kecil Gym',        'Bahan katun.',                  20000, NULL,   NULL,  1, '\x'),
(5, 6, 4, 'Binder Aesthetic',           'Include kertas.',               35000, NULL,   NULL,  1, '\x'),
(5, 6, 4, 'Notebook Spiral',            'Polos 100 lbr.',                15000, NULL,   NULL,  1, '\x'),
(5, 6, 4, 'Pulpen Gel Set',             'Isi 5 pcs.',                    12000, NULL,   NULL,  2, '\x'),
(5, NULL, 3, 'Kopi Botol Kopma',        'Kopi susu gula aren.',          12000, NULL,   NULL,  3, '\x'),
(5, NULL, 3, 'Keripik Singkong',        'Rasa balado.',                   8000, NULL,   NULL,  5, '\x'),
(5, NULL, 5, 'Mug Wisuda',              'Custom foto.',                  25000, NULL,   NULL,  1, '\x');

-- BAGIAN 3: VIEWS
-- 1. VIEW: Katalog Aktif
CREATE OR REPLACE VIEW vw_katalog_aktif AS
SELECT
    p.id_produk,
    po.judul_po,
    kat.nama_kategori,
    p.nama_produk,
    p.harga_dasar,
    p.harga_diskon,
    po.batas_waktu,
    po.info_rekening,
    p.foto_produk
FROM products p
LEFT JOIN preorders   po  ON p.id_po       = po.id_po
LEFT JOIN categories  kat ON p.id_kategori = kat.id_kategori
WHERE
    p.is_deleted = FALSE
   AND (p.id_po IS NULL OR (po.is_aktif = TRUE AND po.batas_waktu >= CURRENT_TIMESTAMP AND po.is_deleted = FALSE));


-- 2. VIEW: Transaksi Lengkap
CREATE OR REPLACE VIEW vw_transaksi_lengkap AS
SELECT
    t.id_transaksi,
    t.id_koordinator,
    u.nama AS nama_koordinator,
    t.tanggal_transaksi,
    t.status_pesanan,
    t.is_valid,
    COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0) AS total_tagihan,
    COALESCE(SUM(td.selisih_refund), 0)                             AS total_cashback
FROM transactions t
LEFT JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
JOIN users u ON t.id_koordinator = u.id_user
GROUP BY t.id_transaksi, t.id_koordinator, u.nama,
         t.tanggal_transaksi, t.status_pesanan, t.is_valid;
 
 
-- 3. VIEW: LPJ Danus per PO
CREATE OR REPLACE VIEW vw_lpj_danus_per_po AS
SELECT
    po.id_po,
    po.judul_po,
    po.jenis_po,
    p.nama_produk,
    COALESCE(SUM(td.jumlah_pesanan), 0)                                                     AS total_barang_terjual,
    COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0)                         AS omzet_kotor,
    COALESCE(SUM(td.selisih_refund), 0)                                                     AS total_refund_dicairkan,
    COALESCE(SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - td.selisih_refund), 0)   AS omzet_bersih_lpj
FROM preorders po
JOIN     products            p  ON po.id_po       = p.id_po
LEFT JOIN transaction_details td ON p.id_produk   = td.id_produk
LEFT JOIN transactions        t  ON td.id_transaksi = t.id_transaksi
    AND t.status_pesanan = 'Selesai'
WHERE po.is_deleted = FALSE
GROUP BY po.id_po, po.judul_po, po.jenis_po, p.nama_produk;
 
 
-- 4. VIEW: Riwayat Log Aktivitas
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


-- BAGIAN 4: PURE FUNCTION & PURE PROCEDURE
-- 1. PURE FUNCTION: Harga saat ini
CREATE OR REPLACE FUNCTION cek_harga_saat_ini(p_id_produk INT)
RETURNS INT AS $$
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
    FROM products
    WHERE id_produk = p_id_produk
    FOR UPDATE;
 
    IF v_id_po IS NULL THEN
        RETURN v_harga_dasar;
    END IF;
 
    SELECT jenis_po INTO v_jenis_po
    FROM preorders
    WHERE id_po = v_id_po
        AND is_deleted = FALSE;
 
    SELECT COALESCE(SUM(jumlah_pesanan), 0) INTO v_total_dipesan
    FROM transaction_details
    WHERE id_produk = p_id_produk;
 
    IF v_jenis_po = 'Gotong Royong' AND v_total_dipesan >= COALESCE(v_target_kuota, 999999) THEN
        RETURN COALESCE(v_harga_diskon, v_harga_dasar);
    ELSE
        RETURN v_harga_dasar;
    END IF;
END;
$$ LANGUAGE plpgsql;
 
 
-- 2. PURE FUNCTION (Table): Statistik dashboard penjual
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
 
 
-- 3. PURE PROCEDURE: Update status massal per PO
CREATE OR REPLACE PROCEDURE sp_update_status_massal_po(
    p_id_po       INT,
    p_status_baru VARCHAR
)
LANGUAGE plpgsql AS $$
BEGIN
    IF NOT EXISTS (
        SELECT 1 FROM preorders
        WHERE id_po    = p_id_po
          AND is_deleted = FALSE
    ) THEN
        RAISE EXCEPTION 'PO dengan id_po=% tidak ditemukan atau sudah dihapus.', p_id_po;
    END IF;
    
    UPDATE transactions
    SET status_pesanan = p_status_baru
    WHERE id_transaksi IN (
        SELECT DISTINCT td.id_transaksi
        FROM transaction_details td
        JOIN products p ON td.id_produk = p.id_produk
        WHERE p.id_po = p_id_po
    );
END;
$$;
 
 
-- 4. PROCEDURE: Tindak Lanjut Aduan dan Pemblokiran Penjual
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

-- BAGIAN 5: TRIGGERS & ARSIP PROCEDURE (TCL DIPINDAH KE C#)
/*
-- Panduan implementasi C# (TransactionRepository.cs):
--   using var conn = new NpgsqlConnection(_connStr);
--   await conn.OpenAsync();
--   using var dbTx = await conn.BeginTransactionAsync();
--   try
--   {
--       // 1. header transaksi (1 baris, 1 id_transaksi)
--       var cmdHeader = new NpgsqlCommand(@"
--           INSERT INTO transactions (id_koordinator, bukti_bayar, status_pesanan)
--           VALUES (@koord, @bukti, 'Menunggu')
--           RETURNING id_transaksi", conn, dbTx);
--       cmdHeader.Parameters.AddWithValue("koord", idKoordinator);
--       cmdHeader.Parameters.AddWithValue("bukti", buktiBayar);  // byte[]
--       int idTransaksi = (int)await cmdHeader.ExecuteScalarAsync();
--
--       // 2. loop insert setiap item keranjang cukup 3 kolom bisnis saja,
--       //    trigger akan mengisi nama_produk_snapshot, harga_satuan_saat_beli,
--       //    dan harga_diskon_saat_beli secara otomatis.
--       foreach (var item in daftarItem)
--       {
--           var cmdDetail = new NpgsqlCommand(@"
--               INSERT INTO transaction_details
--                   (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan)
--               VALUES (@trx, @produk, @penitip, @jumlah, @catatan)",
--               conn, dbTx);
--           cmdDetail.Parameters.AddWithValue("trx",     idTransaksi);
--           cmdDetail.Parameters.AddWithValue("produk",  item.IdProduk);
--           cmdDetail.Parameters.AddWithValue("penitip", item.NamaPenitip);
--           cmdDetail.Parameters.AddWithValue("jumlah",  item.Jumlah);
--           cmdDetail.Parameters.AddWithValue("catatan", item.Catatan ?? (object)DBNull.Value);
--           await cmdDetail.ExecuteNonQueryAsync();
--           // Di balik layar ada
--           // Trigger t_before_insert_detail → isi snapshot & harga otomatis
--           // Trigger trg_cek_waktu_po       → tolak jika PO sudah tutup
--           // Trigger t_after_insert_detail  → hitung refund GR jika kuota terpenuhi
--       }
--       await dbTx.CommitAsync();
--       return idTransaksi;
--   }
--   catch
--   {
--       await dbTx.RollbackAsync();
--       throw;
--   }
*/
 
-- 1. TRIGGER: Cegah produk nyasar ke PO milik orang lain
CREATE OR REPLACE FUNCTION cek_kepemilikan_po() RETURNS TRIGGER AS $$
DECLARE
    v_pemilik_po INTEGER;
BEGIN
    IF NEW.id_po IS NOT NULL THEN
        SELECT id_penjual INTO v_pemilik_po FROM preorders WHERE id_po = NEW.id_po;
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


-- 2. TRIGGER: Otomatis isi snapshot nama produk, harga saat beli, harga diskon saat beli, dan ID PO
CREATE OR REPLACE FUNCTION trg_set_harga_otomatis() RETURNS TRIGGER AS $$
DECLARE
    v_nama_produk  VARCHAR;
    v_harga_diskon INT;
    v_id_po        INT;
BEGIN
    SELECT nama_produk, harga_diskon, id_po
    INTO v_nama_produk, v_harga_diskon, v_id_po
    FROM products
    WHERE id_produk = NEW.id_produk;

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


-- 3. TRIGGER: Tolak pesanan jika batas waktu PO habis
CREATE OR REPLACE FUNCTION cek_validitas_po_saat_beli() RETURNS TRIGGER AS $$
DECLARE
    v_id_po       INT;
    v_is_aktif    BOOLEAN;
    v_batas_waktu TIMESTAMP;
    v_is_deleted  BOOLEAN;
BEGIN
    SELECT id_po INTO v_id_po
    FROM products
    WHERE id_produk = NEW.id_produk;
 
    IF v_id_po IS NULL THEN RETURN NEW; END IF;
 
    SELECT po.is_aktif, po.batas_waktu, po.is_deleted
    INTO v_is_aktif, v_batas_waktu
    FROM preorders po
    WHERE po.id_po = v_id_po;

    IF v_is_deleted = TRUE THEN
        RAISE EXCEPTION 'TRANSAKSI DITOLAK: Sesi PO sudah dihapus!';
    END IF;

    IF v_is_aktif = FALSE THEN
        RAISE EXCEPTION 'TRANSAKSI DITOLAK: Sesi PO sudah ditutup!';
    END IF;
    IF v_batas_waktu < CURRENT_TIMESTAMP THEN
        RAISE EXCEPTION 'TRANSAKSI DITOLAK: Batas waktu PO sudah terlewat!';
    END IF;
 
    RETURN NEW;
END;
$$ LANGUAGE plpgsql;
 
CREATE TRIGGER trg_cek_waktu_po
BEFORE INSERT ON transaction_details
FOR EACH ROW EXECUTE FUNCTION cek_validitas_po_saat_beli();


-- 4. TRIGGER: Otomatis hitung refund Gotong Royong (Disesuaikan menggunakan id_po_saat_beli)
CREATE OR REPLACE FUNCTION trg_hitung_refund_gotong_royong() RETURNS TRIGGER AS $$
DECLARE
    v_id_po          INT;
    v_target         INT;
    v_harga_diskon   INT;
    v_total_sekarang INT;
    v_jenis          VARCHAR;
BEGIN
    SELECT id_po INTO v_id_po
    FROM products WHERE id_produk = NEW.id_produk;

    IF v_id_po IS NULL THEN RETURN NEW; END IF;

    SELECT p.target_kuota, p.harga_diskon, po.jenis_po
    INTO v_target, v_harga_diskon, v_jenis
    FROM products p
    JOIN preorders po ON p.id_po = po.id_po
    WHERE p.id_produk = NEW.id_produk;

    IF v_jenis = 'Gotong Royong' AND v_harga_diskon IS NOT NULL THEN
        SELECT SUM(jumlah_pesanan) INTO v_total_sekarang
        FROM transaction_details
        WHERE id_produk = NEW.id_produk
          AND id_po_saat_beli = v_id_po;

        IF v_total_sekarang >= v_target THEN
            UPDATE transaction_details
            SET selisih_refund = (harga_satuan_saat_beli - v_harga_diskon) * jumlah_pesanan
            WHERE id_produk = NEW.id_produk
              AND id_po_saat_beli = v_id_po        
              AND harga_satuan_saat_beli > v_harga_diskon
              AND selisih_refund = 0;
        END IF;
    END IF;

    RETURN NEW;
END;
$$ LANGUAGE plpgsql;
 
CREATE TRIGGER t_after_insert_detail
AFTER INSERT ON transaction_details
FOR EACH ROW EXECUTE FUNCTION trg_hitung_refund_gotong_royong();
 
-- INSERT TRANSAKSI & DETAIL
INSERT INTO transactions (id_koordinator, tanggal_transaksi, status_pesanan, bukti_bayar, is_valid) VALUES
(6, '2026-05-01 10:00:00', 'Selesai', '\x', TRUE),
(6, '2026-05-02 11:30:00', 'Selesai', '\x', TRUE),
(6, '2026-05-05 14:00:00', 'Selesai', '\x', TRUE);
 
ALTER TABLE transaction_details DISABLE TRIGGER trg_cek_waktu_po;
ALTER TABLE transaction_details DISABLE TRIGGER t_before_insert_detail;

INSERT INTO transaction_details
    (id_transaksi, id_produk, id_po_saat_beli, nama_produk_snapshot, nama_penitip, jumlah_pesanan, catatan, harga_satuan_saat_beli, harga_diskon_saat_beli)
VALUES
    (1, 1, 1, 'PDH BEM Pengurus', 'Tiara',  1, 'Ukuran M', 120000, NULL),
    (1, 1, 1, 'PDH BEM Pengurus', 'Siska',  2, 'Ukuran L', 120000, NULL),
    (1, 2, 1, 'Kaos Panitia',     'Kevin',  1, 'Warna Hitam', 65000, NULL);
 
ALTER TABLE transaction_details ENABLE TRIGGER t_before_insert_detail;
ALTER TABLE transaction_details ENABLE TRIGGER trg_cek_waktu_po;
 
INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES
(2, 6, 'Grup Kelas A', 20, 'Ganci Maskot'),
(2, 7, 'Grup Kelas A', 15, 'Stiker');
 
INSERT INTO transaction_details (id_transaksi, id_produk, nama_penitip, jumlah_pesanan, catatan) VALUES
(3, 8, 'Tiara', 2, 'Lanyard Merah'),
(3, 8, 'Bagas', 3, 'Lanyard Biru');
 
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
 
ALTER TABLE transaction_details DISABLE TRIGGER trg_cek_waktu_po;
ALTER TABLE transaction_details DISABLE TRIGGER t_before_insert_detail;

INSERT INTO transaction_details
    (id_transaksi, id_produk, id_po_saat_beli, nama_produk_snapshot, nama_penitip, jumlah_pesanan, catatan, harga_satuan_saat_beli, harga_diskon_saat_beli)
VALUES
    (7, 15, 6, 'Binder Aesthetic', 'Reza',          1, 'Binder B5', 35000, NULL),
    (7, 16, 6, 'Notebook Spiral',  'Adiknya Reza',  2, 'Notebook',  15000, NULL);
 
ALTER TABLE transaction_details ENABLE TRIGGER t_before_insert_detail;
ALTER TABLE transaction_details ENABLE TRIGGER trg_cek_waktu_po;
 
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
(10, 3,  'Maya',            2, 'Ayam geprek'),
(10, 4,  'Sari',            2, 'Es Teh'),
(11, 8,  'Grup Maba B',    10, 'Lanyard UNEJ'),
(12, 13, 'Maya',            3, 'Botol minum merah');
 
-- REVIEWS & COMPLAINTS
INSERT INTO reviews (id_produk, id_user, rating, komentar, tanggal_ulasan, balasan_penjual) VALUES
(1,  6,  5, 'Jahitannya rapi banget, worth it!',                   '2026-05-03 10:00:00', 'Terima kasih banyak, Tiara!'),
(6,  6,  4, 'Akriliknya lumayan tebal, tapi agak lama sampainya.', '2026-05-04 12:00:00', 'Maaf ya atas keterlambatannya.'),
(15, 8,  5, 'Bindernya aesthetic parah!',                          '2026-05-17 09:00:00', NULL),
(8,  10, 5, 'Desain lanyardnya keren!',                            '2026-05-19 11:00:00', NULL);
 
INSERT INTO complaints (id_user, subjek, deskripsi, tanggal, is_selesai, balasan) VALUES
(7,  'Admin BEM Slow Respon',  'Halo, validasi resi saya untuk ayam geprek belum diproses.',      '2026-05-13 08:00:00', TRUE,  'Sudah dibantu follow up ke admin konsumsi BEM.'),
(9,  'Fitur Keranjang Ngebug', 'Waktu saya nambah list titipan, kadang layarnya ngestuck.',       '2026-05-18 14:00:00', FALSE, NULL),
(10, 'Salah Input Nominal',    'Min, saya salah transfer lebih 10 ribu ke BEM. Bisa di-refund?',  '2026-05-19 15:30:00', FALSE, NULL);
 
INSERT INTO activity_logs (id_user, aktivitas, waktu_akses) VALUES
(1, 'Berhasil login ke sistem (Dashboard Admin)',          '2026-05-20 08:00:00'),
(1, 'Mengunduh Laporan PDF (LPJ Danus PO BEM)',            '2026-05-20 08:30:00'),
(2, 'Menambahkan produk baru ke PO BEM Batch 1',           '2026-05-01 10:15:00'),
(7, 'Mengunduh Kuitansi Transaksi #1 (Format PDF)',        '2026-05-12 10:20:00'),
(8, 'Berhasil melakukan checkout keranjang belanja',       '2026-05-15 16:46:00'),
(1, 'Memblokir akun pengguna akibat manipulasi pesanan',   '2026-05-21 14:00:00');

-- BAGIAN 6 & 7: KUERI ANALITIK ADVANCED & STATEMENTS
/*
-- 1. STATEMENT: Status Ketersediaan Kuota
-- [IMPLEMENTASI C#]: Dipanggil di Dashboard Penjual (Monitor Kuota)
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
  AND p.is_deleted = FALSE
GROUP BY p.id_produk, p.nama_produk, p.target_kuota;
 
 
-- 2. STATEMENT: Klasifikasi Performa Penjual (Tier Penjual)
-- [IMPLEMENTASI C#]: Dipanggil di Dashboard Admin (Leaderboard Penjual)
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
JOIN users u    ON p.id_penjual = u.id_user
GROUP BY u.nama
ORDER BY total_omzet_bersih DESC;
 
 
-- 3. GROUP BY: Total barang terjual tiap produk
-- [IMPLEMENTASI C#]: Dipanggil di Analisis Item Populer
SELECT
    td.nama_produk_snapshot AS nama_produk,
    SUM(td.jumlah_pesanan)  AS total_terjual
FROM transaction_details td
GROUP BY td.nama_produk_snapshot
ORDER BY total_terjual DESC;
 
 
-- 4. CUBE: Kombinasi silang Kategori X Jenis PO
-- [IMPLEMENTASI C#]: Dipanggil di fitur "Analisis Pasar" (Admin)
SELECT
    COALESCE(kat.nama_kategori, 'Semua Kategori')     AS kategori,
    COALESCE(po.jenis_po, 'Tanpa PO / Semua Jenis')  AS jenis_po,
    SUM(td.jumlah_pesanan)                            AS total_barang_terjual
FROM transaction_details td
JOIN      products    p   ON td.id_produk   = p.id_produk
LEFT JOIN preorders   po  ON p.id_po        = po.id_po
AND po.is_deleted = FALSE 
LEFT JOIN categories  kat ON p.id_kategori  = kat.id_kategori
GROUP BY CUBE (kat.nama_kategori, po.jenis_po);
 
 
-- 5. ROLL UP: Hierarki Waktu → Total Tahun → Total Bulan
-- [IMPLEMENTASI C#]: Dipanggil di fitur "Laporan Keuangan Bulanan"
SELECT
    EXTRACT(YEAR  FROM t.tanggal_transaksi) AS tahun,
    EXTRACT(MONTH FROM t.tanggal_transaksi) AS bulan,
    SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli)                                    AS omzet_kotor,
    SUM(td.selisih_refund)                                                                AS total_refund,
    SUM((td.jumlah_pesanan * td.harga_satuan_saat_beli) - COALESCE(td.selisih_refund, 0)) AS omzet_bersih
FROM transactions t
JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
WHERE t.status_pesanan = 'Selesai'
GROUP BY ROLLUP (
    EXTRACT(YEAR  FROM t.tanggal_transaksi),
    EXTRACT(MONTH FROM t.tanggal_transaksi)
);
 
 
-- 6. GROUPING SETS: Rekap per Penjual & per Kategori sekaligus
-- [IMPLEMENTASI C#]: Dipanggil di fitur "Ringkasan Global"
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
 
 
-- 7. SUBQUERY: Deteksi produk dengan sisa kuota <= 5
-- [IMPLEMENTASI C#]: Dipanggil di Dashboard Penjual (Peringatan Stok Tipis)
SELECT nama_produk, target_kuota
FROM products p
WHERE p.target_kuota IS NOT NULL
 AND p.is_deleted = FALSE
  AND (
        p.target_kuota - (
            SELECT COALESCE(SUM(jumlah_pesanan), 0)
            FROM transaction_details td
            WHERE td.id_produk = p.id_produk
        )
      ) <= 5;
 
 
-- 8. UNION: Menggabungkan status Diproses dan Selesai
-- [IMPLEMENTASI C#]: Dipanggil di "Daftar Transaksi Aktif"
SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Diproses'
UNION
SELECT id_transaksi, status_pesanan FROM transactions WHERE status_pesanan = 'Selesai';
 
 
-- 9. INTERSECT: Penjual yang juga pernah menjadi koordinator/pembeli
-- [IMPLEMENTASI C#]: Dipanggil untuk filter "Sultan Member"
SELECT id_user, nama FROM users
WHERE id_user IN (SELECT id_user FROM verifications WHERE is_verifikasi = TRUE)
INTERSECT
SELECT u.id_user, u.nama FROM users u
JOIN transactions t ON u.id_user = t.id_koordinator;
 
 
-- 10. EXCEPT: User yang belum pernah melakukan transaksi (Pengguna Pasif)
-- [IMPLEMENTASI C#]: Dipanggil di menu "Broadcast Promo" Admin
SELECT id_user, nama FROM users
EXCEPT
SELECT u.id_user, u.nama FROM users u
JOIN transactions t ON u.id_user = t.id_koordinator;
 
 
-- 11. ADVANCED LOGGING: Aktivitas Sistem Terbaru
-- [IMPLEMENTASI C#]: Dipanggil di Dashboard Admin (Monitor Audit Trail)
SELECT
    pelaku,
    aktivitas,
    waktu_akses
FROM vw_log_aktivitas
ORDER BY waktu_akses DESC
LIMIT 5;
*/


-- ════════════════════════════════════════════════════════
-- VIEW 1: Detail pesanan koordinator (pembeli) per transaksi
-- Menggabungkan header + detail dalam satu query
-- ════════════════════════════════════════════════════════
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

-- ════════════════════════════════════════════════════════
-- VIEW 2: Detail pesanan per penjual (terfilter id_penjual)
-- Menggabungkan header + detail + join products untuk filter penjual
-- ════════════════════════════════════════════════════════
CREATE OR REPLACE VIEW vw_detail_pesanan_penjual AS
SELECT
    t.id_transaksi,
    u.nama                                               AS nama_pembeli,
    TO_CHAR(t.tanggal_transaksi, 'DD Mon YYYY, HH24:MI') AS tanggal_transaksi,
    t.status_pesanan,
    t.bukti_bayar,
    p.id_penjual,
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

-- ════════════════════════════════════════════════════════
-- VIEW 3: Pesanan masuk per penjual (menggantikan correlated subquery)
-- ════════════════════════════════════════════════════════
CREATE OR REPLACE VIEW vw_pesanan_masuk_penjual AS
SELECT
    t.id_transaksi,
    u.nama                                                          AS nama_pembeli,
    t.tanggal_transaksi,
    t.status_pesanan,
    p.id_penjual,
    COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0) AS total_harga_lapak
FROM transactions t
JOIN users u ON t.id_koordinator = u.id_user
JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
JOIN products p ON td.id_produk = p.id_produk
GROUP BY t.id_transaksi, u.nama, t.tanggal_transaksi, t.status_pesanan, p.id_penjual;

-- ════════════════════════════════════════════════════════
-- VIEW 4: Semua user dengan status akun (menggantikan query inline di GetSemuaUser)
-- ════════════════════════════════════════════════════════
CREATE OR REPLACE VIEW vw_semua_user AS
SELECT
    u.id_user,
    u.nama,
    u.username,
    COALESCE(u.email, '-')          AS email,
    COALESCE(u.nomor_telepon, '-')  AS nomor_telepon,
    u.peran,
    CASE WHEN u.is_diblokir = TRUE THEN 'Diblokir' ELSE 'Aktif' END AS status_akun
FROM users u
ORDER BY u.peran, u.nama;

-- ════════════════════════════════════════════════════════
-- VIEW 5: Katalog produk utama (menggantikan query panjang + correlated subquery)
-- ════════════════════════════════════════════════════════
CREATE OR REPLACE VIEW vw_katalog_produk AS
SELECT
    p.id_produk,
    p.nama_produk,
    kat.nama_kategori,
    po.judul_po,
    p.harga_dasar,
    p.harga_diskon,
    po.batas_waktu,
    p.foto_produk,
    COALESCE(v.nama_toko, u.nama)                           AS nama_toko,
    po.jenis_po,
    p.target_kuota,
    CASE WHEN p.id_po IS NULL THEN FALSE ELSE TRUE END       AS in_sesi_po,
    COALESCE((
        SELECT SUM(td.jumlah_pesanan)
        FROM transaction_details td
        JOIN transactions t ON td.id_transaksi = t.id_transaksi
        WHERE td.id_produk = p.id_produk
          AND t.status_pesanan NOT IN ('Batal', 'Gagal')
    ), 0)                                                    AS terpesan
FROM products p
LEFT JOIN preorders   po  ON p.id_po       = po.id_po
LEFT JOIN categories  kat ON p.id_kategori = kat.id_kategori
LEFT JOIN users       u   ON p.id_penjual  = u.id_user
LEFT JOIN verifications v ON p.id_penjual  = v.id_user
WHERE p.is_deleted = FALSE;

-- ════════════════════════════════════════════════════════
-- FUNCTION 1: Ringkasan penjualan per penjual
-- ════════════════════════════════════════════════════════
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
    WHERE p.id_penjual = p_id_penjual
      AND t.status_pesanan = 'Selesai';
END;
$$ LANGUAGE plpgsql;

-- ════════════════════════════════════════════════════════
-- FUNCTION 2: Riwayat cuan (transaksi selesai) per penjual
-- ════════════════════════════════════════════════════════
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
        COALESCE(SUM(td.jumlah_pesanan * td.harga_satuan_saat_beli), 0)     AS total_harga
    FROM transactions t
    JOIN users u ON t.id_koordinator = u.id_user
    JOIN transaction_details td ON t.id_transaksi = td.id_transaksi
    JOIN products p ON td.id_produk = p.id_produk
    WHERE p.id_penjual = p_id_penjual
      AND t.status_pesanan = 'Selesai'
    GROUP BY t.id_transaksi, u.nama, t.tanggal_transaksi
    ORDER BY t.tanggal_transaksi DESC;
END;
$$ LANGUAGE plpgsql;

-- ════════════════════════════════════════════════════════
-- FUNCTION 3: User detail (dipakai GetById dan GetByUsername)
-- ════════════════════════════════════════════════════════
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



-- ════════════════════════════════════════════════════════
-- VIEW 1: Ulasan per penjual lengkap dengan nama produk & pembeli
-- Menggantikan query JOIN 3 tabel di GetReviewsByPenjual
-- ════════════════════════════════════════════════════════
CREATE OR REPLACE VIEW vw_ulasan_penjual AS
SELECT
    r.id_ulasan,
    p.id_penjual,
    p.nama_produk,
    u.nama                                          AS nama_pembeli,
    r.rating,
    r.komentar,
    r.tanggal_ulasan,
    r.balasan_penjual
FROM reviews r
JOIN products p ON r.id_produk = p.id_produk
JOIN users   u ON r.id_user   = u.id_user
ORDER BY r.tanggal_ulasan DESC;

-- ════════════════════════════════════════════════════════
-- VIEW 2: Aduan yang belum selesai lengkap dengan nama pelapor
-- Menggantikan query JOIN di GetPendingAduan
-- ════════════════════════════════════════════════════════
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

-- ════════════════════════════════════════════════════════
-- VIEW 3: Activity log lengkap dengan nama & peran pelaku
-- Menggantikan query JOIN di GetAllAsDataTable
-- ════════════════════════════════════════════════════════
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

-- ════════════════════════════════════════════════════════
-- FUNCTION 1: Sesi PO aktif dengan pencarian keyword
-- Menggantikan query GROUP BY kompleks di GetSesiPOAktif
-- ════════════════════════════════════════════════════════
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
        po.judul_po::TEXT           AS nama_sesi,
        po.jenis_po::TEXT,
        v.nama_toko::TEXT,
        COUNT(p.id_produk)          AS jumlah_produk,
        COALESCE(MIN(p.harga_dasar), 0)::BIGINT AS harga_min,
        COALESCE(MAX(p.harga_dasar), 0)::BIGINT AS harga_max,
        po.batas_waktu              AS deadline,
        po.is_aktif
    FROM preorders po
    JOIN verifications v ON po.id_penjual = v.id_user
    LEFT JOIN products p ON po.id_po = p.id_po AND p.is_deleted = FALSE
    WHERE po.is_aktif    = TRUE
      AND po.is_deleted  = FALSE
      AND po.batas_waktu >= CURRENT_TIMESTAMP
      AND (po.judul_po ILIKE '%' || p_keyword || '%'
           OR v.nama_toko ILIKE '%' || p_keyword || '%')
    GROUP BY po.id_po, po.judul_po, po.jenis_po,
             v.nama_toko, po.batas_waktu, po.is_aktif
    ORDER BY po.batas_waktu ASC;
END;
$$ LANGUAGE plpgsql;

-- ════════════════════════════════════════════════════════
-- FUNCTION 2: PO milik penjual — dengan flag aktifSaja
-- Menggabungkan GetPOByPenjual dan GetPOAktifByPenjual jadi satu function
-- ════════════════════════════════════════════════════════
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
    WHERE po.id_penjual  = p_id_penjual
      AND po.is_deleted  = FALSE
      AND (NOT p_aktif_saja
           OR (po.is_aktif = TRUE AND po.batas_waktu > NOW()))
    ORDER BY
        CASE WHEN p_aktif_saja THEN po.batas_waktu END ASC,
        CASE WHEN NOT p_aktif_saja THEN po.batas_waktu END DESC;
END;
$$ LANGUAGE plpgsql;

-- ════════════════════════════════════════════════════════
-- FUNCTION 3: Produk yang bisa diulas oleh user tertentu
-- Menggantikan query di GetProdukBisaDiulas
-- ════════════════════════════════════════════════════════
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

-- ════════════════════════════════════════════════════════
-- FUNCTION 4: Riwayat aduan milik user tertentu
-- Menggantikan query di GetRiwayatByUser
-- ════════════════════════════════════════════════════════
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