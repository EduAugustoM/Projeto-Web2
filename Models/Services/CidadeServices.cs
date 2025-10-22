using System.Net.Http.Json;

public class CidadeServices
{
    public HttpClient _client;

    public CidadeServices(HttpClient client)
    {
        _client = client;
    }

    public List<CidadeDTO> GetCidades()
    {
        var resultado = _client.GetAsync("Cidades/GetCidades").Result;
        resultado.EnsureSuccessStatusCode();
        var listaCidade = resultado.Content.ReadFromJsonAsync<List<CidadeDTO>>().Result;
        return listaCidade ?? new List<CidadeDTO>();
    }

    public List<EstadoDTO> GetEstados()
    {
        var resultado = _client.GetAsync("Cidades/GetEstados").Result;
        resultado.EnsureSuccessStatusCode();
        var listaEstados = resultado.Content.ReadFromJsonAsync<List<EstadoDTO>>().Result;
        return listaEstados ?? new List<EstadoDTO>();
    }
}