using TesteTecnicoXp.Models;
using TesteTecnicoXp.Requests;

class Program
{
    static async Task Main(string[] args)
    {
        QueryResponse response = new QueryResponse();

        response = await new Integracao().IntegrarQuery(new QueryRequest().SQL);

        while (true)
        {
            response = await new Integracao().IntegrarQuery(response.TrackingUrl);

            if (response.Status.ToLower().Equals("done"))
            {
                foreach (var item in response.Data)
                {
                    var data = await new Integracao().IntegrarData(item);
                    var resultadoJson = new QueryResponse().ToMetaDadosJson(response.Columns.ToList(), data);
                    Console.WriteLine(resultadoJson);
                }

                break;
            }
        }
    }
}