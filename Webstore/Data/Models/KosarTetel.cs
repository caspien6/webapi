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

        public Kosar Kosar { get; set; }
        public Statusz Statusz { get; set; }
        public Termek Termek { get; set; }
    }
}
