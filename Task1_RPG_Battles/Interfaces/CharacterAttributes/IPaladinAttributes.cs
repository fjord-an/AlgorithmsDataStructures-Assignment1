namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IPaladinAttributes : IWarriorAttributes
{
    public double Mana { get; set; }
    public double MaxMana { get; set; }
    public double HolyPower { get; }
}