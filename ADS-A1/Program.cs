using ADS_A1.objects;

namespace ADS_A1
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Adventure Game!");

            Console.WriteLine("Please choose a character name: ");
            string characterName = Console.ReadLine();
            Console.WriteLine("Please choose a character class: ");
            
            Console.WriteLine("Loading World...");
            World world = new World();
            // Create a zone with a random name and description using a NameGenerator method as well as a random string array length.
            Zone zone1 = new Zone(NameGenerator.ZoneName(new Random().Next(3, 12)), NameGenerator.ZoneDescription());
            world.AddZone(zone1);
            Console.WriteLine("You are in " + zone1.Name + ", " + zone1.Description);
        }
    }
}

