using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Webstore.Data.Models
{
    public partial class KosarTetel
    {
        public int Id { get; set; }
        public int? Mennyiseg { get; set; }
        public double? Ar { get; set; }
        public int? KosarId { get; set; }
        public int? TermekId { get; set; }
        public int? StatuszId { get; set; }

        [JsonIgnore]
        public Kosar Kosar { get; set; }
        [JsonIgnore]
        public Statusz Statusz { get; set; }
        [JsonIgnore]
        public Termek Termek { get; set; }
    }
}
