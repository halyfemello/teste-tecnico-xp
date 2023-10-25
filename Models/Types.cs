using Newtonsoft.Json;

namespace TesteTecnicoXp.Models
{
    public class QueryRequest
    {
        public string SQL { get => "new.json?sql=SELECT * FROM Users"; }
    }

    public class QueryResponse
    {
        public string TrackingId { get; set; }
        public string? TrackingUrl { get; set; }
        public string Status { get; set; }
        public Column[]? Columns { get; set; } = Array.Empty<Column>();
        public string[]? Data { get; set; } = Array.Empty<string>();

        public class Column
        {
            public string Name { get; set; }
            public string Type { get; set; }
        }

        public string ToMetaDadosJson(List<Column> metaDados, string jsonDados)
        {
            var dados = JsonConvert.DeserializeObject<List<List<string>>>(jsonDados);

            var resultadoJson = new List<Dictionary<string, object>>();

            foreach (var dataRow in dados)
            {
                var item = new Dictionary<string, object>();
                
                for (int i = 0; i < metaDados.Count; i++)
                {
                    var nomeColuna = metaDados[i].Name.ToLower();
                    var tipoColuna = metaDados[i].Type.ToUpper();
                    var value = dataRow[i];

                    switch (tipoColuna)
                    {
                        case "INT":
                            item[nomeColuna] = int.Parse(value);
                            break;
                        case "STRING":
                            item[nomeColuna] = value;
                            break;
                        case "BOOLEAN":
                            item[nomeColuna] = bool.Parse(value);
                            break;
                        case "DATETIME":
                            item[nomeColuna] = DateTime.Parse(value);
                            break;
                        default:
                            break;
                    }
                }

                resultadoJson.Add(item);
            }

            return JsonConvert.SerializeObject(resultadoJson);
        }
    }
}