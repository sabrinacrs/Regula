using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseTolerancia : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseTolerancia()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            // drop old table
            //_conexao.DropTable<Tolerancia>();

            // create new table
            _conexao.CreateTable<Tolerancia>();
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

        public Tolerancia ObterPorID(int id)
        {
            return _conexao.Table<Tolerancia>().Where(t => t.DataDesativacao == DateTime.MinValue).FirstOrDefault(t => t.Id == id);
        }

        public Tolerancia ObterPorDescricao(string descricao)
        {
            return _conexao.Table<Tolerancia>().Where(t => t.DataDesativacao == DateTime.MinValue).Where(t => t.Descricao == descricao).FirstOrDefault();
        }

        public List<Tolerancia> Listar()
        {
            return _conexao.Table<Tolerancia>().Where(t => t.DataDesativacao == DateTime.MinValue).OrderBy(t => t.Descricao).ToList();
        }

        public List<Tolerancia> ListarTodas()
        {
            return _conexao.Table<Tolerancia>().OrderBy(t => t.Descricao).ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
