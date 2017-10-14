using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Services
{
    public interface IInformacoesManuais
    {
        InformacaoManual InformacoesClienteCreate();
        InformacaoManual InformacoesLogin();
    }
}
