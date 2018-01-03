using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models.Json
{
    public class TalhaoJson
    {
        public int id { get; set; }
        public string descricao { get; set; }
        public decimal tamanho { get; set; }
        public DateTime data_desativacao { get; set; }
        public int faz_id { get; set; }
    }
}
