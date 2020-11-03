using System;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using iText;
using iText.Kernel.Pdf;

namespace ImageResizer
{
    class Program
    {
        

        /// <summary>
        ///     Reduz o tamanho das imagens geradas do banco de dados, limitando-os a no máximo 500 Kb
        /// </summary>
        public static byte[] Resize2Max500Kbytes(byte[] byteImageIn) {
            byte[] currentByteImageArray = byteImageIn;
            double scale = 1f;

            //FileStream input = new FileStream(byteImageIn);

            MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
            Image fullsizeImage = Image.FromStream(inputMemoryStream);

            while (currentByteImageArray.Length > 1000000) {

                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                MemoryStream resultStream = new MemoryStream();

                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();

                scale -= 0.05f;
            }

            return currentByteImageArray;
        }

        static void Main(string[] args) {

            try {

                foreach (string filePath in Directory.GetFiles(@"E:\Abraao Sebe", "*.JPG", SearchOption.AllDirectories)) {
                    
                    byte[] imagem = File.ReadAllBytes(filePath);

                    Console.WriteLine("Resizing file " + filePath);

                    File.WriteAllBytes(filePath, Resize2Max500Kbytes(imagem));
                }

                Console.WriteLine("");

                Console.WriteLine("Redimensionamento executado com sucesso! Pressione qualquer tecla para sair...");

                Console.ReadKey();

            } catch (Exception e) {
                throw e;
            }
        }
    }
}
