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
        public String Descricao
        {
            get;
            set;
        }

        public DateTime DataDesativacao
        {
            get;
            set;
        }
    }
}
