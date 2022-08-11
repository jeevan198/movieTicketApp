using System;

namespace MoviePlex
{
    internal class Password
    {
        public Password()
        {
        }
        public void checkPassword()
        {
            // Allow only 5 attempts for worng password else send it to main menu.
            for (int i = 4; i > -1; i--)
            {
                var password = string.Empty;
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("\nPlease Enter Administrator Password or press C to go back to menu : ");

                ConsoleKey key;
                do
                {
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.Backspace && password.Length > 0)
                    {
                        Console.Write("\b \b");
                        password = password[0..^1];
                    }
                    else if (!char.IsControl(keyInfo.KeyChar))
                    {
                        Console.Write("*");
                        password += keyInfo.KeyChar;
                    }
                } while (key != ConsoleKey.Enter);

                if (password == "1234")
                {
                    // if right password entered break the loop.
                    
                    break;
                }
                else if (i == 0)
                {
                    Main main = new Main();
                    main.Welcome();
                }
                else
                {
                    if (password == "c")
                    {
                        Main main = new Main();
                        main.Welcome();
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\nPlease enter correct password");
                    Console.WriteLine("You have {0}", i + " more attempts to enter correct password or press C to go back to menu");
                }
            }
        }
    }
}