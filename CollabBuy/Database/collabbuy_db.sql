-- ========================================================================================
-- 1. RESET SCHEMA
-- ========================================================================================
DROP SCHEMA public CASCADE;
CREATE SCHEMA public;

-- =============================================================
-- COLLABBUY DATABASE SCHEMA (PostgreSQL)
-- Versi: 2.0
-- Tanggal: 2026-05-14
-- Deskripsi: Struktur database untuk aplikasi CollabBuy
-- =============================================================

-- Membuat database (jalankan secara terpisah jika belum ada)
-- CREATE DATABASE collabbuy_db;
-- \c collabbuy_db

-- -------------------------------------------------------------
-- 1. Tabel users (Akun User & Admin)
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS users (
    id_user          SERIAL PRIMARY KEY,
    username         VARCHAR(50)  UNIQUE NOT NULL,
    password         VARCHAR(64)  NOT NULL,   -- SHA256 hash
    nama             VARCHAR(100) NOT NULL,
    email            VARCHAR(100) NOT NULL,
    nomor_telepon    VARCHAR(15),
    role             VARCHAR(10)  DEFAULT 'user' CHECK (role IN ('user', 'admin')),
    is_verifikasi    BOOLEAN      DEFAULT FALSE,
    created_at       TIMESTAMP    DEFAULT CURRENT_TIMESTAMP
);

-- -------------------------------------------------------------
-- 2. Tabel verifications (Pengajuan Seller)
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS verifications (
    id_verifikasi   SERIAL PRIMARY KEY,
    id_user         INT          NOT NULL REFERENCES users(id_user) ON DELETE CASCADE,
    nama_toko       VARCHAR(100) NOT NULL,
    nim             VARCHAR(20)  NOT NULL,
    tahun_masuk     INT          NOT NULL,
    ktm_path        VARCHAR(255),          -- path relatif gambar KTM
    status          VARCHAR(20)  DEFAULT 'pending' CHECK (status IN ('pending', 'disetujui', 'ditolak')),
    created_at      TIMESTAMP    DEFAULT CURRENT_TIMESTAMP
);

-- -------------------------------------------------------------
-- 3. Tabel categories (Kategori Produk)
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS categories (
    id_kategori     SERIAL PRIMARY KEY,
    nama_kategori   VARCHAR(100) UNIQUE NOT NULL
);

-- -------------------------------------------------------------
-- 4. Tabel products (Produk yang Dijual)
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS products (
    id_produk       SERIAL PRIMARY KEY,
    nama_produk     VARCHAR(150)   NOT NULL,
    deskripsi       TEXT,
    harga           DECIMAL(12,2)  NOT NULL,   -- harga satuan
    stok            INT            DEFAULT 0,
    id_seller       INT            NOT NULL REFERENCES users(id_user) ON DELETE CASCADE,
    id_kategori     INT            REFERENCES categories(id_kategori) ON DELETE SET NULL,
    foto            VARCHAR(255),              -- path relatif gambar produk
    is_aktif        BOOLEAN        DEFAULT TRUE,
    created_at      TIMESTAMP      DEFAULT CURRENT_TIMESTAMP
);

-- -------------------------------------------------------------
-- 5. Tabel preorders (Preorder / Buka PO)
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS preorders (
    id_po           SERIAL PRIMARY KEY,
    id_produk       INT            NOT NULL REFERENCES products(id_produk) ON DELETE CASCADE,
    id_seller       INT            NOT NULL REFERENCES users(id_user) ON DELETE CASCADE,
    tanggal_buka    DATE           NOT NULL,
    tanggal_tutup   DATE           NOT NULL,
    target_jumlah   INT            NOT NULL,
    jumlah_terkumpul INT           DEFAULT 0,
    status          VARCHAR(20)    DEFAULT 'aktif' CHECK (status IN ('aktif', 'tutup', 'batal')),
    created_at      TIMESTAMP      DEFAULT CURRENT_TIMESTAMP
);

-- -------------------------------------------------------------
-- 6. Tabel checkouts (Transaksi Checkout)
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS checkouts (
    id_checkout           SERIAL PRIMARY KEY,
    id_po                 INT           NOT NULL REFERENCES preorders(id_po) ON DELETE CASCADE,
    id_user_coordinator   INT           NOT NULL REFERENCES users(id_user) ON DELETE CASCADE,
    jumlah                INT           NOT NULL,
    total_bayar_awal      DECIMAL(12,2) NOT NULL,
    bukti_pembayaran      VARCHAR(255),             -- path relatif bukti transfer
    status                VARCHAR(20)   DEFAULT 'pending' CHECK (status IN ('pending', 'dibayar', 'dikirim', 'selesai', 'dibatalkan')),
    created_at            TIMESTAMP     DEFAULT CURRENT_TIMESTAMP
);

-- -------------------------------------------------------------
-- 7. Tabel complaints (Aduan / Keluhan)
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS complaints (
    id_aduan        SERIAL PRIMARY KEY,
    id_user         INT          NOT NULL REFERENCES users(id_user) ON DELETE CASCADE,
    subjek          VARCHAR(150) NOT NULL,
    pesan           TEXT         NOT NULL,
    status          VARCHAR(20)  DEFAULT 'pending' CHECK (status IN ('pending', 'diproses', 'selesai')),
    created_at      TIMESTAMP    DEFAULT CURRENT_TIMESTAMP
);

-- -------------------------------------------------------------
-- 8. Tabel reviews (Ulasan Produk)
-- -------------------------------------------------------------
CREATE TABLE IF NOT EXISTS reviews (
    id_ulasan       SERIAL PRIMARY KEY,
    id_user         INT          NOT NULL REFERENCES users(id_user) ON DELETE CASCADE,
    id_produk       INT          NOT NULL REFERENCES products(id_produk) ON DELETE CASCADE,
    rating          INT          NOT NULL CHECK (rating BETWEEN 1 AND 5),
    komentar        TEXT,
    created_at      TIMESTAMP    DEFAULT CURRENT_TIMESTAMP
);

-- =============================================================
-- INDEX untuk performa
-- =============================================================
CREATE INDEX IF NOT EXISTS idx_users_username ON users(username);
CREATE INDEX IF NOT EXISTS idx_verifications_id_user ON verifications(id_user);
CREATE INDEX IF NOT EXISTS idx_products_id_seller ON products(id_seller);
CREATE INDEX IF NOT EXISTS idx_checkouts_id_po ON checkouts(id_po);
CREATE INDEX IF NOT EXISTS idx_checkouts_id_user ON checkouts(id_user_coordinator);
CREATE INDEX IF NOT EXISTS idx_complaints_id_user ON complaints(id_user);
CREATE INDEX IF NOT EXISTS idx_reviews_id_produk ON reviews(id_produk);

-- =============================================================
-- Insert data awal (admin default)
-- Password: admin123 (hash SHA256 akan dibuat otomatis oleh aplikasi)
-- Tapi untuk testing, kita bisa insert admin langsung dengan hash SHA256 dari 'admin123'
-- =============================================================
-- Hash SHA256 dari 'admin123': 240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9
INSERT INTO users (username, password, nama, email, role, is_verifikasi)
VALUES ('admin', '240be518fabd2724ddb6f04eeb1da5967448d7e831c08c8fa822809f74c720a9', 'Administrator', 'admin@collabbuy.com', 'admin', false)
ON CONFLICT (username) DO NOTHING;

-- Insert kategori default
INSERT INTO categories (nama_kategori) VALUES ('Makanan & Minuman'), ('Fashion'), ('Alat Tulis'), ('Elektronik'), ('Lainnya')
ON CONFLICT (nama_kategori) DO NOTHING;