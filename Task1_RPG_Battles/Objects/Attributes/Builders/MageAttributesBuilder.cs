using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;

namespace ADS_A1.objects.Attributes.builders;

public class MageAttributesBuilder : CharacterAttributesBuilder
{
    public double Mana { get; set; }
    public double MaxMana {get; set; }

    public double Runes { get; set; }

    public MageAttributesBuilder SetMana(double  mana)
    {
        Mana= mana;
        return this;
    }

    public MageAttributesBuilder SetMaxMana(double maxMana)
    {
        MaxMana = maxMana;
        return this;
    }

    public MageAttributesBuilder SetRunes(double  runes)
    {
        Runes = runes;
        return this;
    }
    
    public override IMageAttributes Build()
    {
        return new MageAttributes(this);
    }
}