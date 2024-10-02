using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;

namespace ADS_A1.objects.Characters;

public class Paladin : Warrior
{
    public Paladin(string name, ICharacterAttributes stats) : base(name, stats)
    {
        
    }
}