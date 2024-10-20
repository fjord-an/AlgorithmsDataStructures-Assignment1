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
        private static readonly string ConfigPath = "CharacterConfig.json";
        private static World _world;

        static void Main(string[] args)
        {
            // introduce and enter a new character name
            Console.WriteLine("Welcome to the Text Adventure!");

            // must set the config path first before loading or generating one
            // TODO consider moving this to Config.cs as it is a configuration setting
            Create.SetConfigPath(ConfigPath);

            // Create the character config JSON file if it does not exist with default values.
            Config.NewConfig(ConfigPath, new Warrior("Warrior", new WarriorAttributesBuilder().Build(), zone:null, isPlayer:false));
            Config.NewConfig(ConfigPath, new Mage("Mage", new MageAttributesBuilder().Build(), zone:null, isPlayer:false));
            Config.NewConfig(ConfigPath, new Paladin("Paladin", new PaladinAttributesBuilder().Build(), zone:null, isPlayer:false));
            Config.NewConfig(ConfigPath, new Character("Character", new CharacterAttributesBuilder().Build(), zone:null, isPlayer:false));

            IZone startingZone = new Zone("Haven", "A vast open field with a clear sky.");
            _world = new World(startingZone);
            
            // Prompt the user to create a character
            Character player = CreateCharacterPrompt.Prompt(startingZone);
            _world.SetPlayersCurrentZone(startingZone, player);
            
            Console.WriteLine($"Welcome {player.Name}! You set out on your adventure. before you is a vast world of mystery and danger! \nTo traverse this world, enter the following commands:");
            
            while (_input != "q")
            {
                Console.WriteLine("'b' back, 'n' forward, 'z' current zone, 's' stats, 'o' save, 'q' to quit");
                // Prompt the user for input and change the console color to yellow for user input only
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                _input = Console.ReadLine();
                Console.ResetColor();

                // Get the players current zone in each iteration of the loop to use for commands. That way only one search is needed 

                switch (_input)
                {
                    case "n":
                        IZone nextZone;
                        // check if the next zone needs to be generated or if it
                        // already exists from the player's current zone
                        if (player.CurrentZone.NextZone is null)
                        {
                            nextZone = new Zone(NameGenerator.ZoneName(new Random().Next(3, 12)),
                                NameGenerator.ZoneDescription());
                            
                            // spawn random enemies for the new zone (negative values give extra chances for no enemies)
                            var enemyCount = new Random().Next(-3, 5);
                            for (int i = 0; i <= enemyCount; i++)
                            {
                                Character enemy = Create.NewCharacter(NameGenerator.ZoneName(new Random().Next(2, 9)), "Character", nextZone, player.Level,isPlayer: false);
                                nextZone.ZoneCharacters.AddCharacter(enemy);
                            }
                        }
                        else
                        {
                            nextZone = player.CurrentZone.NextZone;
                        }
                        
                        // Add the new zone to the world and move the player to it with the containsPlayer flag
                        // This will increase the performance of the game as it will not have to search for the player in the world
                        // The player object is also passed to the function to add the player to the new zone if the containsPlayer flag is set to true
                        _world.SetPlayersCurrentZone(nextZone, player);
                        // _world.AddZone(newZone, containsPlayer: true, player);

                        // give the player a message that they have moved and introduce the new zone
                        // TODO possible performance impact:
                        if (!nextZone.ZoneCharacters.GetCharacters().Contains(player))
                            Console.WriteLine("Error! Could not add player to the new zone.");
                        
                        Battle.StartBattle(player, player.CurrentZone.ZoneCharacters.GetCharacters(), _world);
                        break;
                    case "b":
                        // Move the player back to the previous zone
                        _world.SetPlayersCurrentZone(player.CurrentZone.PreviousZone, player);
                        Battle.StartBattle(player, player.CurrentZone.ZoneCharacters.GetCharacters(), _world);
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
                        Stats.Show(player);
                        break;
                }
                
                // Get the players current zone in each iteration of the loop to check if the player has moved zones
                // playersCurrentZone = player.CurrentZone;

                // Always show the player where they are after each input use the property for faster access
                // playersCurrentZone = _world.PlayersCurrentZone;
                Console.WriteLine($"You are in {player.CurrentZone.Name}, a {player.CurrentZone.Description}");

                // #####################
                // // Store the characters in the zone in an IEnumerable to filter them
                // IEnumerable<Character> zoneCharacters = player.CurrentZone.ZoneCharacters.GetCharacters();
                // // to get the enemies in the zone, filter the characters that are not a player. Store the filtered characters in an array
                // // to avoid multiple iterations of the IEnumerable when counting the enemies, increasing performance
                // Character[] enemies = zoneCharacters.Where(c => !c.IsPlayer).ToArray();
                // if (enemies.Any())
                // {
                //     Console.ForegroundColor = ConsoleColor.Red;
                //     // if there are enemies in the zone, print the number of enemies and their names. using the ternary operator to
                //     // print the correct plural form
                //     Console.WriteLine(enemies.Count() == 1
                //         ? $"You encounter {enemies.Count()} enemy in this zone."
                //         : $"You encounter {enemies.Count()} enemies in this zone.");
                //     Console.ResetColor();
                // }
                // else
                // {
                //     // if there are no enemies in the zone, print a message
                //     Random sounds = new Random();
                //     switch (sounds.Next(1, 6))
                //     {
                //         case 1:
                //             if (player.CurrentZone.Name.Contains("Forest"))
                //                 Console.WriteLine("You hear the sound of rustling leaves.");
                //             else
                //                 Console.WriteLine("You hear the sound of a distant waterfall.");
                //             break;
                //         case 2:
                //             Console.WriteLine("You hear the sound of a bird chirping.");
                //             break;
                //         case 3:
                //             Console.WriteLine("You hear the sound of a distant wind.");
                //             break;
                //         case 4:
                //             Console.WriteLine("You hear nothing but the sound of your own footsteps.");
                //             break;
                //         case 5:
                //             Console.WriteLine("You hear the scurrying of unknown creatures.");
                //             break;
                //         default:
                //             Console.WriteLine("You are in a quiet place.");
                //             break;
                //     }
                // }
                //
                // foreach (var character in enemies)
                // {
                //     if (character.IsPlayer)
                //         continue;
                //
                //     string tabSpacing = character.Name.Length > 4 ? "\t" : "\t\t";
                //     Console.Write(
                //         $"| {character.Name} {tabSpacing} : Level {character.Level} {character.GetType().Name} ");
                //     Console.WriteLine($"| HP: {character.Health}/{character.Attribute.MaxHealth} |");
                // }
                //
                // Console.ForegroundColor = ConsoleColor.Green;
                // string tabSpacingPlayer = player.Name.Length > 4 ? "\t" : "\t\t";
                // Console.Write(
                //     $"| {player.Name} {tabSpacingPlayer} : Level {player.Level} {player.GetType().Name} ");
                // Console.WriteLine($"| HP: {player.Health}/{player.Attribute.MaxHealth} |");
                // Console.ResetColor();
                //
                // Console.WriteLine("");
                // #####################
            }
        }
    }
}
