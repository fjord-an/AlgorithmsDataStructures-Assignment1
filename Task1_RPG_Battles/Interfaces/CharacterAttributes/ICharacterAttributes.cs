namespace ADS_A1.Interfaces.CharacterAttributes;

public interface ICharacterAttributes
{
    // No need to implement a setter because we are using a builder function to do this
    string Name { get; set; }
    double Health { get; }
    
    double MaxHealth { get; }
    double Attack { get; set; }
    double Defense { get; }
    double Speed { get; }
    public double Level { get; }
    public double Experience { get; }
    public double ExperienceToNextLevel { get; }
    public double Gold { get; }
    public bool IsAlive { get; }

    public void LevelUp(int level);

    public void GainExperience(double experience);

    public void GainGold(double gold);

    public double SetHealth(double damage);
}