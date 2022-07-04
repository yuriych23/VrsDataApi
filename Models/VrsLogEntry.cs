namespace VrsDataApi.Models;

public class VrsLogEntry
{
    public Guid Id { get; set; }
    public int ImoNumber { get; set; }
    public string VesselName { get; set; }
    public DateTime Date { get; set; }

    public Position Position { get; set; }
}

public class Position
{
    public double Latitude { get; set; }
    public double Longitude { get; set; }
}