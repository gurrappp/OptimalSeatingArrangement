using OptimalSeatingArrangement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OptimalSeatingArrangement.Controllers
{
    public class Controller
    {

        public void AddGuest(string name)
        {
            using var db = new GuestContext();

            db.Add(new Guest
            {
                Name = name
            });

            db.SaveChanges();
        }
        
    }
}
