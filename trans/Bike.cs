using System;

    public class Bike : Vehicle
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Model { get; set; }
        public string NumberPlate { get; set; }
        public string DriverName { get; set; }
        public string Status { get; set; }

        public void PrintDetails()
        {
            Console.WriteLine($"Type: {Type}, Model: {Model}, Number Plate: {NumberPlate}, Driver: {DriverName}, Status: {Status}");
        }

        public void Assign(string customerId)
        {
            // Implement the logic for assigning a bike to a customer
            Console.WriteLine($"Bike assigned to Customer {customerId}");
            Status = "Not Available";
        }

        public int GetAvailableCount(List<Vehicle> allVehicles)
        {
            // Implement the logic to get the count of available bikes
            return allVehicles.Count(v => v.Type == "Bike" && v.Status == "Available");
        }
    }


