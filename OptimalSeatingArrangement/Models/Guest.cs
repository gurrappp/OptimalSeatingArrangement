using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptimalSeatingArrangement.Models
{
    public class Guest
    {
        public int Id { get; set; }
        public required string Name { get; set; }

        [NotMapped]
        public Dictionary<string, int> GuestPoints { get; set; } = [];
    }
}
