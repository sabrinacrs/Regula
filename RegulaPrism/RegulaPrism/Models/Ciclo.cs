using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class Ciclo
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

        public override string ToString()
        {
            return string.Format("Descricao={0}", Descricao);
        }
    }
}
