
using MLQuasar.Domain.Entities;
using MLQuasar.Domain.Repositories;
using System.Linq;


namespace MLQuasar.Infrastructure.Repositories
{
    public class QuasarRepository : IQuasarRepository
    {
        private readonly IDBContext dbContext;
        

        public QuasarRepository(IDBContext dbContext)
        {
            this.dbContext = dbContext;
            
        }

        public Satellite[] GetAll()
            => dbContext.GetDb().Satellites.ToArray();

        public Satellite GetByName(string name)
            => dbContext.GetDb().Satellites
                    .SingleOrDefault(m => string.Compare(m.Name,name,true) == 0);

        public void Reset()
        {
            dbContext.ResetDB();
        }

        public void Save(Satellite satellite)
        {
            dbContext.UpdateSatellite(satellite);
            dbContext.PersistChanges();
    
        }

    }
}
