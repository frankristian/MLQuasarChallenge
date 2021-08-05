using Microsoft.Extensions.Configuration;
using MLQuasar.Domain.Entities;
using System;
using System.Linq;
using System.Text.Json;


namespace MLQuasar.Infrastructure
{
    public class DBContext : IDBContext
    {
        private readonly IConfiguration _configuration;
        private readonly JsonSerializerOptions _options;
        private readonly string _path;
        private Quasar _db;

        public DBContext(IConfiguration configuration) 
        {
            _configuration = configuration;
            _options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true,
                WriteIndented = true
            };
            _path = _configuration.GetConnectionString("dbPath");
        }
        public Quasar GetDb()
        {
            try
            {
                if(_db == null) { 
                    var jsonString = System.IO.File.ReadAllText(_path);
                    _db = JsonSerializer.Deserialize<Quasar>(jsonString, _options);
                }
                return _db;
            }
            catch (Exception)
            {

                throw;
            }
            
        }
        public void UpdateSatellite(Satellite satellite) 
        {
            try
            {
                var record = GetDb().Satellites
                    .Single(m => m.Name == satellite.Name);

                record = satellite;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ResetDB()
        {
            _db.Satellites.All(x => {
                x.Message = null;
                x.Radio = null;
                return true;
            });
            PersistChanges();
        }
        public void PersistChanges() 
        {
            try
            {
                var output = JsonSerializer.Serialize(_db);
                System.IO.File.WriteAllText(_path, output);
                
            }
            catch (Exception)
            {
                throw;

            }
        }

    }
}
