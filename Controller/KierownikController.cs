public class KierownikController
{
    private static List<OpcjaKierownika> opcje = new List<OpcjaKierownika>
    {
      OpcjaKierownika.StanMagazynu,
      OpcjaKierownika.ZmianaCenPaliw,
      OpcjaKierownika.ZmianaCenProduktow,
      OpcjaKierownika.ZarzadzanieProduktami,
      OpcjaKierownika.RaportySprzedazy,
      OpcjaKierownika.ZmianaHasla  
    };
    public static void UruchomPanelKierownika()
    {
        while(true)
        {
            KierownikView.PokazMenu(opcje);
            string wybor = KierownikView.PobierzWybor();
            if(wybor == "0")
            {
                KlientView.PokazKomunikat("Wylogowano kierownika");
                return;
            }
            if(!int.TryParse(wybor,out int numer) || numer < 1 || numer > opcje.Count)
            {
                KierownikView.PokazKomunikat("Nieprawidłowa opcja !");
                continue;
            }
            OpcjaKierownika wybranaOpcja = opcje[numer-1];
            switch(wybranaOpcja)
            {
                case OpcjaKierownika.StanMagazynu:
                    KierownikView.PokazKomunikat("Wybrano stan magazynu");
                    break;
                case OpcjaKierownika.ZmianaCenPaliw:
                    KierownikView.PokazKomunikat("Wybrano zmianę cen paliw");
                    break;
                case OpcjaKierownika.ZmianaCenProduktow:
                    KierownikView.PokazKomunikat("Wybrano zmiannę cen produktów");
                    break;
                case OpcjaKierownika.ZarzadzanieProduktami:
                    KierownikView.PokazKomunikat("Wybrano zarządzanie produktami");
                    break;
                case OpcjaKierownika.RaportySprzedazy:
                    KierownikView.PokazKomunikat("Wybrano raporty sprzedaży");
                    break;
                case OpcjaKierownika.ZmianaHasla:
                    KierownikView.PokazKomunikat("Wybrano zmianę hasła");
                    break;
            }
        }
    }
}