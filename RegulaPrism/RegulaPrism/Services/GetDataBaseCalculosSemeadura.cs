using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Services
{
    public class GetDataBaseCalculosSemeadura : IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDataBaseCalculosSemeadura()
        {
            var config = Xamarin.Forms.DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            _conexao.CreateTable<CalculosSemeadura>();
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

        public CalculosSemeadura ObterPorID(int id)
        {
            return _conexao.Table<CalculosSemeadura>().FirstOrDefault(c => c.Id == id);
        }

        public CalculosSemeadura ObterPorSemeaduraId(int semeaduraId)
        {
            return _conexao.Table<CalculosSemeadura>().First(c => c.SemeaduraId == semeaduraId);
        }

        public List<CalculosSemeadura> Listar()
        {
            return _conexao.Table<CalculosSemeadura>().ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
