using UpSwot.Core.Models;

namespace UpSwot.Core
{
    public interface IPersonManager
    {
        Task<List<PersonDto>> FindPersonAsync(string personName);
        Task<bool?> CheckPersonEpisodAsync(string episodeName, string characterName);

    }
}
