using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Services
{
    public class GetDatabaseSemeadura : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseSemeadura()
        {
            var config = Xamarin.Forms.DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            _conexao.CreateTable<Semeadura>();
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

        public Semeadura ObterPorID(int id)
        {
            return _conexao.Table<Semeadura>().FirstOrDefault(c => c.Id == id);
        }

        public List<Semeadura> ObterPorTalhaoId(int id)
        {
            return _conexao.Table<Semeadura>().Where(c => c.TalhaoId == id).ToList();
        }

        // obter por data

        public List<Semeadura> Listar()
        {
            return _conexao.Table<Semeadura>().ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
