using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Dtos.Queries;
using System;
using System.Collections.Generic;
using MLQuasar.Domain.Entities;
using System.Linq;
using MLQuasar.Domain.Repositories;

namespace MLQuasar.Application.Services
{
    public class SatelliteService : ISatelliteService
    {
        readonly IQuasarRepository _repository;
        public SatelliteService(IQuasarRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

                
        public Satellite[] GetAll()
        {
            return _repository.GetAll();
        }
        public Satellite[] GetSatellitesFromQuery(TopSecretQuery topSecretQuery)
        {
            List<Satellite> satellites = new List<Satellite>();
             topSecretQuery.Satellites.ToList().ForEach(item =>
            {
                if (string.IsNullOrEmpty(item.Name))
                    throw new ArgumentException("Debe proporcionar los nombres de los satélites");
                satellites.Add(GetSatelliteFromQuery(item));
            });
            return satellites.ToArray();
        }

       public void UpdateSatellite(SatelliteQuery query)
        {
            _repository.Save(GetSatelliteFromQuery(query));
        }

        public void ResetSatellites()
        {
            _repository.Reset();
        }

        private Satellite GetSatelliteFromQuery(SatelliteQuery query)
        {
            var satellite = _repository.GetByName(query.Name) ??
                throw new ArgumentException("No se encuentra el satélite con el nombre solicitado: " + query.Name);

            satellite.Message = query.Message;
            satellite.Radio = (double)query.Distance;
            return satellite;
        }

    }
}
