using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using NUnit.Framework;
using ADS_A1.objects.Characters;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes;
using ADS_A1.functions;
using ADS_A1.objects.Attributes.builders;

namespace Tests
{
    public class ConfigTests
    {
        private string _tempFilePath;

        [SetUp]
        public void Setup()
        {
            // Create a temporary file path for testing
            _tempFilePath = Path.Combine(Path.GetTempPath(), "test_characterStats.json");
        }

        [TearDown]
        public void Teardown()
        {
            // Clean up the temporary file after each test
            if (File.Exists(_tempFilePath))
            {
                File.Delete(_tempFilePath);
            }
        }

        [Test]
        public void TestAppendConfig()
        {
            // Initialize test data
            ICharacterAttributes defaultAttributes = new CharacterAttributesBuilder()
                .SetAttack(10)
                .SetDefense(10)
                .SetExperience(10)
                .SetGold(10)
                .SetHealth(10)
                .SetLevel(11)
                .SetSpeed(10)
                .SetExperienceToNextLevel(10)
                .Build();

            ICharacterAttributes warriorAttributes = new WarriorAttributesBuilder()
                .SetRage(10)
                .SetMaxRage(5)
                .SetAttack(10)
                .SetDefense(10)
                .SetExperience(10)
                .SetGold(10)
                .SetHealth(10)
                .SetLevel(11)
                .SetSpeed(10)
                .SetExperienceToNextLevel(10)
                .Build();
            
            var warrior = new Warrior("Warrior", warriorAttributes);
            var bob = new Character("Bob", defaultAttributes);

            // Append characters to the JSON file
            Config.AppendConfig(_tempFilePath, warrior);
            Config.AppendConfig(_tempFilePath, bob);

            // Verify the results
            var jsonString = File.ReadAllText(_tempFilePath);
            var characters = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, int>>>(jsonString);

            Assert.NotNull(characters);
            Assert.AreEqual(2, characters.Count);
            Assert.IsTrue(characters.Any(c => c.Key == "Warrior"));
            Assert.IsTrue(characters.Any(c => c.Key == "Bob"));
            Assert.IsTrue(characters.Any(c => c.Key== "Health"));
            Assert.IsTrue(characters["Warrior"]["Health"] == 10);
        }
    }
}