using ADS_A1.objects.attributes.Interfaces;

namespace ADS_A1.objects.Attributes.builders;

public class CharacterAttributesBuilder
{
    // I have chosen to implement a Builder to construct a Characters Attributes
    // object. In this instance, adding, changing and constructing attributes with 
    // a builder is far more flexable because of the number of properties this 
    // complex object has, and each property can be changed individually
    // whilst decoupling itself and remaining independant so that it can be improved upon later
    // https://www.reddit.com/r/dotnet/comments/1adwyic/factory_vs_builder/
    
    private int _health;
    private int _attack;
    private int _defense;
    private int _speed;
    private int _level;
    private int _experience;
    private int _experienceToNextLevel;
    private int _gold;

    public CharacterAttributesBuilder SetHealth(int health)
    {
        _health = health;
        return this;
    }

    public CharacterAttributesBuilder SetAttack(int attack)
    {
        _attack = attack;
        return this;
    }

    public CharacterAttributesBuilder SetDefense(int defense)
    {
        _defense = defense;
        return this;
    }

    public CharacterAttributesBuilder SetSpeed(int speed)
    {
        _speed = speed;
        return this;
    }

    public CharacterAttributesBuilder SetLevel(int level)
    {
        _level = level;
        return this;
    }

    public CharacterAttributesBuilder SetExperience(int experience)
    {
        _experience = experience;
        return this;
    }

    public CharacterAttributesBuilder SetExperienceToNextLevel(int experienceToNextLevel)
    {
        _experienceToNextLevel = experienceToNextLevel;
        return this;
    }

    public CharacterAttributesBuilder SetGold(int gold)
    {
        _gold = gold;
        return this;
    }

    
    public CharacterAttributesBuilder FromCharacterAttributes(ICharacterAttributes attributes)
    {
        _health = attributes.Health;
        _attack = attributes.Attack;
        _defense = attributes.Defense;
        _speed = attributes.Speed;
        _level = attributes.Level;
        _experience = attributes.Experience;
        _experienceToNextLevel = attributes.ExperienceToNextLevel;
        _gold = attributes.Gold;
        return this;
    }
    
    public virtual ICharacterAttributes Build()
    // The Build function must be virtual so that derived classes will not return this (base) object
    {
        return new CharacterAttributes(this);
        
    }
    
    // these expression bodies interface the private properties of this object
    // to provide read only access to these private properties and allowing
    // the object this class is building safe access to these properties
    // maintaining data integrity of this object
    // TODO add a refernce=> further research of builder functions and this implmenetation
        public int Health => _health;
        public int Attack => _attack;
        public int Defense => _defense;
        public int Speed => _speed;
        public int Level => _level;
        public int Experience => _experience;
        public int ExperienceToNextLevel => _experienceToNextLevel;
        public int Gold => _gold;
}