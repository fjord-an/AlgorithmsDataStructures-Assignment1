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
        // using a builder object avoids using too many method overloaders and simplifies 
        // the initialization of complex objects aswell as providing more maintainable and extensible 
        // code for the future. this came at the expense of writing more code with new implementations.
        // using builders also ensures that the objects properties are immutable 
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

    public double SetHealth(double multiplier)
    {
        // Attribute logic is encapsulated in the CharacterAttributes object.
        // all changes to characters health must be performed using this function
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
        if (healthChange < 0)
            Console.Write($"Inflicting {-1 * (int)healthChange} Damage to ");

        // return the damage so the details can be displayed on the UI and in the character class
        return healthChange;
    }

    public void Heal(double heal) => Health += heal;
    public void GainGold(double gold) => Gold += gold;

    public void LevelUp(int level = 1)
    {
        // TODO unfortunately, leveling system has not currently been fully implemented
        Level += level;
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
