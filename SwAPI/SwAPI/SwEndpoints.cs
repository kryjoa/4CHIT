namespace SwAPI;

public class Character
{
    public int ID { get; set; }
    public string Name { get; set; }
    public string Faction { get; set; }
    public string Homeworld { get; set; }
    public string Species { get; set; }
}

public class SwEndpoints
{
    public static readonly List<Character> CharList = new();
    private static int nextID;

    public static void AddToCharacters()
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

        CharList.Add(char1);
        CharList.Add(char2);
        CharList.Add(char3);

        nextID = CharList.Max(x => x.ID) + 1;
    }

    private SwEndpoints()
    {
        AddToCharacters();
    }

    public static void Map(WebApplication app)
    {
        var swEndpoints = new SwEndpoints();

        app.MapGet("/sw-characters/{id}", (int id) => CharList.FirstOrDefault(c => c.ID == id));
        
        app.MapGet("/sw-characters", (string? faction, string? homeworld, string? species) =>
        {
            IEnumerable<Character> filteredCharacters = CharList;

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

        app.MapPost("/sw-characters", (Character newChar) =>
        {
            newChar.ID = nextID++;
            CharList.Add(newChar);
            return Results.Created($"/sw-characters/{newChar.ID}", newChar);
        });

        app.MapPut("/sw-characters/{ID:int}", async (int ID, HttpRequest request) =>
        {
            var character = await request.ReadFromJsonAsync<Character>();

            if (character == null)
                return Results.BadRequest("Invalid character data");

            var existingCharacter = CharList.FirstOrDefault(c => c.ID == ID);
            if (existingCharacter != null)
            {
                existingCharacter.Name = character.Name;
                existingCharacter.Faction = character.Faction;
                existingCharacter.Homeworld = character.Homeworld;
                existingCharacter.Species = character.Species;
                return Results.Ok(existingCharacter);
            }

            return Results.NotFound();
        });

        app.MapDelete("/sw-characters/{ID:int}", (int ID) =>
        {
            var character = CharList.FirstOrDefault(c => c.ID == ID);
            if (character != null)
            {
                CharList.Remove(character);
                return Results.NoContent();
            }
            return Results.NotFound();
        });
    }
}
