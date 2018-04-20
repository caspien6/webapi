using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Webstore.Models
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

        [JsonIgnore]
        public Kategoria AlkategoriaNavigation { get; set; }
        [JsonIgnore]
        public ICollection<Kategoria> InverseAlkategoriaNavigation { get; set; }
        [JsonIgnore]
        public ICollection<Termek> Termek { get; set; }
    }
}
