using UpSwot.Core.Models;

namespace UpSwot.Core.Models
{
    //public class EpisodDto
    //{
    //    public InfoEpisod Info { get; init; }
    //    public List<ResultEpisod> Results { get; init; }
    //}
    //public class InfoEpisod// : Info
    //{
    //    public int Count { get; init; }
    //    public int Pages { get; init; }
    //    public string Next { get; init; }
    //    public string Prev { get; init; }
    //}
    public class ResultEpisod// : Result
    {
        public int Id { get; init; }
        public string Name { get; init; }
        public string Air_date { get; init; }
        public string Episode { get; init; }
        public List<string> Characters { get; init; }
        public string Url { get; init; }
        public DateTime Created { get; init; }
    }
}
