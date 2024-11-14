using static System.Console;
using System.Net.Http;
using System.Text.Json;
using ConsumerAdviceApi.Models;

public class Program
{
    public static async Task Main()
    {
        WriteLine("Digite o ID do conselho: ");
        var id = ReadLine();

        var url = $@"https://api.adviceslip.com/advice/{id}";

        WriteLine($"Realizando a requisição para {url}...");

        var cliente = new HttpClient();
        cliente.DefaultRequestHeaders.Add("User-Agent", "ConsumerDeConselhos");

        try
        {
            HttpResponseMessage response = await cliente.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string respostaAPI = await response.Content.ReadAsStringAsync();

            var conselho = JsonSerializer.Deserialize<Slip>(respostaAPI, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (conselho?.SlipDetail != null)
            {
                WriteLine("Conselho de Hoje: \n" + conselho.SlipDetail.Advice);
            }
            else
            {
                WriteLine("Conselho não encontrado.");
            }
        }
        catch (HttpRequestException ex) 
        {
            Console.WriteLine($"Ocorreu um erro ao fazer a requisição para {url}:");
            Console.WriteLine($"Erro: {ex.Message}");
        }
        catch (Exception ex) 
        {
            Console.WriteLine("Ocorreu um erro inesperado:");
            Console.WriteLine($"Erro: {ex.Message}");
        }
    }
}
