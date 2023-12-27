using System;
using Newtonsoft.Json;

public class Aeroplane : IVehicles
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
        return allVehicles.Count(v => v.Type == "Aeroplane" && v.Status == "Available");
    }
    public void ViewVehicleDetailsByType()
    {
        // Read existing vehicles from the file
        List<Vehicle> allVehicles = LoadVehicles();

        // Filter vehicles by the provided type
        List<Vehicle> matchingVehicles = allVehicles.Where(v => v.Type == "Aeroplane").ToList();

        if (matchingVehicles.Count > 0)
        {
            Console.WriteLine($"Vehicle Details for Aeroplane:");

            foreach (var vehicle in matchingVehicles)
            {
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
            Console.WriteLine($"No vehicles found for Aeroplane.");
        }
    }


    private List<Vehicle> LoadVehicles()
    {
        string vehicleFilePath = "/Users/ATIFHANIF/Projects/trans/Aeroplanes.json";

        if (File.Exists(vehicleFilePath))
        {
            string vehicleJson = File.ReadAllText(vehicleFilePath);
            return JsonConvert.DeserializeObject<List<Vehicle>>(vehicleJson);
        }

        return new List<Vehicle>();
    }


    public Vehicle AssignVehicle()
    {
        List<Vehicle> allVehicles = LoadVehicles();
        Vehicle availableVehicle = allVehicles.Find(v => v.Type == "Aeroplane" && v.Status == "Available");
        //Vehicle availableVehicle = vehicles.Find(v => v.Type == vehicleType && v.Status == "Available");

        if (availableVehicle != null)
        {
            availableVehicle.Status = "Not Available";
            return availableVehicle;
        }

        return null;
    }
}


