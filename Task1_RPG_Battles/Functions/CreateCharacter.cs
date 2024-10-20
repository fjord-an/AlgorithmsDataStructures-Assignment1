using System.Text.Json;
using ADS_A1.objects;
using ADS_A1;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;
using ADS_A1.objects.Characters;

namespace ADS_A1.functions;

public static class Create
{
    // Must load the config.json file which holds all of the base stats for each class.
    // Using a centralized config file to set complex objects assists in making the game more 
    // maintainable and balanced. (refer to Config.cs). Stats can be easily updated
    // without needing to change the code. The JSON document is therefore tightly
    // coupled with the CreateCharacter class, as it is a core part of the game's functionality.
    
    // A default config should be generated before creating any characters using AppendConfig() method below.
    
    
    // TODO add JsonDocument Documentation for methods used
    private static string _jsonPath;
    // deserializes the JSON file (Document) to use
    private static JsonDocument _config;
    // get the 'RootElement' of the JSON doc, which holds the Key-Value pairs for the object
    private static JsonElement _archetypes;

    public static void SetConfigPath(string path=".")
    // default value assigned to executable root directory (.) if not passed
    // TODO review complexity and coupling of this method: consider moving to Config.cs
    {
        _jsonPath = path;
        _config = Config.LoadConfig(_jsonPath);
        
        // if the config file does not exist, create a new one with default values to avoid errors at character creation before initialising the config Document
        if(_config is null)
            Config.AppendConfig(_jsonPath, new Character("DefaultCharacter", new CharacterAttributesBuilder().Build(), null, false));
        // load the config file if exists
        _archetypes = _config.RootElement;
    }

    public static Character NewCharacter(string name, string characterType, IZone zone, int level, bool isPlayer = false)
    {
        while (_config is null)
            //check if the value type of config object is null, meaning it does not exist. the is keyword must be used
        // with a logical operator as it is a generic or dynamic type
        {
            try
            {
                _config = Config.LoadConfig(_jsonPath);
                _archetypes = _config.RootElement;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"Character Configuration file cannot be loaded for a {characterType}: {name}. Please ensure the file exists and is correctly formatted.");
                Console.WriteLine("Please enter the correct path to the configuration file or type 'quit' to exit the game.");
                _jsonPath = Console.ReadLine();
            }
        }

        // the character is created by passing the character name, type, and the zone it is in
        // to a factory method that will create the character based on the type and stats in the JSON file
        // use of the factory method allows for easy extension of the game with new character types
        // seperating concerns.
        return CharacterFactory.CreateCharacter(name, characterType, _archetypes, zone, level, isPlayer);
          // by using builders, i can efficiently add and update stats at whim, while maintatin data integrity
        // of the Attribute objects themselves
        }

    public static List<string> ListCharacters()
    {
         List<string> classList = new ();
         // iterate and print all keys (classes) in the json
        foreach (var archetype in _archetypes.EnumerateObject())
        {
            if (archetype.Name != "DefaultCharacter")
                classList.Add(archetype.Name);
        }
        return classList;
    }
}