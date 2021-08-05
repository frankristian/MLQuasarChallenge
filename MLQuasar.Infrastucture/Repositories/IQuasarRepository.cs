using MLQuasar.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace MLQuasar.Infrastructure.Repositories
{
    public interface IQuasarRepository
    {
        Satellite[] GetAll();
        Satellite GetByName(String name);
        void Save(Satellite satellite);
        void Reset();
    }
}
