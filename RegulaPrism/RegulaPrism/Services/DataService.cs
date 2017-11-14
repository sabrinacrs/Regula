﻿using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RegulaPrism.Models;
using RegulaPrism.Models.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RegulaPrism.Services
{
    public class DataService
    {
        HttpClient client = new HttpClient();
        public async Task<List<Cultivar>> GetCultivaresAsync()
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/api/cultivares";
                var response = await client.GetStringAsync(url);
                var cultivaresJson = JsonConvert.DeserializeObject<List<CultivarJson>>(response);

                return loadCultivares(cultivaresJson);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Doenca>> GetDoencasAsync()
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/api/doencas";
                var response = await client.GetStringAsync(url);
                var doencas = JsonConvert.DeserializeObject<List<Doenca>>(response);

                return doencas;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<EpocaSemeadura>> GetEpocasSemeaduraAsync()
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/api/epocassemeaduras";
                var response = await client.GetStringAsync(url);
                var es = JsonConvert.DeserializeObject<List<EpocaSemeadura>>(response);

                return es;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Ciclo>> GetCiclosAsync()
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/api/ciclos";
                var response = await client.GetStringAsync(url);
                var ciclos = JsonConvert.DeserializeObject<List<Ciclo>>(response);

                return ciclos;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<Tolerancia>> GetToleranciasAsync()
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/api/tolerancias";
                var response = await client.GetStringAsync(url);
                var tolerancias = JsonConvert.DeserializeObject<List<Tolerancia>>(response);

                return tolerancias;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CultivarDoenca>> GetCultivarDoencasAsync()
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/api/cultivaresdoencas";
                var response = await client.GetStringAsync(url);
                var cd = JsonConvert.DeserializeObject<List<CultivarDoencaJson>>(response);

                return loadCultivaresDoencas(cd);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<CultivarEpocaSemeadura>> GetCultivarEpocaSemeaduraAsync()
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/api/cultivaresepocasemeadura";
                var response = await client.GetStringAsync(url);
                var cep = JsonConvert.DeserializeObject<List<CultivarEpocaSemeaduraJson>>(response);

                return loadCultivaresEpocaSemeadura(cep);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<Cultivar> loadCultivares(List<CultivarJson> cultivaresJson)
        {
            List<Cultivar> cultivares = new List<Cultivar>();

            foreach (var x in cultivaresJson)
            {
                Cultivar cm = new Cultivar();

                cm.AlturaPlanta = x.altura_planta;
                cm.ComprimentoFibraMaximo = x.comprimento_fibra_maximo;
                cm.ComprimentoFibraMinimo = x.comprimento_fibra_minimo;
                //cm.DataDesativacao = DateTime.Parse(x.data_desativacao);
                cm.Fertilidade = x.fertilidade;
                cm.Id = x.id;
                cm.MicronaireMaximo = x.micronaire_maximo;
                cm.MicronaireMinimo = x.micronaire_minimo;
                cm.Nome = x.nome;
                cm.PesoMedioCapulhoMaximo = x.peso_capulho_maximo;
                cm.PesoMedioCapulhoMinimo = x.peso_capulho_minimo;
                cm.PesoSementesMaximo = x.peso_sementes_maximo;
                cm.PesoSementesMinimo = x.peso_sementes_minimo;
                cm.Regulador = x.regulador;
                cm.RendimentoFibraMaximo = x.rendimento_fibra_maximo;
                cm.RendimentoFibraMinimo = x.rendimento_fibra_minimo;
                cm.ResistenciaMaximo = x.resistencia_maximo;
                cm.ResistenciaMinimo = x.resistencia_minimo;
                cm.CicloId = x.cic_id;

                cultivares.Add(cm);
            }

            return cultivares;
        }

        private List<CultivarDoenca> loadCultivaresDoencas(List<CultivarDoencaJson> cultivaresDoencasJson)
        {
            List<CultivarDoenca> cultivaresDoencas = new List<CultivarDoenca>();

            foreach (var x in cultivaresDoencasJson)
            {
                CultivarDoenca cm = new CultivarDoenca();

                cm.CultivarId = x.cult_id;
                cm.DoencaId = x.doe_id;
                cm.ToleranciaId = x.tol_id;

                cultivaresDoencas.Add(cm);
            }

            return cultivaresDoencas;
        }

        private List<CultivarEpocaSemeadura> loadCultivaresEpocaSemeadura(List<CultivarEpocaSemeaduraJson> cultivaresEpocaSemeaduraJson)
        {
            List<CultivarEpocaSemeadura> cultivaresEpocaSemeadura = new List<CultivarEpocaSemeadura>();

            foreach (var x in cultivaresEpocaSemeaduraJson)
            {
                CultivarEpocaSemeadura cm = new CultivarEpocaSemeadura();

                cm.CultivarId = x.cult_id;
                cm.EpocaSemeaduraId = x.ep_id;
                cm.PlantasHa = x.plantas_ha;

                cultivaresEpocaSemeadura.Add(cm);
            }

            return cultivaresEpocaSemeadura;
        }


        // ---------------- Clientes ------------------------------
        public ClienteJson AddClienteAsync(Cliente cliente)
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/testeapi/cliente";

                ClienteJson cj = loadClienteJson(cliente);

                var uri = new Uri(string.Format(url, cj.id));
                var data = JsonConvert.SerializeObject(cj);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = null;
                //response = await client.PostAsync(uri, content);
                var response = client.PostAsync(uri, content).Result;
                var jsonReturned = response.Content.ReadAsStringAsync();
                var clienteServer = JsonConvert.DeserializeObject<ClienteJson>(jsonReturned.Result);

                if (!response.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine(response);
                    //var t = response.RequestMessage;
                    //throw new Exception("Erro ao incluir cliente");
                    return null;
                }
                else
                    return clienteServer;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        public ClienteJson GetClienteAsync(Cliente cliente)
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/testeapi/cliente/{0}";

                ClienteJson cj = loadClienteJson(cliente);

                var uri = new Uri(string.Format(url, cj.id));
                var data = JsonConvert.SerializeObject(cj);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                var response = client.GetAsync(uri).Result;
                var jsonReturned = response.Content.ReadAsStringAsync();
                var clienteServer = JsonConvert.DeserializeObject<ClienteJson>(jsonReturned.Result);

                if (!response.IsSuccessStatusCode)
                {
                    //throw new Exception("Erro ao atualizar cliente");
                    return null;
                }
                else
                    return clienteServer;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        public void UpdateClienteAsync(Cliente cliente)
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/testeapi/cliente/{0}";

                ClienteJson cj = loadClienteJson(cliente);

                var uri = new Uri(string.Format(url, cj.id));
                var data = JsonConvert.SerializeObject(cj);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = null;
                //response = await client.PostAsync(uri, content);
                response = client.PutAsync(uri, content).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception("Erro ao atualizar cliente");
                }
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        public void DeleteClienteAsync(Cliente cliente)
        {
            string url = "http://www.cottonappadm.xyz/testeapi/cliente/{0}";
            var uri = new Uri(string.Format(url, cliente.Id));
            var response = client.DeleteAsync(uri).Result;

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Erro ao deletar cliente");
            }
            //await client.DeleteAsync(uri);
        }


        public void PostLoginAsync()
        {
            var formcontent = new FormUrlEncodedContent(new[]{
                new KeyValuePair<string, string>("nome", "nomeUser"),
                new KeyValuePair<string, string>("email", "email"),
                new KeyValuePair<string, string>("login", "senha"),
                new KeyValuePair<string, string>("senha", "senha")
                });

            var request = client.PostAsync("http://www.cottonappadm.xyz/testeapi/cliente", formcontent).Result;

            request.EnsureSuccessStatusCode();

            var response = request.Content.ReadAsStringAsync().Result;

            var login = JObject.Parse(response).ToObject<Cliente>();

            //return login;
        }

        private ClienteJson loadClienteJson(Cliente cliente)
        {
            ClienteJson cj = new ClienteJson();

            cj.id = cliente.Id;
            cj.nome = cliente.Nome;
            cj.email = cliente.Email;
            cj.login = cliente.Login;
            cj.senha = cliente.Senha;
            cj.telefone = cliente.Telefone;
            cj.cidade = cliente.Cidade;
            cj.uf = cliente.UF;
            cj.cep = cliente.CEP;
            cj.logradouro = cliente.Logradouro;
            cj.numero = cliente.Numero;
            cj.bairro = cliente.Bairro;
            cj.cpf = cliente.CPF;
            cj.data_desativacao = "";//cliente.DataDesativacao.ToString();
            cj.status = cliente.Status;

            return cj;
        }


        // ---------------- Semeaduras ---------------------------------- //
        public SemeaduraJson AddSemeaduraAsync(Semeadura semeadura)
        {
            try
            {
                string url = "http://www.cottonappadm.xyz/testeapi/semeadura";

                SemeaduraJson sj = loadSemeaduraJson(semeadura);

                var uri = new Uri(string.Format(url, sj.id));
                var data = JsonConvert.SerializeObject(sj);
                var content = new StringContent(data, Encoding.UTF8, "application/json");
                //HttpResponseMessage response = null;
                //response = await client.PostAsync(uri, content);
                var response = client.PostAsync(uri, content).Result;
                var jsonReturned = response.Content.ReadAsStringAsync();
                var semeaduraServer = JsonConvert.DeserializeObject<SemeaduraJson>(jsonReturned.Result);

                if (!response.IsSuccessStatusCode)
                {
                    System.Diagnostics.Debug.WriteLine(response);
                    //var t = response.RequestMessage;
                    //throw new Exception("Erro ao incluir cliente");
                    return null;
                }
                else
                    return semeaduraServer;
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        private SemeaduraJson loadSemeaduraJson(Semeadura semeadura)
        {
            SemeaduraJson sj = new SemeaduraJson();

            sj.id = semeadura.Id;
            sj.quilos_sementes = semeadura.QuilosSementes;
            sj.germinacao = semeadura.Germinacao;
            sj.metros_lineares = semeadura.MetrosLineares;
            sj.talhao_id = semeadura.TalhaoId;
            sj.cultivar_id = semeadura.CultivarEpocaSemeaduraCultId;
            sj.epoca_semeadura_id = semeadura.CultivarEpocaSemeaduraEpId;
            sj.data = semeadura.Data.ToString();
            //cliente.DataDesativacao.ToString();

            return sj;
        }
    }
}
