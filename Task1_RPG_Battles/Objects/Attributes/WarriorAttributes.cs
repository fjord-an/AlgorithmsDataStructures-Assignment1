using ADS_A1.functions;
using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes.builders;

namespace ADS_A1.objects.Attributes;

public class WarriorAttributes : CharacterAttributes, IWarriorAttributes
// Inherit the common/generic CharacterAttributes and implement the IWarriorAttributes
// so that only the interface is exposed to the game
{
    public double Rage { get; set; }
    public double MaxRage { get; set; }
    
    public WarriorAttributes(WarriorAttributesBuilder attributes) 
        : base(attributes)
    //using a builder to initialise stats
    {
        // Add the warriors stats passed to the object being used
        Rage = attributes.Rage;
        MaxRage = attributes.MaxRage;
    }
   
}