using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public static class Truncate
    {
        public static string name = string.Empty;
        public static double TruncateNumber(this double value, int decimales)
        {
            //double aux_value = Math.Pow(10, decimales);
            //return (Math.Truncate(value * aux_value) / aux_value);
            double val = Math.Round(value, decimales, MidpointRounding.AwayFromZero);
            return val;
        }
        public static double TruncateNumber(this double value)
        {
            //double aux_value = Math.Pow(10, decimales);
            //return (Math.Truncate(value * aux_value) / aux_value);
            double val = Math.Round(value, 2, MidpointRounding.ToEven);
            return val;
        }
    }
}
