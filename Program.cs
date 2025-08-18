using System;

class Program
{
    static string PasswordGenerator(int length)
    {
        string password = "";
        string allowedCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        Random rnd = new Random();

        for (int i = 0; i < length; i++)
        {
            password += allowedCharacters[rnd.Next(0, allowedCharacters.Length)];
        }

        return password;
    }
    static int GetLengthFromUser(string prompt)
    {
        while (true)
        {
            Console.WriteLine(prompt);
            string input = Console.ReadLine();

            if (int.TryParse(input, out int value))
            {
                return value; // Give back the value as in integer to the length variable
            }
            else
            {
                Console.WriteLine("Invalid number. Please try again."); // Go back to top of loop
            }
        }
    }

    static void Main()
    {
        int length = GetLengthFromUser("Enter the password length:");
        Console.WriteLine($"\nPassword length entered: {length}");
        string password = PasswordGenerator(length);
        Console.WriteLine($"Password: {password}");
    }
}