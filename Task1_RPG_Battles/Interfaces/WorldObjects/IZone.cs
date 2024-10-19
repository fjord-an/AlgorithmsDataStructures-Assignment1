using ADS_A1.Objects.WorldObjects;

namespace ADS_A1.Interfaces.WorldObjects;

    // IZone.cs
public interface IZone
{
    public string Name { get; }
    public string Description { get; }
    public IZone CurrentZone { get; set; }
    public IZone PreviousZone { get; set; }
    public IZone? NextZone { get; set; }
    public List<IInteractiveWorldObject> Doors { get; }
    public CharacterContainer ZoneCharacters { get; }
}