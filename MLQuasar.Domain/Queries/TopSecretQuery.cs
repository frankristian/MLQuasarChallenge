using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLQuasar.Domain.Queries
{
    public class TopSecretQuery
    {
        [Required]
        public IEnumerable<SatelliteQuery> Satellites { get; set; }

    }
}
