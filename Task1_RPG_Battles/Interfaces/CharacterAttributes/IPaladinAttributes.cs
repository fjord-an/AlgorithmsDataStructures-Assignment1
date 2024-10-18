namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IPaladinAttributes : ICharacterAttributes
{
    public int Mana { get; }
    public int MaxMana { get; }
    public int HolyPower { get; }
}