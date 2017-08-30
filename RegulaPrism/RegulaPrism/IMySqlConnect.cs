using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism
{
    public interface IMySqlConnect
    {
        bool ConnectDB { get; }
        List<Cultivar> CarregaCultivares();
        List<EpocaSemeadura> CarregaEpocasSemeadura();
        List<CultivarEpocaSemeadura> CarregaCultivarEpocaSemeadura();
        List<CultivarEpocaSemeadura> CarregaCultivarEpocaSemeadura(int cultivarId);
        List<Cultivar> CarregaDados();
    }
}
