using RegulaPrism.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(RegulaPrism.Services.InformacoesManuais))]
namespace RegulaPrism.Services
{
    public class InformacoesManuais: IInformacoesManuais
    {
        // Login
        public InformacaoManual InformacoesLogin()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Tela de Login";
            im.Texto = "A tela de login contém campos de identificação de usuário para que seja possível usar as funções do aplicativo.\nTodos os campos são obrigatórios. \n\n"
                        + "1. Login ou E-mail: Use o teclado virtual para digitar o login ou e-mail do cliente cadastrado \n\n"
                        + "2. Senha: Digite a senha do cliente \n\n"
                        + "3. Entrar: Use o botão entrar para checar as informações fornecidas e acessar o menu principal do aplicativo \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#login";
            return im;
        }

        // Cliente
        public InformacaoManual InformacoesClienteCreate()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Criando um novo cliente";
            im.Texto = "A tela contém campos para cadastrar um novo cliente.\nTodos os campos desta tela são obrigatórios. \n\n"
                        + "1. Nome Completo: Use o teclado virtual para digitar seu nome completo \n\n"
                        + "2. E-mail: Digite seu e-mail para usar caso precise logar ou recuperar informações do aplicativo \n\n"
                        + "3. Login: O login é seu nome de usuário; digite um nome de usuário para logar-se no aplicativo \n\n"
                        + "4. Senha: A senha deve ter no mínimo três dígitos \n\n"
                        + "5. Confirmar Senha: Use o campo de confirmar senha para verificar se a senha definida foi digitada corretamete \n\n"
                        + "6. Salvar: O botão Salvar registra todas as informações digitadas e cria uma conta para o cliente \n\n";

            im.TitleFunctions = "Cliente cadastrado";
            im.TextFunctions = "Após cadastrar-se como cliente, poderá ter acesso à todas as funcionalidades da aplicação, registrar fazendas,"
                + " talhões, realizar cálculos e registros de semeadura.";

            im.TitleErrors = "Mensagens de erro";
            im.TextErrors = "Possíveis mensagens de erros podem ser exibidas caso: \n"
                          + "1. Campos do formulário não estejam preenchidos corretamente. Neste caso, um alerta será exibido na "
                          + "tela a respeito de qual informação encontra-se incorreta";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#clientecreate";
            return im;
        }

        public InformacaoManual InformacoesClienteUpdate()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Atualizando os dados do cliente";
            im.Texto = "A tela é composta por campos de dados pessoais e de localização do cliente. \n O nome completo é um dado obrigatório e deve ser preenchido. \n Obs: As informações do formulário são mantidas em sigilo, sem qualquer tipo de divulgação \n\n"
                        + "1. Dados Pessoais: \n"
                        + "1.1 Nome Completo: Use o teclado virtual para digitar seu nome completo \n\n"
                        + "1.2 Telefone: Se desejar, informe seu número de telefone \n\n"
                        + "1.3 CPF: Se desejar, informe o número de seu CPF \n\n"
                        + "2. Dados de Localização: \n"
                        + "Você pode utilizar os campos para fornecer uma localização com bairro, cidade, CEP. \n\n";

            im.TitleFunctions = "Cliente atualizado";
            im.TextFunctions = "Ao realizar a atualização, todos os dados do cliente serão substituídos pelas"
                + "informações que foram alteradas ou acrescentadas";

            im.TitleErrors = "Mensagens de erro";
            im.TextErrors = "Possíveis mensagens de erros podem ser exibidas caso: \n"
                          + "1. Campos do formulário não estejam preenchidos corretamente. Neste caso, um alerta será exibido na "
                          + "tela a respeito de qual informação encontra-se incorreta";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#clienteupdate";

            return im;
        }

        // Fazenda
        public InformacaoManual InformacoesFazendaCreate()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Registrando uma nova fazenda";
            im.Texto = "A tela é composta por campos de informações básicas, informações de contato e de localização da fazenda. \n Os campos Nome, Hectares e Telefone devem ser preenchidos. \n\n"
                        + "1. Informações básicas: \n"
                        + "1.1 Nome: Use o teclado virtual para digitar o nome da fazenda \n\n"
                        + "1.2 Hectares: Informe o tamanho da fazenda em hectares \n\n"
                        + "1.3 Observações: Se desejar, adicione observações quaisquer à fazenda \n\n"
                        + "2. Informações de Contato: \n"
                        + "2.1 E-mail: E-mail da fazenda \n\n"
                        + "2.2 Endereço Web: Url do site (www.fazenda.com.br) ou qualquer rede (Facebook, Twitter, Instagram) alternativa que permita entrar em contato com a fazenda \n\n"
                        + "2.2 Telefone: Número de telefone para contato \n\n"
                        + "3. Informações de Localização: \n"
                        + "Você pode utilizar os campos para fornecer uma localização com bairro, cidade, UF. \n\n"
                        + "4. Salvar: Botão para salvar todos os dados \n\n";

            im.TitleFunctions = "Fazenda cadastrada";
            im.TextFunctions = "Ter uma fazenda cadastrada no sistema é importante para cadastrar talhões, salvar cálculos das quantidades de sementes e registrar semeaduras.";

            im.TitleErrors = "Mensagens de erro";
            im.TextErrors = "Possíveis mensagens de erros podem ser exibidas caso: \n"
                          + "1. Campos do formulário não estejam preenchidos corretamente. Neste caso, um alerta será exibido na "
                          + "tela a respeito de qual informação encontra-se incorreta";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#fazendacreate";

            return im;
        }

        public InformacaoManual InformacoesFazendaUpdate()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Atualizando os dados da fazenda";
            im.Texto = "A tela é composta por campos de informações básicas, informações de contato e de localização da fazenda. \n Os campos Nome, Hectares e Telefone devem ser preenchidos. \n\n"
                        + "1. Informações básicas: \n"
                        + "1.1 Nome: Use o teclado virtual para digitar o nome da fazenda \n\n"
                        + "1.2 Hectares: Informe o tamanho da fazenda em hectares \n\n"
                        + "1.3 Observações: Se desejar, adicione observações quaisquer à fazenda \n\n"
                        + "2. Informações de Contato: \n"
                        + "2.1 E-mail: E-mail da fazenda \n\n"
                        + "2.2 Endereço Web: Url do site (www.fazenda.com.br) ou qualquer rede (Facebook, Twitter, Instagram) alternativa que permita entrar em contato com a fazenda \n\n"
                        + "2.2 Telefone: Número de telefone para contato \n\n"
                        + "3. Informações de Localização: \n"
                        + "Você pode utilizar os campos para fornecer uma localização com bairro, cidade, UF. \n\n"
                        + "4. Excluir Fazenda: Todos os dados da fazenda são deletados, impossibilitando a consulta e registro de semeaduras e talhões para a fazenda \n\n"
                        + "5. Salvar: Botão para salvar todos os dados \n\n";

            im.TitleFunctions = "Fazenda atualizada";
            im.TextFunctions = "Ao realizar a atualização, todos os dados da fazenda serão substituídos pelas"
                + "informações que foram alteradas ou acrescentadas";

            im.TitleErrors = "Mensagens de erro";
            im.TextErrors = "Possíveis mensagens de erros podem ser exibidas caso: \n"
                          + "1. Campos do formulário não estejam preenchidos corretamente. Neste caso, um alerta será exibido na "
                          + "tela a respeito de qual informação encontra-se incorreta";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#fazendaupdate";

            return im;
        }

        public InformacaoManual InformacoesFazendaList()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Listando e filtrando fazendas cadastradas";
            im.Texto = "É possível utilizar o campo de texto para procurar fazendas pelo nome. \n\n"
                        + "1. Campo para digitar: Use o teclado virtual para digitar algum trecho do nome da fazenda \n\n"
                        + "2. Botão Buscar: Lista os resultados obtidos com base no que foi digitado no campo de texto \n\n"
                        + "3. Lista de Fazendas: Lista todas as fazendas registradas ou os resultados da busca. Toque na linha de determinada fazenda para obter mais detalhes sobre ela \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#fazendalist";
            //im.TitleFunctions = "Fazenda atualizada";
            //im.TextFunctions = "Ao realizar a atualização, todos os dados da fazenda serão substituídos pelas"
            //    + "informações que foram alteradas ou acrescentadas";

            //im.TitleErrors = "Mensagens de erro";
            //im.TextErrors = "Possíveis mensagens de erros podem ser exibidas caso: "
            //              + "1. Campos do formulário não estejam preenchidos corretamente. Neste caso, um alerta será exibido na "
            //              + "tela a respeito de qual informação encontra-se incorreta";

            return im;
        }

        // Talhão
        public InformacaoManual InformacoesTalhaoCreate()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Registrando um novo talhão";
            im.Texto = "Para registrar um talhão, é necessário que ao menos uma fazenda esteja cadastrada.\nTodos os campos são obrigatórios. \n\n"
                        + "1. Fazenda: Toque no campo Selecionar Fazenda para listar as fazendas. Na lista, selecione a fazenda na qual o talhão pertencerá \n\n"
                        + "2. Descrição: Atribua uma descrição que permita identificar o talhão \n\n"
                        + "3. Tamanho: Informe o tamanho do talhão \n\n"
                        + "4. Salvar: Botão para salvar todos os dados \n\n";

            im.TitleFunctions = "Talhão cadastrado";
            im.TextFunctions = "Após registrar um talhão em uma fazenda, é possível usar as informações para os cálculos e registro de semeadura";

            im.TitleErrors = "Mensagens de erro";
            im.TextErrors = "Possíveis mensagens de erros podem ser exibidas caso: \n"
                          + "1. Campos do formulário não estejam preenchidos corretamente. Neste caso, um alerta será exibido na "
                          + "tela a respeito de qual informação encontra-se incorreta";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#talhaocreate";

            return im;
        }

        public InformacaoManual InformacoesTalhaoList()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Listando e filtrando talhões cadastrados";
            im.Texto = "Listar os talhões de determinada fazenda. \n\n"
                        + "1. Selecionar Fazenda: Todo talhão pertence à uma fazenda; use o campo Selecionar Fazenda para listar as fazendas registradas.\n Na lista, selecione uma fazenda para listar os talhões \n\n"
                        + "2. Campo para digitar: Use o teclado virtual para digitar algum trecho do nome do talhão \n\n"
                        + "3. Botão Buscar: Lista os resultados obtidos com base no que foi digitado no campo de texto \n\n"
                        + "4. Lista de Talhões: Lista todos os talhões registrados ou os resultados da busca. Toque na linha de determinado talhão para obter mais detalhes sobre ele \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#talhaolist";

            return im;
        }

        public InformacaoManual InformacoesTalhaoUpdate()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Atualizando os dados do talhão";
            im.Texto = "Para registrar um talhão, é necessário que ao menos uma fazenda esteja cadastrada.\nTodos os campos são obrigatórios. \n\n"
                        + "1. Fazenda: Toque no campo Selecionar Fazenda para listar as fazendas. Na lista, selecione a fazenda na qual o talhão pertencerá \n\n"
                        + "2. Descrição: Atribua uma descrição que permita identificar o talhão \n\n"
                        + "3. Tamanho: Informe o tamanho do talhão \n\n"
                        + "4. Excluir Talhão: Todos os dados do talhão são deletados \n\n"
                        + "5. Salvar: Botão para salvar todos os dados \n\n";

            im.TitleFunctions = "Talhão atualizada";
            im.TextFunctions = "Ao realizar a atualização, todos os dados do talhão serão substituídos pelas"
                + "informações que foram alteradas ou acrescentadas";

            im.TitleErrors = "Mensagens de erro";
            im.TextErrors = "Possíveis mensagens de erros podem ser exibidas caso: \n"
                          + "1. Campos do formulário não estejam preenchidos corretamente. Neste caso, um alerta será exibido na "
                          + "tela a respeito de qual informação encontra-se incorreta"
                          + "2. Não foi possível excluir o talhão. Neste caso, é necessário observar se o talhão possui semeaduras em aberto.";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#talhaolist";

            return im;
        }

        // Home Page
        public InformacaoManual InformacoesHomePage()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Itens da tela principal";
            im.Texto = "Os ícones representam cada conjunto de operações que podem ser realizadas sobre Cultivares, Semeaduras, Fazendas, Talhões e conta do cliente. \n\n"
                        + "1. Cultivares: Toque no ícone de cultivares se desejar listar ou filtrar cultivares \n\n"
                        + "2. Semeaduras: O ícone de semeaduras leva a uma tela para realizar cálculos de semeadura \n\n"
                        + "3. Fazendas: Operações para registrar, listar ou alterar dados de uma fazenda \n\n"
                        + "4. Talhões: Operações para registrar, listar ou alterar dados de um talhão \n\n"
                        + "5. Usuário: Apresenta todos os dados do usuário \n\n";

            im.TitleFunctions = "Funcionalidades da Aplicação";
            im.TextFunctions = "- Cultivares: Lista todas as cultivares do catálago, permite filtrar cultivares com base no ciclo, no rendimento da fibra e cultivares tolerantes às doenças presentes no solo do produtor. \n\n"
                + "- Semeaduras: Permite fornecer as informações necessárias para realizar os cálculos da quantidade de sementes a semear. \n\n"
                + "- Fazendas: Consulta e gerencia fazendas registradas; realiza buscas, alterações e exclusões de fazendas. \n\n"
                + "- Talhões: Consulta e gerencia talhões registrados; realiza buscas, alterações e exclusões de talhões pertencentes à fazenda selecionada. \n\n"
                + "- Cliente: Permite gerenciar a conta do cliente que faz uso da aplicação; alterar ou acrescentar informações. \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#homepage";

            return im;
        }

        // Cultivares
        public InformacaoManual InformacoesCultivarList()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Lista de cultivares";
            im.Texto = "Lista todas as variedades de cultivares.\n"
                     + "Para ver detalhes da variedade basta tocar na linha em que ela se encontra. Feito isso, uma nova tela com todas as características será exibida \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#cultivarlist";

            return im;
        }

        public InformacaoManual InformacoesCultivarSelected()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Variedade de cultivar selecionada";
            im.Texto = "Exibe todas as características da variedade selecionada.\n"
                     + "Lista todas as doenças e a respectiva tolerância. \n"
                     + "As tolerâncias são representadas por siglas.\n Cada sigla pode ter seu significado consultado na legenda que fica no rodapé da tela. \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#cultivarselected";

            return im;
        }

        public InformacaoManual InformacoesCultivarCiclo()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Filtrar variedades de cultivar por meio do ciclo";
            im.Texto = "Todos os ciclos são listados na tela.\n"
                     + "Selecione um ciclo para listar as variedades que pertencem à ele. \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#cultivaresciclolist";

            return im;
        }

        public InformacaoManual InformacoesCultivarRendimento()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Lista de cultivares";
            im.Texto = "Lista todas as variedades de cultivares ordenadas pelo maior rendimento de fibra.\n"
                     + "Para ver detalhes da variedade basta tocar na linha em que ela se encontra. Feito isso, uma nova tela com todas as características será exibida \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#cultivaresrendimentolist";
        
            return im;
        }

        public InformacaoManual InformacoesCultivarDoencas()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Consultar variedades de cultivares e suas respectivas tolerâncias às doenças";
            im.Texto = "Todas as doenças são listadas na tela.\n"
                     + "Em cada linha, na frente do nome de cada doença, é possível ver um botão para Ativar e Desativar.\n"
                     + "1. Quando o botão muda de cor e o círculo contido dentro dele vai para o lado direito significa que a doença foi selecionada \n\n"
                     + "2. Quando o círculo contido no botão está no lado esquerdo significa que a doença não foi selecionada \n\n"
                     + "3. O botão Próximo levará à tela com a lista das variedades, as doenças selecionadas e tolerâncias \n\n";

            im.TitleFunctions = "Funcionalidade do Filtro de Doenças";
            im.TextFunctions = "As doenças selecionadas são utilizadas para realizar o cruzamento entre doenças, cultivares e tolerâncias. "
                + "É importante selecionar doenças presentes no solo para retornar os resultados das tolerâncias entre as doenças selecionadas e a cultivar. "
                + " Se nenhuma doenças for selecionada, a consulta não retornará nenhum resultado.";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#cultivaresdoencaslist";

            return im;
        }

        // Semeadura
        public InformacaoManual InformacoesSemeadura()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Realizar cálculos para semeadura";
            im.Texto = "A tela apresenta campos de informações cruciais para calcular o peso de sementes por metro, hectare, alqueire e o número de plantas por hectare. \nTodos os campos são obrigatórios. \n\n"
                     + "1. Cultivar: Toque no campo Selecionar Cultivar para listar todas as variedades de cultivar. Na lista, selecione a variedade que deseja semear. \n\n"
                     + "2. Época de Semeadura: Toque no campo Selecionar Época para listar todas as épocas de semeadura. Esta informação é importante para dizer se a variedade de cultivar escolhida é recomendada ou não para a época selecionada. \n\n"
                     + "3. Espaçamento: Utilize o teclado numérico para informar o espaçamento. Ele deve ser um valor decimal, como por exemplo: 0.8 \n\n"
                     + "4. Germinação: Utilize o teclado numérico para informar a taxa de germinação apresentada no saco de sementes \n\n"
                     + "5. Botão Calcular: Para visualizar o resultado de todos os cálculos, toque no botão calcular \n\n";

            im.TitleFunctions = "Funcionalidade dos Cálculos";
            im.TextFunctions = "As informações fornecidas são submetidas às fórmulas para obter as quantidades de sementes indicadas a cada unidade de medida.\n\n"
                + "- Sementes por metro: (Plantas Hectare / Metros Lineares) / (Germinação / 100) \n\n"
                + "- Sementes por hectare: (Sementes por metro * Metros Lineares) * (Peso Sementes / 100) / 1000 \n\n"
                + "- Sementes por alqueire: (Sementes por hectare * 2.42) \n\n";

            im.TitleErrors = "Mensagens de erro";
            im.TextErrors = "Possíveis mensagens de erros podem ser exibidas caso: \n"
                          + "1. Campos do formulário não estejam preenchidos corretamente. Neste caso, um alerta será exibido na "
                          + "tela a respeito de qual informação encontra-se incorreta";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#semeadura";


            return im;
        }

        public InformacaoManual InformacoesCalcularSemeadura()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Resultados dos cálculos";
            im.Texto = "São listadas todas as informações de semeadura e resultados dos cálculos. \n\n"
                     + "1. Época de Semeadura: Toque no campo Selecionar Época para realizar novos cálculos \n\n"
                     + "2. Nome Cultivar: Exibe o nome da variedade de cultivar selecionada para a semeadura \n\n"
                     + "3. Plantas/ha: Exibe a quantidade de plantas por hectare na época selecionada \n\n"
                     + "4. Sementes (m): Quantidade de sementes para semear em cada metro \n\n"
                     + "5. Sementes (kg/ha): Peso, em quilogramas, que deve ser semeado em cada hectare \n\n"
                     + "6. Sementes (kg/alq): Peso, em quilogramas, que deve ser semeado em cada alqueire \n\n"
                     + "7. Recomendação: Um rótulo em vermelho pode surgir quando a varidade de cultivar não é recomendada para a época de semeadura selecionada \n\n"
                     + "8. Botão Salvar: O Botão Salvar permite registrar todos os cálculos realizados \n\n";

            im.TitleFunctions = "Funcionalidade dos Cálculos";
            im.TextFunctions = "As informações fornecidas são submetidas às fórmulas para obter as quantidades de sementes indicadas a cada unidade de medida. \n"
                + "Sementes por metro: (Plantas Hectare / Metros Lineares) / (Germinação / 100) \n\n"
                + "Sementes por hectare: (Sementes por metro * Metros Lineares) * (Peso Sementes / 100) / 1000 \n\n"
                + "Sementes por alqueire: (Sementes por hectare * 2.42) \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#semeaduraresultados";

            return im;
        }

        public InformacaoManual InformacoesFazendaSemeaduraList()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Lista de semeaduras/cálculos realizados";
            im.Texto = "Lista todos os cálculos realizados que foram salvos. \n\n"
                     + "Para ver detalhes e resultados do cálculo, basta tocar na linha de determinado talhão ou data e hora. Feito isso, uma nova tela com todos os resultados será exibida \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#consultarsemeadura";

            return im;
        }

        public InformacaoManual InformacoesFazendaSemeaduraSelected()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Exibindo todos os resultados";
            im.Texto = "São listadas todas as informações de semeadura e resultados dos cálculos. \n\n"
                     + "1. Talhão:  \n\n"
                     + "2. Data:  \n\n"
                     + "3. Cultivar: Exibe o nome da variedade de cultivar selecionada para a semeadura \n\n"
                     + "4. Época Semeadura:  \n\n"
                     + "5. Sementes (m): Quantidade de sementes para semear em cada metro \n\n"
                     + "6. Sementes (kg/ha): Peso mínimo e máximo, em quilogramas, que deve ser semeado em cada hectare \n\n"
                     + "7. Sementes (kg/alq): Peso mínimo e máximo, em quilogramas, que deve ser semeado em cada alqueire \n\n";
            im.LinkHelpOnline = "http://www.cottonappadm.xyz/help#consultarsemeadura";

            return im;
        }
    }
}
