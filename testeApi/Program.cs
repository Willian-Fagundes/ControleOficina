using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

public class Program
{
    // Reutilize o HttpClient para evitar esgotamento de portas
    private static readonly HttpClient client = new HttpClient();

    public static async Task Main()
    {
        try
        {
            // 1. Enviar solicitação GET
            string responseBody = await client.GetStringAsync("https://brasilapi.com.br/api/fipe/marcas/v1/carros");
            Console.WriteLine(responseBody);

            // 2. Deserializar o JSON (exemplo)
            // var dados = JsonSerializer.Deserialize<MeuModelo>(responseBody);
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Erro na requisição: {e.Message}");
        }
    }
}
