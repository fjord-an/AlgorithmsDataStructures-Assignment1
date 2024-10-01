using ADS_A1.objects.Attributes;
using ADS_A1.objects.attributes.Interfaces;

namespace ADS_A1.objects.Characters;

public class Character
{
    public String Name { get; set; }
    // I have decoupled the characters attributes so that they can
    // be easily extended and tested. Considering stats are a very
    // finicky and subjective to change at all times, only the interface is
    // exposed
    public ICharacterAttributes Attribute { get; set; }
    
    private double Level { get; set; }
    private double Health { get; set; }
    private bool IsAlive { get; set; }
    private bool isPlayer { get; set; }
    
    // using generic action delegate of the .NET framework so that the player can execute generic actions 
    // in the game, which can be passed by the object itself, so that many different objects can easily be added => extensible
    // This will result in a composition relationship between the character class and World objects.
    // this will be polymorphic, allowing Interface objects to be passed to the character class 
    public virtual Action<IInteractiveWorldObject>? Interact { get; set; }


    public Character(string name, ICharacterAttributes attribute)
    {
        Name = name;
        Attribute = attribute;
        Level = 1;
        Health = 100;
    }
    
    public void Attack(Character target)
    {
        // the attack will be based on the level of the character and a random number. the formula used here helps normalise the damage to prevent high level characters from dealing too much damage 
        target.Health -= new Random().NextDouble() * (Math.Sqrt(10 * Level) - Math.Sqrt(2 * Level)) + Math.Sqrt(2 * Level);
    }

}