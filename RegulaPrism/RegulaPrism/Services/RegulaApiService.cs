using RegulaPrism.Models;
using RegulaPrism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(RegulaPrism.RegulaApiService))]
namespace RegulaPrism
{
    public class RegulaApiService : IRegulaApiService
    {
        // classe para aplicar os métodos do banco
        GetDatabaseCliente _dataBaseCliente = new GetDatabaseCliente();
        GetDatabaseFazenda _dataBaseFazenda = new GetDatabaseFazenda();
        GetDatabaseTalhao _dataBaseTalhao = new GetDatabaseTalhao();

        public bool DeleteCliente(Cliente cliente)
        {
            if (_dataBaseCliente.Delete(cliente) < 0)
                return false;
            return true;
        }

        public bool DeleteLogicalCliente(Cliente cliente)
        {
            cliente.DataDesativacao = DateTime.Now;
            if (_dataBaseCliente.Update(cliente) < 0)
                return false;
            return true;
        }

        public bool DeleteFazenda(Fazenda fazenda)
        {
            if (_dataBaseFazenda.Delete(fazenda) < 0)
                return false;
            return true;
        }

        public Cliente GetClienteByEmail(string email)
        {
            return _dataBaseCliente.ObterPorEmail(email);
        }

        public Cliente GetClienteById(int id)
        {
            return _dataBaseCliente.ObterPorID(id);
        }

        public Cliente GetClienteByLogin(string login)
        {
            Cliente c = _dataBaseCliente.ObterPorLogin(login);
            return c;
        }

        public List<Cliente> GetClientes()
        {
            return _dataBaseCliente.Listar();
        }

        public Fazenda GetFazendaById(int id)
        {
            return _dataBaseFazenda.ObterPorID(id);
        }

        public List<Fazenda> GetFazendas()
        {
            return _dataBaseFazenda.Listar();
        }

        public List<Fazenda> GetFazendasByCliente(int clienteId)
        {
            return _dataBaseFazenda.ObterPorClienteId(clienteId);
        }

        public List<Fazenda> GetFazendasByNome(string nome)
        {
            return _dataBaseFazenda.ObterPorNome(nome);
        }

        public List<Fazenda> GetFazendasByNomeAndCliente(string nome, int clienteId)
        {
            return _dataBaseFazenda.ObterPorNomeAndClienteId(nome, clienteId);
        }

        public bool InsertCliente(Cliente cliente)
        {
            if (_dataBaseCliente.Insert(cliente) <= 0)
                return false;
            return true;
        }

        public bool InsertFazenda(Fazenda fazenda)
        {
            if (_dataBaseFazenda.Insert(fazenda) <= 0)
                return false;
            return true;
        }

        public bool UpdateCliente(Cliente cliente)
        {
            if (_dataBaseCliente.Update(cliente) <= 0)
                return false;
            return true;
        }

        public bool UpdateFazenda(Fazenda fazenda)
        {
            if (_dataBaseFazenda.Update(fazenda) <= 0)
                return false;
            return true;
        }

        public bool DeleteLogicalFazenda(Fazenda fazenda)
        {
            fazenda.DataDesativacao = DateTime.Now;
            if (_dataBaseFazenda.Update(fazenda) < 0)
                return false;
            return true;
        }

        public Talhao GetTalhaoById(int id)
        {
            return _dataBaseTalhao.ObterPorID(id);
        }

        public List<Talhao> GetTalhoes()
        {
            return _dataBaseTalhao.Listar();
        }

        public List<Talhao> GetTalhoesByFazenda(int fazendaId)
        {
            return _dataBaseTalhao.ObterPorFazendaId(fazendaId);
        }

        public List<Talhao> GetTalhoesByDescricao(string descricao)
        {
            return _dataBaseTalhao.ObterPorDescricao(descricao);
        }

        public List<Talhao> GetTalhoesByDescricaoAndFazenda(string descricao, int fazendaId)
        {
            return _dataBaseTalhao.ObterPorDescricaoAndFazendaId(descricao, fazendaId);
        }

        public bool InsertTalhao(Talhao talhao)
        {
            if (_dataBaseTalhao.Insert(talhao) <= 0)
                return false;
            return true;
        }

        public bool UpdateTalhao(Talhao talhao)
        {
            if (_dataBaseTalhao.Update(talhao) <= 0)
                return false;
            return true;
        }

        public bool DeleteTalhao(Talhao talhao)
        {
            if (_dataBaseTalhao.Delete(talhao) < 0)
                return false;
            return true;
        }

        public bool DeleteLogicalTalhao(Talhao talhao)
        {
            talhao.DataDesativacao = DateTime.Now;
            if (_dataBaseTalhao.Update(talhao) < 0)
                return false;
            return true;
        }
    }
}
