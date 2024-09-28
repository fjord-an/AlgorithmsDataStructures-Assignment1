using ADS_A1.objects;

namespace ADS_A1
{
    class Program
    {
        static string input;
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Adventure Game!");

            Console.WriteLine("Please choose a character name: ");
            string characterName = Console.ReadLine();
            while (input != "Warrior")
            {
                Console.WriteLine("Please choose a character class: ");
                input = Console.ReadLine();
            }
            Warrior player = new Warrior(characterName, new WarriorAttributes());
                
            Console.WriteLine("Loading World...");
            World world = new World();
            // Create a zone with a random name and description using a NameGenerator method as well as a random string array length.
            Zone zone1 = new Zone(NameGenerator.ZoneName(new Random().Next(3, 12)), NameGenerator.ZoneDescription());
            world.AddZone(zone1);
            
            Console.WriteLine("Hail " + player.Name + ".You are in " + zone1.Name + ", " + zone1.Description);
            Console.WriteLine("You can move to the next zone by typing 'next' or type 'stats' to see your stats.");
            input = Console.ReadLine();
            if (input == "stats")
            {
                Console.WriteLine("Your stats are: ");
                Console.WriteLine("Health: " + player.Attribute.Health);
                Console.WriteLine("Attack: " + player.Attribute.Attack);
                Console.WriteLine("Defense: " + player.Attribute.Defense);
                Console.WriteLine("Speed: " + player.Attribute.Speed);
                Console.WriteLine("Level: " + player.Attribute.Level);
                Console.WriteLine("Experience: " + player.Attribute.Experience);
                Console.WriteLine("Experience to next level: " + player.Attribute.ExperienceToNextLevel);
                Console.WriteLine("Gold: " + player.Attribute.Gold);
                Console.WriteLine("Max Health: " + player.Attribute.MaxHealth);
                Console.WriteLine("Max Attack: " + player.Attribute.MaxAttack);
                Console.WriteLine("Max Defense: " + player.Attribute.MaxDefense);
                Console.WriteLine("Max Speed: " + player.Attribute.MaxSpeed);
                Console.WriteLine("Max Level: " + player.Attribute.MaxLevel);
                Console.WriteLine("Max Experience: " + player.Attribute.MaxExperience);
                Console.WriteLine("Max Experience to next level: " + player.Attribute.MaxExperienceToNextLevel);
            }
        }
    }
}

