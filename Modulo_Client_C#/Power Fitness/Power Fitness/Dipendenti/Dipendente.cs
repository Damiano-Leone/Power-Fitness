using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Power_Fitness.Dipendenti
{
    class Dipendente
    {
        // Costanti per le route delle API
        public const string LoginRoute = "Login";
        public const string SiginRoute = "Sigin";
        public const string ValidateRoute = "Validate";
        public const string GetDipendentiRoute = "GetDipendenti";

        // usiamo i vari attributi di annotazione di Newtonsoft.Json (Json.NET)
        // [JsonProperty("nome_attributo")] per indicare come l'oggetto deve essere serializzato
        // o deserializzato rispetto a un nome specifico nel JSON
        [JsonProperty("id")]
        public int ValoreId { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("cognome")]
        public string Cognome { get; set; }

        [JsonProperty("mail")]
        public string Mail { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("ruolo")]
        public string Ruolo { get; set; }

        [JsonProperty("confirmed")]
        public string Confirmed { get; set; }

        // Costruttore senza argomenti
        public Dipendente()
        {

        }

        public Dipendente(int valoreId, string mail,
            string password, string ruolo, string nome, string cognome, string confirmed)
        {
            ValoreId = valoreId;
            Nome = nome;
            Cognome = cognome;
            Mail = mail;
            Password = password;
            Ruolo = ruolo;
            Confirmed = confirmed;
        }

        public Dipendente(int valoreId, string mail,
            string password, string ruolo, string nome, string cognome)
        {
            ValoreId = valoreId;
            Nome = nome;
            Cognome = cognome;
            Mail = mail;
            Password = password;
            Ruolo = ruolo;
        }

        public Dipendente(string mail,
            string password, string ruolo, string nome, string cognome)
        {
            Nome = nome;
            Cognome = cognome;
            Mail = mail;
            Password = password;
            Ruolo = ruolo;
        }

        public Dipendente(string mail, string password)
        {
            Mail = mail;
            Password = password;
        }
    }
}
