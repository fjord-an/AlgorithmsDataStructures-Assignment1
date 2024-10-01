using ADS_A1.objects;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Characters;

namespace ADS_A1.functions;

public class Stats
{
    public static void Show(Character character)
    {
        Console.WriteLine("Your stats are: ");
        Console.WriteLine("Health: " + character.Attribute.Health);
        Console.WriteLine("Attack: " + character.Attribute.Attack);
        Console.WriteLine("Defense: " + character.Attribute.Defense);
        Console.WriteLine("Speed: " + character.Attribute.Speed);
        Console.WriteLine("Level: " + character.Attribute.Level);
        Console.WriteLine("Experience: " + character.Attribute.Experience);
        Console.WriteLine("Experience to next level: " + character.Attribute.ExperienceToNextLevel);
        Console.WriteLine("Gold: " + character.Attribute.Gold);
        if (character is Warrior)
        {
            var warrior = (WarriorAttributes)character.Attribute;
            // Use typecasting to access the Warrior attributes (Rage).
            Console.WriteLine("Rage: " + warrior.Rage);
            Console.WriteLine("Max Rage: " + warrior.MaxRage);
        }
    }
}
    