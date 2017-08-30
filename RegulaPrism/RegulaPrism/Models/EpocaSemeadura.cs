using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class EpocaSemeadura
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

        public char Status
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
