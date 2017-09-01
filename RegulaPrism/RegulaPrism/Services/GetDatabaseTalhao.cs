using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class GetDatabaseTalhao: IDisposable
    {
        private SQLite.Net.SQLiteConnection _conexao;

        public GetDatabaseTalhao()
        {
            var config = DependencyService.Get<IConfig>();
            _conexao = new SQLite.Net.SQLiteConnection(config.Plataforma, System.IO.Path.Combine(config.DiretorioDB, "reguladb.db3"));

            _conexao.CreateTable<Talhao>();
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

        public Talhao ObterPorID(int id)
        {
            return _conexao.Table<Talhao>().Where(t => t.DataDesativacao == DateTime.MinValue).FirstOrDefault(t => t.Id == id);
        }

        public List<Talhao> ObterPorFazendaId(int id)
        {
            return _conexao.Table<Talhao>().Where(t => t.FazendaId == id).Where(t => t.DataDesativacao == DateTime.MinValue).ToList<Talhao>();
        }

        public List<Talhao> ObterPorDescricao(string descricao)
        {
            return _conexao.Table<Talhao>().Where(t => t.DataDesativacao == DateTime.MinValue).Where(t => (t.Descricao).Contains(descricao)).ToList();
        }

        public List<Talhao> ObterPorDescricaoAndFazendaId(string descricao, int fazendaId)
        {
            return _conexao.Table<Talhao>().Where(t => (t.FazendaId) == fazendaId).Where(t => t.DataDesativacao == DateTime.MinValue).Where(t => (t.Descricao).Contains(descricao)).ToList();
        }

        public List<Talhao> Listar()
        {
            return _conexao.Table<Talhao>().Where(t => t.DataDesativacao == DateTime.MinValue).OrderBy(t => t.Descricao).ToList();
        }

        public void Dispose()
        {
            _conexao.Dispose();
        }
    }
}
