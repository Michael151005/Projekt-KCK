using System.Text.Json;
public class MagazynProduktow
{
    private List<Produkt> produkty;
    public MagazynProduktow()
    {
        string sciezka = Path.Combine(AppContext.BaseDirectory,"Data","produkty.json");
        string json = File.ReadAllText(sciezka);
        produkty = JsonSerializer.Deserialize<List<Produkt>>(json)?? new List<Produkt>();
    }
    public List<Produkt> PobierzProdukty()
    {
        return produkty;
    }
}