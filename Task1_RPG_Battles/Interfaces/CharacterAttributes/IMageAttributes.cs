namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IMageAttributes : ICharacterAttributes, IManaAttributes
{
    // Runes are a placeholder for a future feature for new spells
    double Runes { get; set; }
}