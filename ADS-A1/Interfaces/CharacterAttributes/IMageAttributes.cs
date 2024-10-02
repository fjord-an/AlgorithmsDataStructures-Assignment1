namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IMageAttributes : ICharacterAttributes
{
    int Mana { get; }
    int MaxMana { get; }
    int Runes { get; }
}