using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class Tolerancia
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        [MaxLength(45), NotNull]
        public string Descricao
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public DateTime DataDesativacao
        {
            get;
            set;
        }

        [MaxLength(45), NotNull]
        public string Sigla
        {
            get;
            set;
        }
    }
}
