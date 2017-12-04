using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseDoenca: IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseDoenca()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            // drop old table
            //_conexao.DropTable<Doenca>();

            // create new table
            _conexao.CreateTable<Doenca>();
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

        public Doenca ObterPorID(int id)
        {
            return _conexao.Table<Doenca>().Where(d => d.DataDesativacao == DateTime.MinValue).FirstOrDefault(d => d.Id == id);
        }

        public Doenca ObterPorDescricao(string descricao)
        {
            return _conexao.Table<Doenca>().Where(d => d.DataDesativacao == DateTime.MinValue).Where(d => d.Descricao == descricao).FirstOrDefault();
        }

        public List<Doenca> Listar()
        {
            return _conexao.Table<Doenca>().Where(d => !d.Status.Equals("I")).OrderBy(d => d.Descricao).ToList();
        }

        public List<Doenca> ListarTodas()
        {
            //
            return _conexao.Table<Doenca>().OrderBy(x => x.Id).ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
