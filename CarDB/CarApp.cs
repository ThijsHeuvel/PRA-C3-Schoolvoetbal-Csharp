using CarDB;
using CarDB.Data;
using CarDB.Model;
using System.Runtime.CompilerServices;
//using static System.Runtime.InteropServices.JavaScript.JSType;

namespace SimpleCrud
{
    internal class CarApp
    {
        CarContext dataContext;


        public CarApp()
        {
            dataContext = new CarContext();
        }

        internal void Run()
        {
            string userInput = "";

            while(userInput.ToLower() != "x")
            {
                userInput = ShowMenu();
                handleUserInput(userInput);
            }
        }

        private void handleUserInput(string userInput)
        {
            switch (userInput)
            {
                case "1":
                    // Show all
                    ShowAll();
                    break;
                case "2":
                    // Add new
                    AddNew();
                    break;
                case "3":
                    // Update
                    UpdateCar();
                    break;
                case "4":
                    // Delete
                    Delete();
                    break;

                default:
                    Console.WriteLine("Incorrect choice...");
                    // Invalid input
                    break;
            }
            Helpers.Pause();
        }

        private void Delete()
        {
            Car car = SelectCar();
            dataContext.Cars.Remove(car);

            dataContext.SaveChanges();
            Console.WriteLine("Car Deleted.");

        }


        private void AddNew()
        {

            string brand = Helpers.Ask("Brand car:");
            string model = Helpers.Ask("Model car:");
            int year = Helpers.AskForInt("Year car:");
            string color = Helpers.Ask("Color car:");
            Car car = new Car(brand, model, year, color);
            dataContext.Cars.Add(car);
            dataContext.SaveChanges();

            Console.WriteLine("Car Added.");
        }

        private Car SelectCar()
        {
            ShowAll();
            Car? selectedCar = null;

            while (selectedCar == null)
            {
                int id = Helpers.AskForInt("Select ID Car.");
                selectedCar = dataContext.Cars.Find(id);
            }

            return selectedCar;


        }

        private void ShowAll()
        {
                               
            Console.WriteLine("================ All Cars ================");
            List<Car> cars = dataContext.Cars.ToList();

            foreach (Car car in dataContext.Cars)
            {
                Console.WriteLine(car);
            }
            Console.WriteLine("=============================================");
        }

        private void UpdateCar()
        {

            Car car = SelectCar();
            car.Brand = Helpers.Ask("New Brand car:");
            car.Model = Helpers.Ask("New Model car:");
            car.Year = Helpers.AskForInt("New Year car:");
            car.Color = Helpers.Ask("New Color car:");
            dataContext.Cars.Update(car);
            dataContext.SaveChanges();

        }

        private string ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Show all Cars");
            Console.WriteLine("2. Add new Car");
            Console.WriteLine("3. Update Car");
            Console.WriteLine("4. Delete Car");
            Console.WriteLine("X. Exit");

            string userInput = Helpers.Ask("Make your choice and press <ENTER>.");
            return userInput;
        }
    }
}