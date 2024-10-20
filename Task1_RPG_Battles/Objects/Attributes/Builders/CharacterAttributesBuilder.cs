using ADS_A1.Interfaces;
using ADS_A1.Interfaces.CharacterAttributes;

namespace ADS_A1.objects.Attributes.builders;

public class CharacterAttributesBuilder
{
    // I have chosen to implement a Builder to construct a Characters Attributes
    // object. In this instance, adding, changing and constructing attributes with 
    // a builder is far more flexable because of the number of properties this 
    // complex object has, and each property can be changed individually
    // whilst decoupling itself and remaining independant so that it can be improved upon later
    // https://www.reddit.com/r/dotnet/comments/1adwyic/factory_vs_builder/
    
    private double _health;
    private double _maxHealth;
    private double _attack;
    private double _defense;
    private double _speed;
    private int _level;
    private double _experience;
    private double _experienceToNextLevel;
    private double _gold;

    public CharacterAttributesBuilder SetHealth(double health)
    {
        _health = health;
        return this;
    }

    public CharacterAttributesBuilder SetMaxHealth(double maxHealth)
    {
        _maxHealth = maxHealth;
        return this;
    }

    public CharacterAttributesBuilder SetAttack(double attack)
    {
        _attack = attack;
        return this;
    }

    public CharacterAttributesBuilder SetDefense(double defense)
    {
        _defense = defense;
        return this;
    }

    public CharacterAttributesBuilder SetSpeed(double speed)
    {
        _speed = speed;
        return this;
    }

    public CharacterAttributesBuilder SetLevel(int level)
    {
        _level = level;
        return this;
    }

    public CharacterAttributesBuilder SetExperience(double experience)
    {
        _experience = experience;
        return this;
    }

    public CharacterAttributesBuilder SetExperienceToNextLevel(double experienceToNextLevel)
    {
        _experienceToNextLevel = experienceToNextLevel;
        return this;
    }

    public CharacterAttributesBuilder SetGold(double gold)
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
    
    
    // these expression bodies doubleerface the private properties of this object
    // to provide read only access to these private properties and allowing
    // the object this class is building safe access to these properties
    // madoubleaining data doubleegrity of this object
    // TODO add a refernce=> further research of builder functions and this implmenetation
    public double Health => _health;
    public double Attack => _attack;
    public double Defense => _defense;
    public double Speed => _speed;
    public int Level => _level;
    public double Experience => _experience;
    public double ExperienceToNextLevel => _experienceToNextLevel;
    public double Gold => _gold;
}