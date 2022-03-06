using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenCvTexture
{
    internal class TextureCalculator
    {
        public int[] GetGlcmHomogeneity(MatrixMat matrixMat,int filterSize,double threshold)
        {
            var binary = new int[matrixMat.QuantPixels.Length];
            if (filterSize % 2 != 0) { filterSize += 1; }
            Parallel.For(filterSize / 2, matrixMat.Height - filterSize / 2, y =>
             {
                 for (int x = (filterSize / 2); x < matrixMat.Width - filterSize / 2; x++)
                 {
                     var glcmTable = GetGlcm(matrixMat, filterSize, x, y);
                     var homogeneity = GetHomogeneity(glcmTable);
                     if(homogeneity>=threshold)
                     {
                         binary[x + y * matrixMat.Width] = 1;
                     }
                     else { binary[x + y * matrixMat.Width] = 0; }
                    //Debug.WriteLine("x={0},y={1},quant={2},homo={3}", x, y, matrixMat.QuantPixels[x + y * matrixMat.Width], homogeneity[x + y * matrixMat.Width]);
                }
             });
            Console.WriteLine(binary[360]);
            return binary;
        }

        //GLCMを計算する関数
        public double[,] GetGlcm(MatrixMat matrixMat, int filterSize,int centerX,int centerY)
        {
            var glcmTable = new double [matrixMat.QuantLevel, matrixMat.QuantLevel];
            var voteValue = 1.0 / (8 * (filterSize - 2) * (filterSize - 2));

            for(int y = centerY - filterSize / 2 + 1; y < centerY + filterSize / 2 - 1; y++)
            {
                for (int x = centerX - filterSize / 2 + 1; x < centerX + filterSize / 2; x++)
                {
                    var center = matrixMat.QuantPixels[x + y * matrixMat.Width];
                    glcmTable[center, matrixMat.QuantPixels[x - 1 + (y - 1) * matrixMat.Width]] += voteValue;
                    glcmTable[center, matrixMat.QuantPixels[x + (y - 1) * matrixMat.Width]] += voteValue;
                    glcmTable[center, matrixMat.QuantPixels[x + 1 + (y - 1) * matrixMat.Width]] += voteValue;
                    glcmTable[center, matrixMat.QuantPixels[x - 1 + y * matrixMat.Width]] += voteValue;
                    glcmTable[center, matrixMat.QuantPixels[x + 1 + y * matrixMat.Width]] += voteValue;
                    glcmTable[center, matrixMat.QuantPixels[x - 1 + (y + 1) * matrixMat.Width]] += voteValue;
                    glcmTable[center, matrixMat.QuantPixels[x + (y + 1) * matrixMat.Width]] += voteValue;
                    glcmTable[center, matrixMat.QuantPixels[x + 1 + (y + 1) * matrixMat.Width]] += voteValue;
                }
            }

            return glcmTable;
        }

        //Homogeneityを計算する関数
        public double GetHomogeneity(double[,] glcmTable)
        {
            var homogeneity = 0.0;
            for(int x=0;x<glcmTable.GetLength(0);x++)
            {
                for(int y = 0;y<glcmTable.GetLength(1);y++)
                {
                    homogeneity += glcmTable[x, y] / (1.0 + Math.Abs(x - y));
                }
            }
            return homogeneity;
        }
    }
}
