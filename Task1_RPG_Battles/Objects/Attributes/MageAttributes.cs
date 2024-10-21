using ADS_A1.functions;
using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes.builders;

namespace ADS_A1.objects.Attributes;

public class MageAttributes : CharacterAttributes, IMageAttributes
// Inherit the common/generic CharacterAttributes and implement the IMageAttributes
// so that only the interface is exposed to the game, decoupling functions.
{
    public double Mana { get; set; }
    public double MaxMana { get; set; }
    public double Runes { get; set; }

    public MageAttributes(MageAttributesBuilder builder)
        : base(builder)
    //using a builder to initialise stats
    {
        // Add the Mage stats passed to the object being used
        Mana = builder.Mana;
        MaxMana = builder.MaxMana;
        Runes = builder.Runes;
    }
}
