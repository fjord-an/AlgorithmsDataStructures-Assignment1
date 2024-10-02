using ADS_A1.Interfaces;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Characters;

namespace ADS_A1.objects;

public class Zone : INonInteractiveWorldObject
{
    /// <summary>
    /// This class represents a zone in the game world. A zone is a collection of doors and they are connected to each other
    /// via a Double Linked List. The Zone linked list in World.cs will be responsible for managing the rooms and doors in the zone.
    /// It will also spawn enemies in the zone. 
    ///
    /// </summary>
    
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    public Zone _currentZone { get; set; }
    private Zone _firstZone { get; set; }
    private Zone _lastZone { get; set; }
    public List<IInteractiveWorldObject> Doors { get; private set; }
    private List<Character> _enemies { get; set; }
    
    public Zone(string name, string description)
    {
        Name = name;
        Description = description;
        _currentZone = this;
        _firstZone = this;
        _lastZone = this;
        Doors = new List<IInteractiveWorldObject>(); // list of doors in the zone
        _enemies = new List<Character>();
    }
    
    public void AddInteractiveObject(IInteractiveWorldObject InteractiveObject) 
    {
        // Add Interactive objects to the zone such as doors and items
        Doors.Add(InteractiveObject);
    }

}