using NUnit.Framework;
using ADS_A1.objects;
using ADS_A1.objects.Attributes;
using ADS_A1.objects.Characters;
using ADS_A1.functions;
using System.Collections.Generic;
using ADS_A1.Interfaces.CharacterAttributes;
using ADS_A1.objects.Attributes.builders;

namespace Tests
{
    [TestFixture]
    public class StatsTests
    {
        [Test]
        public void ConvertToDictionary_ShouldReturnCorrectDictionary()
        {
            // Arrange
            Character character = new Character("jiub", new CharacterAttributesBuilder()
                .SetAttack(10)
                .SetDefense(10)
                .SetExperience(10)
                .SetGold(10)
                .SetHealth(10)
                .SetLevel(11)
                .SetSpeed(10)
                .SetExperienceToNextLevel(10)
                .Build()
            );

            // Act
            Dictionary<string, int> result = Stats.ConvertStatsToDictionary(character);

            // Assert
            Assert.AreEqual(10, result["Health"]);
            Assert.AreEqual(10, result["Attack"]);
            Assert.AreEqual(10, result["Defense"]);
            Assert.AreEqual(10, result["Speed"]);
            Assert.AreEqual(11, result["Level"]);
            Assert.AreEqual(10, result["Experience"]);
            Assert.AreEqual(10, result["ExperienceToNextLevel"]);
            Assert.AreEqual(10, result["Gold"]);
        }
    }
}