using System;
using System.Collections.Generic;

namespace Webstore.Models
{
    public partial class Vevo
    {
        public Vevo()
        {
            Kosar = new HashSet<Kosar>();
        }

        public int Id { get; set; }
        public string Nev { get; set; }
        public string Szamlaszam { get; set; }
        public string Login { get; set; }
        public string Jelszo { get; set; }
        public string Email { get; set; }

        public ICollection<Kosar> Kosar { get; set; }
    }
}
