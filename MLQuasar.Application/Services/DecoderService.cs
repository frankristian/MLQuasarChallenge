using MLQuasar.Application.Services;
using MLQuasar.Application.Services.Interfaces;
using MLQuasar.Domain.Entities;
using MLQuasar.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MLQuasar.Services
{
    public class DecoderService : IDecoderService
    {
        private readonly ITrilaterationService _trilaterationService;

        public DecoderService(ITrilaterationService trilaterationService) 
        {
            _trilaterationService = trilaterationService;
        }
        public string GetMessage(IEnumerable<Satellite> satellites)
        {
           List<string> finalMessagges = satellites.First().Message.ToList();
            satellites.Skip(1).ToList()
                .ForEach(x =>
                {
                    finalMessagges = finalMessagges.MergeLists(x.Message.ToList());
                });
           
            return finalMessagges.ComposeMessage();
            
        }

        public Point GetLocation(IEnumerable<Satellite> satellites)
        {
            List<Point> positions = new List<Point>();
            satellites.ToList().ForEach(x => positions.Add(x.Position));
                            
            List<double> distances = new List<double>();
            satellites.ToList().ForEach(x => distances.Add((double)x.Radio));
                           
            return _trilaterationService.TrilateratePosition(positions.ToArray(), distances.ToArray());
        }

    }
}
