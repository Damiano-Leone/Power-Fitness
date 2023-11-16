using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Power_Fitness.Utenti.Prenotazioni
{
    class Prenotazione
    {
        // Costanti per le route delle API
        public const string GetPrenotazioniUserRoute = "GetPrenotazioniUser";
        public const string GetPrenotazioniRoute = "GetPrenotazioni";
        public const string AddPrenotazioneRoute = "AddPrenotazione";
        public const string UpdatePrenotazioneRoute = "UpdatePrenotazione";
        public const string DeletePrenotazioneRoute = "DeletePrenotazione";

        // usiamo i vari attributi di annotazione di Newtonsoft.Json (Json.NET)
        // [JsonProperty("nome_attributo")] per indicare come l'oggetto deve essere serializzato
        // o deserializzato rispetto a un nome specifico nel JSON
        [JsonProperty("id")]
        public int ValoreId { get; set; }

        [JsonProperty("id_lezione")]
        public int IdLezione { get; set; }

        [JsonProperty("id_user")]
        public int IdUtente { get; set; }

        [JsonProperty("data")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime Data { get; set; }


        // Costruttore senza argomenti
        public Prenotazione()
        {

        }

        public Prenotazione(int valoreId, int idLezione, int idUtente, DateTime data)
        {
            ValoreId = valoreId;
            IdLezione = idLezione;
            IdUtente = idUtente;
            Data = data;
        }

        public Prenotazione(int idLezione, int idUtente, DateTime data)
        {
            IdLezione = idLezione;
            IdUtente = idUtente;
            Data = data;
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