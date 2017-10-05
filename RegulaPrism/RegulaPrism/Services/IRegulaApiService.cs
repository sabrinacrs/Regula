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

        //Cultivares
        bool InsertCultivar(Cultivar cultivar);
        Cultivar GetCultivarById(int id);
        List<Cultivar> GetCultivar();
        List<Cultivar> GetCultivarByCiclo(int cicloId);

        //Doenças
        bool InsertDoenca(Doenca doenca);
        Doenca GetDoencaById(int id);
        Doenca GetDoencaByDescricao(string descricao);
        List<Doenca> GetDoenca();

        //Épocas Semeadura
        bool InsertEpocaSemeadura(EpocaSemeadura epocaSemeadura);
        EpocaSemeadura GetEpocaSemeaduraById(int id);
        List<EpocaSemeadura> GetEpocaSemeadura();

        //Tolerâncias
        bool InsertTolerancia(Tolerancia tolerancia);
        Tolerancia GetToleranciaById(int id);
        Tolerancia GetToleranciaByDescricao(string descricao);
        List<Tolerancia> GetTolerancias();

        //Ciclos
        bool InsertCiclo(Ciclo ciclo);
        Ciclo GetCicloById(int id);
        Ciclo GetCicloByDescricao(string descricao);
        List<Ciclo> GetCiclos();

        //Cultivares Doenças
        bool InsertCultivarDoenca(CultivarDoenca cultivarDoenca);
        List<CultivarDoenca> GetAllCultivarDoencas();
        List<CultivarDoenca> GetCultivarDoencaCultivarId(int cultId);
        List<CultivarDoenca> GetCultivarDoencaDoencaId(int doeId);
        List<CultivarDoenca> GetCultivarDoencaToleranciaId(int tolId);
        List<CultivarDoenca> GetCultivarDoenca(int cultId, int doeId);
        CultivarDoenca GetCultivarDoencaTolerancia(int cultId, int doeId, int tolId);

        //Cultivares Épocas Semeadura
        bool InsertCultivarEpocaSemeadura(CultivarEpocaSemeadura cultivarEpoca);
        List<CultivarEpocaSemeadura> GetCultivarEpocaSemeadura();
        List<CultivarEpocaSemeadura> GetCultivarEpocaSemeaduraEpocaId(int epocaId);
        List<CultivarEpocaSemeadura> GetCultivarEpocaSemeaduraCultivarId(int cultId);
        List<CultivarEpocaSemeadura> GetCultivarEpocaSemeaduraCultivarEpoca(int cultId, int epocaId);

        //// Semeadura
        bool InsertSemeadura(Semeadura semeadura);
        bool DeleteSemeadura(Semeadura semeadura);
        bool UpdateSemeadura(Semeadura semeadura);
        Semeadura GetSemeaduraById(int id);
        List<Semeadura> GetSemeadurasByTalhaoId(int talhaoId);
        List<Semeadura> GetSemeaduras();

        // Calculos Semeadura
        bool InsertCalculosSemeadura(CalculosSemeadura calculosSemeadura);
        bool DeleteCalculosSemeadura(CalculosSemeadura calculosSemeadura);
        bool UpdateCalculosSemeadura(CalculosSemeadura calculosSemeadura);
        CalculosSemeadura GetCalculosSemeaduraById(int id);
        CalculosSemeadura GetCalculosSemeaduraBySemeaduraId(int semeaduraId);
        List<CalculosSemeadura> GetCalculosSemeaduras();
    }
}
