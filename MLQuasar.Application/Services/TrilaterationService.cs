using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Entities;
using MLQuasar.Domain.Extensions;



namespace MLQuasar.Application.Services
{
    public class TrilaterationService : ITrilaterationService
    {
        public Point TrilateratePosition(Point[] points, double[] distances)
        {
            //Convert doubles to vectors for processing
            Vector<double> vA = Vector<double>.Build.Dense(points[0].ToArray());
            //Declare elements of b vector
            //bBA = 1/2 * (rA^2 - rB^2 + dBA^2)
            Vector<double> vb = GetVectorb(points, distances);

            //Calculate and build matrix A
            Matrix<double> mA = GetMatrixA(points);
            //Declare Transpose of A matrix;
            Matrix<double> mAT = mA.Transpose();

            //Declare solution vector x to 0
            Vector<double> x;
            //Check if A*AT is non-singular (non 0 determinant)
            if (mA.Multiply(mAT).Determinant() > 0.1)
            {
                //x = ((AT * A)^-1)*AT*b
                // x = (((mA.Multiply(mAT)).Inverse()).Multiply(mAT)).Multiply(vb);
                x = (mA.Transpose() * mA).Inverse() * (mA.Transpose() * vb);
            }
            else
            {
                x = (((mA.Multiply(mAT)).Inverse()).Multiply(mAT)).Multiply(vb);
            }

            //final position is x + vA
            //return as double so as not
            var pos = (x.Add(vA)).ToArray();
            return new Point(pos[0], pos[1]);
        }

        //Retorna la distancia euclediana entre 2 puntos
        private double GetDistance(Point point1, Point point2)
        {
            //d^2 = (p1[0] - p2[0])^2 + (p1[1] - p2[1]);
            double distSquared = Math.Pow((point1.X - point2.X), 2) + Math.Pow((point1.X - point2.X), 2);
            return Math.Sqrt(distSquared);
        }

        private Vector<double> GetVectorb(Point[] points, double[] distances)
        {
            //Declare elements of b vector
            //bBA = 1/2 * (rA^2 - rB^2 + dBA^2)
            double[] b = { 0, 0 };
            b[0] = 0.5 * (Math.Pow(distances[0], 2) - Math.Pow(distances[1], 2)
                        + Math.Pow(GetDistance(points[1], points[0]), 2));

            b[1] = 0.5 * (Math.Pow(distances[0], 2) - Math.Pow(distances[2], 2)
                        + Math.Pow(GetDistance(points[2], points[0]), 2));

            //Convert b array to vector form
            return Vector<double>.Build.Dense(b);
        }
        private Matrix<double> GetMatrixA(Point[] points)
        {
            Point pA = points[0];
            Point pB = points[1];
            Point pC = points[2];
            //Build A array
            //A =   {x2 -x1, y2 - y1}
            //      {x3 - x1, y3 - y1}
            double[,] A = {
                            { pB.X - pA.X, pB.Y - pA.Y },
                            { pC.X - pA.X, pC.Y - pA.Y }
                          };

            //Convert A to Matrix form
            return Matrix<double>.Build.DenseOfArray(A);

        }
    }
}
