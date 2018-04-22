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

        public ICollection<Kosar> Kosar { get; set; }
        public ICollection<KosarTetel> KosarTetel { get; set; }
    }
}
