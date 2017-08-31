using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RegulaPrism.Models;
using Xamarin.Forms;

[assembly: Dependency(typeof(RegulaPrism.Droid.MySqlConnect))]
namespace RegulaPrism.Droid
{
    public class MySqlConnect : IMySqlConnect
    {
        MySqlConnection conexaoMySQL;
        MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();

        public bool ConnectDB
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string CriarStringConexao()
        {
            builder.Server = "localhost"; //"mysql552.umbler.com" 192.168.60.1;
            builder.Port = 3306; //41890;
            builder.Database = "reguladb";
            builder.UserID = "root";
            builder.Password = "";

            return builder.ToString();
        }

        public List<Cultivar> CarregaCultivares()
        {
            List<Cultivar> lista;
            string StringConexaoMysql = CriarStringConexao();
            string consulta = "SELECT * FROM cultivares";
            try
            {
                conexaoMySQL = new MySqlConnection(StringConexaoMysql);
                conexaoMySQL.Open();

                MySqlDataReader dataReader = null;
                MySqlCommand command = new MySqlCommand(consulta, conexaoMySQL);

                dataReader = command.ExecuteReader();
                lista = new List<Cultivar>();

                while (dataReader.Read())
                {
                    Cultivar c = new Cultivar();

                    c.Id = Int32.Parse(dataReader["id"].ToString());
                    c.Nome = dataReader["nome"].ToString();
                    c.AlturaPlanta = dataReader["altura_planta"].ToString();
                    c.Fertilidade = dataReader["fertilidade"].ToString();
                    c.Regulador = dataReader["regulador"].ToString();
                    c.RendimentoFibraMinimo = Decimal.Parse(dataReader["rendimento_fibra_minimo"].ToString());
                    c.RendimentoFibraMaximo = Decimal.Parse(dataReader["rendimento_fibra_maximo"].ToString());
                    c.PesoMedioCapulhoMinimo = Decimal.Parse(dataReader["peso_capulho_minimo"].ToString());
                    c.PesoMedioCapulhoMaximo = Decimal.Parse(dataReader["peso_capulho_maximo"].ToString());
                    c.ComprimentoFibraMinimo = Decimal.Parse(dataReader["comprimento_fibra_minimo"].ToString());
                    c.ComprimentoFibraMaximo = Decimal.Parse(dataReader["comprimento_fibra_maximo"].ToString());
                    c.MicronaireMinimo = Decimal.Parse(dataReader["micronaire_minimo"].ToString());
                    c.MicronaireMaximo = Decimal.Parse(dataReader["micronaire_maximo"].ToString());
                    c.ResistenciaMinimo = Decimal.Parse(dataReader["resistencia_minimo"].ToString());
                    c.ResistenciaMaximo = Decimal.Parse(dataReader["resistencia_maximo"].ToString());
                    c.PesoSementesMinimo = Decimal.Parse(dataReader["peso_sementes_minimo"].ToString());
                    c.PesoSementesMaximo = Decimal.Parse(dataReader["peso_sementes_maximo"].ToString());

                    var status = dataReader["status"].ToString();
                    if (!status.Equals(""))
                        c.Status = dataReader["status"].ToString().ToCharArray().ElementAt(0);
                    //c.DataDesativacao = DateTime.Parse(dataReader["data_desativacao"].ToString());
                    c.CicloId = Int32.Parse(dataReader["cic_id"].ToString());

                    lista.Add(c);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaoMySQL.Close();
            }
        }

        public List<EpocaSemeadura> CarregaEpocasSemeadura()
        {
            List<EpocaSemeadura> lista;
            string StringConexaoMysql = CriarStringConexao();
            string consulta = "SELECT * FROM epocassemeaduras";
            try
            {
                conexaoMySQL = new MySqlConnection(StringConexaoMysql);
                conexaoMySQL.Open();

                MySqlDataReader dataReader = null;
                MySqlCommand command = new MySqlCommand(consulta, conexaoMySQL);

                dataReader = command.ExecuteReader();
                lista = new List<EpocaSemeadura>();

                while (dataReader.Read())
                {
                    EpocaSemeadura es = new EpocaSemeadura();

                    es.Id = Int32.Parse(dataReader["id"].ToString());
                    es.Descricao = dataReader["descricao"].ToString();
                    var status = dataReader["status"].ToString();
                    if (!status.Equals(""))
                        es.Status = dataReader["status"].ToString().ToCharArray().ElementAt(0);

                    lista.Add(es);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaoMySQL.Close();
            }
        }

        public List<Doenca> CarregaDoencas()
        {
            List<Doenca> lista;
            string StringConexaoMysql = CriarStringConexao();
            string consulta = "SELECT * FROM doencas";
            try
            {
                conexaoMySQL = new MySqlConnection(StringConexaoMysql);
                conexaoMySQL.Open();

                MySqlDataReader dataReader = null;
                MySqlCommand command = new MySqlCommand(consulta, conexaoMySQL);

                dataReader = command.ExecuteReader();
                lista = new List<Doenca>();

                while (dataReader.Read())
                {
                    Doenca doe = new Doenca();

                    doe.Id = Int32.Parse(dataReader["id"].ToString());
                    doe.Descricao = dataReader["descricao"].ToString();
                    var status = dataReader["status"].ToString();
                    if (!status.Equals(""))
                        doe.Status = dataReader["status"].ToString().ToCharArray().ElementAt(0);

                    lista.Add(doe);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaoMySQL.Close();
            }
        }

        public void CarregaDados(ref List<Cultivar> lista)
        {
            string consulta = "select nome from cultivares";
            string stringConexaoMySQL = CriarStringConexao();
            try
            {
                MySqlConnection con = new MySqlConnection();
                con.Open();

                MySqlDataReader rdr = null;
                MySqlCommand cmd = new MySqlCommand(consulta, con);

                rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    Cultivar p = new Cultivar();
                    p.Nome = rdr["nome"].ToString();
                    //p.Preco = Convert.ToDecimal(rdr["preco"]);

                    lista.Add(p);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public List<Cultivar> CarregaDados()
        {
            throw new NotImplementedException();
        }

        public List<CultivarEpocaSemeadura> CarregaCultivarEpocaSemeadura()
        {
            List<CultivarEpocaSemeadura> lista;
            string StringConexaoMysql = CriarStringConexao();
            string consulta = "SELECT * FROM cultivares_has_epocasemeadura";
            try
            {
                conexaoMySQL = new MySqlConnection(StringConexaoMysql);
                conexaoMySQL.Open();

                MySqlDataReader dataReader = null;
                MySqlCommand command = new MySqlCommand(consulta, conexaoMySQL);

                dataReader = command.ExecuteReader();
                lista = new List<CultivarEpocaSemeadura>();

                while (dataReader.Read())
                {
                    CultivarEpocaSemeadura ces = new CultivarEpocaSemeadura();

                    ces.CultivarId = Int32.Parse(dataReader["cult_id"].ToString());
                    ces.EpocaSemeaduraId = Int32.Parse(dataReader["ep_id"].ToString());
                    ces.PlantasHa = Decimal.Parse(dataReader["plantas_ha"].ToString());

                    lista.Add(ces);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaoMySQL.Close();
            }
        }

        public List<CultivarEpocaSemeadura> CarregaCultivarEpocaSemeadura(int cultivarId)
        {
            List<CultivarEpocaSemeadura> lista;
            string StringConexaoMysql = CriarStringConexao();
            string consulta = "SELECT * FROM cultivares_has_epocasemeadura WHERE cult_id = @cultId";
            consulta = consulta.Replace("@cultId", cultivarId.ToString());

            try
            {
                conexaoMySQL = new MySqlConnection(StringConexaoMysql);
                conexaoMySQL.Open();

                MySqlDataReader dataReader = null;
                MySqlCommand command = new MySqlCommand(consulta, conexaoMySQL);

                dataReader = command.ExecuteReader();
                lista = new List<CultivarEpocaSemeadura>();

                while (dataReader.Read())
                {
                    CultivarEpocaSemeadura ces = new CultivarEpocaSemeadura();

                    ces.CultivarId = Int32.Parse(dataReader["cult_id"].ToString());
                    ces.EpocaSemeaduraId = Int32.Parse(dataReader["ep_id"].ToString());
                    ces.PlantasHa = Decimal.Parse(dataReader["plantas_ha"].ToString());

                    lista.Add(ces);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaoMySQL.Close();
            }
        }

        public List<CultivarEpocaSemeadura> CarregaCultivarEpocaSemeaduraId(int epocaId)
        {
            List<CultivarEpocaSemeadura> lista;
            string StringConexaoMysql = CriarStringConexao();
            string consulta = "SELECT * FROM cultivares_has_epocasemeadura WHERE ep_id = @epId";
            consulta = consulta.Replace("@epId", epocaId.ToString());

            try
            {
                conexaoMySQL = new MySqlConnection(StringConexaoMysql);
                conexaoMySQL.Open();

                MySqlDataReader dataReader = null;
                MySqlCommand command = new MySqlCommand(consulta, conexaoMySQL);

                dataReader = command.ExecuteReader();
                lista = new List<CultivarEpocaSemeadura>();

                while (dataReader.Read())
                {
                    CultivarEpocaSemeadura ces = new CultivarEpocaSemeadura();

                    ces.CultivarId = Int32.Parse(dataReader["cult_id"].ToString());
                    ces.EpocaSemeaduraId = Int32.Parse(dataReader["ep_id"].ToString());
                    ces.PlantasHa = Decimal.Parse(dataReader["plantas_ha"].ToString());

                    lista.Add(ces);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexaoMySQL.Close();
            }
        }
    }
}
