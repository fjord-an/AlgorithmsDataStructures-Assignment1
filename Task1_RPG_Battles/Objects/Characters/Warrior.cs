using System.Net;
using ADS_A1.functions;
using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.Interfaces.Characters;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Attributes;
using ADS_A1.Objects.WorldObjects;

namespace ADS_A1.objects.Characters;

public class Warrior : Character
{
    public override IWarriorAttributes Attribute { get; }
    
    public Warrior(string name, ICharacterAttributes stats, IZone zone, bool isPlayer=false) : base(name,  stats, zone, isPlayer)
    {
        // Attribute property must be cast to the correct type of attributes:
        if (stats is IWarriorAttributes)
            Attribute = (IWarriorAttributes)stats;
    }
    
    public override void DoAction(ICharacter target)
    {
        // implemented a simple switch statement to choose the ability based
        // on the health of the target and the rage of the warrior
        switch (Attribute.Rage)
        {
            case >= 25:
                Attribute.Rage -= 25;
                // use execute if the target is below 30% health else use heavy swing
                switch (target)
                {
                    // Execute is a high damage ability that can only be used when the target is below 30% health
                    case var x when (x.Attribute.Health/x.Attribute.MaxHealth) < 0.3:
                        Execute(target);
                        break;
                    default:
                        HeavySwing(target);
                        break;
                }                
                break;
            case >= 15:
                HeavySwing(target);
                Attribute.Rage -= 15;
                break;
            case < 15:
                BasicAttack(target);
                Attribute.Rage += 10;
                break;
            
        }

    }

    public void HeavySwing(ICharacter target)
    {
        // Attack functions will return void because damage is inflicted by
        // performing a transformation on the Health property of the target object
        // which is encapsulated in the attribute class, limiting the modification
        // and performing all associated logic in the attribute class only!
        Console.Write(" Heavy Swing, ");
        target.SetHealth(-1 * (new Random().NextDouble() * (Math.Sqrt(600 * Level) - Math.Sqrt(2 * Level)) + Math.Sqrt(2 * Level)));
    }
    
    public void Execute(ICharacter target)
    {
        Console.Write(" Execute, ");
        // high damage ability that can only be used when the target is below 30% health
        if (target.Health < 30)
            target.SetHealth(-1 * (new Random().NextDouble() * (Math.Sqrt(3000 * Level) - Math.Sqrt(2 * Level)) + Math.Sqrt(2 * Level)));
        else
        {
            Console.WriteLine("Target is not below 30% health");
            BasicAttack(target);
        }
    }
}