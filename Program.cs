using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("Enter the password type: standard, advanced, apple-keychain, numeric");
        string type = Console.ReadLine();

        int length;
        if (type.ToLower() == "apple-keychain")
        {
            length = 20;
        }
        else
        {
            length = GetLengthFromUser("Enter the password length: ");
        }
        string password = PasswordGenerator(length, type);

        Console.WriteLine($"Password: {password}");
    }

    static string PasswordGenerator(int length, string type)
    {
        string password = "";
        string chars = "";

        Random rnd = new Random();

        switch (type.ToLower())
        {
            case "standard":
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                break;
            case "apple-keychain":
                break;
            case "advanced":
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*";
                break;
            case "numeric":
                chars = "0123456789";
                break;
            default:
                Console.WriteLine("Unknown type, using standard.");
                chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
                break;
        }

        if (type.ToLower() == "apple-keychain")
        {
            password = GenerateAppleStyle();
        }
        else
        {
            for (int i = 0; i < length; i++)
            {
                password += chars[rnd.Next(0, chars.Length)];
            }
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

    static string GenerateAppleStyle() // Apple keychain passwords exclude similar looking characters (e.g. O and 0 or I and l), 20 characters long, 2 dashes, 1 capital letter, and 1 number
    {
        string password = "";

        string allowed_lowercase = "abcdefghjkmnpqrstuvwxyz";
        string allowed_uppercase = "ABCDEFGHJKMNPQRSTUVWXYZ";
        string allowed_digits = "123456789";

        Random rnd = new Random();

        List<char> password_chars = new List<char>();
        
        char random_uppercase = allowed_uppercase[rnd.Next(0, allowed_uppercase.Length)];
        char random_digit = allowed_digits[rnd.Next(0, allowed_digits.Length)];
        password_chars.Add(random_uppercase); 
        password_chars.Add(random_digit);

        for (int i = 0; i < 16; i++)
        {
            password_chars.Add(allowed_lowercase[rnd.Next(0, allowed_lowercase.Length)]);
        }
        
        for (int i = password_chars.Count - 1; i > 0; i--) // Fisher-Yates shuffle so the required uppercase and digit don't appear first
        {
            int j = rnd.Next(i + 1);
            char temp = password_chars[i];
            password_chars[i] = password_chars[j];
            password_chars[j] = temp;
        }
        
        string chunk1 = new string(password_chars.GetRange(0, 6).ToArray());
        string chunk2 = new string(password_chars.GetRange(6, 6).ToArray());
        string chunk3 = new string(password_chars.GetRange(12, 6).ToArray());

        password = chunk1 + "-" + chunk2 + "-" + chunk3;

        return password;
    }
}