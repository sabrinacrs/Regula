using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class HistoricoAtualizacao
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Status { get; set; }

        public string DataAtualizacao { get; set; }
    }
}
