using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

class Administrator
{
    private static Administrator instance = null;

    public static Administrator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Administrator();
            }
            return instance;
        }
    }

    private Administrator()
    {
        // Constructor
    }

    public void AdministratorLogIn()
    {
        Console.Write("Enter administrator username: ");
        string adminUsername = Console.ReadLine();
        Console.Write("Enter administrator password: ");
        string adminPassword = Console.ReadLine();

        // For simplicity, you can hardcode the administrator credentials
        if (adminUsername == "admin" && adminPassword == "adminpass")
        {
            Console.WriteLine();
            Console.WriteLine("Welcome Admin");
            AdministratorMenu();
        }
        else
        {
            Console.WriteLine("Invalid administrator credentials.");
        }
    }

    public void AdministratorMenu()
    {
        while (true)
        {
            Console.WriteLine("----------------------------------------------");
            Console.WriteLine("Administrator Menu:");
            Console.WriteLine("1. View Booking Details by Date");
            Console.WriteLine("2. View Booking Details by ID");
            Console.WriteLine("3. View Vehicle Details by Type");
            Console.WriteLine("4. View Customer Details");
            Console.WriteLine("5. Exit");
            Console.WriteLine("----------------------------------------------");
            int choice = GetIntInput("Enter your choice: ");

            switch (choice)
            {
                case 1:
                    Console.WriteLine();
                    DateTime dateToReview = GetDateTimeInput("Enter the date to review (yyyy-MM-dd): ");
                    ViewBookingDetailsByDate(dateToReview);
                    break;
                case 2:
                    Console.WriteLine();
                    string bookingIdToReview = GetStringInput("Enter the booking ID to review: ");
                    ViewBookingDetailsById(bookingIdToReview);
                    break;
                case 3:
                    Console.WriteLine();
                    string vehicleTypeToReview = GetStringInput("Enter the vehicle type to review: ");
                    ViewVehicleDetailsByType(vehicleTypeToReview);
                    break;
                case 4:
                    Console.WriteLine();
                    string searchCriteria = GetStringInput("Enter the name, ID, or username to search for: ");
                    ViewCustomerDetails(searchCriteria);
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    private void ViewBookingDetailsByDate(DateTime date)
    {
        // Read existing bookings from the file
        List<Booking> allBookings = LoadBookings();

        // Filter bookings based on the provided date
        List<Booking> bookingsOnDate = allBookings
            .Where(booking => booking.DepartureDateTime.Date == date.Date)
            .ToList();

        if (bookingsOnDate.Count > 0)
        {
            Console.WriteLine($"Bookings on {date.ToShortDateString()}:");
            foreach (var booking in bookingsOnDate)
            {
                Console.WriteLine($"Booking ID: {booking.Id}");
                Console.WriteLine($"Customer: {booking.Customer.Name}");
                Console.WriteLine($"Vehicle Type: {booking.VehicleType}");
                Console.WriteLine($"Current Destination: {booking.CurrentDestination}");
                Console.WriteLine($"Arrival Destination: {booking.ArrivalDestination}");
                Console.WriteLine($"Number of Passengers: {booking.NumberOfPassengers}");
                Console.WriteLine($"Departure Date and Time: {booking.DepartureDateTime}");
                Console.WriteLine($"Estimated Fare: Rs. {booking.EstimatedFare}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine($"No bookings on {date.ToShortDateString()}.");
        }
    }

    private List<Booking> LoadBookings()
    {
        string bookingFilePath = Path.Combine(GetProjectDirectory(), "database", "bookings.json");
        if (File.Exists(bookingFilePath))
        {
            string bookingJson = File.ReadAllText(bookingFilePath);
            return JsonConvert.DeserializeObject<List<Booking>>(bookingJson);
        }

        return new List<Booking>();
        //string bookingFilePath = "/Users/ATIFHANIF/Projects/trans/bookings.json";

        //if (File.Exists(bookingFilePath))
        //{
        //    string bookingJson = File.ReadAllText(bookingFilePath);
        //    return JsonConvert.DeserializeObject<List<Booking>>(bookingJson);
        //}

        //return new List<Booking>();
    }


    private void ViewBookingDetailsById(string bookingId)
    {
        // Read existing bookings from the file
        List<Booking> allBookings = LoadBookings();

        // Find the booking with the provided ID
        Booking booking = allBookings.FirstOrDefault(b => b.Id == bookingId);

        if (booking != null)
        {
            Console.WriteLine("Booking Details:");
            Console.WriteLine($"Booking ID: {booking.Id}");
            Console.WriteLine($"Customer: {booking.Customer.Name}");
            Console.WriteLine($"Vehicle Type: {booking.VehicleType}");
            Console.WriteLine($"Current Destination: {booking.CurrentDestination}");
            Console.WriteLine($"Arrival Destination: {booking.ArrivalDestination}");
            Console.WriteLine($"Number of Passengers: {booking.NumberOfPassengers}");
            Console.WriteLine($"Departure Date and Time: {booking.DepartureDateTime}");
            Console.WriteLine($"Estimated Fare: Rs. {booking.EstimatedFare}");
            Console.WriteLine();
        }
        else
        {
            Console.WriteLine($"Booking with ID '{bookingId}' not found.");
        }
    }

    private void ViewVehicleDetailsByType(string vehicleType)
    {
        // Read existing vehicles from the file
        List<Vehicle> allVehicles = LoadVehicles();

        // Filter vehicles by the provided type
        List<Vehicle> matchingVehicles = allVehicles.Where(v => v.Type == vehicleType).ToList();

        if (matchingVehicles.Count > 0)
        {
            Console.WriteLine($"Vehicle Details for Type '{vehicleType}':");

            foreach (var vehicle in matchingVehicles)
            {
                Console.WriteLine();
                Console.WriteLine($"Vehicle ID: {vehicle.Id}");
                Console.WriteLine($"Type: {vehicle.Type}");
                Console.WriteLine($"Model: {vehicle.Model}");
                Console.WriteLine($"Number Plate: {vehicle.NumberPlate}");
                Console.WriteLine($"Driver Name: {vehicle.DriverName}");
                Console.WriteLine($"Status: {vehicle.Status}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine($"No vehicles found for type '{vehicleType}'.");
        }
    }

    // The LoadVehicles function remains the same
    private List<Vehicle> LoadVehicles()
    {
        //    string currentDirectory = Directory.GetCurrentDirectory();
        //    string filePath = Path.Combine("..", "Buses.json");
        //    // Get the full path by combining with the current directory
        //    string fullPath = Path.Combine(currentDirectory, filePath);
        string vehicleFilePath = Path.Combine(GetProjectDirectory(), "database", "vehicles.json");
        if (File.Exists(vehicleFilePath))
        {
            string vehicleJson = File.ReadAllText(vehicleFilePath);
            return JsonConvert.DeserializeObject<List<Vehicle>>(vehicleJson);
        }

        return new List<Vehicle>();
    }


    private void ViewCustomerDetails(string searchCriteria)
    {
       
        List<Customer> allCustomers = LoadCustomers();

        
        List<Customer> matchingCustomers = allCustomers
            .Where(c => c.Name.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                        c.Id.Equals(searchCriteria, StringComparison.OrdinalIgnoreCase) ||
                        c.Username.Contains(searchCriteria, StringComparison.OrdinalIgnoreCase))
            .ToList();

        if (matchingCustomers.Count > 0)
        {
            Console.WriteLine($"Customer Details for Search Criteria '{searchCriteria}':");

            foreach (var customer in matchingCustomers)
            {
                Console.WriteLine();
                Console.WriteLine($"Customer ID: {customer.Id}");
                Console.WriteLine($"Name: {customer.Name}");
                Console.WriteLine($"Age: {customer.Age}");
                Console.WriteLine($"Mobile Number: {customer.MobileNumber}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine($"Username: {customer.Username}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine($"No customers found for search criteria '{searchCriteria}'.");
        }
    }

 
    private List<Customer> LoadCustomers()
    {
        string customerFilePath = Path.Combine(GetProjectDirectory(), "database", "customers.json");
        if (File.Exists(customerFilePath))
        {
            string customerJson = File.ReadAllText(customerFilePath);
            return JsonConvert.DeserializeObject<List<Customer>>(customerJson);
        }

        return new List<Customer>();
    }



    private int GetIntInput(string prompt)
    {
        int input;
        Console.Write(prompt);
        while (!int.TryParse(Console.ReadLine(), out input))
        {
            Console.Write("Invalid input. Please enter a valid number: ");
        }
        return input;
    }

    private string GetStringInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }

    private DateTime GetDateTimeInput(string prompt)
    {
        DateTime input;
        Console.Write(prompt);
        while (!DateTime.TryParse(Console.ReadLine(), out input))
        {
            Console.Write("Invalid input. Please enter a valid date and time: ");
        }
        return input;
    }
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
}
