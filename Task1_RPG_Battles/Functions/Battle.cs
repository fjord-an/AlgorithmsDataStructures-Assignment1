using ADS_A1.Interfaces.Characters;

namespace ADS_A1.Functions;

public class Battle
{
    private static int roundCounter = 0;
    public static void PrintInterface(ICharacter player)
    {
        
        // Store the characters in the zone in an IEnumerable to filter them
        IEnumerable<ICharacter> zoneCharacters = player.CurrentZone.ZoneCharacters.GetCharacters();
        // to get the enemies in the zone, filter the characters that are not a player. Store the filtered characters in an array
        // to avoid multiple iterations of the IEnumerable when counting the enemies, increasing performance
        ICharacter[] enemies = zoneCharacters.Where(c => !c.IsPlayer).ToArray();
        if (enemies.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            // if there are enemies in the zone, print the number of enemies and their names. using the ternary operator to
            // print the correct plural form
            Console.WriteLine(enemies.Count() == 1
                ? $"You encounter {enemies.Count()} enemy in this zone."
                : $"You encounter {enemies.Count()} enemies in this zone.");
            Console.ResetColor();
        }
        else
        {
            // if there are no enemies in the zone, print a message
            Random sounds = new Random();
            switch (sounds.Next(1, 6))
            {
                case 1:
                    if (player.CurrentZone.Name.Contains("Forest"))
                        Console.WriteLine("You hear the sound of rustling leaves.");
                    else
                        Console.WriteLine("You hear the sound of a distant waterfall.");
                    break;
                case 2:
                    Console.WriteLine("You hear the sound of a bird chirping.");
                    break;
                case 3:
                    Console.WriteLine("You hear the sound of a distant wind.");
                    break;
                case 4:
                    Console.WriteLine("You hear nothing but the sound of your own footsteps.");
                    break;
                case 5:
                    Console.WriteLine("You hear the scurrying of unknown creatures.");
                    break;
                default:
                    Console.WriteLine("You are in a quiet place.");
                    break;
            }
        }

        foreach (var character in enemies)
        {
            if (character.IsPlayer)
                continue;

            string tabSpacing = character.Name.Length > 4 ? "\t" : "\t\t";
            Console.Write(
                $"| {character.Name} {tabSpacing} : Level {character.Level} {character.GetType().Name} ");
            Console.WriteLine($"| HP: {character.Health}/{character.Attribute.MaxHealth} |");
        }
        
        Console.ForegroundColor = ConsoleColor.Green;
        string tabSpacingPlayer = player.Name.Length > 4 ? "\t" : "\t\t";
        Console.Write(
            $"| {player.Name} {tabSpacingPlayer} : Level {player.Level} {player.GetType().Name} ");
        Console.WriteLine($"| HP: {player.Health}/{player.Attribute.MaxHealth} |");
        Console.ResetColor();

        Console.WriteLine("");
    }
    public static void StartBattle(ICharacter player, IEnumerable<ICharacter> enemy)
    { 
        // Print the interface before the battle starts
        PrintInterface(player);
        
        
        // To avoid multiple enumerations, convert the IEnumerable to an array
        // before checking the attributes of the characters (i.e. IsAlive())
        var characters = enemy as ICharacter[] ?? enemy.ToArray();
        
        // Check if there are any enemies in the zone
        if (characters.All(c => c == player))
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("There are no enemies in this zone.");
            Console.ResetColor();
            return;
        }
        
        while(characters.Any(e => e.IsAlive()) && player.IsAlive())
        {
            BattleRound(player, characters);
        }
        
        // If the player is dead, print a message and exit the game
        if (!player.IsAlive())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("You have been defeated!");
            Console.ResetColor();
            Environment.Exit(0);
        }
        else
        {
            // If the player is alive, print a message
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("You have defeated all enemies in this zone!");
            Console.ResetColor();
        }
        
        // Print the interface after the battle ends, showing the player's health
        PrintInterface(player);
    }

    public static void BattleRound(ICharacter player, IEnumerable<ICharacter> enemy)
    {
        // To avoid multiple enumerations, convert the IEnumerable to an array
        var characters = enemy as ICharacter[] ?? enemy.ToArray();
        foreach (var e in characters)
        {
            // Skip the player
            if (e == player) continue;
            
            // Increment the round counter
            roundCounter++;
            
            // ask the player if they want to continue the battle or flee every 4 rounds
            if (roundCounter % 4 == 0 && !Prompt(player))
            {
                return;
            }
            
            // If the player is faster than the enemy, the player will attack first
            if (player.Attribute.Speed >= e.Attribute.Speed)
            {
                // The player and enemy will attack each other. State the name of the player and enemy
                // before they attack
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{player.Name} used");
                player.DoAction(e);
                Thread.Sleep(new Random().Next(500, 1500));
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{e.Name} ");
                e.DoAction(player);
            }
            else
            {
                // The enemy will attack first
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"{e.Name} ");
                e.DoAction(player);
                Thread.Sleep(new Random().Next(500, 2500));
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{player.Name} ");
                player.DoAction(e);
            }
            if(!e.IsAlive() && e != player)
            {
                // If the enemy is dead, remove it from the list
                enemy = characters.Where(c => c != e);
                // add experience to the player for each enemy defeated
                player.Attribute.GainExperience(e.Attribute.Experience);
            }
        }
    }

    public static bool Prompt(ICharacter player)
    {
        while (true)
        {
            // Ask the player if they want to continue the battle or flee
            Console.WriteLine("Do you want to continue the battle or flee?");
            Console.WriteLine("1. Continue the battle");
            Console.WriteLine("2. Flee");
            Console.WriteLine("3. Use an item");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
    
            if (choice == "2")
            {
                // If the player chooses to flee, break out of the loop
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("You have fled from the battle!");
                Console.ResetColor();
                return false;
            }
            else if (choice == "3")
            {
                while (true)
                {
                    Console.WriteLine("1: Health potion");
                    Console.WriteLine("2: Attack potion");
                    Console.WriteLine("3: Return");
                    Console.Write("Enter your choice: ");
                    string itemChoice = Console.ReadLine();
    
                    if (itemChoice == "1")
                    {
                        // Use a health potion
                        player.SetHealth(40);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("You have used a health potion!");
                        Console.ResetColor();
                        break;
                    }
                    else if (itemChoice == "2")
                    {
                        // Use an attack potion
                        var rand = new Random().Next(-3, 8);
                        player.Attribute.Attack += rand;
                        if (rand > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine($"You have used an attack potion! (attack increased to {player.Attribute.Attack})");
                        }
                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"You have used an attack potion! It was ineffective! (attack decreased to {player.Attribute.Attack})");
                        }

                        Console.ResetColor();
                        break;
                    }
                    else if (itemChoice == "3")
                    {
                        // Return to the previous menu
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Invalid choice. Please try again.");
                    }
                }
            }
            else if (choice == "1")
            {
                return true;
            }
            else
            {
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }
    }
}