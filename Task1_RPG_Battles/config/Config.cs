using System.ComponentModel.Design;
using System.Diagnostics;
using System.Net;
using System.Text.Json;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;
using ADS_A1.objects.Characters;

namespace ADS_A1.functions;
    
public class Config
{
    // This class will read the JSON config file, which holds all default values for each
    // character class stats. This way, stats can easily be changed in one place and is
    // rather then set in the code which will make the game less maintainable
    public static JsonDocument LoadConfig(string path)
    // Return a JsonDocument object, which is a representation of the JSON file which can be
    // TODO is this a valid statement about data structures?
    // used to access the data in the JSON file as an object data structure
    {
        
        try //return null with message if there is an error
        {
            var jsonString = File.ReadAllText(path);
            // use a generic/dynamic type, returning all objects from the JSON
            // this particular method will return an object from the jsonString, transforming
            // serialized text (1 dimensional) to a multidimensional Datastructure (List/Object) 
            return JsonDocument.Parse(jsonString);
            // return JsonSerializer.Deserialize<dynamic>(jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading config file at {path} with message: {e.Message}");
            return null;
        }
    }
    
    public static void SaveConfig(string path, JsonDocument config)
    {
        try
        {
            var jsonString = config.RootElement.GetRawText();
            File.WriteAllText(path, jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error saving config file at {path} with message: {e.Message}");
        }
    }
    
    public static void NewConfig(string path, Character character)
    {
        try
        {
            switch (character)
            {
                case Paladin:
                    AppendConfig(path, new Paladin("Paladin", new PaladinAttributesBuilder()
                        .SetMana(100)
                        .SetMaxMana(100)
                        .SetHolyPower(0)
                        .SetAttack(20)
                        .SetDefense(20)
                        .SetSpeed(10)
                        .SetHealth(120)
                        .SetMaxHealth(100)
                        .SetExperience(0)
                        .SetGold(0)
                        .SetLevel(1)
                        .SetExperienceToNextLevel(100)
                        .Build(), null, false
                    ));
                    break;
                case Warrior:
                    AppendConfig(path, new Warrior("Warrior", new WarriorAttributesBuilder()
                        .SetRage(0)
                        .SetMaxRage(100)
                        .SetAttack(30)
                        .SetDefense(20)
                        .SetSpeed(10)
                        .SetHealth(100)
                        .SetMaxHealth(100)
                        .SetExperience(0)
                        .SetGold(0)
                        .SetLevel(1)
                        .SetExperienceToNextLevel(100)
                        .Build(), null, false
                    ));
                    break;
                case Mage:
                    AppendConfig(path, new Mage("Mage", new MageAttributesBuilder()
                        .SetMana(100)
                        .SetMaxMana(100)
                        .SetRunes(0)
                        .SetAttack(20)
                        .SetDefense(10)
                        .SetSpeed(20)
                        .SetHealth(80)
                        .SetMaxHealth(100)
                        .SetExperience(0)
                        .SetGold(0)
                        .SetLevel(1)
                        .SetExperienceToNextLevel(100)
                        .Build(), null, false
                    ));
                    break;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating new config file for {character} at location {path}. Error: {e.Message}");
        }
    }


    public static void AppendConfig(string path, Character character)
    {
        // This method will create the Character Stats Configuration file to a default state.
        // Create a dictionary to hold the characters and their stats
        Dictionary<string, Dictionary<string, int>> charactersConfig;
        

        // Check if file exists and read existing content
        if (File.Exists(path))
        {
            string existingJson = File.ReadAllText(path);
            
            // enumerate a dictionary of characters from the JSON file to check for duplicates
            // This ternary operation deserializes (parses) existing JSON content to a dictionary (from a string),
            // else if the file is not found, return an empty (nested) dictionary instead
            charactersConfig = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, int>>>(existingJson)
                               ?? new Dictionary<string, Dictionary<string, int>>();
        }
        else
        {
            // if file doesn't exist create a new Dictionary instance to write to a JSON with the Character stats 
            charactersConfig = new Dictionary<string, Dictionary<string, int>>();
        }

            // checking for duplicate characters:
        if(!charactersConfig.ContainsKey(character.Name))
        {
            // if the character does not exist in the file, add the character to the dictionary to write.
            // The stats are converted from the character objects propertues to a dictionary before writing to a serialized JSON file
            charactersConfig.Add(character.Name, Stats.ConvertStatsToDictionary(character));
            Console.WriteLine($"Adding {character.Name} to a new config file");


            // Serialize updated list of characters to write to JSON file
            var jsonString = JsonSerializer.Serialize(charactersConfig);

            // Write updated content to file
            File.WriteAllText(path, jsonString);
            Console.WriteLine($"Created config file at {path}");
        }
    }
}
