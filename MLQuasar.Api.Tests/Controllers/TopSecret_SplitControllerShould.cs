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
    public class TopSecret_SplitControllerShould
    {
        public TopSecret_SplitController Sut;
        public ISatelliteService SatelliteService;
        public ICommunicationService CommunicationService;
        public TopSecret_SplitControllerShould()
        {

            SatelliteService = Mock.Of<ISatelliteService>();
            CommunicationService = Mock.Of<ICommunicationService>();
            Sut = new TopSecret_SplitController(SatelliteService, CommunicationService);
        }

        public class TheConstructorShould : TopSecret_SplitControllerShould
        {
            [Fact]
            public void Throw_ArgumentNullException_when_CommunicationService_is_null()
            {
                // Arrange
                CommunicationService = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => new TopSecret_SplitController(SatelliteService, CommunicationService));
            }
            [Fact]
            public void Throw_ArgumentNullException_when_SatelliteService_is_null()
            {
                // Arrange
                this.SatelliteService = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => new TopSecret_SplitController(SatelliteService, CommunicationService));
            }
        }
        public class TheMethodGetShould : TopSecret_SplitControllerShould
        {
            [Fact]
            public async Task Invoke_GetAll_from_SatelliteService()
            {
                //arrange
                //act
                await Sut.Get();
                //Assert
                Mock.Get(SatelliteService)
                    .Verify(s => s.GetAll(), Times.Once);
            }
            [Fact]
            public async Task Invoke_ResolveCommunication_from_CommunicationService()
            {
                //arrange

                //act
                await Sut.Get();
                //assert
                Mock.Get(CommunicationService)
                    .Verify(s => s.ResolveCommunicationAsync(It.IsAny<Satellite[]>()), 
                    Times.Once);
            }

            [Fact]
            public async Task Return_an_OkObjectResult_Type()
            {
                // Arrange

                // Act
                var result = await Sut.Get();

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
                var result = await Sut.Get();

                // Assert
                result.Should().BeOfType<OkObjectResult>();
                result.As<OkObjectResult>().Value.Should().BeSameAs(expected);

            }

            [Fact]
            public async Task Return_an_NotFoundObjectResult()
            {
                // Arrange
                Mock.Get(CommunicationService)
                     .Setup(s => s.ResolveCommunicationAsync(It.IsAny<Satellite[]>()))
                     .Throws(new ArgumentException());

                // Act
                var result = await Sut.Get();

                // Assert
                Assert.IsType<NotFoundObjectResult>(result);

            }
        }
        public class TheMethodPostShould : TopSecret_SplitControllerShould
        {
            SatelliteQuery queryMock = new SatelliteQuery { Name = "kenobi", Distance = 1.1, Message = new List<string> { "este", "", "", "mensaje", "" } };
                        //new SatelliteQuery { Name = "skywalker", Distance = 0.1, Message = new List<string> { "", "es", "", "", "secreto" } },
                        //new SatelliteQuery { Name = "sato", Distance = 0.9, Message = new List<string> { "este", "", "un", "", "" } }
                
            

           
            [Fact]
            public void Invoke_UpdateSatellite()
            {
                //arrange

                //act
                Sut.Post("aName",queryMock);

                //Assert
                Mock.Get(SatelliteService)
                    .Verify(s => s.UpdateSatellite(It.Is<SatelliteQuery>(q => q == queryMock)), Times.Once);

            }
            [Theory]
            [InlineData("")]
            [InlineData(null)]
            public void Throw_ArgumentNullException_when_satellite_name_is_nullOrEmpty(string name)
            {
                //arrange

                //act
                Action result = () => Sut.Post(name, queryMock);

                //Assert
                result.Should().Throw<ArgumentNullException>();
                

            }

            [Fact]
            public void Return_an_true_value()
            {
                // Arrange


                // Act
                var result = Sut.Post("kenobi",queryMock);

                // Assert
                result.Should().BeTrue();
            }

          
        }

      
        public class TheMethodDeleteShould : TopSecret_SplitControllerShould
        {
            [Fact]
            public void Invoke_ResetSatellites()
            {
                //arrange

                //act
                Sut.Delete();

                //Assert
                Mock.Get(SatelliteService)
                    .Verify(s => s.ResetSatellites(), Times.Once);
            }
        }

    }
}
