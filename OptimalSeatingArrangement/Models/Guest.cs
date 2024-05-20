using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OptimalSeatingArrangement.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        //public List<Guest>? Neighbours { get; set; }

        [NotMapped]
        public Dictionary<string, int> GuestPointsDictionairy { get; set; } = [];

        public string GuestPointsJson
        {
            get => JsonConvert.SerializeObject(GuestPointsDictionairy);
            set => GuestPointsDictionairy = JsonConvert.DeserializeObject<Dictionary<string, int>>(value) ?? [];
        }

    }

    public class GuestDTO
    {
        public string Name { get; set; }

        public string GuestPointsJson { get; set; }
    }
}
