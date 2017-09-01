using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Services
{
    public class CloneDatabaseServer
    {
        
        private IRegulaApiService _regulaApiService;
        private IMySqlConnect _config;

        public CloneDatabaseServer(IRegulaApiService regulaApiService)
        {
            _regulaApiService = regulaApiService;

            // verificar se houve mudança na base de dados do servidor
            // se houver, verifica quais tabelas mudaram
            // clona as tabelas alteradas

            _config = Xamarin.Forms.DependencyService.Get<IMySqlConnect>();
        }

        private void SaveCultivar()
        {
            List<Cultivar> cultivaresBD = _config.CarregaCultivares();

            foreach(var c in cultivaresBD)
            {
                _regulaApiService.InsertCultivar(c);
            }
        }

        private void SaveDoenca()
        {
            List<Doenca> doencasDB = _config.CarregaDoencas();

            foreach(var d in doencasDB)
            {
                _regulaApiService.InsertDoenca(d);
            }
        }

        private void SaveEpocaSemeadura()
        {
            List<EpocaSemeadura> epocaSemeaduraDB = _config.CarregaEpocasSemeadura();

            foreach(var ep in epocaSemeaduraDB)
            {
                _regulaApiService.InsertEpocaSemeadura(ep);
            }
        }

        private void SaveTolerancia()
        {
            List<Tolerancia> toleranciasDB = _config.CarregaTolerancias();

            foreach(var t in toleranciasDB)
            {
                _regulaApiService.InsertTolerancia(t);
            }
        }

        private void SaveCiclo()
        {
            List<Ciclo> ciclosDB = _config.CarregaCiclos();

            foreach(var c in ciclosDB)
            {
                _regulaApiService.InsertCiclo(c);
            }
        }

        private void SaveCultivarEpocaSemeadura()
        {
            List<CultivarEpocaSemeadura> cultivarEpocaSemeaduraDB = _config.CarregaCultivarEpocaSemeadura();

            foreach(var c in cultivarEpocaSemeaduraDB)
            {
                _regulaApiService.InsertCultivarEpocaSemeadura(c);
            }
        }

        private void SaveCultivarDoenca()
        {
            List<CultivarDoenca> cultivarDoencaDB = _config.CarregaCultivarDoencas();

            foreach(var cd in cultivarDoencaDB)
            {
                _regulaApiService.InsertCultivarDoenca(cd);
            }
        }
    }

    
}
