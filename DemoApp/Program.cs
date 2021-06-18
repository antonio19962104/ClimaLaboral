using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = BL.ReporteD4U.GetPromedioPracticasCulturalesByUNEGocioEE("turismo", 2020, 1);
            Console.Write(data);
        }
    }
}
