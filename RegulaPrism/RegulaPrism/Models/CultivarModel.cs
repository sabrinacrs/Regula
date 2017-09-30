using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class CultivarModel : Cultivar 
    {
        public Ciclo Ciclo { get; set; }
        public List<DoencaTolerancia> DoencasTolerancias { get; set; }
    }
}
