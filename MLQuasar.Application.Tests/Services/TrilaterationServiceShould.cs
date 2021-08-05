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
                Point p1 = new Point { X = -399.7859125000032, Y = 2782.0142249999917 };
                //<<<<<<<<<<<<<<<<<<<<<<<<<<<

                data.Add(d1, p1);
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
            public void Return_Point_given_certain_params(double[] distances, double expected)
            {
                //arrange
                var points = new List<Point> {
                    new Point {X = -500, Y = -200 },
                    new Point {X = 100, Y = -100 },
                    new Point {X = 500, Y = 100 }
                    };
                //act
                var actual = Sut.TrilateratePosition(points.ToArray(), distances);
                //assert
                actual.Should().BeEquivalentTo(expected);
            }
        }
    }
}
