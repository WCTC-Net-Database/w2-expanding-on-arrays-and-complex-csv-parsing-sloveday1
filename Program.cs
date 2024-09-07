using System;
using System.IO;

class Program
{
    static string[] lines;

    static void Main()
    {
        string filePath = "input.csv";
        lines = File.ReadAllLines(filePath);

        while (true)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. Display Characters");
            Console.WriteLine("2. Add Character");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayAllCharacters(lines);
                    break;
                case "2":
                    AddCharacter(ref lines);
                    break;
                case "3":
                    LevelUpCharacter(lines);
                    break;
                case "4":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void DisplayAllCharacters(string[] lines)
    {
        
        // Skip the header row
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            string name;
            int commaIndex = line.IndexOf(",");

            // Check if the name is quoted
            if (line.StartsWith("\""))
            {
                // TODO: Find the closing quote and the comma right after it
                var firstQuote = line.IndexOf("\"");
                line = line.Substring(firstQuote + 1);
                var lastQuote = line.IndexOf("\"");
                commaIndex = line.IndexOf(",", lastQuote);
                // TODO: Remove quotes from the name if present and parse the name
                name = line.Substring(0, commaIndex-1);
            }
            else
            {
                // TODO: Name is not quoted, so store the name up to the first comma
                name = line.Substring(0, commaIndex);
            }

            // TODO: Parse characterClass, level, hitPoints, and equipment
            string[] fields = line.Split(',');
            string characterClass = fields[fields.Length - 4];
            
            int level = Convert.ToInt32(fields[fields.Length - 3]); 
            
            int hitPoints = Convert.ToInt32(fields[fields.Length - 2]);
            
            
            // TODO: Parse equipment noting that it contains multiple items separated by '|'
            string[] equipment = fields[fields.Length - 1].Split("|");
            

            // Display character information
            Console.WriteLine($"Name: {name}, Class: {characterClass}, Level: {level}, HP: {hitPoints}, Equipment: {string.Join(", ", equipment)}");
        }
    }

    static void AddCharacter(ref string[] lines)
    {
        // TODO: Implement logic to add a new character
        // Prompt for character details (name, class, level, hit points, equipment)
        // DO NOT just ask the user to enter a new line of CSV data or enter the pipe-separated equipment string
        // Append the new character to the lines array
        Console.WriteLine("What is the characters name:");
        string name = Console.ReadLine();
        Console.WriteLine("What is the characters class:");
        string characterClass = Console.ReadLine();
        Console.WriteLine("What is the characters level:");
        int level = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("What is the characters hit points:");
        int hitPoints = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Equipment 1:");
        string equipment1 = Console.ReadLine();
        Console.WriteLine("Equipment 2:");
        string equipment2 = Console.ReadLine();
        Console.WriteLine("Equipment 3:");
        string equipment3 = Console.ReadLine();

        string character = $"{name},{characterClass},{level},{hitPoints},{equipment1}|{equipment2}|{equipment3}";

        Array.Resize(ref lines, lines.Length + 1);
        lines[lines.Length-1] = character;
    }

    static void LevelUpCharacter(string[] lines)
    {
        Console.Write("Enter the name of the character to level up: ");
        string nameToLevelUp = Console.ReadLine();

        // Loop through characters to find the one to level up
        for (int i = 1; i < lines.Length; i++)
        {
            string line = lines[i];

            // TODO: Check if the name matches the one to level up
            // Do not worry about case sensitivity at this point
            if (line.Contains(nameToLevelUp))
            {

                // TODO: Split the rest of the fields locating the level field
                string[] fields = line.Split(',');
                int level = Convert.ToInt32(fields[fields.Length - 3]);
                string name = line.Substring(1, line.IndexOf(","));

                // TODO: Level up the character
                level++;
                Console.WriteLine($"Character {name} leveled up to level {level}!");
                fields[fields.Length - 3] = Convert.ToString(level);

                // TODO: Update the line with the new level
                string character = String.Join(",", fields);
                lines[i] = character;
                break;
            }
        }
    }
}
