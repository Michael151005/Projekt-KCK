public class TankowanieService
{
    public static decimal ObliczKosztTankowania(decimal ilość,decimal cena)
    {
        return ilość * cena;
    }
    public static bool CzyMoznaZatankować(decimal ilość,Paliwo paliwo)
    {
        if(paliwo.DostepnaIloscPaliwa >= ilość && ilość >0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static bool WykonajTankowanie(Paliwo paliwo, decimal ilość)
    {
        if(CzyMoznaZatankować(ilość,paliwo))
        {
            paliwo.DostepnaIloscPaliwa -= ilość;
            return true;
        }
        return false;
    }
}