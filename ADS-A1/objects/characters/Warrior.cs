using ADS_A1.functions;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.attributes.Interfaces;

namespace ADS_A1.objects.Characters;

public class Warrior : Character
{
    //should inherit from character or composition from attributes?
    public Warrior(string name, IWarriorAttributes stats) : base(name,  stats)
    {
        Interact = (IInteractiveWorldObject obj) =>
        {
            if (obj is Door door)
            {
                door.Activate();
            }
        };
    }
}