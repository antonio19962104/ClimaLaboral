using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ML
{
    public class PreguntasLikert
    {
        public int IdPreguntaLikert { get; set; }
        public string PreguntaLikert { get; set; }
        public ML.Preguntas Preguntas { get; set; }
        public ML.Encuesta Encuesta { get; set; }
    }
}
