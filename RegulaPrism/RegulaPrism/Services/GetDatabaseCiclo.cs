using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseCiclo : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseCiclo()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            _conexao.CreateTable<Ciclo>();
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

        public Ciclo ObterPorID(int id)
        {
            return _conexao.Table<Ciclo>().Where(d => d.DataDesativacao == DateTime.MinValue).FirstOrDefault(d => d.Id == id);
        }

        public Ciclo ObterPorDescricao(string descricao)
        {
            return _conexao.Table<Ciclo>().Where(d => d.DataDesativacao == DateTime.MinValue).Where(d => d.Descricao == descricao).FirstOrDefault();
        }

        public List<Ciclo> Listar()
        {
            return _conexao.Table<Ciclo>().Where(d => d.DataDesativacao == DateTime.MinValue).OrderBy(d => d.Descricao).ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
