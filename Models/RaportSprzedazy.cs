public class RaportSprzedazy
{
    public int LiczbaTransakcji {get;set;}
    public decimal lacznyPrzychod {get;set;}
    public decimal SprzedaneLitry {get;set;}
    public decimal SprzedaneSztuki {get;set;}
    public Dictionary<string, decimal> SprzedanePaliwa { get; set; } = new Dictionary<string, decimal>();

    public Dictionary<string, decimal> SprzedaneProdukty { get; set; } = new Dictionary<string, decimal>();
}