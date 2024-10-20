using System.Text.Json;
using ADS_A1.Interfaces.Characters;
using ADS_A1.Interfaces.WorldObjects;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Attributes.builders;
using ADS_A1.objects.Characters;

namespace ADS_A1.functions;

public static class CharacterFactory
{
    static double multiplier;
    static double healthChange;
    static double random = new Random().Next(40, 100);
    static double level;
    public static Character CreateCharacter(string name, string characterType, JsonElement archetypes, IZone zone, double playerLevel, bool isPlayer = false)
    {
          // by using builders functions, i can efficiently add and update stats at whim, while maintatin data integrity
        // of the Attribute objects themselves
        
        JsonElement config = archetypes.GetProperty(characterType);
        if(isPlayer)
        {
            multiplier = 1;
            random = 0;
        }
        else
        {
            multiplier = 0.75;
            level = playerLevel;
        }
        
        return characterType switch
        {
            //create with builder pattern and get property values from the json object document using GetProperty method
            "Warrior" => new Warrior(name, (WarriorAttributes)new WarriorAttributesBuilder()
                .SetRage(config.GetProperty("Rage").GetInt32())
                .SetMaxRage(config.GetProperty("MaxRage").GetInt32())
                .SetHealth(config.GetProperty("Health").GetDouble() * multiplier)
                .SetMaxHealth(config.GetProperty("MaxHealth").GetDouble() * multiplier)
                .SetAttack(config.GetProperty("Attack").GetDouble() * multiplier * multiplier)
                .SetDefense(config.GetProperty("Defense").GetDouble() * multiplier)
                .SetSpeed(config.GetProperty("Speed").GetDouble() * multiplier)
                .SetLevel(config.GetProperty("Level").GetInt32())
                .SetExperience(config.GetProperty("Experience").GetDouble() + random)
                .SetExperienceToNextLevel(config.GetProperty("ExperienceToNextLevel").GetDouble())
                .SetGold(config.GetProperty("Gold").GetDouble() * random)
                .Build(), zone, isPlayer),

            "Paladin" => new Paladin(name, (PaladinAttributes)new PaladinAttributesBuilder()
                .SetMana(config.GetProperty("Mana").GetInt32())
                .SetMaxMana(config.GetProperty("MaxMana").GetInt32())
                .SetHolyPower(config.GetProperty("HolyPower").GetInt32())
                .SetHealth(config.GetProperty("Health").GetDouble() * multiplier)
                .SetMaxHealth(config.GetProperty("MaxHealth").GetDouble() * multiplier)
                .SetAttack(config.GetProperty("Attack").GetDouble() * multiplier)
                .SetDefense(config.GetProperty("Defense").GetDouble() * multiplier)
                .SetSpeed(config.GetProperty("Speed").GetDouble() * multiplier)
                .SetLevel(config.GetProperty("Level").GetInt32())
                .SetExperience(config.GetProperty("Experience").GetDouble() * random)
                .SetExperienceToNextLevel(config.GetProperty("ExperienceToNextLevel").GetDouble() * multiplier)
                .SetGold(config.GetProperty("Gold").GetDouble() * random)
                .Build(), zone, isPlayer),

            "Mage" => new Mage(name, (MageAttributes)new MageAttributesBuilder()
                .SetMana(config.GetProperty("Mana").GetInt32())
                .SetMaxMana(config.GetProperty("MaxMana").GetInt32())
                .SetRunes(config.GetProperty("Runes").GetInt32())
                .SetHealth(config.GetProperty("Health").GetDouble() * multiplier)
                .SetMaxHealth(config.GetProperty("MaxHealth").GetDouble() * multiplier)
                .SetAttack(config.GetProperty("Attack").GetDouble() * multiplier)
                .SetDefense(config.GetProperty("Defense").GetDouble() * multiplier)
                .SetSpeed(config.GetProperty("Speed").GetDouble() * multiplier)
                .SetLevel(config.GetProperty("Level").GetInt32())
                .SetExperience(config.GetProperty("Experience").GetDouble() * random)
                .SetExperienceToNextLevel(config.GetProperty("ExperienceToNextLevel").GetDouble() * multiplier)
                .SetGold(config.GetProperty("Gold").GetDouble() * random)
                .Build(), zone, isPlayer),
            
            "Character" => new Character(name, new CharacterAttributesBuilder()
                .SetHealth(config.GetProperty("Health").GetDouble() * multiplier)
                .SetMaxHealth(config.GetProperty("MaxHealth").GetDouble() * multiplier)
                .SetAttack(config.GetProperty("Attack").GetDouble() * multiplier)
                .SetDefense(config.GetProperty("Defense").GetDouble() * multiplier)
                .SetSpeed(config.GetProperty("Speed").GetDouble() * multiplier)
                .SetLevel(config.GetProperty("Level").GetInt32())
                .SetExperience(config.GetProperty("Experience").GetDouble() * random)
                .SetExperienceToNextLevel(config.GetProperty("ExperienceToNextLevel").GetDouble() * multiplier)
                .SetGold(config.GetProperty("Gold").GetDouble() * random)
                .Build(), zone, isPlayer),
            
            "" => new Character(name, new CharacterAttributesBuilder()
                .SetHealth(config.GetProperty("Health").GetDouble() * multiplier)
                .SetAttack(config.GetProperty("Attack").GetDouble() * multiplier)
                .SetDefense(config.GetProperty("Defense").GetDouble() * multiplier)
                .SetSpeed(config.GetProperty("Speed").GetDouble() * multiplier)
                .SetLevel(config.GetProperty("Level").GetInt32())
                .SetExperience(config.GetProperty("Experience").GetDouble() * random)
                .SetExperienceToNextLevel(config.GetProperty("ExperienceToNextLevel").GetDouble() * multiplier)
                .SetGold(config.GetProperty("Gold").GetDouble() * random)
                .Build(), zone, isPlayer),
                // catch an invalid class/null/empty entry from configuration file before defaulting to Character type
                // ^ default character type if none is specified.
            
            _ => throw new InvalidOperationException($"Error in Initializing class: {characterType}. \tCannot determine this class type")
        };
    }
}
