using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes.builders;

namespace ADS_A1.objects.Attributes;

public class PaladinAttributes : WarriorAttributes, IPaladinAttributes
{
    private double _mana;
    public double Mana
    {
        get => _mana;
        // Ensure Mana never exceeds MaxMana by filtering all setter calls with a ternary operator
        set => _mana = value > MaxMana ? MaxMana : value;
    }
    public double MaxMana { get; set; }
    // future Unique Paladin ability resource:
    public double HolyPower { get; }

    public PaladinAttributes(PaladinAttributesBuilder attributes) : base(attributes)
    {
        Mana = attributes.Mana;
        MaxMana = attributes.MaxMana;
        HolyPower = attributes.HolyPower;
    }
}
