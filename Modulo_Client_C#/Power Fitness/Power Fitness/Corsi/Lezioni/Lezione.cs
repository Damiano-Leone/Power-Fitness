using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Power_Fitness.Corsi.Lezioni
{
    class Lezione
    {
        // Costanti per le route delle API
        public const string GetLezioneRoute = "GetLezione";
        public const string GetLezioniRoute = "GetLezioni";
        public const string AddLezioneRoute = "AddLezione";
        public const string UpdateLezioneRoute = "UpdateLezione";
        public const string DeleteLezioneRoute = "DeleteLezione";

        // Crea un dizionario non modificabile
        public static ReadOnlyDictionary<string, string> mappaGiorni = new ReadOnlyDictionary<string, string>(new Dictionary<string, string>
        {
            { "Lunedì", "Monday"},
            { "Martedì", "Tuesday"},
            { "Mercoledì", "Wednesday"},
            { "Giovedì", "Thursday"},
            { "Venerdì", "Friday"},
            { "Sabato", "Saturday"},
            { "Domenica", "Sunday"}
        });

        // usiamo i vari attributi di annotazione di Newtonsoft.Json (Json.NET)
        // [JsonProperty("nome_attributo")] per indicare come l'oggetto deve essere serializzato
        // o deserializzato rispetto a un nome specifico nel JSON
        [JsonProperty("id")]
        public int ValoreId { get; set; }

        [JsonProperty("id_corso")]
        public int IdCorso { get; set; }

        [JsonProperty("giorno")]
        public string Giorno { get; set; }

        [JsonProperty("ora_inizio")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime OraInizio { get; set; }

        [JsonProperty("ora_fine")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime OraFine { get; set; }

        [JsonProperty("max_partecipanti")]
        public int MaxPartecipanti { get; set; }


        // Costruttore senza argomenti
        public Lezione()
        {

        }

        public Lezione(int valoreId, int idCorso, string giorno, DateTime oraInizio, DateTime oraFine, int maxPartecipanti)
        {
            ValoreId = valoreId;
            IdCorso = idCorso;
            Giorno = giorno;
            OraInizio = oraInizio;
            OraFine = oraFine;
            MaxPartecipanti = maxPartecipanti;
        }

        public Lezione(int idCorso, string giorno, DateTime oraInizio, DateTime oraFine, int maxPartecipanti)
        {
            IdCorso = idCorso;
            Giorno = giorno;
            OraInizio = oraInizio;
            OraFine = oraFine;
            MaxPartecipanti = maxPartecipanti;
        }
    }

    public class CustomDateTimeConverter : IsoDateTimeConverter
    {
        public CustomDateTimeConverter()
        {
            DateTimeFormat = "HH:mm";
        }
    }
}
