using System;


public interface IVehicles

	{
        string Id { get; set; }
        string Type { get; set; }
        string Model { get; set; }
        string NumberPlate { get; set; }
        string DriverName { get; set; }
        string Status { get; set; }
        DateTime DepartureDateTime { get; set; }
        int TotalSeats { get; set; }
        int AvailableSeats { get; set; }

        int GetAvailableCount();
        void ViewVehicleDetailsByType();
        Vehicle AssignVehicle();




}



	


