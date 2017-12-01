using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Services
{
    public class GetDataBaseHistoricoAtualizacao : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDataBaseHistoricoAtualizacao()
        {
            var config = Xamarin.Forms.DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            // create new table
            _conexao.CreateTable<HistoricoAtualizacao>();
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

        public HistoricoAtualizacao ObterPorID(int id)
        {
            return _conexao.Table<HistoricoAtualizacao>().FirstOrDefault(x => x.Id == id);
        }

        public HistoricoAtualizacao GetLastHistoricoAtualizacao()
        {
            List<HistoricoAtualizacao> ha = _conexao.Table<HistoricoAtualizacao>().OrderByDescending(x => x.DataAtualizacao).ToList();

            if (ha.Count <= 0)
                return null;

            return ha.First();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
