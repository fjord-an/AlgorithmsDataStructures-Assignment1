namespace ADS_A1.Interfaces.CharacterAttributes;

public interface IWarriorAttributes : ICharacterAttributes
{
    int Rage { get; }
    int MaxRage { get; }
}