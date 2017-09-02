using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism
{
    public class ItemMenuLateral
    {
        public ItemMenuLateral(String Titulo, String Descricao)
        {
            this.Titulo = Titulo;
            this.Descricao = Descricao;
        }
        public string Titulo
        {
            get;
            set;
        }

        public string Descricao
        {
            get;
            set;
        }
    }
}
