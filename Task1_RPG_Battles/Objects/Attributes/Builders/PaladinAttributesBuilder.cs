using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;

namespace ADS_A1.objects.Attributes.builders;

public class PaladinAttributesBuilder : WarriorAttributesBuilder
{
    public double Mana { get; private set; }

    public double MaxMana { get; private set; }

    public double HolyPower { get; private set; }
    
    
    public PaladinAttributesBuilder SetMana(double mana)
    {
        Mana = mana;
        return this;
    }

    public PaladinAttributesBuilder SetMaxMana(double maxMana)
    {
        MaxMana = maxMana;
        return this;
    }

    public PaladinAttributesBuilder SetHolyPower(double holyPower) 
    {
        HolyPower = holyPower;
        return this;
    }

    public override IPaladinAttributes Build()
    {
        return new PaladinAttributes(this);
    }
}