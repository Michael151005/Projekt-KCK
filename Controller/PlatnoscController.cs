public class PlatnośćController
{
    public static void UruchomPlatność()
    {
        PlatnoscView.PokazMenuPlatnosci();
        string wybór = PlatnoscView.PobierzWybórPłatności();
        switch(wybór)
        {
            case "1":
                Console.WriteLine("Wybrano płatność gotówką");
                break;
            case "2":
                Console.WriteLine("Wybrano płatność kartą");
                break;
            case "3":
                Console.WriteLine("Wybrano płatność BLIKIEM");
                break;
            case "0":
                Console.WriteLine("Powrót do koszyka");
                break;   
        }
    }
    
}