using System;
using System.Collections.Generic;
using System.Linq;

namespace MLQuasar.Domain.Extensions
{
    public static class StringListExtentions
    {
        public static List<string> MergeLists(this List<string> me, List<string> second)
        {
            if (me.Count != second.Count)
            {
                throw new ArgumentOutOfRangeException("Ambas listas deben tener la misma cantidad de elementos");
            }

            List<string> lResult = new List<string>();
            foreach ((string value, int i) item in me.Select((value, i) => (value, i)))
            {
                //verifico igualdad de valores
                if (!string.IsNullOrEmpty(item.value) && !string.IsNullOrEmpty(second[item.i])
                    && item.value != second[item.i])
                {
                    lResult = null;
                    throw new ArgumentException("Ambos elementos deben ser iguales si ambos tienen valor asignado");
                }
                if (!string.IsNullOrEmpty(item.value.Trim()))
                {
                    lResult.Add(item.value);
                }
                else
                {
                    lResult.Add(second[item.i]);
                }

            }
            return lResult;
        }

        public static string ComposeMessage(this List<string> me) => string.Join(" ", me);
    }
}
