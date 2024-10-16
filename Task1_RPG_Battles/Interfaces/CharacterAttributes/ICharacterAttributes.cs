namespace ADS_A1.Interfaces.CharacterAttributes;

public interface ICharacterAttributes
{
    // No need to implement a setter because we are using a builder function to do this
    int Health { get; }
    
    int MaxHealth { get; }
    int Attack { get; }
    int Defense { get; }
    int Speed { get; }
    public int Level { get; }
    public int Experience { get; }
    public int ExperienceToNextLevel { get; }
    public int Gold { get; }
}