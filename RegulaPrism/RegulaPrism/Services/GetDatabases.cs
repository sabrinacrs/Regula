using RegulaPrism.Models;
using RegulaPrism.Models.Json;
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

        public GetDatabases()
        {
            _dataService = new DataService();
        }

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

        // Cliente
        #region Cliente Operations Sync
        public ClienteJson SendClienteToServer(Cliente cliente)
        {
            return _dataService.AddClienteAsync(cliente);
        }

        public ClienteJson GetClienteServer(Cliente cliente)
        {
            return _dataService.GetClienteAsync(cliente);
        }

        public void UpdateClienteOnServer(Cliente cliente)
        {
            _dataService.UpdateClienteAsync(cliente);
        }

        public void DeleteClienteOnServer(Cliente cliente)
        {
            _dataService.DeleteClienteAsync(cliente);
        }
        #endregion

        // Semeadura
        public SemeaduraJson SendSemeaduraToServer(Semeadura semeadura)
        {
            return _dataService.AddSemeaduraAsync(semeadura);
        }

        // Historico Atualizacao
        public HistoricoAtualizacao GetLastRelease()
        {
            return _dataService.GetLastReleaseAsync();
        }


        // Cultivares
        #region Cultivar Operations Sync
        private async void SaveCultivar()
        {
            List<Cultivar> cultivaresServer = await _dataService.GetCultivaresAsync();
            List<Cultivar> cultivaresSQLite = _regulaApiService.GetAllCultivar();

            // verifica nova cultivar ou atualização
            insertUpdateSincCultivar(cultivaresServer, cultivaresSQLite);

            // verifica quantidade de itens em cada base, para verificar se houve exclusão no servidor
            if(cultivaresSQLite.Count >= cultivaresServer.Count)
            {
                deleteUpdateSincCultivar(cultivaresServer, cultivaresSQLite);
            }
        }

        private void insertUpdateSincCultivar(List<Cultivar> cultivaresServer, List<Cultivar> cultivaresSQLite)
        {
            foreach (Cultivar c in cultivaresServer)
            {
                if (cultivaresSQLite.Any(x => x.Id == c.Id))
                    _regulaApiService.UpdateCultivar(c);
                else
                    _regulaApiService.InsertCultivar(c);
            }
        }

        private void deleteUpdateSincCultivar(List<Cultivar> cultivaresServer, List<Cultivar> cultivaresSQLite)
        {
            for (int i = 0; i < cultivaresSQLite.Count; i++)
            {
                Cultivar c = cultivaresSQLite.ElementAt(i);
                if (!cultivaresServer.Any(x => x.Id == c.Id))
                {
                    // exclui cultivar da base SQLite
                    _regulaApiService.DeleteCultivar(c);
                    // remove cultivar da lista de cultivares
                    cultivaresSQLite.Remove(c);
                }

            }
        }
        #endregion

        // Doenças 
        #region Doenca Operations Sync
        private async void SaveDoenca()
        {
            List<Doenca> doencasServer = await _dataService.GetDoencasAsync();
            List<Doenca> doencasSQLite = _regulaApiService.GetAllDoenca();

            // verifica nova cultivar ou atualização
            insertUpdateSincDoenca(doencasServer, doencasSQLite);

            // verifica quantidade de itens em cada base, para verificar se houve exclusão no servidor
            if (doencasSQLite.Count >= doencasServer.Count)
            {
                deleteUpdateSincDoenca(doencasServer, doencasSQLite);
            }


            //List<Doenca> doencasDB = await _dataService.GetDoencasAsync();

            //foreach (var d in doencasDB)
            //{
            //    _regulaApiService.InsertDoenca(d);
            //}
        }

        private void insertUpdateSincDoenca(List<Doenca> doencasServer, List<Doenca> doencasSQLite)
        {
            foreach (Doenca d in doencasServer)
            {
                if (doencasSQLite.Any(x => x.Id == d.Id))
                    _regulaApiService.UpdateDoenca(d);
                else
                    _regulaApiService.InsertDoenca(d);
            }
        }

        private void deleteUpdateSincDoenca(List<Doenca> doencasServer, List<Doenca> doencasSQLite)
        {
            for (int i = 0; i < doencasSQLite.Count; i++)
            {
                Doenca d = doencasSQLite.ElementAt(i);
                if (!doencasServer.Any(x => x.Id == d.Id))
                {
                    // exclui cultivar da base SQLite
                    _regulaApiService.DeleteDoenca(d);
                    // remove cultivar da lista de cultivares
                    doencasSQLite.Remove(d);
                }

            }
        }
        #endregion

        // Epoca Semeadura
        #region Epoca Semeadura Operations Sync
        private async void SaveEpocaSemeadura()
        {
            List<EpocaSemeadura> epocasServer = await _dataService.GetEpocasSemeaduraAsync();
            List<EpocaSemeadura> epocasSQLite = _regulaApiService.GetAllEpocaSemeadura();

            // verifica nova cultivar ou atualização
            insertUpdateSincEpocaSemeadura(epocasServer, epocasSQLite);

            // verifica quantidade de itens em cada base, para verificar se houve exclusão no servidor
            if (epocasSQLite.Count >= epocasServer.Count)
            {
                deleteUpdateSincEpocaSemeadura(epocasServer, epocasSQLite);
            }
        }

        private void insertUpdateSincEpocaSemeadura(List<EpocaSemeadura> epocasServer, List<EpocaSemeadura> epocasSQLite)
        {
            foreach (EpocaSemeadura ep in epocasServer)
            {
                if (epocasSQLite.Any(x => x.Id == ep.Id))
                    _regulaApiService.UpdateEpocaSemeadura(ep);
                else
                    _regulaApiService.InsertEpocaSemeadura(ep);
            }
        }

        private void deleteUpdateSincEpocaSemeadura(List<EpocaSemeadura> epocasServer, List<EpocaSemeadura> epocasSQLite)
        {
            for (int i = 0; i < epocasSQLite.Count; i++)
            {
                EpocaSemeadura ep = epocasSQLite.ElementAt(i);
                if (!epocasServer.Any(x => x.Id == ep.Id))
                {
                    // exclui cultivar da base SQLite
                    _regulaApiService.DeleteEpocaSemeadura(ep);
                    // remove cultivar da lista de cultivares
                    epocasSQLite.Remove(ep);
                }

            }
        }
        #endregion

        // Tolerancias 
        #region Tolerancia Operations Sync
        private async void SaveTolerancia()
        {
            List<Tolerancia> toleranciasServer = await _dataService.GetToleranciasAsync();
            List<Tolerancia> toleranciasSQLite = _regulaApiService.GetAllTolerancia();

            // verifica nova cultivar ou atualização
            insertUpdateSincTolerancia(toleranciasServer, toleranciasSQLite);

            // verifica quantidade de itens em cada base, para verificar se houve exclusão no servidor
            if (toleranciasSQLite.Count >= toleranciasServer.Count)
            {
                deleteUpdateSincTolerancia(toleranciasServer, toleranciasSQLite);
            }
        }

        private void insertUpdateSincTolerancia(List<Tolerancia> toleranciasServer, List<Tolerancia> toleranciasSQLite)
        {
            foreach (Tolerancia t in toleranciasServer)
            {
                if (toleranciasSQLite.Any(x => x.Id == t.Id))
                    _regulaApiService.UpdateTolerancia(t);
                else
                    _regulaApiService.InsertTolerancia(t);
            }
        }

        private void deleteUpdateSincTolerancia(List<Tolerancia> toleranciasServer, List<Tolerancia> toleranciasSQLite)
        {
            for (int i = 0; i < toleranciasSQLite.Count; i++)
            {
                Tolerancia t = toleranciasSQLite.ElementAt(i);
                if (!toleranciasServer.Any(x => x.Id == t.Id))
                {
                    // exclui cultivar da base SQLite
                    _regulaApiService.DeleteTolerancia(t);
                    // remove cultivar da lista de cultivares
                    toleranciasSQLite.Remove(t);
                }

            }
        }
        #endregion

        // Ciclos
        #region Ciclo Operations Sync
        private async void SaveCiclo()
        {
            List<Ciclo> ciclosServer = await _dataService.GetCiclosAsync();
            List<Ciclo> ciclosSQLite = _regulaApiService.GetAllCiclo();

            // verifica nova cultivar ou atualização
            insertUpdateSincCiclo(ciclosServer, ciclosSQLite);

            // verifica quantidade de itens em cada base, para verificar se houve exclusão no servidor
            if (ciclosSQLite.Count >= ciclosServer.Count)
            {
                deleteUpdateSincCiclo(ciclosServer, ciclosSQLite);
            }
        }

        private void insertUpdateSincCiclo(List<Ciclo> ciclosServer, List<Ciclo> ciclosSQLite)
        {
            foreach (Ciclo c in ciclosServer)
            {
                if (ciclosSQLite.Any(x => x.Id == c.Id))
                    _regulaApiService.UpdateCiclo(c);
                else
                    _regulaApiService.InsertCiclo(c);
            }
        }

        private void deleteUpdateSincCiclo(List<Ciclo> ciclosServer, List<Ciclo> ciclosSQLite)
        {
            for (int i = 0; i < ciclosSQLite.Count; i++)
            {
                Ciclo c = ciclosSQLite.ElementAt(i);
                if (!ciclosServer.Any(x => x.Id == c.Id))
                {
                    // exclui cultivar da base SQLite
                    _regulaApiService.DeleteCiclo(c);
                    // remove cultivar da lista de cultivares
                    ciclosSQLite.Remove(c);
                }

            }
        }
        #endregion

        // Cultivares Epoca Semeadura
        #region Cultivar Epoca Semeadura Operations Sync
        private async void SaveCultivarEpocaSemeadura()
        {
            List<CultivarEpocaSemeadura> cultivarEpocaSemeaduraDB = await _dataService.GetCultivarEpocaSemeaduraAsync();

            foreach (var c in cultivarEpocaSemeaduraDB)
            {
                _regulaApiService.InsertCultivarEpocaSemeadura(c);
            }
        }

        private void insertUpdateSincCultivarEpocaSemeadura(List<CultivarEpocaSemeadura> cultivarEpocaSemeaduraServer, List<CultivarEpocaSemeadura> cultivarEpocaSemeaduraSQLite)
        {
            foreach (CultivarEpocaSemeadura c in cultivarEpocaSemeaduraServer)
            {
                if (cultivarEpocaSemeaduraSQLite.Any(x => x.CultivarId == c.CultivarId && x.EpocaSemeaduraId == c.EpocaSemeaduraId))
                    _regulaApiService.UpdateCultivarEpocaSemeadura(c);
                else
                    _regulaApiService.InsertCultivarEpocaSemeadura(c);
            }
        }

        private void deleteUpdateSincEpocaSemeadura(List<CultivarEpocaSemeadura> cultivarEpocaSemeaduraServer, List<CultivarEpocaSemeadura> cultivarEpocaSemeaduraSQLite)
        {
            for (int i = 0; i < cultivarEpocaSemeaduraSQLite.Count; i++)
            {
                CultivarEpocaSemeadura c = cultivarEpocaSemeaduraSQLite.ElementAt(i);
                if (!cultivarEpocaSemeaduraServer.Any(x => x.CultivarId == c.CultivarId && x.EpocaSemeaduraId == c.EpocaSemeaduraId))
                {
                    // exclui cultivar da base SQLite
                    _regulaApiService.DeleteCultivarEpocaSemeadura(c);
                    // remove cultivar da lista de cultivares
                    cultivarEpocaSemeaduraSQLite.Remove(c);
                }

            }
        }
        #endregion

        //Cultivares Doencas
        #region Cultivar Doenca Operations Sync
        private async void SaveCultivarDoenca()
        {
            List<CultivarDoenca> cultivarDoencaDB = await _dataService.GetCultivarDoencasAsync();

            foreach (var cd in cultivarDoencaDB)
            {
                _regulaApiService.InsertCultivarDoenca(cd);
            }
        }
        private void insertUpdateSincCultivarDoenca(List<CultivarDoenca> cultivarDoencaServer, List<CultivarDoenca> cultivarDoencaSQLite)
        {
            foreach (CultivarDoenca c in cultivarDoencaServer)
            {
                if (cultivarDoencaSQLite.Any(x => x.CultivarId == c.CultivarId && x.DoencaId == c.DoencaId))
                    _regulaApiService.UpdateCultivarDoenca(c);
                else
                    _regulaApiService.InsertCultivarDoenca(c);
            }
        }

        private void deleteUpdateSincCultivarDoenca(List<CultivarDoenca> cultivarDoencaServer, List<CultivarDoenca> cultivarDoencaSQLite)
        {
            for (int i = 0; i < cultivarDoencaSQLite.Count; i++)
            {
                CultivarDoenca c = cultivarDoencaSQLite.ElementAt(i);
                if (!cultivarDoencaServer.Any(x => x.CultivarId == c.CultivarId && x.DoencaId == c.DoencaId))
                {
                    // exclui cultivar da base SQLite
                    _regulaApiService.DeleteCultivarDoenca(c);
                    // remove cultivar da lista de cultivares
                    cultivarDoencaSQLite.Remove(c);
                }

            }
        }
        #endregion

        //  Historico de Atualização

        // Fazendas
        #region Fazenda Operations Sync
        public FazendaJson SendFazendaToServer(Fazenda fazenda)
        {
            return _dataService.AddFazendaAsync(fazenda);
        }
        #endregion

        // Talhão
        #region Talhão Operations Sync
        public TalhaoJson SendTalhaoToServer(Talhao talhao)
        {
            return _dataService.AddTalhaoAsync(talhao);
        }
        #endregion
    }
}
