using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes.builders;

namespace ADS_A1.objects.Attributes;

public class PaladinAttributes : CharacterAttributes, IPaladinAttributes
{
    public int Mana { get; }
    public int MaxMana { get; }
    public int HolyPower { get; }

    public PaladinAttributes(PaladinAttributesBuilder attributes) : base(attributes)
    {
        Mana = attributes.Mana;
        MaxMana = attributes.MaxMana;
        HolyPower = attributes.HolyPower;
    }
}