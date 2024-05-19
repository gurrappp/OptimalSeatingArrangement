using Microsoft.EntityFrameworkCore.Metadata.Internal;
using OptimalSeatingArrangement.Models;
using OptimalSeatingArrangement.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OptimalSeatingArrangement.TableVizualisation;

namespace OptimalSeatingArrangement.Controllers
{
    public class Controller
    {
        private readonly Validate validate;

        public Controller() 
        { 
            validate = new Validate();
        }

        public void GetAllGuests()
        {
            using var db = new GuestContext();

            var guests = db.Guests.Select(x => x).ToList();

            foreach(var guest in guests)
            {
                guest.GuestPointsDictionairy = JsonConvert.DeserializeObject<Dictionary<string, int>>(guest.GuestPointsJson) ?? [];

            }


            TableVisualizationEngine.ShowTable(guests, "Guests");
        }

        public void GetGuestByName(string name)
        {
            using var db = new GuestContext();

            var guest = db.Guests.FirstOrDefault(x => x.Name == name);
            if (guest == null)
            {
                Console.WriteLine($"\nCould not find guest with name: {name}");
                return;
            }
            
            guest.GuestPointsDictionairy = JsonConvert.DeserializeObject<Dictionary<string, int>>(guest.GuestPointsJson) ?? [];
           
            TableVisualizationEngine.ShowTable(new List<Guest>{guest}, "Guest");

        }

        public void AddGuest(string name)
        {
            using var db = new GuestContext();

            var existingGuest = db.Guests.FirstOrDefault(g => g.Name == name);
            if(existingGuest != null)
            {
                Console.WriteLine("\nThat Guest already exist. Choose another Guest to add.");
                return;
            }

            
            db.Add(new Guest
            {
                Name = name
            });

            db.SaveChanges();
            
        }

        public void UpdateSeatingPoints(string name)
        {
            using var db = new GuestContext();

            var guest = db.Guests.FirstOrDefault(g => g.Name == name);
            if(guest == null)
            {
                Console.WriteLine("\nCant find guest. Write another name.");
                return;
            }

            var guestList = db.Guests.Where(g => g.Name != name).ToList();
            Dictionary<string, int> temp = new Dictionary<string, int>();
            foreach(var item in guestList)
            {
                Console.WriteLine($"\nSet happiness points for {name} sitting next to {item.Name}");
                var points = validate.ValidateHappinessPoints(Console.ReadLine());

                temp.Add(item.Name, points == null ? 0 : (int)points);
            }

            guest.GuestPointsJson = JsonConvert.SerializeObject(temp);

            db.SaveChanges();

        }
        
    }
}
