using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    static async Task Main(string[] args)
    {
        // URL do endpoint da API
        string apiUrl = "https://api.github.com/users/mojombo";

        // Inicializa o HttpClient para fazer a requisição
        using (HttpClient client = new HttpClient())
        {
            // Configura o User-Agent (necessário para consumir a API do GitHub)
            client.DefaultRequestHeaders.Add("User-Agent", "C# Console App");

            try
            {
                // Faz a requisição GET ao endpoint
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();

                // Lê a resposta e converte para string
                string responseBody = await response.Content.ReadAsStringAsync();

                // Converte a resposta JSON em um objeto
                JObject json = JObject.Parse(responseBody);

                // Imprime os dados no console
                Console.WriteLine($"Nome: {json["name"]}");
                Console.WriteLine($"Empresa: {json["company"]}");
                Console.WriteLine($"Localização: {json["location"]}");
                Console.WriteLine($"Login: {json["login"]}");
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("\nErro ao acessar a API:");
                Console.WriteLine(e.Message);
            }
        }
    }
}
