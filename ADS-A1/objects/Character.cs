namespace ADS_A1.objects;

public abstract class Character
{
    public String Name { get; set; }
    public CharacterAttributes Attribute { get; set; }
    
    private double Level { get; set; }
    private double Health { get; set; }
    private bool IsAlive { get; set; }
    private bool isPlayer { get; set; }
    
    // using generic action delegate of the .NET framework so that the player can execute generic actions 
    // in the game, which can be passed by the object itself, so that many different objects can easily be added => extensible
    // This will result in a composition relationship between the character class and World objects.
    // this will be polymorphic, allowing Interface objects to be passed to the character class 
    public virtual Action<IInteractiveWorldObject>? Interact { get; set; }


    protected Character(string name, CharacterAttributes attribute)
    {
        Name = name;
        Attribute = attribute;
        Level = 1;
        Health = 100;
    }
    
    public void Attack(Character target)
    {
        // the attack will be based on the level of the character and a random number. the formula used here helps normalise the damage to prevent high level characters from dealing too much damage 
        target.Health -= new Random().NextDouble() * (Math.Sqrt(10 * Level) - Math.Sqrt(2 * Level)) + Math.Sqrt(2 * Level);
    }

}

public class Warrior : Character
{
    //should inherit from character or composition from attributes?
    public Warrior(string name, WarriorAttributes stats) : base(name,  stats)
    {
        Interact = (IInteractiveWorldObject obj) =>
        {
            if (obj is Door door)
            {
                door.Activate();
            }
        };
    }
}
