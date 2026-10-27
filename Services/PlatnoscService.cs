public class PlatnoscService
{
    public static bool WykonajPlatnosc(Platnosc platnosc)
    {
        if(platnosc.Kwota > 0 && platnosc.CzyOplacona ==false)
        {
            platnosc.CzyOplacona = true;
            return true;
        }
        return false;
    }
}