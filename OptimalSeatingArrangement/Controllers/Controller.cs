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

            TableVisualizationEngine.ShowTable(GuestToDTO(guests), "Guests");
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
            
            TableVisualizationEngine.ShowTable(GuestToDTO(new List<Guest> { guest}), "Guest");

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

        public void RemoveGuest(string name)
        {
            using var db = new GuestContext();

            var existingGuest = db.Guests.FirstOrDefault(g => g.Name == name);
            if (existingGuest == null)
            {
                Console.WriteLine("\nThat Guest does not exist. Choose another Guest to remove.");
                return;
            }

            db.Remove(existingGuest);
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

        public void OptimalSeatingArrangement()
        {

            using var db = new GuestContext();

            var guests = db.Guests.Select(x => x).ToList();
            var seats = new List<Seat>();
            var table = new Models.Table();


            foreach ( var guest in guests) 
            {
                guest.GuestPointsDictionairy = JsonConvert.DeserializeObject<Dictionary<string, int>>(guest.GuestPointsJson) ?? [];
                if(guest.GuestPointsDictionairy.Count != guests.Count)
                {
                    Console.WriteLine("\nAll Guests need to have points to other Guests! please update seating points");
                    return;
                }

                seats.Add(new Seat
                {
                    Guest = guest
                });
            }

            table.Seats = seats;

            foreach(var seat in table.Seats)
            {
                
            }

        }

        public void SetUpDatabase()
        {
            using var db = new GuestContext();

            var Alice = new Guest { 
                Name = "Alice",
                GuestPointsDictionairy= new Dictionary<string, int>
                {
                    {"Bob",54 },
                    {"Carol",-81 },
                    {"David",-42 },
                    {"Eric",89 },
                    {"Frank",-89 },
                    {"George",97 },
                    {"Mallory",-94 }
                }
            };
            Alice.GuestPointsJson = JsonConvert.SerializeObject(Alice.GuestPointsDictionairy);
            
            var Bob = new Guest
            {
                Name = "Bob",
                GuestPointsDictionairy = new Dictionary<string, int>
                {
                    {"Alice",3 },
                    {"Carol",-70 },
                    {"David",-31 },
                    {"Eric",72 },
                    {"Frank",-25 },
                    {"George",-95 },
                    {"Mallory",11 }
                }
            };
            Bob.GuestPointsJson = JsonConvert.SerializeObject(Bob.GuestPointsDictionairy);

            var Carol = new Guest
            {
                Name = "Carol",
                GuestPointsDictionairy = new Dictionary<string, int>
                {
                    {"Alice",-83 },
                    {"Bob",8 },
                    {"David",35 },
                    {"Eric",10 },
                    {"Frank",61 },
                    {"George",10 },
                    {"Mallory",29 }
                }
            };
            Carol.GuestPointsJson = JsonConvert.SerializeObject(Carol.GuestPointsDictionairy);

            var David = new Guest
            {
                Name = "David",
                GuestPointsDictionairy = new Dictionary<string, int>
                {
                    {"Alice",67 },
                    {"Bob",25 },
                    {"Carol",48 },
                    {"Eric",-65 },
                    {"Frank",8 },
                    {"George",84 },
                    {"Mallory",9 }
                }
            };
            David.GuestPointsJson = JsonConvert.SerializeObject(David.GuestPointsDictionairy);

            var Eric = new Guest
            {
                Name = "Eric",
                GuestPointsDictionairy = new Dictionary<string, int>
                {
                    {"Alice",-51 },
                    {"Bob",-39 },
                    {"Carol",84 },
                    {"David",-98 },
                    {"Frank",-20 },
                    {"George",-6 },
                    {"Mallory",60 }
                }
            };
            Eric.GuestPointsJson = JsonConvert.SerializeObject(Eric.GuestPointsDictionairy);

            var Frank = new Guest
            {
                Name = "Frank",
                GuestPointsDictionairy = new Dictionary<string, int>
                {
                    {"Alice",51 },
                    {"Bob",79 },
                    {"Carol",88 },
                    {"David",33 },
                    {"Eric",43 },
                    {"George",77 },
                    {"Mallory",-3 }
                }
            };
            Frank.GuestPointsJson = JsonConvert.SerializeObject(Frank.GuestPointsDictionairy);

            var George = new Guest
            {
                Name = "George",
                GuestPointsDictionairy = new Dictionary<string, int>
                {
                    {"Alice",-14 },
                    {"Bob",-12 },
                    {"Carol",-52 },
                    {"David",14 },
                    {"Eric",-62 },
                    {"Frank",-18 },
                    {"Mallory",-36 }
                }
            };
            George.GuestPointsJson = JsonConvert.SerializeObject(George.GuestPointsDictionairy);

            var Mallory = new Guest
            {
                Name = "Mallory",
                GuestPointsDictionairy = new Dictionary<string, int>
                {
                    {"Alice",-36 },
                    {"Bob",76 },
                    {"Carol",-34 },
                    {"David",37 },
                    {"Eric",40 },
                    {"Frank",18 },
                    {"George",7 }
                }
            };
            Mallory.GuestPointsJson = JsonConvert.SerializeObject(Mallory.GuestPointsDictionairy);

            db.Add(Alice);
            db.Add(Bob);
            db.Add(Carol);
            db.Add(David);
            db.Add(Eric);
            db.Add(Frank);
            db.Add(George);
            db.Add(Mallory);
            db.SaveChanges();
        }

        public void RemoveAllGuests()
        {
            using var db = new GuestContext();

            var all = from c in db.Guests select c;
            db.Guests.RemoveRange(all);
            db.SaveChanges();
            
        }

        public List<GuestDTO> GuestToDTO( List<Guest> guests)
        {
            List<GuestDTO> guestToDTO = new List<GuestDTO>();
            foreach(var guest in guests)
            {
                guestToDTO.Add(new GuestDTO
                {
                    Name = guest.Name,
                    GuestPointsJson = guest.GuestPointsJson
                });
            }

            return guestToDTO;
        } 
        
    }
}
