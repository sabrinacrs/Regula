using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Services
{
    public class GetDatabases
    {
        private IRegulaApiService _regulaApiService;
        public DataService _dataService;

        public void getDatabases(IRegulaApiService regulaApiService)
        {
            _dataService = new DataService();
            _regulaApiService = regulaApiService;

            // verificar se houve mudança na base de dados do servidor
            // se houver, verifica quais tabelas mudaram
            // clona as tabelas alteradas

            // copia dados
            Clone();
        }

        private void Clone()
        {
            // save cultivar
            SaveCultivar();

            // save doencas
            SaveDoenca();

            // save tolerancias
            SaveTolerancia();

            // save epocas semeadura
            SaveEpocaSemeadura();

            // save ciclos
            SaveCiclo();

            // save cultivar doencas
            SaveCultivarDoenca();

            // save cultivar epocas semeadura
            SaveCultivarEpocaSemeadura();
        }

        private async void SaveCultivar()
        {
            List<Cultivar> cultivaresBD = await _dataService.GetCultivaresAsync();

            foreach (Cultivar c in cultivaresBD)
            {
                _regulaApiService.InsertCultivar(c);
            }
        }

        private async void SaveDoenca()
        {
            List<Doenca> doencasDB = await _dataService.GetDoencasAsync();

            foreach (var d in doencasDB)
            {
                _regulaApiService.InsertDoenca(d);
            }
        }

        private async void SaveEpocaSemeadura()
        {
            List<EpocaSemeadura> epocaSemeaduraDB = await _dataService.GetEpocasSemeaduraAsync();

            foreach (var ep in epocaSemeaduraDB)
            {
                _regulaApiService.InsertEpocaSemeadura(ep);
            }
        }

        private async void SaveTolerancia()
        {
            List<Tolerancia> toleranciasDB = await _dataService.GetToleranciasAsync();

            foreach (var t in toleranciasDB)
            {
                _regulaApiService.InsertTolerancia(t);
            }
        }

        private async void SaveCiclo()
        {
            List<Ciclo> ciclosDB = await _dataService.GetCiclosAsync();

            foreach (var c in ciclosDB)
            {
                _regulaApiService.InsertCiclo(c);
            }
        }

        private async void SaveCultivarEpocaSemeadura()
        {
            List<CultivarEpocaSemeadura> cultivarEpocaSemeaduraDB = await _dataService.GetCultivarEpocaSemeaduraAsync();

            foreach (var c in cultivarEpocaSemeaduraDB)
            {
                _regulaApiService.InsertCultivarEpocaSemeadura(c);
            }
        }

        private async void SaveCultivarDoenca()
        {
            List<CultivarDoenca> cultivarDoencaDB = await _dataService.GetCultivarDoencasAsync();

            foreach (var cd in cultivarDoencaDB)
            {
                _regulaApiService.InsertCultivarDoenca(cd);
            }
        }
    }
}
