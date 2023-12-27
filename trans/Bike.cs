using System;
using Newtonsoft.Json;

public class Bike : IVehicles
{
    public string Id { get; set; }
    public string Type { get; set; }
    public string Model { get; set; }
    public string NumberPlate { get; set; }
    public string DriverName { get; set; }
    public string Status { get; set; }
    public DateTime DepartureDateTime { get; set; }
    public int TotalSeats { get; set; }
    public int AvailableSeats { get; set; }
    public DateTime time { get; set; }

    /* public void PrintDetails()
     {
         Console.WriteLine($"Type: {Type}, Model: {Model}, Number Plate: {NumberPlate}, Driver: {DriverName}, Status: {Status}");
     }

     public void Assign(string customerId)
     {
         // Implement the logic for assigning a car to a customer
         Console.WriteLine($"Car assigned to Customer {customerId}");
         Status = "Not Available";
     }*/

    public int GetAvailableCount()
    {
        // Implement the logic to get the count of available cars
        List<Vehicle> allVehicles = LoadVehicles();
        return allVehicles.Count(v => v.Type == "Bike" && v.Status == "Available");
    }
    public Vehicle ViewVehicleDetailsByDate(DateTime date)
    {
        Vehicle availableVehicle = null;
        DateTime obj;
        // Read existing vehicles from the file
        List<Vehicle> allVehicles = LoadVehicles();

        // Filter vehicles based on the provided date
        List<Vehicle> vehiclesOnDate = allVehicles
            .Where(vehicle => vehicle.DepartureDateTime.Date == date.Date && vehicle.Type == "Aeroplane" && vehicle.Status == "Available")
            .ToList();

        if (vehiclesOnDate.Count > 0)
        {
            Console.WriteLine($"Vehicles on {date.ToShortDateString()}:");
            Dictionary<int, DateTime> myDic = new Dictionary<int, DateTime>();
            int i = 1;
            foreach (var vehicle in vehiclesOnDate)
            {

                Console.WriteLine($"Departure Date and Time: {vehicle.DepartureDateTime}");
                Console.WriteLine("-----------------");
                myDic.Add(i, vehicle.DepartureDateTime);
                i++;

            }
            Console.WriteLine("Enter Slot Number : ");
            string strNumber = Console.ReadLine();
            int intValue = int.Parse(strNumber);

            if (myDic.ContainsKey(intValue))
            {
                obj = myDic[intValue];
                Console.WriteLine("time = " + obj.TimeOfDay);

                availableVehicle = AssignVehicle(obj);

            }

            //return obj;


        }
        return availableVehicle;


    }
    public void ViewVehicleDetailsByType()
    {
        // Read existing vehicles from the file
        List<Vehicle> allVehicles = LoadVehicles();

        // Filter vehicles by the provided type
        List<Vehicle> matchingVehicles = allVehicles.Where(v => v.Type == "bike").ToList();

        if (matchingVehicles.Count > 0)
        {
            Console.WriteLine($"Vehicle Details for Bike:");

            foreach (var vehicle in matchingVehicles)
            {
                Console.WriteLine($"Vehicle ID: {vehicle.Id}");
                Console.WriteLine($"Type: {vehicle.Type}");
                Console.WriteLine($"Model: {vehicle.Model}");
                Console.WriteLine($"Number Plate: {vehicle.NumberPlate}");
                Console.WriteLine($"Driver Name: {vehicle.DriverName}");
                Console.WriteLine($"Status: {vehicle.Status}");
                Console.WriteLine($"Departure DateTime: {vehicle.DepartureDateTime}");
                Console.WriteLine($"Total Seats: {vehicle.TotalSeats}");
                Console.WriteLine($"Available Seats: {vehicle.AvailableSeats}");
                Console.WriteLine();
            }
        }
        else
        {
            Console.WriteLine($"No vehicles found for Bike.");
        }
    }


    private List<Vehicle> LoadVehicles()
    {
        string vehicleFilePath = Path.Combine(GetProjectDirectory(), "database", "Bikes.json");
        if (File.Exists(vehicleFilePath))
        {
            string vehicleJson = File.ReadAllText(vehicleFilePath);
            return JsonConvert.DeserializeObject<List<Vehicle>>(vehicleJson);
        }

        return new List<Vehicle>();
    }
    


    public Vehicle AssignVehicle(DateTime obj)
    {
        List<Vehicle> allVehicles = LoadVehicles();
        Vehicle availableVehicle = allVehicles.Find(v => v.Type == "Bike" && v.Status == "Available" && v.DepartureDateTime.TimeOfDay == obj.TimeOfDay);
        //Vehicle availableVehicle = vehicles.Find(v => v.Type == vehicleType && v.Status == "Available");

        if (availableVehicle != null)
        {
            //availableVehicle.Status = "Assigned";
            return availableVehicle;
        }

        return null;
    }
    static string GetProjectDirectory()
    {
        string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        return RemoveBinDebugFromPath(baseDirectory);
    }

    // Helper method to remove "bin/Debug/net7.0" from the path
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


