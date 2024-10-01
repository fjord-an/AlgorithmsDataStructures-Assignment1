using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;
using ADS_A1.objects.attributes.Interfaces;

namespace ADS_A1.objects.Characters;

public class Paladin : Warrior
{
    public Paladin(string name, ICharacterAttributes stats) : base(name, stats)
    {
        
    }
}