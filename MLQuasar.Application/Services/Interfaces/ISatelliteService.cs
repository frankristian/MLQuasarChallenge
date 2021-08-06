using MLQuasar.Domain.Entities;
using MLQuasar.Domain.Queries;

namespace MLQuasar.Application.Services.Interfaces
{
    public interface ISatelliteService
    {
        void UpdateSatellite(SatelliteQuery query);
        Satellite[] GetAll();
        void ResetSatellites();
        Satellite[] GetSatellitesFromQuery(TopSecretQuery topSecretQuery);
    }
}