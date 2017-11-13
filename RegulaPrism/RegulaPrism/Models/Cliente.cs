using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class Cliente
    {
        [PrimaryKey] // AutoIncrement
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

        [MaxLength(45), NotNull]
        public string Email
        {
            get;
            set;
        }

        [MaxLength(45), NotNull]
        public string Login
        {
            get;
            set;
        }

        [MaxLength(20), NotNull]
        public string Senha
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

        [MaxLength(10)]
        public string CEP
        {
            get;
            set;
        }

        [MaxLength(45)]
        public string Logradouro
        {
            get;
            set;
        }

        public int Numero
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

        [MaxLength(15)]
        public string CPF
        {
            get;
            set;
        }

        public DateTime DataDesativacao
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("Nome={0}, Email={1}, Telefone={2}", Nome, Email, Telefone);
        }
    }
}
