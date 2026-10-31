using System;

public class UIHelper
{
    public static void PokazLogo()
    {
        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine(@"
   _____ _______       _____     _          _____ _  __ ____  _      _____ __  __          
  / ____|__   __|/\   / ____|   | |   /\   / ____| |/ // __ \| |    |_   _|  \/  |   /\    
 | (___    | |  /  \ | |      _ | |  /  \ | (___ | ' /| |  | | |      | | | \  / |  /  \   
  \___ \   | | / /\ \| |     | || | / /\ \ \___ \|  < | |  | | |      | | | |\/| | / /\ \  
  ____) |  | |/ ____ \ |____ | || |/ ____ \____) | . \| |__| | |____ _| |_| |  | |/ ____ \ 
 |_____/   |_/_/    \_\_____| \__//_/    \_\_____/_|\_\\____/|______|_____|_|  |_/_/    \_\
                                                                                           
        ");
        Console.ResetColor();
    }

    public static int PokazInteraktywneMenu(string tytul, string[] opcje)
    {
        Console.CursorVisible = false;
        int wybranaOpcja = 0;

        Console.Clear();
        PokazLogo();

        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine($"  {tytul}");
        Console.WriteLine("========================================");
        Console.ResetColor();

        int startY = Console.CursorTop;

        while (true)
        {
            for (int i = 0; i < opcje.Length; i++)
            {
                Console.SetCursorPosition(0, startY + i);
                string tekst = string.IsNullOrWhiteSpace(opcje[i]) ? $"Opcje {i+1}" : opcje[i];

                if (i == wybranaOpcja)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write($"  > {tekst} <  ".PadRight(40));
                    Console.ResetColor();
                }
                else
                {
                    Console.Write($"    {tekst}    ".PadRight(40));
                }
            }

            ConsoleKeyInfo klawisz = Console.ReadKey(true);

            if (klawisz.Key == ConsoleKey.UpArrow)
            {
                wybranaOpcja--;
                if (wybranaOpcja < 0) wybranaOpcja = opcje.Length - 1;
            }
            else if (klawisz.Key == ConsoleKey.DownArrow)
            {
                wybranaOpcja++;
                if (wybranaOpcja >= opcje.Length) wybranaOpcja = 0;
            }
            else if (klawisz.Key == ConsoleKey.Enter)
            {
                Console.Clear();
                Console.CursorVisible = true;
                return wybranaOpcja;
            }
        }
    }

    public static void PokazBlad(string komunikat)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(komunikat);
        Console.ResetColor();
        Console.WriteLine("Naciśnij dowolny klawisz, aby powrócić...");
        Console.ReadKey(true);
    }

    public static void PokazSukces(string komunikat)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine(komunikat);
        Console.ResetColor();
        Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować...");
        Console.ReadKey(true);
    }

    public static void PoczekajNaKlawisz()
    {
        Console.WriteLine();
        Console.WriteLine("Naciśnij dowolny klawisz, aby powrócić do menu...");
        Console.ReadKey(true);
    }
}