using ADS_A1.objects;
using ADS_A1.functions;
using ADS_A1.Functions;
using ADS_A1.functions.prompt;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;
using ADS_A1.objects.Characters;

namespace ADS_A1
{
    class Program
    {
        private static string? _input;
        // TODO still not generating new file properly 12/10/2024
        private static readonly string ConfigPath = "Test.json";
        static void Main(string[] args)
        {
            // introduce and enter a new character name
            Console.WriteLine("Welcome to the Text Adventure!");
            
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
            
            Console.WriteLine("Loading World...");
            World world = new World();
            // Create a zone with a random name and description using a NameGenerator method as well as a random string array length.
            Zone zone1 = new Zone(NameGenerator.ZoneName(new Random().Next(3, 12)), NameGenerator.ZoneDescription());
            world.AddZone(zone1);
            
            //introduce the player to the world
            Console.WriteLine("Hail " + player.GetType().Name + ", " + player.Name + " .You are in " + zone1.Name + ", " + zone1.Description);
            Console.WriteLine("You can move to the next zone by typing 'next' or type 'stats' to see your stats." +
                              " type 'quit' to exit the game. type 'save' to save the state");

            while (_input != "quit")
            {
                // Prompt the user for input and change the console color to yellow for user input only
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                _input = Console.ReadLine();
                Console.ResetColor();

                switch (_input)
                {
                    case "next":
                        // Add a new zone to the world and move the player to it
                        Zone newZone = new Zone(NameGenerator.ZoneName(new Random().Next(3, 12)),
                            NameGenerator.ZoneDescription());
                        
                        // Add the new zone to the world and move the player to it with the containsPlayer flag
                        // This will increase the performance of the game as it will not have to search for the player in the world
                        world.AddZone(newZone, containsPlayer: true);
                        
                        // move the player to the new zone and remove them from the previous zone
                        newZone.ZoneCharacters.AddCharacter(player);
                        newZone.PreviousZone.ZoneCharacters.RemoveCharacter(player);
                        
                        // give the player a message that they have moved and introduce the new zone
                        if (newZone.ZoneCharacters.GetCharacters().Contains(player))
                        {
                            // remove the player from the previous zone
                            Console.WriteLine("* Player Added! You are now in " + newZone.Name + ", " + newZone.Description);
                        }
                        else
                        {
                            Console.WriteLine("Error! Could not add player to the new zone.");
                            // TODO remove the new zone if the player could not be added
                        }
                        
                        // spawn random enemies in the new zone
                        var enemyCount = new Random().Next(-2, 5);
                        for(int i = 0; i <= enemyCount; i++)
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
                    case "back":
                        // Move the player back to the previous zone
                        // TODO implement: move the player back to the previous zone
                        
                        break;
                    case "save":
                        // Save the game state
                        // SaveGame.Save(player, zone1);
                        throw new NotImplementedException("Save game functionality not implemented yet.");
                        break;
                    case "stats":
                        // Show the player's stats if they type 'stats'. Can pass any character object to the function.
                        // TODO implement: pass a target character object to the function to show their stats by prompting the user to type the character name.
                        Stats.Show(player);
                        break;
                }
                
                // Always show the player where they are after each input
                // TODO find the player in the world with the containsPlayer flag instead of searching for the player in the worlds zones
                Zone playersCurrentZone = world.GetPlayersCurrentZone(player);
                Console.WriteLine($"You are in {playersCurrentZone.Name}, a {playersCurrentZone.Description}");
                // TODO implement: show the player the enemies in the zone
                Console.WriteLine($"You encounter {playersCurrentZone.ZoneCharacters.GetCharacters()} enemies in this zone.");
            }
        }
    }
}

