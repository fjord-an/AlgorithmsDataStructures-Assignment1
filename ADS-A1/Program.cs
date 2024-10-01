using ADS_A1.objects;
using ADS_A1.functions;
using ADS_A1.functions.prompt;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Characters;

namespace ADS_A1
{
    class Program
    {
        static string input;
        static void Main(string[] args)
        {
            //TODO consider making new character classes and attributes addable by a json file for player customisation
            // introduce and enter a new character name
            Console.WriteLine("Welcome to the Text Adventure!");
            Character player = CreateCharacterPrompt.Prompt();
            
            Console.WriteLine("Loading World...");
            World world = new World();
            // Create a zone with a random name and description using a NameGenerator method as well as a random string array length.
            Zone zone1 = new Zone(NameGenerator.ZoneName(new Random().Next(3, 12)), NameGenerator.ZoneDescription());
            world.AddZone(zone1);
            
            //introduce the player to the world
            Console.WriteLine("Hail " + player.GetType().Name + ", " + player.Name + " .You are in " + zone1.Name + ", " + zone1.Description);
            Console.WriteLine("You can move to the next zone by typing 'next' or type 'stats' to see your stats. type 'quit' to exit the game. type 'save' to save the state");

            while (input != "quit")
            {
                Console.WriteLine($"You are in {zone1.Name}, a {zone1.Description}");
                // if (zone1.enemies > 0){
                //    cw("You Enounter {zone.Characters ? zone.Character != isPlayer : }")
                // }

            Console.ForegroundColor = ConsoleColor.DarkYellow;
                input = Console.ReadLine();
                Console.ResetColor();
                if (input == "stats")
                {
                    // Show the player's stats if they type 'stats'. Can pass any character object to the function.
                    // TODO implement: pass a target character object to the function to show their stats by prompting the user to type the character name.
                    functions.Stats.Show(player);
                }
            }

        }
    }
}

