
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseEpocaSemeadura : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseEpocaSemeadura()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            // drop old table
            //_conexao.DropTable<EpocaSemeadura>();

            // create new table
            _conexao.CreateTable<EpocaSemeadura>();
        }

        public int Insert<T>(T elemento)
        {
            return _conexao.Insert(elemento);
        }

        public int Update<T>(T elemento)
        {
            return _conexao.Update(elemento);
        }

        public int Delete<T>(T elemento)
        {
            return _conexao.Delete(elemento);
        }

        public EpocaSemeadura ObterPorID(int id)
        {
            return _conexao.Table<EpocaSemeadura>().Where(ep => ep.DataDesativacao == DateTime.MinValue).FirstOrDefault(ep => ep.Id == id);
        }

        public List<EpocaSemeadura> Listar()
        {
            return _conexao.Table<EpocaSemeadura>().Where(ep => ep.DataDesativacao == DateTime.MinValue).OrderBy(ep => ep.Descricao).ToList<EpocaSemeadura>();
        }

        public List<EpocaSemeadura> ListarTodas()
        {
            return _conexao.Table<EpocaSemeadura>().OrderBy(ep => ep.Descricao).ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
