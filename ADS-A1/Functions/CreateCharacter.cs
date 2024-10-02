using System.Text.Json;
using ADS_A1.objects;
using ADS_A1;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;
using ADS_A1.objects.Characters;

namespace ADS_A1.functions;

public class Create
{
    // Must load the config.json file which holds all of the base stats for each class.
    // Using a config file to set complex objects makes the game/code more clean and
    // maintainable (refer to Config.cs)
    
    //can pass config path in optionally
    
    // TODO add JsonDocument Documentation for methods used
    private static string _jsonPath = "config.json";
    // deserialises the JSON file to use
    private static readonly JsonDocument _config = Config.LoadConfig(_jsonPath);
    // get the 'RootElement' of the JSON doc, which holds the Key-Value pairs for the object
    private static readonly JsonElement _archetypes = _config.RootElement;
        
    public static void SetConfigPath(string path)
    // default value assigned to executable root location (.) if not passed
    {
        _jsonPath = path;
    }

    public static Character NewCharacter(string name, string characterType)
    {
        if (_config is null) 
            //check if the value type of config object is null, meaning it does not exist. the is keyword must be used
        // with a logical operator as it is a generic or dynamic type
        {
            throw new InvalidOperationException(
                $"Character/Class Configuration file cannot be loaded for a {characterType}: {name}. Refer to the config/ directory");
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
                    .SetAttack(warriorConfig.GetProperty("Attack").GetInt32())
                    .SetDefense(warriorConfig.GetProperty("Defense").GetInt32())
                    .SetSpeed(warriorConfig.GetProperty("Speed").GetInt32())
                    .SetLevel(warriorConfig.GetProperty("Level").GetInt32())
                    .SetExperience(warriorConfig.GetProperty("Experience").GetInt32())
                    .SetExperienceToNextLevel(warriorConfig.GetProperty("ExperienceToNextLevel").GetInt32())
                    .SetGold(warriorConfig.GetProperty("Gold").GetInt32())
                    .Build()
                );
            case "Paladin":
                JsonElement paladinConfig = _archetypes.GetProperty("Paladin");
                return new Paladin(name, (PaladinAttributes) new PaladinAttributesBuilder()
                    .SetMana(paladinConfig.GetProperty("Mana").GetInt32())
                    .SetMaxMana(paladinConfig.GetProperty("MaxMana").GetInt32())
                    .SetHolyPower(paladinConfig.GetProperty("HolyPower").GetInt32())
                    .SetHealth(paladinConfig.GetProperty("Health").GetInt32())
                    .SetAttack(paladinConfig.GetProperty("Attack").GetInt32())
                    .SetDefense(paladinConfig.GetProperty("Defense").GetInt32())
                    .SetSpeed(paladinConfig.GetProperty("Speed").GetInt32())
                    .SetLevel(paladinConfig.GetProperty("Level").GetInt32())
                    .SetExperience(paladinConfig.GetProperty("Experience").GetInt32())
                    .SetExperienceToNextLevel(paladinConfig.GetProperty("ExperienceToNextLevel").GetInt32())
                    .SetGold(paladinConfig.GetProperty("Gold").GetInt32())
                    .Build()
                );
            case "Mage":
                JsonElement mageConfig = _archetypes.GetProperty("Mage");
                return new Mage(name, (MageAttributes) new MageAttributesBuilder()
                    .SetMana(mageConfig.GetProperty("Mana").GetInt32())
                    .SetMaxMana(mageConfig.GetProperty("MaxMana").GetInt32())
                    .SetRunes(mageConfig.GetProperty("Runes").GetInt32())
                    .SetHealth(mageConfig.GetProperty("Health").GetInt32())
                    .SetAttack(mageConfig.GetProperty("Attack").GetInt32())
                    .SetDefense(mageConfig.GetProperty("Defense").GetInt32())
                    .SetSpeed(mageConfig.GetProperty("Speed").GetInt32())
                    .SetLevel(mageConfig.GetProperty("Level").GetInt32())
                    .SetExperience(mageConfig.GetProperty("Experience").GetInt32())
                    .SetExperienceToNextLevel(mageConfig.GetProperty("ExperienceToNextLevel").GetInt32())
                    .SetGold(mageConfig.GetProperty("Gold").GetInt32())
                    .Build()
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
                    .Build()
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