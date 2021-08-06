using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MLQuasar.Api.Queries
{
    public class TopSecretQuery
    {
        [Required]
        public IEnumerable<SatelliteQuery> Satellites { get; set; }

    }
}
