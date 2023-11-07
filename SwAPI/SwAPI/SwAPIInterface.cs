using SwAPI.Classes;

namespace SwAPI.Interface;

public interface ICharacterRepository
{
    List<Character> GetAll();
    Character GetByID(int ID);     
    void Post(Character character);        
    void Put(int ID);
    bool Update(int ID, Character character);
    void Delete(int ID);
}