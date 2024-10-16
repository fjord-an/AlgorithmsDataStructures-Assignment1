using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.Interfaces.Characters;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Attributes;
using ADS_A1.Objects.WorldObjects;

namespace ADS_A1.objects.Characters;

public class Character : ICharacter
{
    public String Name { get; set; }
    // I have decoupled the characters attributes so that they can
    // be easily extended and tested. Considering stats are a very
    // finicky and subjective to change at all times, only the interface is
    // exposed
    public ICharacterAttributes Attribute { get; set; }
    
    public double Level { get; set; }
    public double Health { get; set; }
    public bool IsAlive { get; set; }
    public bool IsPlayer { get; private set; }
    public IZone CurrentZone { get; set; }
    
    // using generic action delegate of the .NET framework so that the player can execute generic actions 
    // in the game, which can be passed by the object itself, so that many different objects can easily be added => extensible
    // This will result in a composition relationship between the character class and World objects.
    // this will be polymorphic, allowing Interface objects to be passed to the character class 
    public virtual Action<IInteractiveWorldObject>? Interact { get; set; }

    public Character(string name, ICharacterAttributes attribute, bool isPlayer=false)
    {
        Name = name;
        Attribute = attribute;
        Level = 1;
        Health = 100;
        IsPlayer = isPlayer;
    }

    public void UpdateCurrentZone(World world)
    {
        CurrentZone = world.FindPlayersCurrentZone(this);
    }
    
    public void BasicAttack(Character target)
    {
        // the attack will be based on the level of the character and a random number. the formula used here helps normalise the damage to prevent high level characters from dealing too much damage 
        target.Health -= new Random().NextDouble() * (Math.Sqrt(10 * Level) - Math.Sqrt(2 * Level)) + Math.Sqrt(2 * Level);
    }

}