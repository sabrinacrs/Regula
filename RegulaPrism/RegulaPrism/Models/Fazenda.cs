
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
        public string Nome
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
        public string Telefone
        {
            get;
            set;
        }

        [MaxLength(45)]
        public string Email
        {
            get;
            set;
        }

        [MaxLength(45)]
        public string EnderecoWeb
        {
            get;
            set;
        }

        [MaxLength(45)]
        public string Cidade
        {
            get;
            set;
        }

        [MaxLength(2)]
        public string UF
        {
            get;
            set;
        }

        [MaxLength(45)]
        public string Bairro
        {
            get;
            set;
        }

        [MaxLength(200)]
        public string Observacoes
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
