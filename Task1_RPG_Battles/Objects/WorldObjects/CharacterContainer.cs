using ADS_A1.Interfaces.Characters;
using ADS_A1.objects.Characters;

public class CharacterContainer
{
    private List<ICharacter> _characters { get; } = new();

    public void AddCharacter(ICharacter character)
    {
        if(!_characters.Contains(character))
            _characters.Add(character);
    }

    public void RemoveCharacter(ICharacter character)
    {
        _characters.Remove(character);
    }

    public IEnumerable<ICharacter> GetCharacters()
    // IEnumerable is used to return a sequence of characters
    // This is useful for iterating over the characters in the container
    // and performing LINQ queries on the collection of characters
    // Hamedani, M. (Director). (2015). LINQ - C# Advanced Course [Video recording].
    {
        return _characters;
    }
}
