namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IPaladinAttributes : IWarriorAttributes, IManaAttributes
{
    public double HolyPower { get; }
}