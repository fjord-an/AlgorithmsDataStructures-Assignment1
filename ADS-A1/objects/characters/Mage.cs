using ADS_A1.functions;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.attributes.Interfaces;

namespace ADS_A1.objects.Characters;

public class  Mage: Character
{
    //should inherit from character or composition from attributes?
    public Mage(string name, ICharacterAttributes stats) : base(name,  stats)
    {
        Interact = (IInteractiveWorldObject obj) =>
        {
            if (obj is Door door)
            {
                door.Activate();
            }
        };
    }

    public void FireBall(Character target)
    {
        // Attack functions will return void because damage is inflicted by performing a transformation on the Health property of the target object
        target.Health -= new Random().NextDouble() * (Math.Sqrt(10 * Level) - Math.Sqrt(2 * Level)) + Math.Sqrt(2 * Level);
    }
}