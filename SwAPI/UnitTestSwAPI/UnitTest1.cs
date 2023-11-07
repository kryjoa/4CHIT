using SwAPI;

namespace UnitTest1
{
    [TestFixture]
    public class SwEndpointTests
    {
        [SetUp]
        public void Setup()
        {
            SwEndpoints.CharList.Clear(); // Leert die CharList vor jedem Test
            SwEndpoints.AddToCharacters(); // Fügt Standard-Charaktere hinzu
        }
        [Test]
        public void Test_AddNewCharacter()
        {
            var initialCount = SwEndpoints.CharList.Count;
            var newCharacter = new Character
            {
                ID = 4,
                Name = "Luke Skywalker",
                Faction = "Rebel Alliance",
                Homeworld = "Tatooine",
                Species = "Human"
            };
            
            SwEndpoints.CharList.Add(newCharacter);
            
            Assert.AreEqual(initialCount + 1, SwEndpoints.CharList.Count);
            Assert.Contains(newCharacter, SwEndpoints.CharList);
        }

        [Test]
        public void Test_GetCharacterById()
        {
            var expectedCharacter = SwEndpoints.CharList.FirstOrDefault();
            
            var retrievedCharacter = SwEndpoints.CharList.FirstOrDefault(c => c.ID == expectedCharacter.ID);
            
            Assert.IsNotNull(retrievedCharacter);
            Assert.AreEqual(expectedCharacter, retrievedCharacter);
        }

        [Test]
        public void Test_UpdateCharacter()
        {
            var characterToUpdate = SwEndpoints.CharList.FirstOrDefault();
            var updatedCharacter = new Character
            {
                ID = characterToUpdate.ID,
                Name = "Updated Name",
                Faction = "Updated Faction",
                Homeworld = "Updated Homeworld",
                Species = "Updated Species"
            };
            
            var result = SwEndpoints.CharList.FirstOrDefault(c => c.ID == characterToUpdate.ID);
            result.Name = updatedCharacter.Name;
            result.Faction = updatedCharacter.Faction;
            result.Homeworld = updatedCharacter.Homeworld;
            result.Species = updatedCharacter.Species;
            
            Assert.AreEqual(updatedCharacter.Name, result.Name);
            Assert.AreEqual(updatedCharacter.Faction, result.Faction);
            Assert.AreEqual(updatedCharacter.Homeworld, result.Homeworld);
            Assert.AreEqual(updatedCharacter.Species, result.Species);
        }

        [Test]
        public void Test_DeleteCharacter()
        {
            var characterToDelete = SwEndpoints.CharList.FirstOrDefault();
            var initialCount = SwEndpoints.CharList.Count;
            
            var result = SwEndpoints.CharList.FirstOrDefault(c => c.ID == characterToDelete.ID);
            SwEndpoints.CharList.Remove(result);
            
            Assert.IsFalse(SwEndpoints.CharList.Contains(characterToDelete));
            Assert.AreEqual(initialCount - 1, SwEndpoints.CharList.Count);
        }
        
    }
}