using SwAPI.Classes;

namespace SwAPI.Interface;

public interface ICharRepos
{
    List<Character> GetAll();
    Character GetByID(int ID);     
    void Post(Character character);        
    bool Update(int ID, Character character);
}