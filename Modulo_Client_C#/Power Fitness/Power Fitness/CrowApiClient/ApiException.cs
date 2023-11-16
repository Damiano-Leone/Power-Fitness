using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Power_Fitness.CrowApiException
{
    public class ApiException : Exception
    {
        public string ResponseBody { get; }

        public ApiException(HttpStatusCode statusCode, string responseBody)
            : base(GetErrorMessage(statusCode, responseBody))
        {
            ResponseBody = responseBody;
        }

        private static string GetErrorMessage(HttpStatusCode statusCode, string responseBody)
        {
            string errorMessage = "Errore: " + statusCode;

            if (statusCode == HttpStatusCode.NotFound)
            {
                errorMessage = "Risorsa non trovata.";
            }
            else if (statusCode == HttpStatusCode.BadRequest)
            {
                errorMessage = "Richiesta non valida.";
            }
            else if (statusCode == HttpStatusCode.InternalServerError)
            {
                errorMessage = "Errore interno del server.";
            }
            // ... altri controlli per altri status code

            if (!string.IsNullOrEmpty(responseBody))
            {
                errorMessage += Environment.NewLine + Environment.NewLine + "Dettagli: " + responseBody;
            }

            return errorMessage;
        }
    }
}
