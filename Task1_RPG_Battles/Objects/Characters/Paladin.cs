using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.Interfaces.Characters;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;

namespace ADS_A1.objects.Characters;

// The Defensive Warrior
public class Paladin : Warrior
{
    // Cannot override the Attribute property because we are inheriting from the Warrior class
    // therefore, in this situation, the Attribute property must be hidden using the new keyword
    // this may generally be bad practise as it introduces possible bugs, however in this situation it is necassary
    // because the requirements of the game require that the Paladin class inherit from Warrior
    // i think that in a real implementation of the game, the Paladin would have a completly unique implementation,
    // mutually exclusive from other classes.
    //
    private new IPaladinAttributes Attribute { get; }

    public Paladin(string name, IPaladinAttributes stats, IZone zone, bool isPlayer = false) : base(name, stats, zone, isPlayer)
    {
        // Attribute property must be cast to the correct type of attributes.
        // A new property is created to store the Paladin attributes in conjunction
        // with the inherited Warrior attributes
        Attribute = (IPaladinAttributes)stats;
    }


    // Set the mana cost of Heal (initialially 25)
    private double HealCost { get; set; } = 25;

    private void Heal(ICharacter target)
    {
        // heal logic to heal allies (that are not currently implemented). aswell as self        
        if (Attribute.Mana >= HealCost)
        {
            Attribute.Mana -= HealCost;
            Console.Write(" Heal on " + target.Name);
            target.SetHealth(new Random().NextDouble() * (Math.Sqrt(600 * Level) - Math.Sqrt(2 * Level)) +
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
        else if (Attribute.Rage >= 25)
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
