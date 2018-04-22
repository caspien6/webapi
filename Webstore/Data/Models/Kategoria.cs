using System;
using System.Collections.Generic;

namespace Webstore.Data.Models
{
    public partial class Kategoria
    {
        public Kategoria()
        {
            InverseAlkategoriaNavigation = new HashSet<Kategoria>();
            Termek = new HashSet<Termek>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        public int? Alkategoria { get; set; }

        public Kategoria AlkategoriaNavigation { get; set; }
        public ICollection<Kategoria> InverseAlkategoriaNavigation { get; set; }
        public ICollection<Termek> Termek { get; set; }
    }
}
