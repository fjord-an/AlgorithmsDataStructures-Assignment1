namespace ADS_A1.objects;

public class CharacterAttributes
{
    public string Name { get; private set; }
    public int Health { get; private set; }
    public int Attack { get; private set; }
    public int Defense { get; private set; }
    public int Speed { get; private set; }
    public int Level { get; private set; }
    public int Experience { get; private set; }
    public int ExperienceToNextLevel { get; private set; }
    public int Gold { get; private set; }
    public int MaxHealth { get; private set; }
    public int MaxAttack { get; private set; }
    public int MaxDefense { get; private set; }
    public int MaxSpeed { get; private set; }
    public int MaxLevel { get; private set; }
    public int MaxExperience { get; private set; }
    public int MaxExperienceToNextLevel { get; private set; }
    public int MaxGold { get; private set; }

    public CharacterAttributes(int health, int attack, int defense, int speed, int level, int experience, int experienceToNextLevel, int gold)
    {
        Health = health;
        Attack = attack;
        Defense = defense;
        Speed = speed;
        Level = level;
        Experience = experience;
        ExperienceToNextLevel = experienceToNextLevel;
        Gold = gold;
        MaxHealth = health;
        MaxAttack = attack;
        MaxDefense = defense;
        MaxSpeed = speed;
        MaxLevel = level;
        MaxExperience = experience;
        MaxExperienceToNextLevel = experienceToNextLevel;
        MaxGold = gold;
    }

    public void LevelUp()
    {
        Level++;
        Experience = 0;
        ExperienceToNextLevel = ExperienceToNextLevel * 2;
        MaxHealth += 10;
        MaxAttack += 5;
        MaxDefense += 5;
        MaxSpeed += 5;
        MaxLevel++;
        MaxExperience += 100;
        MaxExperienceToNextLevel += 50;
        MaxGold += 50;
    }

    public void GainExperience(int experience)
    {
        Experience += experience;
        if (Experience >= ExperienceToNextLevel)
        {
            LevelUp();
        }
    }

    public void GainGold(int gold)
    {
        Gold += gold;
    }

    public void TakeDamage(int damage)
    {
        Health -= damage;
    }
}

public class WarriorAttributes : CharacterAttributes
{
    public WarriorAttributes() : base(100, 10, 5, 5, 1, 0, 100, 0)
    {
    }
}