using System;
using System.IO;

namespace CollabBuy.CollabBuyApp.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// Menyalin file ke folder Uploads/<subfolder> dan mengembalikan path relatif.
        /// </summary>
        public static string SimpanFile(string pathFileSumber, string subfolder)
        {
            if (string.IsNullOrWhiteSpace(pathFileSumber))
                throw new ArgumentException("Path file sumber tidak boleh kosong.");
            if (!File.Exists(pathFileSumber))
                throw new ArgumentException("File sumber tidak ditemukan.");

            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string targetFolder = Path.Combine(baseDir, "Uploads", subfolder);

            if (!Directory.Exists(targetFolder))
                Directory.CreateDirectory(targetFolder);

            // Nama file unik: timestamp + nama asli
            string namaFile = $"{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(pathFileSumber)}";
            string fullPath = Path.Combine(targetFolder, namaFile);

            File.Copy(pathFileSumber, fullPath, overwrite: true);

            // Path relatif dari BaseDirectory: Uploads/KTM/2026...
            return Path.Combine("Uploads", subfolder, namaFile);
        }

        /// <summary>
        /// Mengembalikan path absolut dari path relatif.
        /// </summary>
        public static string DapatkanFullPath(string relativePath)
        {
            if (string.IsNullOrWhiteSpace(relativePath))
                return null;
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativePath);
        }
    }
}