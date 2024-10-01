using ADS_A1.functions;
using ADS_A1.objects.Attributes.builders;
using ADS_A1.objects.attributes.Interfaces;

namespace ADS_A1.objects.Attributes;

public class MageAttributes : CharacterAttributes, IMageAttributes
// Inherit the common/generic CharacterAttributes and implement the IWarriorAttributes
// so that only the interface is exposed to the game
{
    public int Mana { get; }
    public int MaxMana { get; }
    public int Runes { get; }
    
    public MageAttributes(MageAttributesBuilder attributes) 
        : base(attributes)
    //using a builder to initialise stats
    {
        // Add the warriors stats passed to the object being used
        Mana = attributes.Mana;
        MaxMana = attributes.MaxMana;
        Runes = attributes.Runes;
    }
   
}