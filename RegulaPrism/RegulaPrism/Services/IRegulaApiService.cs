using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism
{
    public interface IRegulaApiService
    {
        // Metodos de Acesso a Dados do Banco
        // Clientes
        Cliente GetClienteById(int id);
        List<Cliente> GetClientes();
        Cliente GetClienteByEmail(string email);
        Cliente GetClienteByLogin(string login);
        bool InsertCliente(Cliente cliente);
        bool UpdateCliente(Cliente cliente);
        bool DeleteCliente(Cliente cliente);
        bool DeleteLogicalCliente(Cliente cliente);

        // Fazendas
        Fazenda GetFazendaById(int id);
        List<Fazenda> GetFazendas();
        List<Fazenda> GetFazendasByCliente(int clienteId);
        List<Fazenda> GetFazendasByNome(string nome);
        List<Fazenda> GetFazendasByNomeAndCliente(string nome, int clienteId);
        bool InsertFazenda(Fazenda fazenda);
        bool UpdateFazenda(Fazenda fazenda);
        bool DeleteFazenda(Fazenda fazenda);
        bool DeleteLogicalFazenda(Fazenda fazenda);

        // Talhões
        Talhao GetTalhaoById(int id);
        List<Talhao> GetTalhoes();
        List<Talhao> GetTalhoesByFazenda(int fazendaId);
        List<Talhao> GetTalhoesByDescricao(string descricao);
        List<Talhao> GetTalhoesByDescricaoAndFazenda(string descricao, int fazendaId);
        bool InsertTalhao(Talhao talhao);
        bool UpdateTalhao(Talhao talhao);
        bool DeleteTalhao(Talhao talhao);
        bool DeleteLogicalTalhao(Talhao talhao);
    }
}
