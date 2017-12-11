using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class InformacaoManual
    {
        public string Titulo { get; set; }
        public string Texto { get; set; }

        public string TitleInsert { get; set; }
        public string TextInsert { get; set; }
        public string TitleUpdate { get; set; }
        public string TextUpdate { get; set; }
        public string TitleDelete { get; set; }
        public string TextDelete { get; set; }
        public string TitleErrors { get; set; }
        public string TextErrors { get; set; }
        public string TitleFunctions { get; set; }
        public string TextFunctions { get; set; }
        public string LinkHelpOnline { get; set; }

        public string Resultados { get; set; }
    }
}
