using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;

namespace ADS_A1.objects.Attributes.builders;

public class WarriorAttributesBuilder : CharacterAttributesBuilder
{
    private int _rage;
    private int _maxRage;

    
    public WarriorAttributesBuilder SetRage(int rage)
    {
        _rage = rage;
        return this;
    }

    public WarriorAttributesBuilder SetMaxRage(int maxRage)
    {
        _maxRage = maxRage;
        return this;
    }

    public override IWarriorAttributes Build()
    {
        return new WarriorAttributes(this);
    }

    public int Rage => _rage;
    public int MaxRage => _maxRage;
}