using ADS_A1.objects;
using ADS_A1.functions;
using ADS_A1.Functions;
using ADS_A1.functions.prompt;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;
using ADS_A1.objects.Characters;
using ADS_A1.Objects.WorldObjects;

namespace ADS_A1
{
    class Program
    {
        private static string? _input;

        // TODO still not generating new file properly 12/10/2024
        private static readonly string ConfigPath = "Test.json";
        private static World _world;

        static void Main(string[] args)
        {
            // introduce and enter a new character name
            Console.WriteLine("Welcome to the Text Adventure!");
            
            IZone startingZone = new Zone("Haven", "A vast open field with a clear sky.");
            _world = new World(startingZone);

            // must set the config path first before loading or generating one
            // TODO consider moving this to Config.cs as it is a configuration setting
            Create.SetConfigPath(ConfigPath);

            // Create the character config JSON file if it does not exist with default values.
            // TODO is name necassary? change the paramater to get from the character object, also the isPlayer value
            // TODO need a cleaner way to create the default configs for each character in Config.cs
            Config.NewConfig(ConfigPath, new Warrior("Warrior", new WarriorAttributesBuilder().Build(), false));
            Config.NewConfig(ConfigPath, new Mage("Mage", new MageAttributesBuilder().Build(), false));
            Config.NewConfig(ConfigPath, new Paladin("Paladin", new PaladinAttributesBuilder().Build(), false));

            // Prompt the user to create a character
            Character player = CreateCharacterPrompt.Prompt();
            _world.SetPlayersCurrentZone(startingZone, player);
            
            Console.WriteLine($"Welcome {player.Name}! You set out on your adventure. before you is a vast world of mystery and danger! to traverse this world, enter the following commands:");
            
            while (_input != "q")
            {
                Console.WriteLine("'b' back, 'n' forward, 'z' current zone, 's' stats, 'o' save, 'q' to quit");
                // Prompt the user for input and change the console color to yellow for user input only
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                _input = Console.ReadLine();
                Console.ResetColor();

                // Get the players current zone in each iteration of the loop to use for commands. That way only one search is needed 
                player.UpdateCurrentZone(_world);

                switch (_input)
                {
                    case "n":
                        // Add a new zone to the world and move the player to it
                        IZone newZone = new Zone(NameGenerator.ZoneName(new Random().Next(3, 12)),
                            NameGenerator.ZoneDescription());

                        // Add the new zone to the world and move the player to it with the containsPlayer flag
                        // This will increase the performance of the game as it will not have to search for the player in the world
                        // The player object is also passed to the function to add the player to the new zone if the containsPlayer flag is set to true
                        // TODO SetZone method that adds new zone if next zone is null, currently the zone is added to the end of the list
                        _world.SetPlayersCurrentZone(newZone, player);
                        // _world.AddZone(newZone, containsPlayer: true, player);

                        // give the player a message that they have moved and introduce the new zone
                        if (!newZone.ZoneCharacters.GetCharacters().Contains(player))
                            Console.WriteLine("Error! Could not add player to the new zone.");
                        // TODO remove the new zone if the player could not be added

                        // spawn random enemies in the new zone
                        var enemyCount = new Random().Next(-2, 5);
                        for (int i = 0; i <= enemyCount; i++)
                        {
                            newZone.ZoneCharacters.AddCharacter(
                                new Warrior(
                                    NameGenerator.ZoneName(new Random().Next(2, 9)),
                                    new WarriorAttributesBuilder()
                                        .SetAttack(new Random().Next(10, 30))
                                        .SetDefense(new Random().Next(10, 30))
                                        .SetSpeed(new Random().Next(10, 30))
                                        .SetHealth(new Random().Next(50, 150))
                                        .SetMaxHealth(new Random().Next(50, 150))
                                        .SetExperience(0)
                                        .SetGold(new Random().Next(0, 80))
                                        .SetLevel(1)
                                        .SetExperienceToNextLevel(100)
                                        .Build()
                                )
                            );
                        }

                        break;
                    case "b":
                        // Move the player back to the previous zone
                        // TODO implement: move the player back to the previous zone
                        _world.SetPlayersCurrentZone(player.CurrentZone.PreviousZone, player);
                        break;
                    case "z":
                        // Show the player where they are
                        Console.WriteLine($"You are in {player.CurrentZone.Name}, a {player.CurrentZone.Description}");
                        break;
                    case "o":
                        // Save the game state
                        // SaveGame.Save(player, player.CurrentZone);
                        Console.WriteLine("Save game functionality not implemented yet.");
                        break;
                    case "s":
                        // Show the player's stats if they type 'stats'. Can pass any character object to the function.
                        // TODO implement: pass a target character object to the function to show their stats by prompting the user to type the character name.
                        Stats.Show(player);
                        break;
                }
                
                // TODO find the player in the world with the containsPlayer flag instead of searching for the player in the worlds zones
                // Get the players current zone in each iteration of the loop to check if the player has moved zones
                // playersCurrentZone = player.CurrentZone;

                // Always show the player where they are after each input use the property for faster access
                // playersCurrentZone = _world.PlayersCurrentZone;
                Console.WriteLine($"You are in {player.CurrentZone.Name}, a {player.CurrentZone.Description}");

                // Store the characters in the zone in an IEnumerable to filter them
                IEnumerable<Character> zoneCharacters = player.CurrentZone.ZoneCharacters.GetCharacters();
                // to get the enemies in the zone, filter the characters that are not a player. Store the filtered characters in an array
                // to avoid multiple iterations of the IEnumerable when counting the enemies, increasing performance
                Character[] enemies = zoneCharacters.Where(c => !c.IsPlayer).ToArray();
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

                Console.WriteLine("");
            }
        }
    }
}
