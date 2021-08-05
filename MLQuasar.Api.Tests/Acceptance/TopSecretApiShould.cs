using MLQuasar.Api.Controllers;
using MLQuasar.Domain.Queries;
using System.Collections.Generic;
using Xunit;

namespace MLQuasar.Api.Tests.Acceptance
{
    public class TopSecretApiShould
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
        public void Return_imperial_ship_carrier()
        {
            //Arrange - given
            //var controller = new TopSecretController();
            //var expected
            //act - when 
            //var result = await controller.Post();

            //assert - then
            
        }
    }
}
