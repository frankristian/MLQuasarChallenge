

using MLQuasar.Services;
using Xunit;
using FluentAssertions;
using MLQuasar.Domain.Entities;
using System.Collections.Generic;
using Moq;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Responses;

namespace MLQuasar.Application.Tests.Services
{
    public class DecoderServiceShould
    {
        public DecoderService Sut;
        public ITrilaterationService TrilaterationMock;

        public List<Satellite> SatellitesMock = new List<Satellite>{
                        new Satellite { Name = "kenobi",
                            Radio = 100,
                            Message = new List<string> { "este", "", "", "mensaje", "" },
                            Position = new Point() {X = -500, Y = -200 }
                        },
                        new Satellite { Name = "skywalker",
                            Radio = 115.5,
                            Message = new List<string> { "", "es", "", "", "secreto" },
                            Position = new Point() {X = 100, Y = -100 }
                        },
                        new Satellite { Name = "sato",
                            Radio = 142.7,
                            Message = new List<string> { "este", "", "un", "", "" } ,
                            Position = new Point() {X = 500, Y = 100 }
                        }
                    };
        public ImperialShipCarrierResponse ImperialShipCarrierResponseMock = new ImperialShipCarrierResponse
        {
            Message = "este es un mensaje secreto",
            Position = new Point() { X = -399.7859125000032, Y = 2782.0142249999917 }
        };
        public DecoderServiceShould()
        {
            TrilaterationMock = Mock.Of<ITrilaterationService>();
            Sut = new DecoderService(TrilaterationMock);
        }

        
        public class TheMethod_GetMessageShould : DecoderServiceShould
        {
            [Fact]
            public void Return_a_MessageString_from_param() 
            {
                //arrange
                
                //act
                var actual = Sut.GetMessage(SatellitesMock);
                //assert
                actual.Should().BeEquivalentTo(ImperialShipCarrierResponseMock.Message);
            }
        }

        public class TheMethod_GetLocation : DecoderServiceShould
        {
            [Fact]
            public void Invoke_TrilateratioPosition_method_with_certain_params()
            {
                //arrange
                
                List <Point> positions = new List<Point>();
                SatellitesMock.ForEach(x => positions.Add(x.Position));

                List<double> distances = new List<double>();
                SatellitesMock.ForEach(x => distances.Add((double) x.Radio));
                //act
                var actual = Sut.GetLocation(SatellitesMock);

                //assert            
                Mock.Get(TrilaterationMock).Verify(t => t.TrilateratePosition(
                    It.Is<Point[]>(p => p[0] == positions[0] && p[1] == positions[1] && p[2] == positions[2]),
                    It.Is<double[]>(d => d[0] == distances[0] && d[1] == distances[1] && d[2] == distances[2])),Times.Once);
            }


        }
    }
}
