using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseCultivarDoenca : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseCultivarDoenca()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            _conexao.CreateTable<CultivarDoenca>();
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

        public List<CultivarDoenca> ObterPorCultivarId(int cultId)
        {
            return _conexao.Table<CultivarDoenca>().Where(c => c.CultivarId == cultId).ToList();
        }

        public List<CultivarDoenca> ObterPorDoencaId(int doencaId)
        {
            return _conexao.Table<CultivarDoenca>().Where(c => c.DoencaId == doencaId).ToList();
        }

        public List<CultivarDoenca> ObterPorToleranciaId(int toleranciaId)
        {
            return _conexao.Table<CultivarDoenca>().Where(c => c.ToleranciaId == toleranciaId).ToList();
        }

        public List<CultivarDoenca> ObterPorCultivarDoenca(int cultId, int doencaId)
        {
            return _conexao.Table<CultivarDoenca>().Where(c => c.CultivarId == cultId).Where(d => d.DoencaId == doencaId).ToList();
        }

        public CultivarDoenca ObterPorCultivarDoencaTolerancia(int cultId, int doencaId, int tolId)
        {
            return _conexao.Table<CultivarDoenca>().Where(c => c.CultivarId == cultId).Where(d => d.DoencaId == doencaId).Where(t => t.ToleranciaId == tolId).First();
        }

        public List<CultivarDoenca> Listar()
        {
            return _conexao.Table<CultivarDoenca>().ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
