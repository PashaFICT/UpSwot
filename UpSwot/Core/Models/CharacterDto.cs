using UpSwot.Core.Models;

namespace UpSwot.Core.Models
{
    public class RickAndMortyDto<T>
    {
        public Info Info { get; set; }
        public List<T> Results { get; set; }
    }
    public class Info// : Info
    {
        public int Count { get; set; }
        public int Pages { get; set; }
        public string Next { get; set; }
        public string Prev { get; set; }
    }
    public class ResultCharacter// : Result
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Status { get; set; }
        public string Species { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public AdditInfo Origin { get; set; }
        public AdditInfo Location { get; set; }
        public string Image { get; set; }
        public List<string> Episode { get; set; }
        public string Url { get; set; }
        public DateTime Created { get; set; }
    }
    public class AdditInfo
    {
        public string Name { get; set; }
        public string Url { get; set; }
    }
}
