using Newtonsoft.Json;
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
    }
}
