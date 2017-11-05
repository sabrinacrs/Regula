using RegulaPrism.Models;
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class Talhao
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        [MaxLength(50), NotNull]
        public string Descricao
        {
            get;
            set;
        }

        [NotNull]
        public decimal Tamanho
        {
            get;
            set;
        }

        public DateTime DataDesativacao
        {
            get;
            set;
        }

        [NotNull]
        public int FazendaId
        {
            get;
            set;
        }
    }
}
