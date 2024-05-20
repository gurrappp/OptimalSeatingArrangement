using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalSeatingArrangement.Models
{
    public class Seat
    {
        public Guest? Guest { get; set; }
        public Guest Neighbour1 { get; set; }
        public Guest Neighbour2 { get; set; }
    }
}
