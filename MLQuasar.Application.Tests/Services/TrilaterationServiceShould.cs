using Xunit;
using FluentAssertions;
using MLQuasar.Domain.Entities;
using System.Collections.Generic;
using Moq;
using MLQuasar.Application.Services;


namespace MLQuasar.Application.Tests.Services
{
    public class TrilaterationServiceShould
    {
        public TrilaterationService Sut;

        public static TheoryData<double[], Point> Data
        {
            get
            {
                var data = new TheoryData<double[], Point>();
                
               
                double[] d1 = { 100.0, 115.5, 142.7};
                Point p1 = new Point { X = -487.28591250000136, Y = 1557.014224999999 };
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<
                double[] d2 = { 223.60679775, 447.2135955, 894.427191 };
                Point p2 = new Point { X = -299.999999999965, Y = -300.0000000003529 };
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<
                double[] d3 = { 560, 147, 500 };
                Point p3 = new Point { X = 20.233124999998836, Y = -11.443749999998545 };
               
                data.Add(d1, p1);
                data.Add(d2, p2);
                data.Add(d3, p3);
                return data;
            }
        }



        public TrilaterationServiceShould() 
        {
            Sut = new TrilaterationService();
        }

        public class TheMethod_TrilateratePositionShould : TrilaterationServiceShould
        {
            [Theory]
            [MemberData(nameof(Data))]
            public void Return_Point_given_certain_params(double[] distances, Point expected)
            {
                //arrange
                var points = new List<Point> {
                    new Point {X = -500, Y = -200 },
                    new Point {X = 100, Y = -100 },
                    new Point {X = 500, Y = 100 }
                    };
                //act
                var result = Sut.TrilateratePosition(points.ToArray(), distances);
                //assert
                result.Should().BeEquivalentTo(expected);
            }
        }
    }
}
