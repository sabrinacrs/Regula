using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class CultivarDoenca
    {
        public int CultivarId
        {
            get;
            set;
        }

        public int DoencaId
        {
            get;
            set;
        }

        public int ToleranciaId
        {
            get;
            set;
        }
    }
}
