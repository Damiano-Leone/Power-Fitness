using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Power_Fitness.CrowApiException;
using System.Net;

namespace Power_Fitness.FlaskApi
{
    class FlaskApiClient
    {
        private string apiUrl = "http://localhost:8500"; // L'URL del server Flask


        // Funzione generica per eseguire una richiesta GET
        public async Task<string> GetAsync(string route)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    // Invia la richiesta GET
                    HttpResponseMessage response = await client.GetAsync($"{apiUrl}/{route}");

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        // La richiesta ha restituito un codice di stato diverso da 200 OK
                        HandleApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
                        return string.Empty;
                    }
                }
                catch (HttpRequestException ex)
                {
                    // La richiesta va male per un problema di connessione 
                    HandleApiException(ex);
                    return string.Empty;
                }
            }
        }

        // Funzione generica per eseguire una richiesta POST
        // Utilizzando il parametro generico <TRequest>
        // il compilatore verificherà che il tipo dei dati di richiesta
        // corrisponda al tipo specificato quando andremo a chiamare il metodo
        public async Task<string> PostAsync<TRequest>(string route, TRequest requestData)
        {
            using (HttpClient client = new HttpClient())
            {
                // Imposta l'header "Expect: 100-continue" a false
                client.DefaultRequestHeaders.ExpectContinue = false;

                string jsonBody;

                // Verifica se requestData è un id, e che quindi sia di tipo stringa
                if (requestData is string)
                {
                    // Creazione dell'oggetto anonimo da serializzare
                    // La creazione dell'oggetto anonimo é necessaria altrimenti essendo il json 
                    // un unico attributo, non viene creato come {"data":"valore"} ma semplicemente come "valore"
                    // Quindi occore creare un oggetto anonimo
                    var oggettoIdRimozione = new
                    {
                        data = requestData
                    };

                    // Serializza l'id in JSON
                    jsonBody = JsonConvert.SerializeObject(oggettoIdRimozione, Formatting.Indented);
                }

                else
                {
                    // Serializza requestData in JSON
                    jsonBody = JsonConvert.SerializeObject(requestData, Formatting.Indented);
                }

                Console.WriteLine($"Serialized JSON: {jsonBody}");

                try
                {
                    // Crea il contenuto da inviare con la richiesta
                    StringContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                    // Invia la richiesta POST
                    HttpResponseMessage response = await client.PostAsync($"{apiUrl}/{route}", content);

                    if (response.IsSuccessStatusCode)
                    {
                        return await response.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        // La richiesta ha restituito un codice di stato diverso da 200 OK
                        HandleApiException(response.StatusCode, await response.Content.ReadAsStringAsync());
                        return string.Empty;
                    }
                }
                catch (HttpRequestException ex)
                {
                    // La richiesta va male per un problema di connessione 
                    HandleApiException(ex);
                    return string.Empty;
                }
            }
        }

        private void HandleApiException(HttpStatusCode statusCode, string responseBody)
        {
            throw new ApiException(statusCode, responseBody);
        }

        private void HandleApiException(HttpRequestException ex)
        {
            if (ex.InnerException is WebException webException &&
                webException.Status == WebExceptionStatus.ConnectFailure)
            {
                throw new ApiException(HttpStatusCode.ServiceUnavailable, "Il server non è disponibile. Controlla la connessione o riprova più tardi.");
            }

            throw new ApiException(HttpStatusCode.InternalServerError, "Errore di connessione al server: " + ex.Message);
        }
    }
}
