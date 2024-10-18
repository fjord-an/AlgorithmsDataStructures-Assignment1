using ADS_A1.Interfaces.WorldObjects;

namespace ADS_A1.Objects.WorldObjects;

public class Zone : IZone
{
    /// <summary>
    /// This class represents a zone in the game world. A zone is a collection of doors and they are connected to each other
    /// via a Double Linked List. The Zone linked list in World.cs will be responsible for managing the rooms and doors in the zone.
    /// It will also spawn enemies in the zone. 
    ///
    /// </summary>
    
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public IZone CurrentZone { get; set; }
    public IZone PreviousZone { get; set; }
    public IZone NextZone { get; set; }
    public List<IInteractiveWorldObject> Doors { get; private set; }
    public CharacterContainer ZoneCharacters { get; private set; }
    
    public Zone(string name, string description)
    {
        Name = name;
        Description = description;
        CurrentZone = this;
        PreviousZone = null;
        NextZone = null;
        Doors = new List<IInteractiveWorldObject>(); // list of doors in the zone
        ZoneCharacters = new CharacterContainer();
    }
    
    public void AddInteractiveObject(IInteractiveWorldObject InteractiveObject) 
    {
        // Add Interactive objects to the zone such as doors and items
        Doors.Add(InteractiveObject);
    }
}
