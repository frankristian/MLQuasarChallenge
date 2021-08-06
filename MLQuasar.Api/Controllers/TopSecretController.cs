using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Dtos.Queries;

namespace MLQuasar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopSecretController : ControllerBase
    {
        private readonly ISatelliteService _satelliteService;
        private readonly ICommunicationService _communicationService;

        public TopSecretController(ISatelliteService satelliteService, ICommunicationService communicationService)
        {
            _satelliteService = satelliteService ?? 
                throw new ArgumentNullException(nameof(satelliteService));
            _communicationService = communicationService ??
                throw new ArgumentNullException(nameof(communicationService)); ;
        }
        /// <summary>
        /// Permite obtener el posicionamiento de la nave Imperial y decodificación del mensaje a partir
        /// de los parámetros ingresados.
        /// </summary>
        /// <param name="query">Payload con información de la distancia a cada satélite 
        /// y mensaje a decodificar.</param>
        /// <returns>ImperialShipCarrierResponse</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TopSecretQuery query)
        {

            try
            {
                return query == null
                    ? throw new ArgumentNullException(nameof(query))
                    : Ok(await _communicationService.ResolveCommunicationAsync(
                   _satelliteService.GetSatellitesFromQuery(query)));
            }
            catch (Exception ex)
            {
                //_logger.LogError(ex.Message);
                var data = new { Message = "No se puede determinar la información solicitada --> " + ex.Message };
                return new NotFoundObjectResult(data);
            }

        }

    }
}
