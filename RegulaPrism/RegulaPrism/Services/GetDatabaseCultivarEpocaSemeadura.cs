using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseCultivarEpocaSemeadura : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseCultivarEpocaSemeadura()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            _conexao.CreateTable<CultivarEpocaSemeadura>();
        }

        public int Insert<T>(T elemento)
        {
            return _conexao.Insert(elemento);
        }

        public int Update<T>(T elemento)
        {
            int i = _conexao.Update(elemento);
            return i;
        }

        public int Delete<T>(T elemento)
        {
            return _conexao.Delete(elemento);
        }

        public List<CultivarEpocaSemeadura> ObterPorCultivarId(int cultId)
        {
            return _conexao.Table<CultivarEpocaSemeadura>().Where(c => c.CultivarId == cultId).ToList();
        }

        public List<CultivarEpocaSemeadura> ObterPorEpocaSemeaduraId(int epocaId)
        {
            return _conexao.Table<CultivarEpocaSemeadura>().Where(ep => ep.EpocaSemeaduraId == epocaId).ToList();
        }

        public List<CultivarEpocaSemeadura> ObterPorCultivarEpocaSemeadura(int cultId, int epocaId)
        {
            return _conexao.Table<CultivarEpocaSemeadura>().Where(ep => ep.CultivarId == cultId).Where(ep => ep.EpocaSemeaduraId == epocaId).ToList();
        }

        public List<CultivarEpocaSemeadura> Listar()
        {
            return _conexao.Table<CultivarEpocaSemeadura>().ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
