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
        public Guest() { }
        
        public Guest(Guest guest)
        {
            Id = guest.Id;
            Name = guest.Name;
            LeftNeighbour = guest.LeftNeighbour;
            RightNeighbour = guest.RightNeighbour;
            GuestPointsDictionairy = guest.GuestPointsDictionairy;
            GuestPointsJson = guest.GuestPointsJson;
        }

        public int Id { get; set; }
        public string? Name { get; set; }

        [NotMapped]
        public Guest? LeftNeighbour { get; set; }
        [NotMapped]
        public Guest? RightNeighbour { get; set; }

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

    public class GuestTableListDTO
    {
        public int Index { get; set; }
        public string Name { get; set; }
        public string LeftNeighbour { get; set; }
        public string RightNeighbour { get; set; }
    }
}
