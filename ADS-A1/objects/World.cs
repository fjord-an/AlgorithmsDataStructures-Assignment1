namespace ADS_A1.objects;


public class World
{
    private Zone _currentZone { get; set; }
    private Zone _firstZone { get; set; }
    private Zone _lastZone { get; set; }
    private Door _firstDoor { get; set; }
    private Door _lastDoor { get; set; }
    private List<Character> _enemies { get; set; }
    
    public World()
    {
        _currentZone = null;
        _firstZone = null;
        _lastZone = null;
        _firstDoor = null;
        _lastDoor = null;
        _enemies = new List<Character>();
    }
    
    public void AddZone(Zone zone)
    {
        if (_firstZone == null)
        {
            _firstZone = zone;
            _lastZone = zone;
        }
        else
        {
            _lastZone._currentZone = zone;
            _lastZone = zone;
        }
    }
    
    public void AddEnemy(Character enemy)
    {
        _enemies.Add(enemy);
    }
    
    public void SpawnEnemies()
    {
        foreach (Character enemy in _enemies)
        {
            Console.WriteLine("You encounter " + enemy.Name);
        }
    }
}
    
