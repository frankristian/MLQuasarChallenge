
using MLQuasar.Domain.Entities;

namespace MLQuasar.Domain.Extensions
{
    public static class PointExtensions
    {
        public static double[] ToArray(this Point me) 
        {
            double[] result = { me.X, me.Y };
            return result;
        }
    }
}
