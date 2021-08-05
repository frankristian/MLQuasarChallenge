using MLQuasar.Domain.Entities;

namespace MLQuasar.Application.Services.Interfaces
{
    public interface ITrilaterationService
    {
        public Point TrilateratePosition(Point[] points, double[] distances);
    }
}