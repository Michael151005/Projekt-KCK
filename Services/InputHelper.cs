using System;

public class InputHelper
{
    public static string PobierzUkrytyTekst()
    {
        string wpisanyTekst = "";

        while (true)
        {
            ConsoleKeyInfo klawisz = Console.ReadKey(true);

            if (klawisz.Key == ConsoleKey.Enter)
            {
                Console.WriteLine();
                break;
            }
            else if (klawisz.Key == ConsoleKey.Backspace)
            {
                if (wpisanyTekst.Length > 0)
                {
                    wpisanyTekst = wpisanyTekst.Substring(0, wpisanyTekst.Length - 1);
                    Console.Write("\b \b");
                }
            }
            else if (!char.IsControl(klawisz.KeyChar))
            {
                wpisanyTekst += klawisz.KeyChar;
                Console.Write("*");
            }
        }

        return wpisanyTekst;
    }
}