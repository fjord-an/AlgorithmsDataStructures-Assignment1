namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IPaladinAttributes : ICharacterAttributes
{
    public double Mana { get; }
    public double MaxMana { get; }
    public int HolyPower { get; }
}