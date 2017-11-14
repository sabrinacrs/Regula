using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models.Json
{
    public class CultivarJson
    {
        public int id { get; set; }

        public string nome { get; set; }

        public string altura_planta { get; set; }

        public string fertilidade { get; set; }

        public string regulador { get; set; }

        public decimal rendimento_fibra_minimo { get; set; }

        public decimal rendimento_fibra_maximo { get; set; }
        public decimal peso_capulho_minimo { get; set; }

        public decimal peso_capulho_maximo { get; set; }

        public decimal comprimento_fibra_minimo { get; set; }

        public decimal comprimento_fibra_maximo { get; set; }
        public decimal micronaire_minimo { get; set; }

        public decimal micronaire_maximo { get; set; }

        public decimal resistencia_minimo { get; set; }
        public decimal resistencia_maximo { get; set; }

        public decimal peso_sementes_minimo { get; set; }
        public decimal peso_sementes_maximo { get; set; }

        public string status { get; set; }

        public string data_desativacao { get; set; }

        public int cic_id { get; set; }
    }
}
