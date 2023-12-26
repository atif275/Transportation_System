public class Booking
{
    public string Id { get; set; }
    public Customer Customer { get; set; }
    public string VehicleType { get; set; }
    public string CurrentDestination { get; set; }
    public string ArrivalDestination { get; set; }
    public int NumberOfPassengers { get; set; }
    public DateTime DepartureDateTime { get; set; }
    public decimal EstimatedFare { get; set; }
}
