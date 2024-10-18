using System.ComponentModel.Design;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Characters;

namespace ADS_A1.Objects.WorldObjects;


public class World
{
    private IZone _startingZone { get; set; }
    private IZone _lastZone { get; set; }
    private Door _firstDoor { get; set; }
    private Door _lastDoor { get; set; }
    public IZone? PlayersCurrentZone { get; private set; }
    
    public World(IZone startingZone)
    {
        _startingZone = startingZone;
        _lastZone = startingZone;
    }
    
    public IZone FindPlayersCurrentZone(Character player)
    {
        IZone zone = _startingZone;
            
        while (zone is not null)
        {
            if (zone.ZoneCharacters.GetCharacters().Contains(player))
            {
                // player is in this zone, update the field for future reference and return the zone
                PlayersCurrentZone = zone;
                return zone;
            }
            zone = zone.NextZone;
        }
        return null;
    }

    public IZone GetPlayersCurrentZone(Character player)
    {
        return player.CurrentZone;
    }
    
    public void SetPlayersCurrentZone(IZone zone, Character player)
    {        
        // remove the player from the previous zone before adding the player to the new zone
        if (player.CurrentZone != null)
        {
            player.CurrentZone.ZoneCharacters.RemoveCharacter(player);
        }

        zone.ZoneCharacters.AddCharacter(player);
        player.CurrentZone = zone;
        PlayersCurrentZone = zone;
    }
    
    public void AddZone(IZone zone, bool containsPlayer=false, Character player=null!)
    // set the default value of containsPlayer to false so that the player is not added to the zone by default
    // However, if the boolean is set to true, the player will be added to the zone if the player is not already in the zone
    {
        if (_startingZone == null)
        {
            // initialize the world with the first zone
             _startingZone = zone;
             
            // If the zone does not contain the player, add the player to the zone
            // a player must be in a zone at all times
            if (!zone.ZoneCharacters.GetCharacters().Any(c => c.IsPlayer))
            {
                SetPlayersCurrentZone(zone, player);
            }
        }
        else
        {
            IZone current = _startingZone;
            
            // add the zone to the end of the linked list
            while(current.NextZone is not null)
            {
                current = current.NextZone;
            }
            // change the pointers of the current zone to the new zone and vice versa
            current.NextZone = zone;
            zone.PreviousZone = current;
            _lastZone = zone;
            // If the zone does not contain the player, and the flag is set, add the player to the zone
            if (containsPlayer)
            {
                PlayersCurrentZone = zone;
                if (zone.ZoneCharacters.GetCharacters().All(c => c != player))
                {
                    if(player is not null)
                    {
                        SetPlayersCurrentZone(zone.PreviousZone, player);
                    }
                    else
                    {
                        Console.WriteLine("Error Adding Player to Zone: No player found");
                    }
                }
            }
        }
    }
}
