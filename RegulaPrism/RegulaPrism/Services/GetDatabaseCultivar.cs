using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseCultivar : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseCultivar()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            _conexao.CreateTable<Cultivar>();
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

        public Cultivar ObterPorID(int id)
        {
            return _conexao.Table<Cultivar>().Where(c => c.DataDesativacao == DateTime.MinValue).FirstOrDefault(c => c.Id == id);
        }

        public List<Cultivar> Listar()
        {
            return _conexao.Table<Cultivar>().Where(c => c.DataDesativacao == DateTime.MinValue).OrderBy(c => c.Nome).ToList<Cultivar>();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
