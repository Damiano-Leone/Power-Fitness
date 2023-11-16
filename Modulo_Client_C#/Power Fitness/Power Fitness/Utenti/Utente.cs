using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Power_Fitness.Utenti
{
    class Utente
    {
        // Costanti per le route delle API
        public const string GetUtentiRoute = "GetUsers";
        public const string AddUtenteRoute = "AddUser";
        public const string UpdateUtenteRoute = "UpdateUser";
        public const string DeleteUtenteRoute = "DeleteUser";

        // usiamo i vari attributi di annotazione di Newtonsoft.Json (Json.NET)
        // [JsonProperty("nome_attributo")] per indicare come l'oggetto deve essere serializzato
        // o deserializzato rispetto a un nome specifico nel JSON
        [JsonProperty("id")]
        public int ValoreId { get; set; }

        [JsonProperty("codice_fiscale")]
        public string CodiceFiscale { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("cognome")]
        public string Cognome { get; set; }

        [JsonProperty("genere")]
        public string Genere { get; set; }

        [JsonProperty("data_nascita")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataNascita { get; set; }

        [JsonProperty("cellulare")]
        public string Cellulare { get; set; }

        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("indirizzo")]
        public string Indirizzo { get; set; }

        [JsonProperty("numero_civico")]
        public string NumeroCivico { get; set; }


        // Costruttore senza argomenti
        public Utente()
        {
            
        }

        public Utente(int valoreId, string codiceFiscale, string nome, string cognome, string genere, 
            DateTime dataNascita, string cellulare, string email, string indirizzo, string numeroCivico)
        {
            ValoreId = valoreId;
            CodiceFiscale = codiceFiscale;
            Nome = nome;
            Cognome = cognome;
            Genere = genere;
            DataNascita = dataNascita;
            Cellulare = cellulare;
            Email = email;
            Indirizzo = indirizzo;
            NumeroCivico = numeroCivico;
        }

        public Utente(string codiceFiscale, string nome, string cognome, string genere,
            DateTime dataNascita, string cellulare, string email, string indirizzo, string numeroCivico)
        {
            CodiceFiscale = codiceFiscale;
            Nome = nome;
            Cognome = cognome;
            Genere = genere;
            DataNascita = dataNascita;
            Cellulare = cellulare;
            Email = email;
            Indirizzo = indirizzo;
            NumeroCivico = numeroCivico;
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
