using System.Collections.Generic;

namespace MLQuasar.Domain.Entities
{
    public class Satellite
    {
        public IEnumerable<string> Message { get; set; }
        public double? Radio { get; set; }
        public string Name { get; set; }
        public Point Position { get; set; }


    }
}
