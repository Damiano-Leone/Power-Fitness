using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Power_Fitness.Abbonamenti
{
    class Abbonamento
    {
        // Costanti per le route delle API
        public const string GetAbbonamentiRoute = "GetAbbonamenti";
        public const string GetAbbonamentoAttivoRoute = "GetAbbonamentoAttivo";
        public const string AddAbbonamentoRoute = "AddAbbonamento";
        public const string RinnovaAbbonamentoRoute = "RinnovaAbbonamento";
        public const string UpdateAbbonamentoRoute = "UpdateAbbonamento";
        public const string DeleteAbbonamentoRoute = "DeleteAbbonamento";
        public const string GetDatiRoute = "getDati";


        // usiamo i vari attributi di annotazione di Newtonsoft.Json (Json.NET)
        // [JsonProperty("nome_attributo")] per indicare come l'oggetto deve essere serializzato
        // o deserializzato rispetto a un nome specifico nel JSON
        [JsonProperty("id")]
        public int ValoreId { get; set; }

        [JsonProperty("id_utente")]
        public string CodiceFiscale { get; set; }

        [JsonProperty("data_inizio")]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime DataIscrizione { get; set; }

        [JsonProperty("durata_abbonamento")]
        public string DurataAbbonamento { get; set; }


        // Costruttore senza argomenti
        public Abbonamento()
        {

        }

        public Abbonamento(int valoreId, string codiceFiscale, DateTime dataIscrizione, string durataAbbonamento)
        {
            ValoreId = valoreId;
            CodiceFiscale = codiceFiscale;
            DataIscrizione = dataIscrizione;
            DurataAbbonamento = durataAbbonamento;
        }

        public Abbonamento(string codiceFiscale, DateTime dataIscrizione, string durataAbbonamento)
        {
            CodiceFiscale = codiceFiscale;
            DataIscrizione = dataIscrizione;
            DurataAbbonamento = durataAbbonamento;
        }

        public static DateTime calcoloDataFineAbbonamento(DateTime dataInizio, string tipoAbbonamento)
        {
            // Calcola la data di fine in base al tipo di abbonamento
            DateTime dataFine;
            switch (tipoAbbonamento)
            {
                case "1 Mese":
                    dataFine = dataInizio.AddMonths(1).AddDays(-1); ;
                    break;
                case "2 Mesi":
                    dataFine = dataInizio.AddMonths(2).AddDays(-1); ;
                    break;
                case "3 Mesi":
                    dataFine = dataInizio.AddMonths(3).AddDays(-1); ;
                    break;
                case "6 Mesi":
                    dataFine = dataInizio.AddMonths(6).AddDays(-1); ;
                    break;
                case "12 Mesi":
                    dataFine = dataInizio.AddMonths(12).AddDays(-1); ;
                    break;
                default:
                    // Nel caso in cui il tipo di abbonamento non sia valido di default vale 1 mese
                    dataFine = dataInizio.AddMonths(1).AddDays(-1); ;
                    break;
            }
            return dataFine;
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
