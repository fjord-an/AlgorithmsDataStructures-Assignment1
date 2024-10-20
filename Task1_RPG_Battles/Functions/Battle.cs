using ADS_A1.Interfaces.Characters;

namespace ADS_A1.Functions;

public class Battle
{
    public static void StartBattle(ICharacter player, IEnumerable<ICharacter> enemy)
    {
        // To avoid multiple enumerations, convert the IEnumerable to an array
        // before checking the attributes of the characters (i.e. IsAlive())
        var characters = enemy as ICharacter[] ?? enemy.ToArray();
        while(characters.Any(e => e.IsAlive()) && player.IsAlive())
        {
            BattleRound(player, characters);
        }
    }

    public static void BattleRound(ICharacter player, IEnumerable<ICharacter> enemy)
    {
        foreach (var e in enemy)
        {
            // Skip the player
            if (e == player) continue;
            
            // If the player is faster than the enemy, the player will attack first
            if (player.Attribute.Speed >= e.Attribute.Speed && player.IsAlive() && e.IsAlive())
            {
                // The player and enemy will attack each other
                player.DoAction(e);
                Task.Delay(500);
                e.DoAction(player);
            }
            else
            {
                // The enemy will attack first
                e.DoAction(player);
                Task.Delay(500);
                player.DoAction(e);
            }
        }
    }
}