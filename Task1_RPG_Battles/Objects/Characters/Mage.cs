using ADS_A1.functions;
using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.Interfaces.Characters;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Attributes;
using ADS_A1.Objects.WorldObjects;

namespace ADS_A1.objects.Characters;

// Glass Cannon
public class Mage : Character
{
    public override IMageAttributes Attribute { get; }

    public Mage(string name, ICharacterAttributes stats, IZone zone, bool isPlayer = false) : base(name, stats, zone,
        isPlayer)
    {
        Attribute = (IMageAttributes)stats;
    }

    // different classes of characters will have different attributes:

    public override void DoAction(ICharacter target)
    {
        // Mages generally have many spells to choose from, so I have
        // implemented a simple switch statement to choose the spell based
        // on the mana available. Mana levels should b managed by setter method like health
        // in future iterations of the game
        switch (Attribute.Mana)
        {
            case > 20:
                PyroBlast(target);
                Attribute.Mana -= 10;
                break;
            case > 10:
                Fireball(target);
                Attribute.Mana -= 5;
                break;
            default:
                BasicAttack(target);
                Attribute.Mana += 4;
                break;
        }
    }

    private void PyroBlast(ICharacter target)
    {
        Console.Write(" PyroBlast, ");
        // Attack functions will return void because damage is inflicted by performing a transformation on the Health property of the target object
        target.SetHealth(-1 * new Random().NextDouble() * (Math.Sqrt(3500 * Level) - Math.Sqrt(2 * (Level + Attribute.Attack)) + Math.Sqrt(2 * (Level + Attribute.Attack))));
    }

    private void Fireball(ICharacter target)
    {
        Console.Write(" Fireball, ");
        target.SetHealth(-1 * new Random().NextDouble() * (Math.Sqrt(2000 * Level) - Math.Sqrt(2 * (Level + Attribute.Attack)) + Math.Sqrt(2 * (Level + Attribute.Attack))));
    }
}
