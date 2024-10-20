namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IMageAttributes : ICharacterAttributes
{
    double Mana { get; set; }
    double MaxMana { get; set; }
    // Runes are a placeholder for a future feature for new spells
    double Runes { get; set; }
}