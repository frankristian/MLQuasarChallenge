using MLQuasar.Application.Services;
using MLQuasar.Domain.Dtos.Queries;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;
using MLQuasar.Domain.Entities;
using System.Linq;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Repositories;

namespace MLQuasar.Application.Tests.Services
{
    public class SatelliteServiceShould
    {
        public IQuasarRepository Repository;
        public SatelliteService Sut;


        public TopSecretQuery queryMock = new TopSecretQuery()
        {
            Satellites = new List<SatelliteQuery>(){
                        new SatelliteQuery { Name = "kenobi", Distance = 1.1, Message = new List<string> { "este", "", "", "mensaje", "" } },
                        new SatelliteQuery { Name = "skywalker", Distance = 0.1, Message = new List<string> { "", "es", "", "", "secreto" } },
                        new SatelliteQuery { Name = "sato", Distance = 0.9, Message = new List<string> { "este", "", "un", "", "" } }
                    }
        };
        public SatelliteServiceShould()
        {
            Repository = Mock.Of<IQuasarRepository>();
            Sut = new SatelliteService(Repository);
        }
        public class TheConstructorShould : SatelliteServiceShould
        {
            [Fact]
            public void Throw_ArgumentNullException_when_DecoderService_is_null()
            {
                // Arrange
                Repository = null;

                // Act & Assert
                Assert.Throws<ArgumentNullException>(() => new SatelliteService(Repository));
            }

            
            public class TheMethod_GetAllShould : SatelliteServiceShould
            {
                [Fact]
                public void Invoke_GetAll_form_repository()
                {
                    //arrange
                    //act
                    var result = Sut.GetAll();
                    //assert
                    Mock.Get(Repository).Verify(r => r.GetAll(), Times.Once);
                }
            }
           
            public class TheMethod_GetSatellitesFromQuery : SatelliteServiceShould
            {
                [Fact]
                public void Return_expected_object_type()
                {
                    //arrange

                    //act
                    var serviceMock = Mock.Of<ISatelliteService>();
                    Mock.Get(serviceMock).Setup(s =>
                    s.GetSatellitesFromQuery(It.IsAny<TopSecretQuery>()))
                        .Returns(It.IsAny<Satellite[]>);
                    //assert

                }

                [Fact]
                public void Throws_an_ArgumentException_when_a_satellite_name_is_missing()
                {
                    //arrange
                    queryMock.Satellites.ToArray()[1].Name = string.Empty;
                    //act

                    //assert
                    Assert.Throws<ArgumentException>(() => Sut.GetSatellitesFromQuery(queryMock));

                }

                [Fact]
                public void Throws_an_ArgumentException_when_a_satellite_name_is_not_found()
                {
                    //arrange
                    queryMock.Satellites.ToArray()[1].Name = "pepe";
                    //act

                    //assert
                    Assert.Throws<ArgumentException>(() => Sut.GetSatellitesFromQuery(queryMock));

                }
                [Fact]
                public void Return_expected_result()
                {
                    //arrange
                    Mock.Get(Repository).Setup(r => r.GetByName(It.IsAny<string>())).Returns(new Satellite());
                    //act
                    var result = Sut.GetSatellitesFromQuery(queryMock);
                    //assert
                    result.Length.Should().Be(3);

                }
            }
            public class TheMethod_UpdateSatellite : SatelliteServiceShould
            {
                [Fact]
                public void Invoke_Save_method_from_repository()
                {
                    //arrange
                    Mock.Get(Repository).Setup(r => r.GetByName(It.IsAny<string>())).Returns(new Satellite());

                    //act
                    Sut.UpdateSatellite(queryMock.Satellites.First());
                    //assert
                    Mock.Get(Repository).Verify(r => r.Save(It.IsAny<Satellite>()), Times.Once);
                }
            }
            public class TheMethod_ResetSatellites : SatelliteServiceShould
            {
                [Fact]
                public void Invoke_Reset_method_from_repository()
                {
                    
                    //act
                    Sut.ResetSatellites();
                    //assert
                    Mock.Get(Repository).Verify(r => r.Reset(), Times.Once);
                }
            }
        }
    }
}
