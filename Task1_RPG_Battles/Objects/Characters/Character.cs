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
    public virtual ICharacterAttributes Attribute { get; }
    
    public double Level { get; set; }
    public bool Alive { get; set; }
    public bool IsPlayer { get; private set; }
    public IZone CurrentZone { get; set; }
    
    public double Health => Attribute.Health;

    // using generic action delegate of the .NET framework so that the player can execute generic actions 
    // in the game, which can be passed by the object itself, so that many different objects can easily be added => extensible
    // This will result in a composition relationship between the character class and World objects.
    // this will be polymorphic, allowing Interface objects to be passed to the character class 
    public virtual Action<IInteractiveWorldObject>? Interact { get; set; }

    public Character(string name, ICharacterAttributes attribute, IZone zone,  bool isPlayer=false)
    {
        // Using method to change health, so that the health is encapsulated
        // in the attribute class and limits modification to the health except
        // through the attribute class with its own logic
        Name = name;
        Attribute = attribute;
        Level = 1;
        IsPlayer = isPlayer;
        // the character and zone is pointing to each other, ensuring that the character is in the correct zone
        // and allowing easier access to the zone the character is in when traversing the game world through teleportation
        // making a reference to the previous/next zone the character was in, irrespective of the current zone of the world linked list
        CurrentZone = zone;
        // add character to the zone when it is created, ensuring consistency in the game world
        zone?.ZoneCharacters.AddCharacter(this);
        SetAttributeName(Name);
        IsAlive();
    }
    
    
    // I have seperated the logic from character attributes to the character class
    // All stats are encapsulated there, the character class will access each instances
    // unique attributes

    private void SetAttributeName(string name)
    {
        if(Attribute != null)
            Attribute.Name = name;
    }

    // check if the character is alive by checking if the health is above 0 in the character attributes
    public bool IsAlive()
    {
        if (Attribute != null)
        {
            if (Attribute.IsAlive)
            {
                Alive = true;
                return true;
            }
            
            Alive = false;
            return false;
        }

        return false;
    }

    public void TakeDamage(int damage) => Attribute.SetHealth((-1 * damage));

    public void SetHealth(double multiplier)
    {
        Attribute.SetHealth(multiplier);
        // print the Name of the character inflicted with damage
        
        Console.Write($"| {Name} | HP: {Health}/{Attribute.MaxHealth} | ");
        Console.WriteLine();
        
        // Console colour must be reset here as it is the end of the rounds line
        Console.ResetColor();
    }
    
    public void FindCurrentZone(World world)
    {
        CurrentZone = world.FindPlayersCurrentZone(this);
    }
    
    // currently, damage multipliers are calculated by multiplying the damage
    // by -1, so that the damage is subtracted from the health of the target
    public void BasicAttack(ICharacter target)
    {
        Console.Write(" used Basic Attack, ");
        // the attack will be based on the level of the character and a random number. the formula used here helps normalise the damage to prevent high level characters from dealing too much damage 
        target.SetHealth(-1 * new Random().NextDouble() * (Math.Sqrt(200 * Level) - Math.Sqrt(2 * (Level + Attribute.Attack)) + Math.Sqrt(2 * (Level + Attribute.Attack))));
    }
    
    public virtual void DoAction(ICharacter target)
    {
        // the attack will be based on the level of the character and a random number. the formula used here helps normalise the damage to prevent high level characters from dealing too much damage 
        BasicAttack(target);
    }
}
