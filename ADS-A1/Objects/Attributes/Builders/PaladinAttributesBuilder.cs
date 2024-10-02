using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;

namespace ADS_A1.objects.Attributes.builders;

public class PaladinAttributesBuilder : CharacterAttributesBuilder
{
    private int _mana;
    private int _maxMana;
    private int _holyPower;

    public PaladinAttributesBuilder SetMana(int mana)
    {
        _mana = mana;
        return this;
    }

    public PaladinAttributesBuilder SetMaxMana(int maxMana)
    {
        _maxMana = maxMana;
        return this;
    }

    public PaladinAttributesBuilder SetHolyPower(int holyPower) 
    {
        _holyPower = holyPower;
        return this;
    }

    public override IPaladinAttributes Build()
    {
        return new PaladinAttributes(this);
    }

    public int Mana => _mana;
    public int MaxMana => _maxMana;
    public int HolyPower => _holyPower;
}