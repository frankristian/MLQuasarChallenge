using System;
using System.Collections.Generic;
using System.Text;

namespace MLQuasar.Domain.Entities
{
    public class Quasar
    {
        public IEnumerable<Satellite> Satellites { get; set; }
    }
}
