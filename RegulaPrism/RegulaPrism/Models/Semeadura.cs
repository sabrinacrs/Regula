using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class Semeadura
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        [NotNull]
        public decimal QuilosSementes
        {
            get;
            set;
        }

        [NotNull]
        public decimal Germinacao
        {
            get;
            set;
        }

        [NotNull]
        public decimal MetrosLineares
        {
            get;
            set;
        }

        public int TalhaoId
        {
            get;
            set;
        }

        public int CultivarEpocaSemeaduraCultId
        {
            get;
            set;
        }

        public int CultivarEpocaSemeaduraEpId
        {
            get;
            set;
        }
    }
}
