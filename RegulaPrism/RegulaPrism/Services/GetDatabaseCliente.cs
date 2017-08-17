using Prism.Services;
using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseCliente : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseCliente()
        {
            var config = Xamarin.Forms.DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            _conexao.CreateTable<Cliente>();
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

        public Cliente ObterPorID(int id)
        {
            return _conexao.Table<Cliente>().FirstOrDefault(c => c.Id == id);
        }

        public Cliente ObterPorEmail(String email)
        {
            return _conexao.Table<Cliente>().FirstOrDefault(c => c.Email == email);
        }

        public Cliente ObterPorLogin(String login)
        {
            return _conexao.Table<Cliente>().FirstOrDefault(c => c.Login == login);
        }

        public List<Cliente> Listar()
        {
            return _conexao.Table<Cliente>().OrderBy(c => c.Nome).ToList<Cliente>();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
