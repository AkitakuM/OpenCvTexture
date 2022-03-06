using OpenCvSharp;
using System;
using System.Diagnostics;


namespace OpenCvTexture
{
    public struct MatrixMat
    {
        public int Height;
        public int Width;
        public int QuantLevel;
        public byte[]? Pixels;
        public byte[]? QuantPixels;
        public int[]? BinaryPixels;
       
    }
    class Program
    {
        static void Main(string[] args)
        {
            var fileName = args[0];
            var quantizationLevel = Int32.Parse(args[1]);
            var threshold=Double.Parse(args[2]);
            var filtersize=Int32.Parse(args[3]);
            var frame = Cv2.ImRead(fileName);
            var matrixMat = new MatrixMat();
            matrixMat.Height = frame.Height;
            matrixMat.Width = frame.Width;
            var matConvertor = new MatConvertor();
            var textureCal = new TextureCalculator();
            matrixMat.Pixels = matConvertor.MatToByte(frame);
            matrixMat.QuantLevel = quantizationLevel;
            matrixMat.QuantPixels = matConvertor.Quantize(matrixMat);
            matrixMat.BinaryPixels = textureCal.GetGlcmHomogeneity(matrixMat, filtersize, threshold);
            Mat mat = matConvertor.MakeBmpMask(matrixMat);
            Cv2.ImShow("mat", mat);
            Cv2.WaitKey(1);

        }

       
    }
}