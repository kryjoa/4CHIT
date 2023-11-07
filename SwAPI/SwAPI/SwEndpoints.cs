using SwAPI.Classes;
using SwAPI.Interface;

namespace SwAPI;

public class SwEndpoints
{
    private readonly ICharacterRepository _characterRepository;
    public static readonly List<Character> CharacterList = new();
    public static int NextId;

    private static void AddToCharacters()
    {
        var char1 = new Character()
        {
            ID = 1,
            Name = "Luke Skywalker",
            Faction = "Rebel Alliance",
            Homeworld = "Tatooine",
            Species = "Human"
        };
        var char2 = new Character()
        {
            ID = 2,
            Name = "Darth Vader",
            Faction = "Galactic Empire",
            Homeworld = "Tatooine",
            Species = "Human"
        };
        var char3 = new Character()
        {
            ID = 3,
            Name = "R2-D2",
            Faction = "Rebel Alliance",
            Homeworld = "Naboo",
            Species = "Droid"
        };

        CharacterList.Add(char1);
        CharacterList.Add(char2);
        CharacterList.Add(char3);

        NextId = CharacterList.Max(x => x.ID) + 1;
    }

    private SwEndpoints(ICharacterRepository characterRepository)
    {
        this._characterRepository = characterRepository;
    }

    public static void Map(WebApplication app)
    {
        AddToCharacters();

        var characterRepository = new CharacterRepository();
        var swEndpoints = new SwEndpoints(characterRepository);

        // GET - all / filters
        app.MapGet("/sw-characters", (string? faction, string? homeworld, string? species) =>
        {
            IEnumerable<Character> filteredCharacters = swEndpoints._characterRepository.GetAll();

            if (!string.IsNullOrWhiteSpace(faction))
            {
                filteredCharacters = filteredCharacters.Where(c => c.Faction == faction);
            }

            if (!string.IsNullOrWhiteSpace(homeworld))
            {
                filteredCharacters = filteredCharacters.Where(c => c.Homeworld == homeworld);
            }

            if (!string.IsNullOrWhiteSpace(species))
            {
                filteredCharacters = filteredCharacters.Where(c => c.Species == species);
            }

            return Results.Ok(filteredCharacters.ToList());
        });

        // POST - add char
        app.MapPost("/sw-characters", (Character newChar) =>
        {
            swEndpoints._characterRepository.Post(newChar);
            return Results.Created($"/sw-characters/{newChar.ID}", newChar);
        });

        // GET - with ID
        app.MapGet("/sw-characters/{ID:int}", (int ID) => Results.Ok(swEndpoints._characterRepository.GetByID(ID)));


        // PUT - update(ID is valid)/add
        app.MapPut("/sw-characters/{ID:int}", async (int ID, HttpRequest request) =>
        {
            // gets character 
            var character = await request.ReadFromJsonAsync<Character>();

            if (character == null)
                return Results.BadRequest("Invalid character data");

            // updating existing character
            if (swEndpoints._characterRepository.Update(ID, character))
                return Results.Ok(character);
            else
            {
                // if does not exist - creates new one
                swEndpoints._characterRepository.Post(character);
                return Results.Created($"/sw-characters/{character.ID}", character);
            }
        });

        // DELETE - deletes existing character
        app.MapDelete("/sw-characters/{ID:int}", (int ID) =>
            {
                var character = characterRepository.GetByID(ID);
                characterRepository.Delete(character.ID);
            }
        );
    }
}

public class CharacterRepository : ICharacterRepository
{
    public List<Character> GetAll() => SwEndpoints.CharacterList;

    public Character GetByID(int ID)
    {
        return SwEndpoints.CharacterList.FirstOrDefault(x => x.ID == ID);
    }

    public void Post(Character character)
    {
        character.ID = SwEndpoints.NextId;
        SwEndpoints.NextId++;
        SwEndpoints.CharacterList.Add(character);
    }

    public void Put(int ID)
    {
        Character ChosenChar = GetByID(ID);
        if (ChosenChar == null)
            Post(ChosenChar);
        throw new Exception("This ID already exists!");
    }

    public bool Update(int ID, Character updatedCharacter)
    {
        Character existingCharacter = GetByID(ID);

        if (existingCharacter != null)
        {
            // Update the existing character with new data
            existingCharacter.Name = updatedCharacter.Name;
            existingCharacter.Faction = updatedCharacter.Faction;
            existingCharacter.Homeworld = updatedCharacter.Homeworld;
            existingCharacter.Species = updatedCharacter.Species;
            return true;
        }

        return false;
    }

    public void Delete(int ID)
    {
        Character existingCharacter = GetByID(ID);

        if (existingCharacter != null)
            SwEndpoints.CharacterList.Remove(existingCharacter);
        else
            throw new Exception("This character does not exist!");
    }
}