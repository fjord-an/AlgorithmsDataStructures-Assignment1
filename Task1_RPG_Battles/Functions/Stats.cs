using System.Reflection;
using ADS_A1.objects;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Characters;

namespace ADS_A1.functions;

public static class Stats
{
    public static void Show(Character character)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine("Your stats are: ");
        Console.ResetColor();
        Console.WriteLine("Health:\n\t\t\t" + character.Attribute.Health);
        Console.WriteLine("Max Health:\n\t\t\t" + character.Attribute.MaxHealth);
        Console.WriteLine("Attack:\n\t\t\t" + character.Attribute.Attack);
        Console.WriteLine("Defense:\n\t\t\t" + character.Attribute.Defense);
        Console.WriteLine("Speed:\n\t\t\t" + character.Attribute.Speed);
        Console.WriteLine("Level:\n\t\t\t" + character.Attribute.Level);
        Console.WriteLine("Experience:\n\t\t\t" + character.Attribute.Experience);
        Console.WriteLine("Experience to next level:\n\t\t\t" + character.Attribute.ExperienceToNextLevel);
        Console.WriteLine("Gold:\n\t\t\t" + character.Attribute.Gold);
        // show class specific stats with the switch statement:
        switch (character)
        {
            //The paladin must come first in the statement because Paladin is a type of warrior,
            // if the character is a paladin the case Warrior: will be true, so the case Paladin: must
            // break first
            case Paladin:
                var paladin = (PaladinAttributes)character.Attribute;
                Console.WriteLine($"Mana:\n\t\t\t{paladin.Mana}");
                Console.WriteLine($"Maximum Mana:\n\t\t\t{paladin.MaxMana}");
                Console.WriteLine($"Holy Power:\n\t\t\t{paladin.HolyPower}");
                break;
            case Warrior:
                var warrior = (WarriorAttributes)character.Attribute;
                // Use typecasting to access the Warrior attributes (Rage).
                Console.WriteLine("Rage:\n\t\t\t" + warrior.Rage);
                Console.WriteLine("Max Rage:\n\t\t\t" + warrior.MaxRage);
                break;
            case Mage:
                var mage = (MageAttributes)character.Attribute;
                Console.WriteLine($"Mana:\n\t\t\t{mage.Mana}");
                Console.WriteLine($"Maximum Mana:\n\t\t\t{mage.MaxMana}");
                Console.WriteLine($"Runes:\n\t\t\t{mage.Runes}");
                break;
        }
    }
    
    public static Dictionary<string, double> ConvertStatsToDictionary(Character character)
    {
        Dictionary<string, double> dict = new();

        foreach (var a in character.Attribute.GetType().GetProperties())
        {
            // iterate through all properties of the character attribute object and convert them to a string
            // Skip the Name property as it is not a stat (it is the character name/identifier)
            // Then add the property to the dictionary to assign to the character object string
            if (a.Name == "Name") continue;
            
            var value = a.GetValue(character.Attribute);
            dict.Add(a.Name, value is null ? 0 : (double)value);
        }

        return dict;
    }
}
    