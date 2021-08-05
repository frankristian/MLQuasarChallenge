using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Moq;
using FluentAssertions;
using Xunit;

using MLQuasar.Api.Controllers;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Entities;
using MLQuasar.Domain.Queries;
using MLQuasar.Domain.Responses;

namespace MLQuasar.Api.Tests.Controllers
{
    public class TopSecretControllerShould
    {
        public TopSecretController Sut;
        public ISatelliteService SatelliteService;
        public ICommunicationService CommunicationService;

        public TopSecretControllerShould()
        {
            SatelliteService = Mock.Of<ISatelliteService>();
            CommunicationService = Mock.Of<ICommunicationService>();
            Sut = new TopSecretController(SatelliteService, CommunicationService);
        }

        public class TheConstructorShould : TopSecretControllerShould
        {
            [Fact]
            public void Throw_ArgumentNullException_when_CommunicationService_is_null()
            {
                // Arrange
                CommunicationService = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => new TopSecretController(SatelliteService, CommunicationService));
            }
            [Fact]
            public void Throw_ArgumentNullException_when_SatelliteService_is_null()
            {
                // Arrange
                this.SatelliteService = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => new TopSecretController(SatelliteService, CommunicationService));
            }
        }

        public class TheMethodPostShould : TopSecretControllerShould
        {
            private TopSecretQuery queryMock = new TopSecretQuery()
            {
                Satellites = new List<SatelliteQuery>(){
                        new SatelliteQuery { Name = "kenobi", Distance = 1.1, Message = new List<string> { "este", "", "", "mensaje", "" } },
                        new SatelliteQuery { Name = "skywalker", Distance = 0.1, Message = new List<string> { "", "es", "", "", "secreto" } },
                        new SatelliteQuery { Name = "sato", Distance = 0.9, Message = new List<string> { "este", "", "un", "", "" } }
                    }
            };

            [Fact]
            public async Task Invoke_ResolveCommunication()
            {
                //arrange

                //act
                await Sut.Post(queryMock);

                //Assert
                Mock.Get(CommunicationService)
                    .Verify(s => s.ResolveCommunicationAsync(It.IsAny<Satellite[]>()), Times.Once);

            }
            [Fact]
            public async Task Invoke_GetSatellitesFromQuery()
            {
                //arrange

                //act
                await Sut.Post(queryMock);

                //Assert
                Mock.Get(SatelliteService)
                    .Verify(s => s.GetSatellitesFromQuery(It.Is<TopSecretQuery>(q => q == queryMock)), Times.Once);

            }

            [Fact]
            public async Task Return_an_OkObjectResult_Type()
            {
                // Arrange

                // Act
                var result = await Sut.Post(queryMock);

                // Assert
                result.Should().BeOfType<OkObjectResult>();
            }

            [Fact]
            public async Task Return_an_OkObjectResult()
            {
                // Arrange


                ImperialShipCarrierResponse expected = new ImperialShipCarrierResponse
                {
                    Message = "este es un mensaje secreto",
                    Position = new Point { X = -58.315252587138595, Y = -69.55141837312165 }
                };
                Mock.Get(CommunicationService)
                     .Setup(s => s.ResolveCommunicationAsync(It.IsAny<Satellite[]>()))
                     .ReturnsAsync(expected);
                // Act
                var result = await Sut.Post(queryMock);

                // Assert
                result.Should().BeOfType<OkObjectResult>();
                result.As<OkObjectResult>().Value.Should().BeSameAs(expected);

            }

            [Fact]
            public async Task Return_an_NotFoundObjectResult()
            {
                // Arrange
                Mock.Get(SatelliteService)
                     .Setup(s => s.GetSatellitesFromQuery(It.IsAny<TopSecretQuery>()))
                     .Throws(new ArgumentException());
                
                // Act
                var result = await Sut.Post(queryMock);

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);

            }
        }

    }
}
