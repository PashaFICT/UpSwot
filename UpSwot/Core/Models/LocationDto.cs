namespace UpSwot.Core.Models
{
    public class LocationDto
    {
        public List<ResultLocation> Results { get; init; }
    }
    public class ResultLocation
    {
        public string Name { get; init; }
        public string Type { get; init; }
        public string Dimension { get; init; }
    }
}
