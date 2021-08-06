using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Dtos.Queries;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MLQuasar.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TopSecret_SplitController : ControllerBase
    {
        private readonly ICommunicationService _communicationService;
        private readonly ISatelliteService _satelliteService;

        public TopSecret_SplitController(ISatelliteService satelliteService, ICommunicationService communicationService) 
        {

            _satelliteService = satelliteService ??
                throw new ArgumentNullException(nameof(satelliteService));
            _communicationService = communicationService ??
                throw new ArgumentNullException(nameof(communicationService)); ;
        }
        /// <summary>
        /// Permite obtener el posicionamiento de la nave Imperial y decodificación del mensaje a partir
        /// de los datos guardados previamente para cada satélite mediante método POST.
        /// </summary>
        /// <returns>ImperialShipCarrierResponse</returns>
        // GET api/
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var satellites = _satelliteService.GetAll();
                return Ok(await _communicationService.ResolveCommunicationAsync(satellites));
            }
            catch (Exception ex)
            {
                var data = new { Message = "No se puede determinar la información solicitada --> " + ex.Message };
                return new NotFoundObjectResult(data);
            }

        }
        /// <summary>
        /// Permite guardar la información de distancia y mensaje para un satélite en particular
        /// </summary>
        /// <param name="satellite_name">Nombre del satélite al cual le vamos a resultizar la información --> (kenobi, skywalker y sato)</param>
        /// <param name="satellite">Payload con los datos distancia y mensaje para resultizar la información guardada</param>
        /// <returns></returns>
        // POST api/<TopSecret_SplitController>
        [HttpPost("{satellite_name}")]
        public bool Post(string satellite_name, [FromBody] SatelliteQuery satellite)
        {
            try
            {
                satellite.Name = !string.IsNullOrEmpty(satellite_name)
                    ? satellite_name : throw new ArgumentNullException(nameof(satellite_name));
                _satelliteService.UpdateSatellite(satellite);
               
                return true;
                
            }
            catch (Exception)
            {

                throw;
            }

        }
        /// <summary>
        /// Vuelve al estado inicial la información de los satélites kenobi, skywalker y sato
        /// </summary>
        // DELETE api/
        [HttpDelete]
        public void Delete()
        {
           _satelliteService.ResetSatellites();

        }


    }
}
