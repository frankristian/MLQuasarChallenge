
using MLQuasar.Domain.Entities;

namespace MLQuasar.Infrastructure
{
    public interface IDBContext
    {
        Quasar GetDb();
        void PersistChanges();
        void UpdateSatellite(Satellite satellite);
        void ResetDB();
    }
}