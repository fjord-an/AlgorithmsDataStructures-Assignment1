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
    // IEnumerable is used to return a sequence of characters
    // This is useful for iterating over the characters in the container
    // and performing LINQ queries on the collection of characters
    // Hamedani, M. (Director). (2015). LINQ - C# Advanced Course [Video recording].
    {
        return _characters;
    }
}
