using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class Cultivar
    {
        [PrimaryKey]
        public int Id
        {
            get;
            set;
        }

        [MaxLength(45), NotNull]
        public string Nome
        {
            get;
            set;
        }

        [MaxLength(45), NotNull]
        public string AlturaPlanta
        {
            get;
            set;
        }

        [MaxLength(45), NotNull]
        public string Fertilidade
        {
            get;
            set;
        }

        [MaxLength(45), NotNull]
        public string Regulador
        {
            get;
            set;
        }

        [NotNull]
        public decimal RendimentoFibraMinimo
        {
            get;
            set;
        }

        [NotNull]
        public decimal RendimentoFibraMaximo
        {
            get;
            set;
        }

        [NotNull]
        public decimal PesoMedioCapulhoMinimo
        {
            get;
            set;
        }

        [NotNull]
        public decimal PesoMedioCapulhoMaximo
        {
            get;
            set;
        }

        [NotNull]
        public decimal ComprimentoFibraMinimo
        {
            get;
            set;
        }

        [NotNull]
        public decimal ComprimentoFibraMaximo
        {
            get;
            set;
        }

        [NotNull]
        public decimal MicronaireMinimo
        {
            get;
            set;
        }

        [NotNull]
        public decimal MicronaireMaximo
        {
            get;
            set;
        }

        [NotNull]
        public decimal ResistenciaMinimo
        {
            get;
            set;
        }

        [NotNull]
        public decimal ResistenciaMaximo
        {
            get;
            set;
        }

        [NotNull]
        public decimal PesoSementesMinimo
        {
            get;
            set;
        }

        [NotNull]
        public decimal PesoSementesMaximo
        {
            get;
            set;
        }

        public string Status
        {
            get;
            set;
        }

        public DateTime DataDesativacao
        {
            get;
            set;
        }

        public int CicloId
        {
            get;
            set;
        }

        public override string ToString()
        {
            return string.Format("Nome={0}", Nome);
        }
    }
}
