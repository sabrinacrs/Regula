using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class SemeaduraModel
    {
        public int Id { get; set; }

        public decimal QuilosSementes { get; set; }

        public decimal Germinacao { get; set; }

        public decimal MetrosLineares { get; set; }

        public Talhao Talhao { get; set; }

        public Cultivar Cultivar { get; set; }

        public EpocaSemeadura EpocaSemeadura { get; set; }

        public DateTime Data { get; set; }
    }
}
