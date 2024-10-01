using System.Text.Json;

namespace ADS_A1.functions;
    
public class Config
{
    // This class will read the JSON config file, which holds all default values for each
    // character class stats. This way, stats can easily be changed in one place and is
    // rather then set in the code which will make the game less maintainable
    public static JsonDocument LoadConfig(string path)
    // using dynamic means that the return type can be anything, so that this method
    // is flexible, and can adapt to the different character types that the JSON file represents
    {
        try //return null with message if there is an error
        {
            var jsonString = File.ReadAllText(path);
            // use a generic/dynamic type, returning all objects from the JSON
            // this particular method will return an object from the jsonString, transforming
            // serialized text (1 dimensional) to a multidimensional Datastructure (List/Object) 
            return JsonDocument.Parse(jsonString);
            // return JsonSerializer.Deserialize<dynamic>(jsonString);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error loading config file at {path} with message: {e.Message}");
            return null;
        }
    }
}