using System.Collections;
using ADS_A1.functions;
using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes.builders;

namespace ADS_A1.objects.Attributes;

// Exposed Interfaces:

// Implementations:
public class CharacterAttributes : ICharacterAttributes
{
    public string Name { get; set; }
    public double Health { get; private set; }
    public double MaxHealth { get; private set; }
    public double Attack { get; set; }
    public double Defense { get; private set; }
    public double Speed { get; private set; }
    public double Level { get; private set; }
    public double Experience { get; private set; }
    public double ExperienceToNextLevel { get; private set; }
    public double Gold { get; private set; }
    public bool IsAlive => Health > 0;


    public CharacterAttributes(CharacterAttributesBuilder builder)
    {
        // I pass a Builder object to this constructor so that the constructor is more readable
        // and maintainable. utilizing a builder avoids using too many method overloaders
        // aswell as providing more maintainable and extensible code for the future, at the expense
        // of writing slightly more code with new implementations, however interfacing with 
        // these properties is far more flexible using a builder object
        // using builders also ensures that the objects properties are immutable unless explicitely
        // ??????changed through the builder methods???????????
        // to maintain simplicity, i only made a builder for the base CharacterAttributes object
        // TODO add reference of builder object and using them
        Health = builder.Health;
        MaxHealth = builder.MaxHealth;
        Attack = builder.Attack;
        Defense = builder.Defense;
        Speed = builder.Speed;
        Level = builder.Level;
        Experience = builder.Experience;
        ExperienceToNextLevel = builder.ExperienceToNextLevel;
        Gold = builder.Gold;
    }

    public void SetHealth(double multiplier)
    {
        double healthChange = multiplier * Math.Sqrt(Level);
        Health += healthChange;
        if (Health > MaxHealth)
        {
            Health = MaxHealth;
        }
        if (Health <= 0)
        {
            Health = 0;
        }
        if(healthChange < 0)
            Console.Write($"Inflicting {-1 * (int)healthChange} Damage to ");
    }
    public void Heal(double heal) => Health += heal;
    public void GainGold(double gold) => Gold += gold;
    
    public void LevelUp(int level = 1)
    {
        Level+= level;
        Experience = 0;
        ExperienceToNextLevel *= 2;
        Console.WriteLine($"{Name} has leveled up to level {Level}");
    }

    public void GainExperience(double experience)
    {
        Experience += experience;
        if (Experience >= ExperienceToNextLevel)
        {
            LevelUp();
        }
    }
}
