using SQLite.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegulaPrism.Models
{
    public class CalculosSemeadura
    {
        [PrimaryKey, AutoIncrement]
        public int Id
        {
            get;
            set;
        }

        [NotNull]
        public decimal QtdeSementesMetro
        {
            get;
            set;
        }

        [NotNull]
        public decimal PesoMinimoSementesHa
        {
            get;
            set;
        }

        [NotNull]
        public decimal PesoMaximoSementesHa
        {
            get;
            set;
        }

        [NotNull]
        public decimal PesoMinimoSementesAlq
        {
            get;
            set;
        }

        [NotNull]
        public decimal PesoMaximoSementesAlq
        {
            get;
            set;
        }

        public int SemeaduraId
        {
            get;
            set;
        }

        //public Semeadura Semeadura
        //{
        //    get;
        //    set;
        //}
    }
}
