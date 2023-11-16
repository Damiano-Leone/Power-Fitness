using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Power_Fitness.Corsi
{
    class Corso
    {
        // Costanti per le route delle API
        public const string GetCorsoRoute = "GetCorso";
        public const string GetCorsiRoute = "GetCorsi";
        public const string AddCorsoRoute = "AddCorso";
        public const string UpdateCorsoRoute = "UpdateCorso";
        public const string DeleteCorsoRoute = "DeleteCorso";

        // usiamo i vari attributi di annotazione di Newtonsoft.Json (Json.NET)
        // [JsonProperty("nome_attributo")] per indicare come l'oggetto deve essere serializzato
        // o deserializzato rispetto a un nome specifico nel JSON
        [JsonProperty("id")]
        public int ValoreId { get; set; }

        [JsonProperty("nome_corso")]
        public string Nome { get; set; }

        [JsonProperty("data_inizio_corso")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataInizioCorso { get; set; }

        [JsonProperty("data_fine_corso")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataFineCorso { get; set; }

        [JsonProperty("descrizione")]
        public string Descrizione { get; set; }


        // Costruttore senza argomenti
        public Corso()
        {

        }

        public Corso(int valoreId, string nome, DateTime dataInizio, DateTime dataFine, string descrizione)
        {
            ValoreId = valoreId;
            Nome = nome;
            DataInizioCorso = dataInizio;
            DataFineCorso = dataFine;
            Descrizione = descrizione;
        }

        public Corso(string nome, DateTime dataInizio, DateTime dataFine, string descrizione)
        {
            Nome = nome;
            DataInizioCorso = dataInizio;
            DataFineCorso = dataFine;
            Descrizione = descrizione;
        }
    }

    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "yyyy-MM-dd";
        }
    }
}
