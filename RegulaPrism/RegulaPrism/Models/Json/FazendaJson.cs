using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models.Json
{
    public class FazendaJson
    {
        public int id { get; set; }
        public string nome { get; set; }
        public decimal hectares { get; set; }
        public string data_desativacao { get; set; }
        public string cidade { get; set; }
        public string uf { get; set; }
        public string bairro { get; set; }
        public string email { get; set; }
        public string endereco_web { get; set; }
        public string telefone { get; set; }
        public int cli_id { get; set; }
    }
}
