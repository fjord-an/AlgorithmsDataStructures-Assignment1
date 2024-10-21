namespace ADS_A1.Functions;

class NameGenerator
{
    // This function (ZoneName) was taken from a User's answer to a stack overflow question. The Random Name generator was provided by:
    // Khan, W. (2013, February 4). Random name generator in c# [Forum post]. Stack Overflow. https://stackoverflow.com/q/14687658
    public static string ZoneName(int len)
    {
        Random r = new Random();
        string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
        string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
        string Name = "";
        Name += consonants[r.Next(consonants.Length)].ToUpper();
        Name += vowels[r.Next(vowels.Length)];
        int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
        while (b < len)
        {
            Name += consonants[r.Next(consonants.Length)];
            b++;
            Name += vowels[r.Next(vowels.Length)];
            b++;
        }
        return Name;
    }

    public static string ZoneDescription()
    {
        string[] descriptions = { "A dark and spooky forest", "A bright and sunny meadow", "A cold and icy mountain", "A hot and sandy desert", "A wet and rainy swamp", "A dry and dusty wasteland", "A rocky and barren canyon", "A lush and green jungle", "A bright and colourful garden", "A dark and gloomy cave", "A bright and sunny beach", "A cold and icy tundra" };
        return descriptions[new Random().Next(descriptions.Length)];
    }
}
