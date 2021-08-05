using MLQuasar.Domain.Entities;
using System.Collections.Generic;

namespace MLQuasar.Application.Services.Interfaces
{
    public interface IDecoderService 
    {
        public Point GetLocation(IEnumerable<Satellite> satellites);
        public string GetMessage(IEnumerable<Satellite> satellites);
    }
}
