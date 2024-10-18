using System.Text.Json;
using ADS_A1.objects;
using ADS_A1;
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
            Config.AppendConfig(_jsonPath, new Character("DefaultCharacter", new CharacterAttributesBuilder().Build(), false));
        // load the config file if exists
        _archetypes = _config.RootElement;
    }

    public static Character NewCharacter(string name, string characterType, bool isPlayer = false)
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

          // by using builders, i can efficiently add and update stats at whim, while maintatin data integrity
        // of the Attribute objects themselves
        switch (characterType)
        {
            // cleanest method of checking which type (class) the character is and assigning the correct stats according to the characters class
            case "Warrior":
                // The character objects requires a name (set by the users) and a CharacterAttributes object. This attributes object is 
                // responsible for maintaining all game characters stats. A builder object initializes and changes these stats, acting as the middle man
                // to change the stats to protect the attirbute objects integrity, and allowing for future expansion of the objects. Builders provide
                // flexibility to complex objects like a roleplaying games stat system, which has a large number of properties that change radidly.
                // this is why i have adopted this object heriarchy
                // TODO add a source for futher research of builder and stat system
                JsonElement warriorConfig = _archetypes.GetProperty("Warrior");
                // These Class archetype character objects are following an object builder design pattern to initialise these objects because
                // they have many properties (stats). The builder object returns the desired object (CharacterAttributes object) the CharacterAttributes
                // object must be cast to the desired overriding derived object otherwise the base object type will be returned. 
                return new Warrior(name, (WarriorAttributes) new WarriorAttributesBuilder()
                    .SetRage(warriorConfig.GetProperty("Rage").GetInt32())
                    .SetMaxRage(warriorConfig.GetProperty("MaxRage").GetInt32())
                    .SetHealth(warriorConfig.GetProperty("Health").GetInt32())
                    .SetMaxHealth(warriorConfig.GetProperty("MaxHealth").GetInt32())
                    .SetAttack(warriorConfig.GetProperty("Attack").GetInt32())
                    .SetDefense(warriorConfig.GetProperty("Defense").GetInt32())
                    .SetSpeed(warriorConfig.GetProperty("Speed").GetInt32())
                    .SetLevel(warriorConfig.GetProperty("Level").GetInt32())
                    .SetExperience(warriorConfig.GetProperty("Experience").GetInt32())
                    .SetExperienceToNextLevel(warriorConfig.GetProperty("ExperienceToNextLevel").GetInt32())
                    .SetGold(warriorConfig.GetProperty("Gold").GetInt32())
                    .Build(), isPlayer
                );
            case "Paladin":
                JsonElement paladinConfig = _archetypes.GetProperty("Paladin");
                return new Paladin(name, (PaladinAttributes) new PaladinAttributesBuilder()
                    .SetMana(Convert.ToInt32(paladinConfig.GetProperty("Mana").GetInt32()))
                    .SetMaxMana(paladinConfig.GetProperty("MaxMana").GetInt32())
                    .SetHolyPower(paladinConfig.GetProperty("HolyPower").GetInt32())
                    .SetHealth(paladinConfig.GetProperty("Health").GetInt32())
                    .SetMaxHealth(paladinConfig.GetProperty("MaxHealth").GetInt32())
                    .SetAttack(paladinConfig.GetProperty("Attack").GetInt32())
                    .SetDefense(paladinConfig.GetProperty("Defense").GetInt32())
                    .SetSpeed(paladinConfig.GetProperty("Speed").GetInt32())
                    .SetLevel(paladinConfig.GetProperty("Level").GetInt32())
                    .SetExperience(paladinConfig.GetProperty("Experience").GetInt32())
                    .SetExperienceToNextLevel(paladinConfig.GetProperty("ExperienceToNextLevel").GetInt32())
                    .SetGold(paladinConfig.GetProperty("Gold").GetInt32())
                    .Build(), isPlayer
                );
            case "Mage":
                JsonElement mageConfig = _archetypes.GetProperty("Mage");
                return new Mage(name, (MageAttributes) new MageAttributesBuilder()
                    .SetMana(mageConfig.GetProperty("Mana").GetInt32())
                    .SetMaxMana(mageConfig.GetProperty("MaxMana").GetInt32())
                    .SetRunes(mageConfig.GetProperty("Runes").GetInt32())
                    .SetHealth(mageConfig.GetProperty("Health").GetInt32())
                    .SetMaxHealth(mageConfig.GetProperty("MaxHealth").GetInt32())
                    .SetAttack(mageConfig.GetProperty("Attack").GetInt32())
                    .SetDefense(mageConfig.GetProperty("Defense").GetInt32())
                    .SetSpeed(mageConfig.GetProperty("Speed").GetInt32())
                    .SetLevel(mageConfig.GetProperty("Level").GetInt32())
                    .SetExperience(mageConfig.GetProperty("Experience").GetInt32())
                    .SetExperienceToNextLevel(mageConfig.GetProperty("ExperienceToNextLevel").GetInt32())
                    .SetGold(mageConfig.GetProperty("Gold").GetInt32())
                    .Build(), isPlayer 
                );
            case null:
                // catch an invalid class/null entry from configuration file before defaulting to Character type
                throw new InvalidOperationException($"Error in Initializing class: {characterType}. \tCannot determine this class type");
            default:
                // default character type if none is specified.
                JsonElement characterConfig = _archetypes.GetProperty("DefaultCharacter");
                return new Character(name, new CharacterAttributesBuilder()
                    .SetHealth(characterConfig.GetProperty("Health").GetInt32())
                    .SetAttack(characterConfig.GetProperty("Attack").GetInt32())
                    .SetDefense(characterConfig.GetProperty("Defense").GetInt32())
                    .SetSpeed(characterConfig.GetProperty("Speed").GetInt32())
                    .SetLevel(characterConfig.GetProperty("Level").GetInt32())
                    .SetExperience(characterConfig.GetProperty("Experience").GetInt32())
                    .SetExperienceToNextLevel(characterConfig.GetProperty("ExperienceToNextLevel").GetInt32())
                    .SetGold(characterConfig.GetProperty("Gold").GetInt32())
                    .Build(), isPlayer
                );
        }
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

    public static void Prompt()
    {
        
    }
    
}