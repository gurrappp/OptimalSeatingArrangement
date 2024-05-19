using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OptimalSeatingArrangement.Validation;

namespace OptimalSeatingArrangement.UI
{
    

    public class UserInput
    {
        private Validate validate;

        public UserInput()
        {
            validate = new Validate();
        }


        public async Task Menu()
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
                Console.WriteLine("4 - Insert new guest");
                Console.WriteLine("5 - Remove a guest");
                Console.WriteLine("6 - Show optimal seating arrangement");

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
                    case 4:
                        break;
                    default:
                        break;
                }

            }
        }
    }
}
