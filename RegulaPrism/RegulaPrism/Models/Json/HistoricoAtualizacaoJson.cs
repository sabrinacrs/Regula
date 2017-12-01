using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models.Json
{
    public class HistoricoAtualizacaoJson
    {
        public int his_id { get; set; }
        public string data_atualizacao { get; set; }
        public int adm_id { get; set; }
        public string status { get; set; }
    }
}
