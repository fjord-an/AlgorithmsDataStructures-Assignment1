using System.Text.Json;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes;
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
            var jsonString = JsonSerializer.Serialize(character); 
            File.WriteAllText(path, jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error creating new config file for {character} at location {path}. Error: {e.Message}");
        }
    }

    public static void AppendConfig(string path, Character character)
    {
        // Create a dictionary to hold the characters and their stats
        Dictionary<string, Dictionary<string, string>> charactersConfig = new();

        // Check if file exists and read existing content
        if (File.Exists(path))
        {
            var existingJson = File.ReadAllText(path);
            // enumerate a dictionary of characters from the JSON file to check for duplicates
            // This ternary operation deserializes existing JSON content to a dictionary, else if the file is not found, return an empty (nested) dictionary instead
            charactersConfig = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(existingJson) ?? new Dictionary<string, Dictionary<string, string>>();

            // Create instances of each character type
            // ICharacterAttributes defaultAttributes = new CharacterAttributes(); // Assuming a default implementation
            // var warrior = new Warrior("Warrior", defaultAttributes);
            // var paladin = new Paladin("Paladin", defaultAttributes);
            // var character = new Character("Character", defaultAttributes);

            // check for duplicate characters
            foreach (var c in charactersConfig)
                // will make the AppendConfig method 0(n^2) because it is called within a loop but this is fine as the
                // number of characters will be relatively small and the configuration file will only need to be created once
            {
                // compare the key of the dictionary (name of the character type) to the name of the character being added to the config file
                if (c.Key == character.Name || charactersConfig.ContainsKey(c.Key))
                {
                    // if character is in the deserialized Json Object, print message and continue to next character
                    Console.WriteLine($"Character {character.Name} already exists in the config file at {path}");
                    continue;
                }

                // if character is not in the deserialized Json Object add character to the list
                Console.WriteLine($"Adding {c.Key} to config file");
                // TODO uncomment when string conversion is implemented
                charactersConfig.Add(c.Key, c.Value);
            }
        }
        else
        {
            // if file does not exist, create a new file with the character (add character to the Dictionary)
            Console.WriteLine($"Adding {character.Name} to a new config file");
            // TODO uncomment when string conversion is implemented
            charactersConfig.Add(character.Name, Stats.ConvertStatsToDictionary(character));
        }

        // Serialize updated list of characters to write to JSON file
        var jsonString = JsonSerializer.Serialize(charactersConfig);

        // Write updated content to file
        File.WriteAllText(path, jsonString);
        Console.WriteLine($"Created config file at {path}");
    }
}
