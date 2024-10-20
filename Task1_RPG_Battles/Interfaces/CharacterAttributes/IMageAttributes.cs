namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IMageAttributes : ICharacterAttributes
{
    int Mana { get; set; }
    int MaxMana { get; set; }
    // Runes are a placeholder for a future feature for new spells
    int Runes { get; set; }
}