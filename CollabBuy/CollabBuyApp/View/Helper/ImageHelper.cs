using System;
using System.Collections.Generic;
using System.IO;

namespace CollabBuy.CollabBuyApp.View.Helper
{
    public static class ImageHelper
    {
        // Membungkus banyak foto jadi 1 byte[] untuk disimpan ke kolom foto_produk
        public static byte[] PackImages(List<byte[]> images)
        {
            if (images == null || images.Count == 0) return null;
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    bw.Write(images.Count); // Tulis jumlah foto
                    foreach (var img in images)
                    {
                        bw.Write(img.Length); // Tulis ukuran foto
                        bw.Write(img);        // Tulis data fotonya
                    }
                }
                return ms.ToArray();
            }
        }

        // Membuka bungkusan byte[] kembali menjadi List foto
        public static List<byte[]> UnpackImages(byte[] data)
        {
            List<byte[]> list = new List<byte[]>();
            if (data == null || data.Length == 0) return list;

            try
            {
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using (BinaryReader br = new BinaryReader(ms))
                    {
                        int count = br.ReadInt32();
                        // Sanity check biar nggak error kalau datanya cuma 1 foto lama (bukan paketan)
                        if (count > 0 && count < 50)
                        {
                            for (int i = 0; i < count; i++)
                            {
                                int len = br.ReadInt32();
                                if (len > 0 && len < data.Length)
                                    list.Add(br.ReadBytes(len));
                                else throw new Exception("Bukan format paketan");
                            }
                            return list;
                        }
                        else throw new Exception("Bukan format paketan");
                    }
                }
            }
            catch
            {
                // Fallback: Kalau gagal dibongkar, berarti ini foto tunggal dari sistem lama
                list.Add(data);
            }
            return list;
        }
    }
}