namespace ADS_A1.objects.attributes.Interfaces;

public interface ICharacterAttributes
{
    int Health { get; }
    int Attack { get; }
    int Defense { get; }
    int Speed { get; }
    public int Level { get; }
    public int Experience { get; }
    public int ExperienceToNextLevel { get; }
    public int Gold { get; }
}