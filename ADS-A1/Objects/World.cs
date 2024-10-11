using ADS_A1.objects.Characters;

namespace ADS_A1.objects;


public class World
{
    private Zone _startingZone { get; set; }
    private Zone _lastZone { get; set; }
    private Door _firstDoor { get; set; }
    private Door _lastDoor { get; set; }
    public Zone PlayersCurrentZone { get; set; }
    
    public Zone GetPlayersCurrentZone(Character player)
    {
        Zone zone = _startingZone;
        while (zone is not null)
        {
            if (zone.ZoneCharacters.GetCharacters().Contains(player))
            {
                // player is in this zone
                return zone;
            }
            zone = zone.NextZone;
        }
        throw new Exception("Player not found in any zone, please try again or load a saved game");
    }
    
    public void AddZone(Zone zone, bool containsPlayer=false)
    {
        if (_startingZone == null)
        {
            // initialize the world with the first zone
             _startingZone = zone;
        }
        else
        {
            Zone current = _startingZone;
            // add the zone to the end of the linked list
            while(current.NextZone is not null)
            {
                current = current.NextZone;
            }
            current.NextZone = zone;
            zone.PreviousZone = current;
            _lastZone = zone;
            if (containsPlayer)
            {
                PlayersCurrentZone = zone;
            }
        }
    }
}
