using ADS_A1.functions;
using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Attributes;
using ADS_A1.Objects.WorldObjects;

namespace ADS_A1.objects.Characters;

public class Warrior : Character
{
    //should inherit from character or composition from attributes?
    public Warrior(string name, ICharacterAttributes stats, bool isPlayer=false) : base(name,  stats, isPlayer)
    {
        Interact = (IInteractiveWorldObject obj) =>
        {
            if (obj is Door door)
            {
                door.Activate();
            }
        };
    }

    public void HeavySwing(Character target)
    {
        // Attack functions will return void because damage is inflicted by performing a transformation on the Health property of the target object
        target.Health -= new Random().NextDouble() * (Math.Sqrt(10 * Level) - Math.Sqrt(2 * Level)) + Math.Sqrt(2 * Level);
    }
}