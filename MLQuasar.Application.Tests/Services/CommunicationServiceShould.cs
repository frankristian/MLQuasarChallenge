using System.Threading.Tasks;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Entities;
using MLQuasar.Services;
using Moq;
using FluentAssertions;
using System;
using Xunit;
using System.Collections.Generic;
using MLQuasar.Domain.Responses;

namespace MLQuasar.Application.Tests.Services
{
    public class CommunicationServiceShould
    {
        public CommunicationService Sut;
        public IDecoderService DecoderService;
       
        
        public CommunicationServiceShould()
        {
            DecoderService = Mock.Of<IDecoderService>();
            Sut = new CommunicationService(DecoderService);
        }
        public class TheConstructorShould : CommunicationServiceShould
        {
            [Fact]
            public void Throw_ArgumentNullException_when_DecoderService_is_null()
            {
                // Arrange
                DecoderService = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => new CommunicationService(DecoderService));
            }
           
        }

        public class TheMethod_ResolveCommunicationAsyncShould : CommunicationServiceShould
        {
            public Satellite[] SatellitesMock = {
                        new Satellite { Name = "kenobi",
                            Radio = 100,
                            Message = new List<string> { "este", "", "", "mensaje", "" }
                           
                        },
                        new Satellite { Name = "skywalker",
                            Radio = 115.5,
                            Message = new List<string> { "", "es", "", "", "secreto" }
                            
                        },
                        new Satellite { Name = "sato",
                            Radio = 142.7,
                            Message = new List<string> { "este", "", "un", "", "" } 
                            
                        }
                    };
            public ImperialShipCarrierResponse ImperialShipCarrierResponseMock = new ImperialShipCarrierResponse
            {
                Message = "este es un mensaje secreto",
                Position = new Point() { X = -399.7859125000032, Y = 2782.0142249999917 }
            };

            [Fact]
            public async Task Throws_ArgumentNullExceptionAsync()
            {
                //arrange
                Satellite[] satellites = null;

                //act
                
                //assert
                await Assert.ThrowsAsync<ArgumentNullException>(
                    "satellites"
                    , ()=> Sut.ResolveCommunicationAsync(satellites));
            }
            [Fact]
            public async Task Throws_ArgumentExceptionAsync()
            {
                //arrange
                Satellite[] satellites = { new Satellite(), new Satellite()};

                //act

                //assert
                await Assert.ThrowsAsync<ArgumentException>(() => Sut.ResolveCommunicationAsync(satellites));

            }

            [Fact]
            public async Task Return_ImperialShipCarrierResponse()
            {
                //arrange
                //act
                var actual = await Sut.ResolveCommunicationAsync(SatellitesMock);
                //assert
                actual.Should().BeOfType<ImperialShipCarrierResponse>();
            }
            [Fact]
            public async Task Invoke_GetLocation_from_DecoderService()
            {
                //arrange
                //act
                var actual = await Sut.ResolveCommunicationAsync(SatellitesMock);
                //assert
                Mock.Get(DecoderService).
                    Verify(d => d.GetLocation(SatellitesMock), Times.Once);
                //actual.Should().BeOfType<ImperialShipCarrierResponse>();
            }
             [Fact]
            public async Task Invoke_GetMessage_from_DecoderService()
            {
                //arrange
                //act
                var actual = await Sut.ResolveCommunicationAsync(SatellitesMock);
                //assert
                Mock.Get(DecoderService).
                    Verify(d => d.GetMessage(SatellitesMock), Times.Once);
                
            }

            [Fact]
            public async Task Return_expected_result_from_DecoderService()
            {
                //arrange
                Mock.Get(DecoderService)
                    .Setup(d => d.GetLocation(SatellitesMock))
                    .Returns(Mock.Of<Point>(p => 
                    p.X == -399.7859125000032 && 
                    p.Y == 2782.0142249999917));
                Mock.Get(DecoderService)
                    .Setup(d => d.GetMessage(SatellitesMock))
                    .Returns("este es un mensaje secreto");
                //act
                var actual = await Sut.ResolveCommunicationAsync(SatellitesMock);
                //assert
                actual.Should().BeEquivalentTo(ImperialShipCarrierResponseMock);

            }
        }
    }
}
