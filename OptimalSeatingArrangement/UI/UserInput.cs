using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimalSeatingArrangement.Validation;
using OptimalSeatingArrangement.Controllers;

namespace OptimalSeatingArrangement.UI
{
    

    public class UserInput
    {
        private Validate validate;
        private Controller controller;

        public UserInput()
        {
            validate = new Validate();
            controller = new Controller();
        }


        public void Menu()
        {
            bool closeApp = false;

            Console.Clear();

            while (!closeApp) 
            {
                Console.WriteLine("==== MENU ====");
                Console.WriteLine("Options:");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("1 - Get all guests");
                Console.WriteLine("2 - Get guest by name");
                Console.WriteLine("3 - Update seating points for guest");
                Console.WriteLine("4 - Add new guest");
                Console.WriteLine("5 - Remove a guest");
                Console.WriteLine("6 - Show optimal seating arrangement");
                Console.WriteLine("7 - Set up database according to example given by customer");

                var option = validate.ValidateMenuOption(Console.ReadLine());
                if (option == -1)
                {
                    Console.Clear();
                    break;
                }

                switch (option)
                {
                    case 0:
                        closeApp = true;
                        break;
                    case 1:
                        controller.GetAllGuests();
                        break;
                    case 2:
                        controller.GetAllGuests();
                        GetGuestByName();
                        break;
                    case 3:
                        UpdateSeatingPoints();
                        break;
                    case 4:
                        AddGuest();
                        Console.Clear();
                        break;
                    case 5:
                        RemoveGuest();
                        Console.Clear();
                        break;
                    case 6:
                        controller.OptimalSeatingArrangement();
                        break;
                    case 7:
                        controller.RemoveAllGuests();
                        controller.SetUpDatabase();
                        break;
                    default:
                        break;
                }

            }
        }

        public void GetGuestByName()
        {
            Console.WriteLine("Name of Guest:");
            var name = validate.ValidateName(Console.ReadLine());
            while (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("Wrong input, write name in string:");
                name = validate.ValidateName(Console.ReadLine());
            }
            controller.GetGuestByName(name);
        }

        public void AddGuest()
        {
            Console.Clear();
            Console.WriteLine("Adding Guest\n");
            bool close = false;
            while (!close)
            {
                Console.WriteLine("Name of Guest:");
                var name = validate.ValidateName(Console.ReadLine());
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Wrong input, write name in string:");
                    name = validate.ValidateName(Console.ReadLine());
                }
                controller.AddGuest(name);

                Console.WriteLine("Add more? write [yes] to add more");
                var more = Console.ReadLine();
                if (more != "yes")
                    close = true;
            }
        }

        public void RemoveGuest()
        {
            Console.Clear();
            Console.WriteLine("Removing Guest\n");
            bool close = false;
            while (!close)
            {
                Console.WriteLine("Name of Guest:");
                var name = validate.ValidateName(Console.ReadLine());
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Wrong input, write name in string:");
                    name = validate.ValidateName(Console.ReadLine());
                }
                controller.RemoveGuest(name);

                Console.WriteLine("Remove more? write [yes] to remove more");
                var more = Console.ReadLine();
                if (more != "yes")
                    close = true;
            }
        }

        public void UpdateSeatingPoints()
        {
            Console.Clear();
            Console.WriteLine("Updating seating points\n");
            bool close = false;
            while (!close)
            {
                Console.WriteLine("Name of Guest to update:");
                var name = validate.ValidateName(Console.ReadLine());
                while (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Wrong input, write name in string:");
                    name = validate.ValidateName(Console.ReadLine());
                }
                controller.UpdateSeatingPoints(name);

                Console.WriteLine("Update more? write [yes] to update more");
                var more = Console.ReadLine();
                if (more != "yes")
                    close = true;
            }
        }
    }
}
