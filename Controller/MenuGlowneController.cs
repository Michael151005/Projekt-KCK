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
        AutoryzacjaService.ZainicjujHasla();

        while (true)
        {
            string wybor = MenuGlowneView.PobierzWyborMenuGlownego(opcje);

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
                    UIHelper.PokazSukces("Zalogowano jako Klient");
                    KlientController.UruchomPanelKlienta();
                    break;

                case OpcjaMenuGlownego.Pracownik:
                    string hasloPracownika = MenuGlowneView.PobierzHaslo();

                    if (AutoryzacjaService.WeryfikujHaslo("Pracownik", hasloPracownika))
                    {
                        UIHelper.PokazSukces("Zalogowano jako Pracownik stacji");
                        PracownikController.UruchomPanelPracownika();
                    }
                    else
                    {
                        UIHelper.PokazBlad("Błędne hasło! Dostęp odmówiony.");
                    }

                    break;
                    
                case OpcjaMenuGlownego.Kierownik:
                    string hasloKierownika = MenuGlowneView.PobierzHaslo();

                    if (AutoryzacjaService.WeryfikujHaslo("Kierownik", hasloKierownika))
                    {
                        UIHelper.PokazSukces("Zalogowano jako Kierownik stacji");
                        KierownikController.UruchomPanelKierownika();
                    }
                    else
                    {
                        UIHelper.PokazBlad("Błędne hasło! Dostęp odmówiony.");
                    }

                    break;
            }
        }
    }
}

