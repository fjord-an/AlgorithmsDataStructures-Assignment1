namespace ADS_A1.objects.attributes.Interfaces;

public interface IMageAttributes : ICharacterAttributes
{
    int Mana { get; }
    int MaxMana { get; }
    int Runes { get; }
}