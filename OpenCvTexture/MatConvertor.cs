using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace OpenCvTexture
{
    internal class MatConvertor
    {
        public byte[] MatToByte(Mat mat)
        {
          
            var pixels = new byte[mat.Width * mat.Height* mat.Channels()];

            System.Runtime.InteropServices.Marshal.Copy(mat.Data , pixels, 0, pixels.Length);
            Console.WriteLine(pixels[800]);
            return pixels;

        }

        public byte[] Quantize(MatrixMat matrixMat)
        {
            var quantPixels = new byte[matrixMat.Width * matrixMat.Height];
            for (int i = 0; i < matrixMat.Width * matrixMat.Height; i++)
            {
                quantPixels[i] = (byte)(matrixMat.Pixels[i * 3 + 2] * matrixMat.QuantLevel / 256);
            }
            Console.WriteLine("quant={0}", quantPixels[266]);
            return quantPixels;
        }

        public Mat MakeBmpMask(MatrixMat matrixMat, double threshold)
        {
            var mat = new Mat(matrixMat.Width, matrixMat.Height);
            for (int y = 0; y < matrixBmp.Height; y++)
            {
                for (int x = 0; x < matrixBmp.Width; x++)
                {
                    var c = new Color();
                    if (matrixBmp.GlcmHomogeneity[x + y * matrixBmp.Width] >= threshold && matrixBmp.ZPixels[x + y * matrixBmp.Width] > 10000)
                    {
                        c = Color.FromArgb(255, 255, 255);
                    }
                    else
                    {
                        c = Color.FromArgb(0, 0, 0);
                    }
                    bmp.SetPixel(x, y, c);
                }
            }
            return bmp;
        }
    }
}
