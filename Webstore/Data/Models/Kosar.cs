using System;
using System.Collections.Generic;

namespace Webstore.Data.Models
{
    public partial class Kosar
    {
        public Kosar()
        {
            KosarTetel = new HashSet<KosarTetel>();
        }

        public int Id { get; set; }
        public DateTime? Datum { get; set; }
        public int? TelephelyId { get; set; }
        public int? VevoId { get; set; }
        public int? StatuszId { get; set; }

        public Statusz Statusz { get; set; }
        public Vevo Vevo { get; set; }
        public ICollection<KosarTetel> KosarTetel { get; set; }
    }
}
