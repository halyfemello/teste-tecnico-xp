using Newtonsoft.Json;
using TesteTecnicoXp.Models;

namespace TesteTecnicoXp.Requests
{
    public class Integracao
    {
        private readonly HttpClient HttpClient;
        private string BaseAddress = "https://gist.githubusercontent.com/tiago-soczek/31ee2a28fbd357b578d3b2254ec9847a/raw/";

        public Integracao()
        {
            HttpClient = new HttpClient();
        }

        public async Task<string> IntegrarBase(string metodo)
        {
            try
            {
                var response = await HttpClient.GetAsync($"{BaseAddress}{metodo}");

                if (response.IsSuccessStatusCode)
                    return await response.Content.ReadAsStringAsync();

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao realizar integração query: {ex.Message}");
            }

            return "";
        }

        public async Task<QueryResponse> IntegrarQuery(string metodo)
        {
            try
            {
                return JsonConvert.DeserializeObject<QueryResponse>(await IntegrarBase(metodo));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao realizar integração query: {ex.Message}");
            }

            return new QueryResponse();
        }

        public async Task<string> IntegrarData(string metodo)
        {
            try
            {
                return await IntegrarBase(metodo);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao realizar integração data: {ex.Message}");
            }

            return "";
        }
    }
}