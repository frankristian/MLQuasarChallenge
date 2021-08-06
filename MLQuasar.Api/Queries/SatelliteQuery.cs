using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MLQuasar.Api.Queries
{
    public class SatelliteQuery
    {
        public string Name { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Only positive distance allowed.")]
        public double? Distance { get; set; }
        [Required]
        public IEnumerable<string> Message { get; set; }
    }
}
