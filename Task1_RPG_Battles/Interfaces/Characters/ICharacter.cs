using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Characters;
using ADS_A1.Objects.WorldObjects;

namespace ADS_A1.Interfaces.Characters;

public interface ICharacter
{
    public String Name { get; set; }
    public ICharacterAttributes Attribute { get; }
    public double Level { get; set; }
    public bool Alive { get; set; }
    public bool IsPlayer { get; }
    public IZone CurrentZone { get; set; }
    public Action<IInteractiveWorldObject>? Interact { get; set; }
    public double Health => Attribute.Health;
    
    public void DoAction(ICharacter target)
    {
        BasicAttack(target);
    }

    public bool IsAlive() => Attribute.IsAlive;
    public void TakeDamage(int damage);
    public double SetHealth(double multiplier);
    public void FindCurrentZone(World world);
    public void BasicAttack(ICharacter target);

}