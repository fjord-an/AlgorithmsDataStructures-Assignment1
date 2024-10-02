using ADS_A1.Interfaces;
using ADS_A1.Interfaces.WorldObjects;

namespace ADS_A1.objects;

public class Door : IInteractiveWorldObject
{
    private string _name { get; set; }
    private string _description { get; set; }
    private bool _isActivated { get; set; }
    private bool _isInteractive { get; set; }
    public Zone NextZone { get; set; }

    // default constructor, if no parameters are passed, the door will be an unlocked wooden door
    public Door(bool isInteractive=true, string name = "Wooden Door", string description = "An Unlocked Wooden Door")
    {
        _name = name;
        _description = description;
        _isInteractive = isInteractive;
        _isActivated = false;
    }

    public void Activate()
    {
        if (!_isInteractive)
        {
            Console.WriteLine("This door is locked!");
            return;
        }
        Console.WriteLine("Opening Door...");
        _isActivated = true;
        
        Enter();
    }

    public void Deactivate()
    {
        Console.WriteLine("Closing Door...");
    }
    
    public void Enter()
    {
        if (_isActivated)
        {
            Console.WriteLine("Entering " + NextZone);
            
            _isActivated = false;
        }
        else
        {
            Console.WriteLine("Door is locked!");
        }
    }
}