using System;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace EmprestimosJogos.Infra.CrossCutting.CompressionManager
{
    public static class CompressionManagerService
    {
        public static string CompressToText(string text)
        {
            return Convert.ToBase64String(CompressToBytes(text));
        }

        public static byte[] CompressToBytes(string text)
        {
            if (string.IsNullOrEmpty(text))
                throw new ArgumentNullException(nameof(text));

            return Compress(Encoding.UTF8.GetBytes(text));
        }

        public static byte[] Compress(byte[] raw)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (GZipStream gzipStream = new GZipStream(memoryStream, CompressionMode.Compress, true))
                    gzipStream.Write(raw, 0, raw.Length);

                return memoryStream.ToArray();
            }
        }

        public static string Decompress(string compressedText)
        {
            if (string.IsNullOrEmpty(compressedText))
                throw new ArgumentNullException(nameof(compressedText));

            return DecompressToText(Convert.FromBase64String(compressedText));
        }

        public static string DecompressToText(byte[] compressedBytes)
        {
            return Encoding.UTF8.GetString(DecompressToBytes(compressedBytes));
        }

        public static byte[] DecompressToBytes(byte[] gzip)
        {
            using (GZipStream gzipStream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                var buffer = new byte[4096];

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    int count;

                    do
                    {
                        count = gzipStream.Read(buffer, 0, 4096);
                        if (count > 0)
                            memoryStream.Write(buffer, 0, count);
                    }
                    while (count > 0);

                    return memoryStream.ToArray();
                }
            }
        }

        public static byte[] DecompressToBytes(string compressedText)
        {
            if (string.IsNullOrEmpty(compressedText))
                throw new ArgumentNullException(nameof(compressedText));

            return DecompressToBytes(Convert.FromBase64String(compressedText));
        }

        public static Stream CompressedBase64ToDecompressedStream(string base64)
        {
            return new MemoryStream(DecompressToBytes(base64));
        }

        public static Stream CompressedBytesToDecompressedStream(byte[] bytes)
        {
            return new MemoryStream(DecompressToBytes(bytes));
        }
    }
}
