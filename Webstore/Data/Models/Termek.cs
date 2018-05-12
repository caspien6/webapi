using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Webstore.Data.Models
{
    public partial class Termek
    {
        public Termek()
        {
            KosarTetel = new HashSet<KosarTetel>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        public double? Ar { get; set; }
        public int? Raktarkeszlet { get; set; }
        public int? KategoriaId { get; set; }
        public string Leiras { get; set; }
        public int Views { get; set; }
        public string KepUrl { get; set; }

        [JsonIgnore]
        public Kategoria Kategoria { get; set; }
        [JsonIgnore]
        public ICollection<KosarTetel> KosarTetel { get; set; }
    }
}
