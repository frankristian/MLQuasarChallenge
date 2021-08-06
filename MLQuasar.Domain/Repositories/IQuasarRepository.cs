using MLQuasar.Domain.Entities;
using System;

namespace MLQuasar.Domain.Repositories
{
    public interface IQuasarRepository
    {
        Satellite[] GetAll();
        Satellite GetByName(String name);
        void Save(Satellite satellite);
        void Reset();
    }
}
