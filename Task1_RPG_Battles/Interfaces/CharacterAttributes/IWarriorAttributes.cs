namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IWarriorAttributes : ICharacterAttributes
{
    double Rage { get; set; }
    double MaxRage { get; set; }
}