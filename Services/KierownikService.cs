public class KierownikService
{
    public static bool ZmienCenePaliwa (Paliwo paliwo, decimal nowaCena, MagazynPaliw magazyn)
    {
        if(nowaCena <= 0)
        {
            return false;
        }
        decimal poprzedniaCena = paliwo.CenaZaLitr;
        paliwo.CenaZaLitr = nowaCena;
        return true;
    }
}