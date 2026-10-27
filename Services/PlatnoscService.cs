public class PlatnoscService
{
    public static bool WykonajPlatnosc(Platnosc platnosc)
    {
        if(platnosc.kwota > 0 && platnosc.CzyOpłacona ==false)
        {
            platnosc.CzyOpłacona = true;
            return true;
        }
        return true;
    }
}