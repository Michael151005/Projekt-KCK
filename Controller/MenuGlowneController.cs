
public class MenuGlowneController
{
    public static void Uruchom()
    {
        while (true)
        {
            MenuGlowneView.WyswietlMenu();
            string wybranaRola = MenuGlowneView.PobierzWybor();
            switch (wybranaRola)
            {
                case "1":
                    MenuGlowneView.PokazKomunikat(
                        "Zalogowano jako Klient");
                    KlientController.UruchomPanelKlienta();
                    break;
                case "2":
                    MenuGlowneView.PokazKomunikat(
                        "Zalogowano jako Pracownik stacji");
                    break;
                case "3":
                    MenuGlowneView.PokazKomunikat(
                        "Zalogowano jako Kierownik stacji");
                    break;
                case "0":
                    MenuGlowneView.PokazKomunikat(
                        "Dziekujemy za skorzystanie z uslug!");
                    return;
            }
        }
    }
}
