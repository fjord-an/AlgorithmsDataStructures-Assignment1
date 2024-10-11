using ADS_A1.objects.Characters;

public class CharacterContainer
{
    private List<Character> _characters;

    public CharacterContainer()
    {
        _characters = new List<Character>();
    }

    public void AddCharacter(Character character)
    {
        _characters.Add(character);
    }

    public void RemoveCharacter(Character character)
    {
        _characters.Remove(character);
    }

    public IEnumerable<Character> GetCharacters()
    {
        return _characters;
    }
}
