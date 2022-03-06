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

        public Mat MakeBmpMask(MatrixMat matrixMat)
        {
            var mat = new Mat(matrixMat.Width, matrixMat.Height,MatType.CV_64FC1);
            var black=new Vec3b(0,0,0);
            var white=new Vec3b(255,255,255);
            for (int y = 0; y < matrixMat.Height; y++)
            {
                for (int x = 0; x < matrixMat.Width; x++)
                {
                    if(matrixMat.BinaryPixels[y * matrixMat.Width + x] == 1)
                    {
                        mat.Set<Vec3b>(y, x, white);
                    }
                    else
                    {
                        mat.Set<Vec3b>(y, x, black);
                    }
                   
                }
            }
            return mat;
        }
    }
}
