using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Webstore.Data.Models
{
    public partial class Statusz
    {
        public Statusz()
        {
            Kosar = new HashSet<Kosar>();
            KosarTetel = new HashSet<KosarTetel>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        [JsonIgnore]
        public ICollection<Kosar> Kosar { get; set; }
        [JsonIgnore]
        public ICollection<KosarTetel> KosarTetel { get; set; }
    }
}
