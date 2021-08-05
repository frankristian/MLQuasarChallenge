using System.Collections.Generic;

namespace MLQuasar.Domain.Entities
{
    public class Satellite
    {
        public IEnumerable<string> Message { get; set; }
        public double? Radio { get; set; }
        public string Name { get; set; }
        public Point Position { get; set; }

        //private readonly IConfiguration _configuration;
       // public Satellite() { }

        //public Satellite(string name, double distance, IEnumerable<string> message, IConfiguration configuration)
        //{
        //    if (string.IsNullOrEmpty(name))
        //        throw new ArgumentNullException("Name", "El nombre del satélite no puede estar vacío o nulo");
        //    if (message == null || message.Count() == 0)
        //        throw new ArgumentNullException("Message", "el mensaje no puede estar vacío");

        //    Message = message;
        //    Name = name;
        //    Radio = distance;
        //    _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        //}


        //public Point GetLocation()
        //{
        //    if (Position == null)
        //        SetPositionFromConfig();

        //    return Position;
        //}
        //private void SetPositionFromConfig()
        //{
        //    if (double.TryParse(_configuration["SatelliteLocations:" + Name + ":X"], out double x) &&
        //        double.TryParse(_configuration["SatelliteLocations:" + Name + ":Y"], out double y))
        //    {
        //        Position = new Point(x, y);
        //    }
        //    else
        //    {
        //        throw new ArgumentException("No se pueden leer las coordenadas del satélite: " + Name);
        //    }

        //}

    }
}
