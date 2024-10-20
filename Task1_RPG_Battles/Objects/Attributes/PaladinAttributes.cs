using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes.builders;

namespace ADS_A1.objects.Attributes;

public class PaladinAttributes : CharacterAttributes, IPaladinAttributes
{
    public double Mana { get; set; }
    public double MaxMana { get; set; }
    // future Paladin ability resource:
    public int HolyPower { get; }

    public PaladinAttributes(PaladinAttributesBuilder attributes) : base(attributes)
    {
        Mana = attributes.Mana;
        MaxMana = attributes.MaxMana;
        HolyPower = attributes.HolyPower;
    }
}