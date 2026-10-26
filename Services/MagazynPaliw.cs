public class MagazynPaliw
{
    Paliwo pb98 = new Paliwo();
    Paliwo pb95 = new Paliwo();
    Paliwo ON = new Paliwo();
    Paliwo LPG = new Paliwo();
    List<Paliwo> paliwa = new List<Paliwo>();
    public MagazynPaliw()
    {
        pb98.NazwaPaliwa = "PB 98";
        pb98.CenaZaLitr = 5.19m;
        pb98.DostepnaIloscPaliwa = 5000m;
        pb95.NazwaPaliwa = "PB 95";
        pb95.CenaZaLitr = 5.30m;
        pb95.DostepnaIloscPaliwa = 2137m;
        ON.NazwaPaliwa = "ON";
        ON.CenaZaLitr = 6.10m;
        ON.DostepnaIloscPaliwa = 3500m;
        LPG.NazwaPaliwa = "LPG";
        LPG.CenaZaLitr = 3.10m;
        LPG.DostepnaIloscPaliwa = 1500m;
        paliwa.Add(pb98);
        paliwa.Add(pb95);
        paliwa.Add(ON);
        paliwa.Add(LPG);
    }
    public List<Paliwo> PobierzPaliwa()
    {
        return paliwa;
    }
    
}



