using ADS_A1.functions;
using ADS_A1.objects.Attributes.builders;
using ADS_A1.objects.attributes.Interfaces;

namespace ADS_A1.objects.Attributes;

// Exposed Interfaces:

// Implementations:
public class CharacterAttributes : ICharacterAttributes
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Speed { get; private set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int ExperienceToNextLevel { get; private set; }
    public int Gold { get; private set; }


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
        Attack = builder.Attack;
        Defense = builder.Defense;
        Speed = builder.Speed;
        Level = builder.Level;
        Experience = builder.Experience;
        ExperienceToNextLevel = builder.ExperienceToNextLevel;
        Gold = builder.Gold;
    } 
    
    public void LevelUp()
    {
        Level++;
        Experience = 0;
        ExperienceToNextLevel *= 2;
    }

    public void GainExperience(int experience)
    {
        Experience += experience;
        if (Experience >= ExperienceToNextLevel)
        {
            LevelUp();
        }
    }

    public void GainGold(int gold)
    {
        Gold += gold;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}