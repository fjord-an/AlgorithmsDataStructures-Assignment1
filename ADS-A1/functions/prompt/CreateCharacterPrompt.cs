using ADS_A1.objects.Characters;

namespace ADS_A1.functions.prompt;

public class CreateCharacterPrompt
{
    public static Character Prompt()
    {
        Console.WriteLine("Please choose a character name: ");
        string characterName = Console.ReadLine();
        
        // Create class. loop until the user input matches a valid class in the config.json
        // TODO consider replacing this method with actual types for code safety
        var archetypeList = Create.ListCharacters();
        string classSelection;
        do
        {
            Console.WriteLine("Please Choose a character class! Type one of the classes below to choose:");
            
            foreach (var archetype in Create.ListCharacters())
            {
                Console.Write(archetype + " ");
            }

            Console.WriteLine();
            classSelection = Console.ReadLine();

            if (!archetypeList.Contains(classSelection))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid class selection. Please Try again");
                Console.ResetColor();
            }
        } while (!archetypeList.Contains(classSelection));

        Character player = Create.NewCharacter(characterName, classSelection);

        return player;
    }
}