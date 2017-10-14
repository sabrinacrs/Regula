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
                        
            return im;
        }

        public InformacaoManual InformacoesLogin()
        {
            InformacaoManual im = new InformacaoManual();

            im.Titulo = "Tela de Login";
            im.Texto = "A tela de login contém campos de identificação de usuário para que seja possível usar as funções do aplicativo.\nTodos os campos são obrigatórios. \n\n"
                        + "1. Login ou E-mail: Use o teclado virtual para digitar o login ou e-mail do cliente cadastrado \n\n"
                        + "2. Senha: Digite a senha do cliente \n\n"
                        + "3. Entrar: Use o botão entrar para checar as informações fornecidas e acessar o menu principal do aplicativo \n\n";

            return im;
        }

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

            return im;
        }


    }
}
