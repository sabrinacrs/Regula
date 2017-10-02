using RegulaPrism.Models;
using RegulaPrism.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(RegulaPrism.Services.RegulaApiService))]
namespace RegulaPrism.Services
{
    public class RegulaApiService : IRegulaApiService
    {
        // classe para aplicar os métodos do banco
        GetDatabaseCliente _dataBaseCliente = new GetDatabaseCliente();
        GetDatabaseFazenda _dataBaseFazenda = new GetDatabaseFazenda();
        GetDatabaseTalhao _dataBaseTalhao = new GetDatabaseTalhao();
        GetDatabaseSemeadura _dataBaseSemeadura = new GetDatabaseSemeadura();
        GetDataBaseCalculosSemeadura _dataBaseCalculoSemeadura = new GetDataBaseCalculosSemeadura();

        // clone database server
        GetDatabaseCultivar _dataBaseCultivar = new GetDatabaseCultivar();
        GetDatabaseDoenca _dataBaseDoenca = new GetDatabaseDoenca();
        GetDatabaseCiclo _dataBaseCiclo = new GetDatabaseCiclo();
        GetDatabaseTolerancia _dataBaseTolerancia = new GetDatabaseTolerancia();
        GetDatabaseEpocaSemeadura _dataBaseEpocaSemeadura = new GetDatabaseEpocaSemeadura();
        GetDatabaseCultivarEpocaSemeadura _dataBaseCultivarEpocaSemeadura = new GetDatabaseCultivarEpocaSemeadura();
        GetDatabaseCultivarDoenca _dataBaseCultivarDoenca = new GetDatabaseCultivarDoenca();

        // ------------------
        // CLIENTE OPERATIONS
        public bool InsertCliente(Cliente cliente)
        {
            if (_dataBaseCliente.Insert(cliente) <= 0)
                return false;
            return true;
        }

        // Delete Cliente
        public bool DeleteCliente(Cliente cliente)
        {
            if (_dataBaseCliente.Delete(cliente) < 0)
                return false;
            return true;
        }

        // Delete Logical
        public bool DeleteLogicalCliente(Cliente cliente)
        {
            cliente.DataDesativacao = DateTime.Now;
            if (_dataBaseCliente.Update(cliente) < 0)
                return false;
            return true;
        }

        // GETS CLIENTE
        // Get CLiente by Email
        public Cliente GetClienteByEmail(string email)
        {
            return _dataBaseCliente.ObterPorEmail(email);
        }

        // get cliente by ID
        public Cliente GetClienteById(int id)
        {
            return _dataBaseCliente.ObterPorID(id);
        }

        // get cliente by LOGIN
        public Cliente GetClienteByLogin(string login)
        {
            Cliente c = _dataBaseCliente.ObterPorLogin(login);
            return c;
        }

        public bool UpdateCliente(Cliente cliente)
        {
            if (_dataBaseCliente.Update(cliente) <= 0)
                return false;
            return true;
        }

        // List Clientes
        public List<Cliente> GetClientes()
        {
            return _dataBaseCliente.Listar();
        }


         //------------------
         //FAZENDA OPERATIONS
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

        public bool DeleteFazenda(Fazenda fazenda)
        {
            if (_dataBaseFazenda.Delete(fazenda) < 0)
                return false;
            return true;
        }

        public bool InsertFazenda(Fazenda fazenda)
        {
            if (_dataBaseFazenda.Insert(fazenda) <= 0)
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

        // -----------------
        // TALHAO OPERATIONS
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

        // -------------------
        //// CULTIVAR OPERATIONS
        public bool InsertCultivar(Cultivar cultivar)
        {
            if (_dataBaseCultivar.Insert(cultivar) <= 0)
                return false;
            return true;
        }

        // Obter Cultivar por Id
        public Cultivar GetCultivarById(int id)
        {
            Cultivar c = _dataBaseCultivar.ObterPorID(id);
            return c;
        }

        public List<Cultivar> GetCultivar()
        {
            return _dataBaseCultivar.Listar();
        }

        public List<Cultivar> GetCultivarByCiclo(int cicloId)
        {
            return _dataBaseCultivar.ObterPorCiclo(cicloId);
        }

        // -------------------
        // EPÓCA SEMEADURA OPERATIONS
        public bool InsertEpocaSemeadura(EpocaSemeadura epocaSemeadura)
        {
            if (_dataBaseEpocaSemeadura.Insert(epocaSemeadura) <= 0)
                return false;
            return true;
        }

        public EpocaSemeadura GetEpocaSemeaduraById(int id)
        {
            EpocaSemeadura ep = _dataBaseEpocaSemeadura.ObterPorID(id);
            return ep;
        }

        public List<EpocaSemeadura> GetEpocaSemeadura()
        {
            return _dataBaseEpocaSemeadura.Listar().OrderBy(x => x.Id).ToList();
        }

        // -------------------
        // CULTIVAR EPÓCA SEMEADURA OPERATIONS
        public bool InsertCultivarEpocaSemeadura(CultivarEpocaSemeadura cultivarEpoca)
        {
            if (_dataBaseCultivarEpocaSemeadura.Insert(cultivarEpoca) <= 0)
                return false;
            return true;
        }

        public List<CultivarEpocaSemeadura> GetCultivarEpocaSemeadura()
        {
            return _dataBaseCultivarEpocaSemeadura.Listar();
        }

        public List<CultivarEpocaSemeadura> GetCultivarEpocaSemeaduraEpocaId(int epocaId)
        {
            return _dataBaseCultivarEpocaSemeadura.ObterPorEpocaSemeaduraId(epocaId);
        }

        public List<CultivarEpocaSemeadura> GetCultivarEpocaSemeaduraCultivarId(int cultId)
        {
            return _dataBaseCultivarEpocaSemeadura.ObterPorCultivarId(cultId);
        }

        public List<CultivarEpocaSemeadura> GetCultivarEpocaSemeaduraCultivarEpoca(int cultId, int epocaId)
        {
            return _dataBaseCultivarEpocaSemeadura.ObterPorCultivarEpocaSemeadura(cultId, epocaId);
        }

        // -------------------
        // DOENÇA OPERATIONS
        public bool InsertDoenca(Doenca doenca)
        {
            if (_dataBaseDoenca.Insert(doenca) <= 0)
                return false;
            return true;
        }

        public Doenca GetDoencaById(int id)
        {
            Doenca d = _dataBaseDoenca.ObterPorID(id);
            return d;
        }

        public Doenca GetDoencaByDescricao(string descricao)
        {
            return _dataBaseDoenca.ObterPorDescricao(descricao);
        }

        public List<Doenca> GetDoenca()
        {
            return _dataBaseDoenca.Listar();
        }

        // -------------------
        // CULTIVAR DOENÇA OPERATIONS
        public bool InsertCultivarDoenca(CultivarDoenca cultivarDoenca)
        {
            if (_dataBaseCultivarDoenca.Insert(cultivarDoenca) <= 0)
                return false;
            return true;
        }

        public List<CultivarDoenca> GetAll()
        {
            return _dataBaseCultivarDoenca.Listar();
        }

        public List<CultivarDoenca> GetCultivarDoencaCultivarId(int cultId)
        {
            return _dataBaseCultivarDoenca.ObterPorCultivarId(cultId);
        }

        public List<CultivarDoenca> GetCultivarDoencaDoencaId(int doeId)
        {
            return _dataBaseCultivarDoenca.ObterPorDoencaId(doeId);
        }

        public List<CultivarDoenca> GetCultivarDoencaToleranciaId(int tolId)
        {
            return _dataBaseCultivarDoenca.ObterPorToleranciaId(tolId);
        }

        public List<CultivarDoenca> GetCultivarDoenca(int cultId, int doeId)
        {
            return _dataBaseCultivarDoenca.ObterPorCultivarDoenca(cultId, doeId);
        }

        public CultivarDoenca GetCultivarDoencaTolerancia(int cultId, int doeId, int tolId)
        {
            return _dataBaseCultivarDoenca.ObterPorCultivarDoencaTolerancia(cultId, doeId, tolId);
        }

        // -------------------
        // CICLO OPERATIONS
        public bool InsertCiclo(Ciclo ciclo)
        {
            if (_dataBaseCiclo.Insert(ciclo) <= 0)
                return false;
            return true;
        }

        public Ciclo GetCicloById(int id)
        {
            Ciclo c = _dataBaseCiclo.ObterPorID(id);
            return c;
        }

        public Ciclo GetCicloByDescricao(string descricao)
        {
            return _dataBaseCiclo.ObterPorDescricao(descricao);
        }

        public List<Ciclo> GetCiclos()
        {
            return _dataBaseCiclo.Listar();
        }

        // -------------------
        // TOLERÂNCIA OPERATIONS
        public bool InsertTolerancia(Tolerancia tolerancia)
        {
            if (_dataBaseTolerancia.Insert(tolerancia) <= 0)
                return false;
            return true;
        }
        public Tolerancia GetToleranciaById(int id)
        {
            Tolerancia t = _dataBaseTolerancia.ObterPorID(id);
            return t;
        }

        public Tolerancia GetToleranciaByDescricao(string descricao)
        {
            return _dataBaseTolerancia.ObterPorDescricao(descricao);
        }

        public List<Tolerancia> GetTolerancias()
        {
            return _dataBaseTolerancia.Listar();
        }

        // -------------------
        // SEMEADURA OPERATIONS
        public bool InsertSemeadura(Semeadura semeadura)
        {
            if (_dataBaseSemeadura.Insert(semeadura) <= 0)
                return false;
            return true;
        }

        public bool DeleteSemeadura(Semeadura semeadura)
        {
            if (_dataBaseSemeadura.Delete(semeadura) < 0)
                return false;
            return true;
        }

        public bool UpdateSemeadura(Semeadura semeadura)
        {
            if (_dataBaseSemeadura.Update(semeadura) <= 0)
                return false;
            return true;
        }

        public Semeadura GetSemeaduraById(int id)
        {
            return _dataBaseSemeadura.ObterPorID(id);
        }

        public List<Semeadura> GetSemeadurasByTalhaoId(int talhaoId)
        {
            return _dataBaseSemeadura.ObterPorTalhaoId(talhaoId);
        }

        public List<Semeadura> GetSemeaduras()
        {
            return _dataBaseSemeadura.Listar();
        }

        // -------------------
        // CCALCULOS SEMEADURA OPERATIONS
        public bool InsertCalculosSemeadura(CalculosSemeadura calculosSemeadura)
        {
            if (_dataBaseCalculoSemeadura.Insert(calculosSemeadura) <= 0)
                return false;
            return true;
        }

        public bool DeleteCalculosSemeadura(CalculosSemeadura calculosSemeadura)
        {
            if (_dataBaseCalculoSemeadura.Delete(calculosSemeadura) < 0)
                return false;
            return true;
        }

        public bool UpdateCalculosSemeadura(CalculosSemeadura calculosSemeadura)
        {
            if (_dataBaseCalculoSemeadura.Update(calculosSemeadura) <= 0)
                return false;
            return true;
        }

        public CalculosSemeadura GetCalculosSemeaduraById(int id)
        {
            return _dataBaseCalculoSemeadura.ObterPorID(id);
        }

        public CalculosSemeadura GetCalculosSemeaduraBySemeaduraId(int semeaduraId)
        {
            return _dataBaseCalculoSemeadura.ObterPorSemeaduraId(semeaduraId);
        }

        public List<CalculosSemeadura> GetCalculosSemeaduras()
        {
            return _dataBaseCalculoSemeadura.Listar();
        }
    }
}
