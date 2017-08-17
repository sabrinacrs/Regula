
using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class Fazenda
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        [MaxLength(50), NotNull]
        public String Nome
        {
            get;
            set;
        }

        [NotNull]
        public decimal Hectares
        {
            get;
            set;
        }

        [MaxLength(15)]
        public String Telefone
        {
            get;
            set;
        }

        [MaxLength(45)]
        public String Email
        {
            get;
            set;
        }

        [MaxLength(45)]
        public String EnderecoWeb
        {
            get;
            set;
        }

        [MaxLength(45)]
        public String Cidade
        {
            get;
            set;
        }

        [MaxLength(2)]
        public String UF
        {
            get;
            set;
        }

        [MaxLength(45)]
        public String Bairro
        {
            get;
            set;
        }

        [MaxLength(200)]
        public String Observacoes
        {
            get;
            set;
        }

        public DateTime DataDesativacao
        {
            get;
            set;
        }

        public int ClienteId
        {
            get;
            set;
        }
    }
}
