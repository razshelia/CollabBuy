using System;
using System.IO;

namespace CollabBuy.CollabBuyApp.Helpers
{
    public class FileHelper
    {
        private string direktoriAsas;

        public FileHelper()
        {
            // Mendapatkan direktori tempat aplikasi dijalankan (bin/Debug)
            this.direktoriAsas = AppDomain.CurrentDomain.BaseDirectory;
        }

        public string SimpanGambar(string pathAsal, string namaFolder, string namaFailBaru)
        {
            if (string.IsNullOrEmpty(pathAsal) || string.IsNullOrEmpty(namaFolder))
            {
                return string.Empty;
            }
            else
            {
                try
                {
                    string folderTujuan = Path.Combine(this.direktoriAsas, "Images", namaFolder);

                    if (Directory.Exists(folderTujuan))
                    {
                        // Folder sudah wujud, teruskan proses
                    }
                    else
                    {
                        // Cipta folder jika belum ada (contoh: mencipta folder KTM)
                        Directory.CreateDirectory(folderTujuan);
                    }

                    // Mengambil format fail (contoh: .jpg atau .png)
                    string ekstensi = Path.GetExtension(pathAsal);
                    string namaPenuhFail = namaFailBaru + ekstensi;
                    string pathTujuan = Path.Combine(folderTujuan, namaPenuhFail);

                    // Menyalin fail ke folder projek (tulis ganti jika sudah wujud)
                    File.Copy(pathAsal, pathTujuan, true);

                    // Mengembalikan Relative Path untuk disimpan ke dalam Database
                    return Path.Combine("Images", namaFolder, namaPenuhFail);
                }
                catch (Exception)
                {
                    // ERROR HANDLING: Gagal secara senyap (UX Friendly), 
                    // pangkalan data hanya akan menerima string kosong tanpa menyebabkan sistem berhenti berfungsi.
                    return string.Empty;
                }
            }
        }
    }
}