using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Characters;

namespace ADS_A1.Interfaces.Characters;

public interface ICharacter
{
    public String Name { get; set; }
    public ICharacterAttributes Attribute { get; set; }
    public double Level { get; set; }
    public double Health { get; set; }
    public bool IsAlive { get; set; }
    public bool IsPlayer { get; }
    public IZone CurrentZone { get; set; }
    public Action<IInteractiveWorldObject>? Interact { get; set; }
    
    public void BasicAttack(Character target)
    {
        // the attack will be based on the level of the character and a random number. the formula used here helps normalise the damage to prevent high level characters from dealing too much damage 
        target.Health -= new Random().NextDouble() * (Math.Sqrt(10 * Level) - Math.Sqrt(2 * Level)) + Math.Sqrt(2 * Level);
    }
    
}