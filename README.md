# CollabBuy - Sistem Pengelolaan Dana Usaha Terintegrasi

## Deskripsi
CollabBuy adalah platform desktop berbasis C# WinForms yang dirancang khusus untuk memfasilitasi transaksi dan manajemen Pre-Order (PO) kolaboratif di lingkungan kampus. 
Sistem ini mengotomatisasi interaksi antara penjual dan agregator (koordinator pembeli), serta mendukung fitur PO Gotong Royong dengan penyesuaian harga dinamis berdasarkan kuota.

## Fitur Utama
1. **Autentikasi & Manajemen Hak Akses**: Memisahkan peran Admin dan User
2. **Transaksi Kolektif**: Menggunakan antarmuka tabel (DataGridView) untuk entri puluhan pesanan secara masif dalam satu kali *checkout*.
3. **Otomasi Harga Dinamis**: Harga otomatis turun saat target kuota PO Gotong Royong terpenuhi.
4. **Dashboard Analitik**: Laporan omset berdasarkan fakultas, kategori, dan periode

## Teknologi
* **Bahasa**: C# (Object-Oriented Programming)
* **Framework UI**: Windows Forms (.NET)
* **Database**: PostgreSQL (Npgsql)
