using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using System.Reflection;
class Program
{



    //static string customerFilePath = "/database/customers.json";
    //static string bookingFilePath = "/database/bookings.json";
    ////static string vehicleFilePath = "/Users/ATIFHANIF/Desktop/sda/Transportation_System/vehicles.json";

    
    
    static string customerFileName = "customers.json";
    static string bookingFileName = "bookings.json";
    //static string vehicleFileName = "vehicles.json";

   
    static string customerFilePath = Path.Combine(GetProjectDirectory(), "database", customerFileName);
    static string bookingFilePath = Path.Combine(GetProjectDirectory(), "database", bookingFileName);
    //static string vehicleFilePath = Path.Combine(GetProjectDirectory(), "vehicles.json");
    static List<Customer> customers = new List<Customer>();
    static List<Booking> bookings = new List<Booking>();
    //static List<Vehicle> vehicles = new List<Vehicle>();

    static Customer loggedInCustomer;

    static string GetProjectDirectory()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        return RemoveBinDebugFromPath(baseDirectory);
    }

    
    static string RemoveBinDebugFromPath(string path)
    {
        int binDebugIndex = path.IndexOf("bin/Debug/net7.0", StringComparison.OrdinalIgnoreCase);
        if (binDebugIndex >= 0)
        {
            return path.Substring(0, binDebugIndex);
        }
        return path;
    }


    static void LoadData()
    {
        if (File.Exists(customerFilePath))
        {
            string customerJson = File.ReadAllText(customerFilePath);
            customers = JsonConvert.DeserializeObject<List<Customer>>(customerJson);
        }

        if (File.Exists(bookingFilePath))
        {
            string bookingJson = File.ReadAllText(bookingFilePath);
            bookings = JsonConvert.DeserializeObject<List<Booking>>(bookingJson);
        }

        //if (File.Exists(vehicleFilePath))
        //{
        //    string vehicleJson = File.ReadAllText(vehicleFilePath);
        //    vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(vehicleJson);
        //}
    }
    static void SaveData()
    {
        string customerJson = JsonConvert.SerializeObject(customers);
        File.WriteAllText(customerFilePath, customerJson);

        string bookingJson = JsonConvert.SerializeObject(bookings);
        File.WriteAllText(bookingFilePath, bookingJson);

        //string vehicleJson = JsonConvert.SerializeObject(vehicles);
        //File.WriteAllText(vehicleFilePath, vehicleJson);
    }
    static int GetIntInput(string prompt)
    {
        int input;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.Write("Invalid input. Please enter a valid number: ");
        }
        return input;
    }

    static void Main()
    {
        int i = 1;


        LoadData();

        while (true)
        {
            if (i == 0)
            {
                SaveData();
            }
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("1. Sign Up");
            Console.WriteLine("2. Log In");
            Console.WriteLine("3. Administrator Log In");
            Console.WriteLine("4. Exit");
            Console.WriteLine("----------------------------------------------");
            i = 0;

            int choice = GetIntInput("Enter your choice: ");

            switch (choice)
            {
                case 1:
                    SignUp();
                    SaveData();
                    break;
                case 2:
                    LogIn();
                    break;
                case 3:
                    Administrator.Instance.AdministratorLogIn();
              
                    break;
                case 4:
                    SaveData();
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void SignUp()
    {
        Console.Write("Enter your name: ");
        string name = Console.ReadLine();
        int age = GetIntInput("Enter your age: ");
        Console.Write("Enter your mobile number: ");
        string mobileNumber = Console.ReadLine();
        Console.Write("Enter your email: ");
        string email = Console.ReadLine();
        Console.Write("Enter a username: ");
        string username = Console.ReadLine();
        Console.Write("Enter a password: ");
        string password = Console.ReadLine();

        Customer newCustomer = new Customer
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            Age = age,
            MobileNumber = mobileNumber,
            Email = email,
            Username = username,
            Password = password
        };
        Console.WriteLine();
        Console.WriteLine("id = " + newCustomer.Id);
        Console.WriteLine("Name = " + newCustomer.Name);
        if (customers == null)
        {
            customers = new List<Customer>();
        }
        customers.Add(newCustomer);
        Console.WriteLine("Account created successfully.");
    }

    static void LogIn()
    {
        Console.Write("Enter your username: ");
        string username = Console.ReadLine();
        Console.Write("Enter your password: ");
        string password = Console.ReadLine();


        loggedInCustomer = customers.Find(c => c.Username == username && c.Password == password);

        if (loggedInCustomer != null)
        {
            Console.WriteLine();
            Console.WriteLine($"Welcome, {loggedInCustomer.Name}!");

            while (true)
            {
                Console.WriteLine();
                Console.WriteLine("----------------------------------------------");
                Console.WriteLine("1. Book a Vehicle");
                Console.WriteLine("2. Reset Password");
                Console.WriteLine("3. View Booking Details");
                Console.WriteLine("4. Complain/query");
                Console.WriteLine("5. Log Out");
                Console.WriteLine("----------------------------------------------");

                int choice = GetIntInput("Enter your choice: ");

                switch (choice)
                {
                    case 1:
                        BookVehicle();
                        break;
                    case 2:
                        ResetPassword();
                        break;
                    case 3:
                        ViewBookingDetails();
                        break;
                    case 4:
                        HandleComplaint();
                        break;
                    case 5:
                        loggedInCustomer = null;
                        Console.WriteLine("Logged out successfully.");
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        if(loggedInCustomer == null)
        {
            Console.WriteLine();
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxx");
            Console.WriteLine("Invalid Credientials");
            Console.WriteLine("xxxxxxxxxxxxxxxxxxxx");
        }
        static void HandleComplaint()
        {
            Console.Write("Enter your complaint or query: ");
            //string complaint = Console.ReadLine();

           
            Client client = new Client();

            //client.SendComplaint(complaint);

        }

        static void ViewBookingDetails()
        {
            if (bookings == null)
            {
                bookings = new List<Booking>();
            }
            List<Booking> customerBookings = bookings.Where(b => b.Customer.Id == loggedInCustomer.Id).ToList();

            if (customerBookings.Count > 0)
            {
                Console.WriteLine("Booking Details:");
                foreach (var booking in customerBookings)
                {
                    Console.WriteLine();
                    Console.WriteLine("{");
                    Console.WriteLine($"Booking ID: {booking.Id}");
                    Console.WriteLine($"Vehicle Type: {booking.VehicleType}");
                    Console.WriteLine($"Current Destination: {booking.CurrentDestination}");
                    Console.WriteLine($"Arrival Destination: {booking.ArrivalDestination}");
                    Console.WriteLine($"Number of Passengers: {booking.NumberOfPassengers}");
                    Console.WriteLine($"Departure Date and Time: {booking.DepartureDateTime}");
                    Console.WriteLine($"Estimated Fare: Rs. {booking.EstimatedFare}");
                    Console.WriteLine("}");
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine("No bookings yet.");
            }
        }

        static void BookVehicle()
        {
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Vehicle Types:");
            Console.WriteLine("1. Car");
            Console.WriteLine("2. Truck");
            Console.WriteLine("3. Bike");
            Console.WriteLine("4. Aeroplane");
            Console.WriteLine("5. Train");
            Console.WriteLine("6. Bus");
            Console.WriteLine("----------------------------------------------");

            int vehicleTypeChoice = GetIntInput("Enter the number corresponding to the vehicle type: ");
            string vehicleType = GetVehicleTypeFromChoice(vehicleTypeChoice);

            Console.Write("Enter current location: ");
            string currentDestination = Console.ReadLine();
            Console.Write("Enter arrival location: ");
            string arrivalDestination = Console.ReadLine();
            int numberOfPassengers = GetIntInput("Enter the number of passengers: ");
            DateTime departureDateTime = GetDateTimeInput("Enter departure date and time (yyyy-MM-dd): ");
            IVehicles Type = VehiclesFactory.getVahicles(vehicleTypeChoice);
            //Vehicle assignedVehicle = Type.AssignVehicle();
            Console.WriteLine("The Avaliable Vehicle at you time slot are : ");
            DateTime obj ;
            
            Vehicle assignedVehicle = null;
            if (assignedVehicle == null)
            {
                

                 assignedVehicle = Type.ViewVehicleDetailsByDate(departureDateTime);
            }
            decimal estimatedFare = GenerateRandomFare( vehicleTypeChoice);
            Console.WriteLine($"Estimated Fare per Passenger: Rs. {estimatedFare}");
            Console.WriteLine($" Total Estimated Fare : Rs. {estimatedFare * numberOfPassengers}");


            Console.Write("Do you want to confirm the booking? (yes/no): ");
            string confirmBooking = Console.ReadLine();
            obj = Type.time;
            Console.WriteLine("obj = " + obj);

            if (confirmBooking.ToLower() == "yes")
            {
                Booking newBooking = new Booking
                {
                    Id = Guid.NewGuid().ToString(),
                    Customer = loggedInCustomer,
                    VehicleType = vehicleType,
                    CurrentDestination = currentDestination,
                    ArrivalDestination = arrivalDestination,
                    NumberOfPassengers = numberOfPassengers,
                    DepartureDateTime = obj,
                    EstimatedFare = estimatedFare
                };
                if (bookings == null)
                {
                    bookings = new List<Booking>();
                }

                bookings.Add(newBooking);

                //Vehicle assignedVehicle = AssignVehicle(vehicleType);
                //IVehicles Type = VehiclesFactory.getVahicles(vehicleTypeChoice);
                //Vehicle assignedVehicle = Type.AssignVehicle();
                if (assignedVehicle != null)
                {
                    assignedVehicle.Status = "Assigned";
                    Console.WriteLine("Booking confirmed successfully. Here is your booking slip:");
                    Console.WriteLine("***********************************************************");
                    Console.WriteLine($"Booking ID: {newBooking.Id}");
                    Console.WriteLine($"Vehicle Type: {assignedVehicle.Type}");
                    Console.WriteLine($"Model: {assignedVehicle.Model}");
                    Console.WriteLine($"Number Plate: {assignedVehicle.NumberPlate}");
                    Console.WriteLine($"Driver Name: {assignedVehicle.DriverName}");
                    Console.WriteLine($"Status: {assignedVehicle.Status}");
                    Console.WriteLine("***********************************************************");

                }
                else
                {
                    Console.WriteLine("No available vehicles for the selected type. Booking failed.");
                }
            }
            else
            {
                Console.WriteLine("Booking canceled.");
            }
        }

        static void ResetPassword()
        {
            Console.Write("Enter a new password: ");
            string newPassword = Console.ReadLine();
            loggedInCustomer.Password = newPassword;
            Console.WriteLine("Password reset successfully.");
            SaveData(); // Save changes to the customers.json file
        }

        static void ResetPasswordByVerification()
        {
            Console.Write("Enter your name: ");
            string name = Console.ReadLine();
            Console.Write("Enter your mobile number: ");
            string mobileNumber = Console.ReadLine();

            Customer customerToReset = customers.Find(c => c.Name == name && c.MobileNumber == mobileNumber);

            if (customerToReset != null)
            {
                Console.Write("Enter a new password: ");
                string newPassword = Console.ReadLine();
                customerToReset.Password = newPassword;
                Console.WriteLine("Password reset successfully. You can now log in with your new password.");
                SaveData(); // Save changes to the customers.json file
            }
            else
            {
                Console.WriteLine("Verification failed. Password reset aborted.");
            }
        }

        static decimal GenerateRandomFare(int type)
        {
            Random rand = new Random();
            if (type == 1)
            {
                
                return rand.Next(1000, 3000);

            }
            else if(type == 2)
            {
                return rand.Next(5000, 8000);

            }
            else if (type == 3)
            {
                return rand.Next(200, 1000);

            }
            else if (type == 4)
            {
                return rand.Next(15000, 40000);

            }
            else if (type == 5)
            {
                return rand.Next(3000, 5000);

            }
            else if (type == 6)
            {
                return rand.Next(1000, 3500);

            }
            
            return 0;
        }

        //static Vehicle AssignVehicle(string vehicleType)
        //{
        //    if (vehicles == null)
        //    {
        //        Console.WriteLine("vehicle obj created");
        //        vehicles = new List<Vehicle>();
        //    }
        //    Vehicle availableVehicle = vehicles.Find(v => v.Type == vehicleType && v.Status == "Available");

        //    if (availableVehicle != null)
        //    {
        //        availableVehicle.Status = "Not Available";
        //        return availableVehicle;
        //    }

        //    return null;
        //}

        static string GetVehicleTypeFromChoice(int choice)
        {
            switch (choice)
            {
                case 1:
                    return "Car";
                case 2:
                    return "Truck";
                case 3:
                    return "Bike";
                case 4:
                    return "Aeroplane";
                case 5:
                    return "Train";
                default:
                    return "";
            }
        }

        

        static DateTime GetDateTimeInput(string prompt)
        {
            DateTime input;
            Console.Write(prompt);
            while (!DateTime.TryParse(Console.ReadLine(), out input))
            {
                Console.Write("Invalid input. Please enter a valid date and time: ");
            }
            return input;
        }

        

        
    }
}

