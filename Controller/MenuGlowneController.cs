public class MenuGlowneController
{
    private static readonly List<OpcjaMenuGlownego> opcje = new()
    {
        OpcjaMenuGlownego.Klient,
        OpcjaMenuGlownego.Pracownik,
        OpcjaMenuGlownego.Kierownik
    };

    public static void Uruchom()
    {
        while (true)
        {
            MenuGlowneView.WyswietlMenu(opcje);
            string wybor = MenuGlowneView.PobierzWybor();

            if (wybor == "0")
            {
                MenuGlowneView.PokazKomunikat("Dziękujemy za skorzystanie z usług!");
                return;
            }

            if (!int.TryParse(wybor, out int numer) || numer < 1 || numer > opcje.Count)
            {
                MenuGlowneView.PokazKomunikat("Niepoprawna opcja! Wybierz ponownie.");
                continue;
            }

            OpcjaMenuGlownego wybranaOpcja = opcje[numer - 1];
            switch (wybranaOpcja)
            {
                case OpcjaMenuGlownego.Klient:
                    MenuGlowneView.PokazKomunikat("Zalogowano jako Klient");
                    KlientController.UruchomPanelKlienta();
                    break;
                case OpcjaMenuGlownego.Pracownik:
                    MenuGlowneView.PokazKomunikat("Zalogowano jako Pracownik stacji");
                    break;
                case OpcjaMenuGlownego.Kierownik:
                    MenuGlowneView.PokazKomunikat("Zalogowano jako Kierownik stacji");
                    break;
            }
        }
    }
}

