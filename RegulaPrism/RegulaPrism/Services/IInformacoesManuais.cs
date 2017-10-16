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
        InformacaoManual InformacoesLogin();
        InformacaoManual InformacoesClienteCreate();
        InformacaoManual InformacoesClienteUpdate();
        InformacaoManual InformacoesFazendaCreate();
        InformacaoManual InformacoesFazendaUpdate();
        InformacaoManual InformacoesFazendaList();
        InformacaoManual InformacoesTalhaoCreate();
        InformacaoManual InformacoesTalhaoUpdate();
        InformacaoManual InformacoesTalhaoList();
        InformacaoManual InformacoesHomePage();
        InformacaoManual InformacoesCultivarList();
        InformacaoManual InformacoesCultivarSelected();
        InformacaoManual InformacoesCultivarCiclo();
        InformacaoManual InformacoesCultivarRendimento();
        InformacaoManual InformacoesCultivarDoencas();
        InformacaoManual InformacoesSemeadura();
        InformacaoManual InformacoesCalcularSemeadura();
        InformacaoManual InformacoesFazendaSemeaduraList();
        InformacaoManual InformacoesFazendaSemeaduraSelected();
    }
}
