
using MLQuasar.Domain.Entities;
using MLQuasar.Domain.Dtos.Responses;
using System.Threading.Tasks;

namespace MLQuasar.Application.Services.Interfaces
{
    public interface ICommunicationService
    {
        public Task<ImperialShipCarrierResponse> ResolveCommunicationAsync(Satellite[] satellites);
        
    }
}
