public class SklepService
{
    public static bool CzyMoznaKupic(Produkt produkt, int ilosc)
    {
        if (ilosc <= 0)
        {
            return false;
        }

        if (produkt.DostepnaIlosc < ilosc)
        {
            return false;
        }

        return true;
    }
    
    public static bool WykonajZakup(Produkt produkt, int ilosc)
    {
        if (!CzyMoznaKupic(produkt, ilosc))
        {
            return false;
        }

        produkt.DostepnaIlosc -= ilosc;
        return true;
    }
}