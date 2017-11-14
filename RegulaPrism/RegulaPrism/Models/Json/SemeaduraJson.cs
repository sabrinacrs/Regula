using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models.Json
{
    public class SemeaduraJson
    {
        public int id { get; set; }
        public decimal quilos_sementes { get; set; }
        public decimal germinacao { get; set; }
        public decimal metros_lineares { get; set; }
        public int talhao_id { get; set; }
        public int cultivar_id { get; set; }
        public int epoca_semeadura_id { get; set; }
        public string data { get; set; }
    }
}
