namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IPaladinAttributes : ICharacterAttributes
{
    public double Mana { get; set; }
    public double MaxMana { get; set; }
    public int HolyPower { get; }
}