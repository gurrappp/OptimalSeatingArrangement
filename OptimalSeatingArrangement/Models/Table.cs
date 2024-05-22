using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalSeatingArrangement.Models
{
    public class Table
    {

        public Table()
        {
            TableGuests = new List<Guest>();
            TotalHappinessPoints = 0;
        }
        
        public List<Guest>? TableGuests { get; set; }
        public int? TotalHappinessPoints { get; set; }

        public int? CalculateHappinessPoints()
        {
            int points = 0;
            int index = 0;
            if (TableGuests == null) return 0;

            foreach (var guest in TableGuests)
            {
                index = TableGuests.IndexOf(guest);
                guest.LeftNeighbour = TableGuests[index == 0 ? TableGuests.Count - 1 : index - 1];
                guest.RightNeighbour = TableGuests[index == TableGuests.Count - 1 ? 0 : index + 1];
                TotalHappinessPoints += guest.GuestPointsDictionairy[guest.LeftNeighbour.Name] + guest.GuestPointsDictionairy[guest.RightNeighbour.Name];
            }
            
            return TotalHappinessPoints;
        }
    }
}
