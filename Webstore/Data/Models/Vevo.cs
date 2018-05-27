using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Webstore.Data.Models
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
        [JsonIgnore]
        public string Jelszo { get; set; }
        public string Email { get; set; }

        [JsonIgnore]
        public string IdentityId { get; set; }
        [JsonIgnore]
        public ApplicationUser Identity { get; set; }
        [JsonIgnore]
        public ICollection<Kosar> Kosar { get; set; }
    }
}
