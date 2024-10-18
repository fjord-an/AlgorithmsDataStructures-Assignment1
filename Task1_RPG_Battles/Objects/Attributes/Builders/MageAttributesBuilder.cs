using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;

namespace ADS_A1.objects.Attributes.builders;

public class MageAttributesBuilder : CharacterAttributesBuilder
{
    private int _mana;
    private int _maxMana;
    private int _runes;

    public MageAttributesBuilder SetMana(int mana)
    {
        _mana= mana;
        return this;
    }

    public MageAttributesBuilder SetMaxMana(int maxMana)
    {
        _maxMana = maxMana;
        return this;
    }

    public MageAttributesBuilder SetRunes(int runes)
    {
        _runes = runes;
        return this;
    }
    
    public override IMageAttributes Build()
    {
        return new MageAttributes(this);
    }

    public int Mana => _mana;
    public int MaxMana => _maxMana;
    public int Runes => _runes;
}