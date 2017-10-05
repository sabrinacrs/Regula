using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class CultivarRecomendada
    {
        public int PlantasHectare
        {
            get;
            set;
        }

        public Cultivar Cultivar
        {
            get;
            set;
        }

        public EpocaSemeadura EpocaSemeadura
        {
            get;
            set;
        }

        public List<DoencaTolerancia> DoencasTolerancias
        {
            get;
            set;
        }

        public string Recomendacao
        {
            get;
            set;
        }
    }
}
