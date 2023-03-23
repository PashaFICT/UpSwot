using UpSwot.Core.Models;

namespace UpSwot.Core;

public class PersonManager : IPersonManager
{
    private const string _character = "character";
    private const string _location = "location";
    private const string _episod = "episode";
    private readonly IRickAndMortyClient _rickAndMortyClient;

    public PersonManager(IRickAndMortyClient rickAndMortyClient)
    {
        _rickAndMortyClient = rickAndMortyClient;
    }
    public async Task<List<PersonDto>> FindPersonAsync(string personName)
    {
        var characters = await FindAllAsync<ResultCharacter>(personName, _character);
        var uniqLocationName = characters.Select(p => p.Location.Name)
            .Distinct()
            .Select(p => _rickAndMortyClient.GetDataAsync<LocationDto>(p, _location));

        var data = await Task.WhenAll(uniqLocationName);
        var locationHash = data
            .Select(p => p.Results?.FirstOrDefault())
            .Where(p => p != null)
            .ToDictionary(p => p.Name, v => v);
        return Convert(characters, locationHash);
    }

    public async Task<bool?> CheckPersonEpisodAsync(string episodeName, string characterName)
    {
        var character = await FindCharacterAsync(characterName, p => p.Name == characterName);
        if (character == null)
            return null;

        var episods = await FindAllAsync<ResultEpisod>(episodeName, _episod);
        if (episods == null)
            return null;
        return episods.Any(e => e.Characters.Any(p => p.EndsWith($"/{character.Id}")));
    }

    private List<PersonDto> Convert(List<ResultCharacter> resultCharacters, Dictionary<string, ResultLocation?> location)
    {
        List<PersonDto> persons = new List<PersonDto>();
        foreach (var p in resultCharacters)
        {
            PersonDto person = new PersonDto();
            person.Type = p.Type;
            person.Name = p.Name;
            person.Gender = p.Gender;
            person.Status = p.Status;
            person.Species = p.Species;
            OriginDto origin = new OriginDto();
            if (p.Location.Name != "unknown")
            {
                origin.Name = location[p.Location.Name].Name;
                origin.Dimension = location[p.Location.Name].Dimension;
                origin.Type = location[p.Location.Name].Type;
            }
            person.Origin = origin;
            persons.Add(person);
        }
        return persons;
    }
    private async Task<List<T>> FindAllAsync<T>(string personName, string method)
    {
        var character = await _rickAndMortyClient.GetDataAsync<RickAndMortyDto<T>>(personName, method);
        if (character.Results == null)
            return null;
        var results = character.Results;
        while (character?.Info.Next != null)
        {
            character = await _rickAndMortyClient.GetNextPageAsync<RickAndMortyDto<T>>(character.Info.Next);
            results.AddRange(character.Results);
        }

        return results;
    }

    private async Task<ResultCharacter> FindCharacterAsync(string personName, Func<ResultCharacter, bool> conditionForFinish)
    {
        var character = await _rickAndMortyClient.GetDataAsync<RickAndMortyDto<ResultCharacter>>(personName, _character);
        if (character.Results == null)
            return null;
        var tempResult = character.Results.FirstOrDefault(conditionForFinish);

        if (tempResult != null)
            return tempResult;

        while (character?.Info.Next != null)
        {
            character = await _rickAndMortyClient.GetNextPageAsync<RickAndMortyDto<ResultCharacter>>(character.Info.Next);
            tempResult = character.Results.FirstOrDefault(conditionForFinish);

            if (tempResult != null)
                return tempResult;
        }

        return null;
    }
}