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
        magazyn.ZapiszPaliwa();
        return true;
    }

    public static bool ZmienCeneProduktu( Produkt produkt, decimal nowaCena, MagazynProduktow magazyn)
    {
        if (nowaCena <= 0)
        {
            return false;
        }
        produkt.Cena = nowaCena;
        magazyn.ZapiszProdukty();
        return true;
    }
}