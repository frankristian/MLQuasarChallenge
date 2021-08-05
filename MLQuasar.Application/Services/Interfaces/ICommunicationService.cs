
using MLQuasar.Domain.Entities;
using MLQuasar.Domain.Responses;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MLQuasar.Application.Services.Interfaces
{
    public interface ICommunicationService
    {
        public Task<ImperialShipCarrierResponse> ResolveCommunicationAsync(Satellite[] satellites);
        
    }
}
