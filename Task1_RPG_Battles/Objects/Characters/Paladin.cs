using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.Interfaces.Characters;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;

namespace ADS_A1.objects.Characters;

public class Paladin : Warrior
{
    // Cannot override the Attribute property because we are inheriting from the Warrior class
    // therefore, in this situation, the Attribute property must be hidden using the new keyword
    // i believe that this is generally a bad practice, however in this situation, it is necessary
    // because the requirements of the game require that the Paladin class inherit from Warrior
    private new IPaladinAttributes Attribute { get; }
    
    public Paladin(string name, IPaladinAttributes stats, IZone zone, bool isPlayer = false) : base(name, stats, zone, isPlayer)
    {
        // Attribute property must be cast to the correct type of attributes.
        // A new property is created to store the Paladin attributes in conjunction
        // with the inherited Warrior attributes
        Attribute = (IPaladinAttributes)stats;
    }


    private double HealCost { get; set; } = 25;

    private void Heal(ICharacter target)
    {
        if (Attribute.Mana >= HealCost)
        {
            Attribute.Mana -= HealCost;
            Console.Write(" Heal on " + target.Name);
            target.SetHealth(new Random().NextDouble() * (Math.Sqrt(900 * Level) - Math.Sqrt(2 * Level)) +
                             Math.Sqrt(2 * Level));
        }
        else
            Console.WriteLine("Not enough mana to heal");
    }

    // If no target is passed, heal self
    private void Heal() => Heal(this);

    public override void DoAction(ICharacter target)
    {
        // Paladins have the ability to heal themselves and their allies
        
        if (Attribute.Health < 50 && Attribute.Mana > HealCost)
        {
            Heal();
        }
        else if(Attribute.Rage >= 25)
        {
            HeavySwing(target);
            Attribute.Rage -= 25;
            Attribute.Mana += new Random().Next(8, 11);
        }
        else
        {
            BasicAttack(target);
            Attribute.Mana += new Random().Next(2, 17);
            Attribute.Rage += new Random().Next(9, 23);
        }
    }
}
