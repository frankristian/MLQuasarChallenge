using System.Threading.Tasks;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Entities;
using MLQuasar.Domain.Dtos.Responses;

namespace MLQuasar.Services
{
    public class CommunicationService : ICommunicationService
    {

        private readonly IDecoderService _decoder;

        public CommunicationService(IDecoderService decoder)
        {
            _decoder = decoder ?? throw new System.ArgumentNullException(nameof(decoder));
        }

        public async Task<ImperialShipCarrierResponse> ResolveCommunicationAsync(Satellite[] satellites)
        {
            if(satellites == null) throw new System.ArgumentNullException(nameof(satellites));
            if (satellites.Length != 3)
                throw new System.ArgumentException("La cantidad de satélites es incorrecta, " +
                    "deben ser exactamente 3 se encontraron: " + satellites.Length);

            ImperialShipCarrierResponse response = new ImperialShipCarrierResponse
            {

                //1 - Resolver la posición de la nave Imperial
                Position = _decoder.GetLocation(satellites),
                //2 - Resolver y decodificar el mensaje
                Message = _decoder.GetMessage(satellites)
            };
            return await Task.FromResult(response);
        }

       
    }

}
