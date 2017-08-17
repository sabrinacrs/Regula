using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseFazenda : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseFazenda()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            _conexao.CreateTable<Fazenda>();
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

        public Fazenda ObterPorID(int id)
        {
            return _conexao.Table<Fazenda>().Where(f => f.DataDesativacao == DateTime.MinValue).FirstOrDefault(f => f.Id == id);
        }

        public List<Fazenda> ObterPorClienteId(int id)
        {
            return _conexao.Table<Fazenda>().Where(f => f.DataDesativacao == DateTime.MinValue).Where(f => f.ClienteId == id).ToList<Fazenda>();
        }

        public List<Fazenda> ObterPorNome(string nome)
        {
            return _conexao.Table<Fazenda>().Where(f => f.DataDesativacao == DateTime.MinValue).Where(f => (f.Nome).Contains(nome)).ToList<Fazenda>();
        }

        public List<Fazenda> ObterPorNomeAndClienteId(string nome, int clienteId)
        {
            return _conexao.Table<Fazenda>().Where(f => f.DataDesativacao == DateTime.MinValue).Where(f => (f.ClienteId) == clienteId).Where(f => (f.Nome).Contains(nome)).ToList<Fazenda>();
        }

        public List<Fazenda> Listar()
        {
            return _conexao.Table<Fazenda>().Where(f => f.DataDesativacao == DateTime.MinValue).OrderBy(f => f.Nome).ToList<Fazenda>();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
