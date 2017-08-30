using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class CultivarEpocaSemeadura
    {
        public int CultivarId
        {
            get;
            set;
        }

        public int EpocaSemeaduraId
        {
            get;
            set;
        }

        public decimal PlantasHa
        {
            get;
            set;
        }
    }
}
