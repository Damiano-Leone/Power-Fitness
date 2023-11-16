#include <iostream>
#include <string>
#include "DatabaseManager.h"
#include "Corso.h"
#include "User.h"
#include "Abbonamento.h"
#include "Lezione.h"
#include "Prenotazione.h"
#include "Login.h"
#include "Sigin.h"
#include "Validate.h"
#include "JWTAuthMiddleware.h"
#include "Dipendente.h"

using namespace std;

void PrintHttpRequestDetails(const std::string& routeName, const crow::request& req) {
    cout << endl << "Request received for " << routeName << endl;

    // Stampa gli header della richiesta
    for (const auto& header : req.headers) {
        cout << header.first << ": " << header.second << endl;
    }

    // Stampa il corpo della richiesta
    cout << "Request Body: " << req.body << endl
        << "---------------------" << endl << endl;
}

int main()
{
    // Inizializza il gestore del database
    DatabaseManager dbManager("tcp://127.0.0.1:3306", "root", "yourPassword", "powerFitnessDB");
    
    // Connessione al database
    if (dbManager.connect()) {
       
        crow::App<JWTAuthMiddleware> app;
        app.get_middleware<JWTAuthMiddleware>().setDb(&dbManager);
        
        CROW_ROUTE(app, "/Login")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("mail") || !reqBody.has("password")) {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }

                string mail = reqBody["mail"].s();
                string password = reqBody["password"].s();

                Login login(mail, password);
                login.effettuaLogin(res, dbManager);
            });
        
        CROW_ROUTE(app, "/Sigin")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
            crow::json::rvalue reqBody;
            reqBody = crow::json::load(req.body);

            if (!reqBody || !reqBody.has("mail") || !reqBody.has("password")
                || !reqBody.has("nome") || !reqBody.has("cognome") || !reqBody.has("ruolo")           
                ) {
                res.code = 400;
                res.body = "Corpo della richiesta non valido";
                res.end();
                return;
            }

            string mail = reqBody["mail"].s();
            string password = reqBody["password"].s();
            string nome = reqBody["nome"].s();
            string cognome = reqBody["cognome"].s();
            string ruolo = reqBody["ruolo"].s();

            Sigin sigin(mail, password, nome, cognome, ruolo);
            sigin.effettuaSigin(res, dbManager);       
                });
        
        CROW_ROUTE(app, "/Validate")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {      
                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }
            
                int id = reqBody["id"].i();  
                string token = req.get_header_value("Authorization");

                Validate val(id);
                val.validateDip(res, dbManager, token);        
                });

        CROW_ROUTE(app, "/GetDipendenti")
            .methods("GET"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

            PrintHttpRequestDetails("/GetDipendenti", req);

            Dipendente::getDipendenti(res, dbManager);
                });

        CROW_ROUTE(app, "/AddCorso")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/AddCorso", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("nome_corso") || !reqBody.has("data_inizio_corso")
                    || !reqBody.has("data_fine_corso")    || !reqBody.has("descrizione")) {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }

                string nomeCorso = reqBody["nome_corso"].s();
                string dataInizioCorso = reqBody["data_inizio_corso"].s();
                string dataFineCorso = reqBody["data_fine_corso"].s();
                string descrizione = reqBody["descrizione"].s();

                Corso nuovoCorso(nomeCorso, dataInizioCorso, dataFineCorso, descrizione);
                nuovoCorso.addCorso(res, dbManager);
                });

        CROW_ROUTE(app, "/GetCorsi")
            .methods("GET"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
                PrintHttpRequestDetails("/GetCorsi", req);
                Corso::getCorsi(res, dbManager);
                });

        CROW_ROUTE(app, "/GetCorso")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/GetCorso", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);
                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }

                int idCorso = reqBody["id"].i();
                Corso showCorso(idCorso);
                showCorso.getCorso(res, dbManager);
                });

        CROW_ROUTE(app, "/DeleteCorso")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
                PrintHttpRequestDetails("/DeleteCorso", req);
                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }

                int id = reqBody["id"].i();

                Corso removeCorso(id);
                removeCorso.deleteCorso(res, dbManager);
                });

        CROW_ROUTE(app, "/UpdateCorso")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
                
                PrintHttpRequestDetails("/UpdateCorso", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id") || !reqBody.has("nome_corso") ||
                    !reqBody.has("data_inizio_corso") || !reqBody.has("data_fine_corso")
                    || !reqBody.has("descrizione")) {
                    res.code = 400;
                    res.body = "Campi mancanti nel JSON";
                    res.end();
                    return;
                }

                int id = reqBody["id"].i();
                string nomeCorso = reqBody["nome_corso"].s();
                string dataInizioCorso = reqBody["data_inizio_corso"].s();
                string dataFineCorso = reqBody["data_fine_corso"].s();
                string descrizione = reqBody["descrizione"].s();

                Corso updateCorso(id, nomeCorso, dataInizioCorso, dataFineCorso, descrizione);
                updateCorso.updateCorso(res, dbManager);

                });
        
        CROW_ROUTE(app, "/AddUser")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/AddUser", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("codice_fiscale") ||
                    !reqBody.has("nome") || !reqBody.has("cognome") ||
                    !reqBody.has("genere") || !reqBody.has("data_nascita") ||
                    !reqBody.has("cellulare") || !reqBody.has("email") ||
                    !reqBody.has("indirizzo") || !reqBody.has("numero_civico")
                    )
                {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }

                string codiceFiscale = reqBody["codice_fiscale"].s();
                string nome = reqBody["nome"].s();
                string cognome = reqBody["cognome"].s();
                string genere = reqBody["genere"].s();
                string dataNascita = reqBody["data_nascita"].s();
                string cellulare = reqBody["cellulare"].s();
                string email = reqBody["email"].s();
                string indirizzo = reqBody["indirizzo"].s();
                string numeroCivico = reqBody["numero_civico"].s();

                User newUser(codiceFiscale, nome, cognome, genere, dataNascita,
                    cellulare, email, indirizzo, numeroCivico);
                newUser.addUser(res, dbManager);
                });
       
        CROW_ROUTE(app, "/UpdateUser")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
                PrintHttpRequestDetails("/UpdateUser", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id") ||
                    !reqBody.has("cellulare") || !reqBody.has("email") ||
                    !reqBody.has("indirizzo") || !reqBody.has("numero_civico")
                    )
                {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }

                int id = reqBody["id"].i();
                string cellulare = reqBody["cellulare"].s();
                string email = reqBody["email"].s();
                string indirizzo = reqBody["indirizzo"].s();
                string numeroCivico = reqBody["numero_civico"].s();

                User updateUser(id, cellulare, email, indirizzo, numeroCivico);
                updateUser.updateUser(res, dbManager);

                });
       
        CROW_ROUTE(app, "/DeleteUser")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/DeleteUser", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }

                int id = reqBody["id"].i();

                User removeUser(id);
                removeUser.deleteUser(res, dbManager);

                });
        
        CROW_ROUTE(app, "/GetUsers")
            .methods("GET"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
                PrintHttpRequestDetails("/GetUsers", req);
                User::getUsers(res, dbManager);
                });
       
       
        CROW_ROUTE(app, "/AddAbbonamento")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/AddAbbonamento", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id_utente") || !reqBody.has("data_inizio")
                    || !reqBody.has("durata_abbonamento"))
                {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }

                int idUser = reqBody["id_utente"].i();
                string dataInizio = reqBody["data_inizio"].s();
                string durataAbbonamento = reqBody["durata_abbonamento"].s();

                Abbonamento addAbbonamento(idUser, dataInizio, durataAbbonamento);
                addAbbonamento.addAbbonamento(res, dbManager);
                });    

        CROW_ROUTE(app, "/RinnovaAbbonamento")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/RinnovaAbbonamento", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id_utente") || !reqBody.has("data_inizio") || !reqBody.has("durata_abbonamento"))
                {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }

                int idUser = reqBody["id_utente"].i();
                string dataInizio = reqBody["data_inizio"].s();
                string durataAbbonamento = reqBody["durata_abbonamento"].s();

                Abbonamento rinnovaAbbonamento(idUser, dataInizio, durataAbbonamento);
                rinnovaAbbonamento.rinnovaAbbonamento(res, dbManager);
                });

        CROW_ROUTE(app, "/UpdateAbbonamento")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/UpdateAbbonamento", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id") || !reqBody.has("durata_abbonamento")
                    || !reqBody.has("id_utente") || !reqBody.has("data_inizio"))
                {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }

                int idAbbonamento = reqBody["id"].i();
                int idUser = reqBody["id_utente"].i();

                string dataIscrizione = reqBody["data_inizio"].s();
                string durataAbbonamento = reqBody["durata_abbonamento"].s();

                Abbonamento updateAbbonamento(idAbbonamento, idUser, dataIscrizione, durataAbbonamento);
                updateAbbonamento.updateAbbonamento(res, dbManager);
                });
       
        CROW_ROUTE(app, "/GetAbbonamenti")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/GetAbbonamenti", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);
                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }
                int idUser = reqBody["id"].i();
                Abbonamento getAbb;
                getAbb.setIdUtente(idUser);
                getAbb.getAbbonamentiUser(res, dbManager);
                });



        CROW_ROUTE(app, "/GetAbbonamentoAttivo")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/GetAbbonamentoAttivo", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);
                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }
                int idUser = reqBody["id"].i();
                Abbonamento getAbb;
                getAbb.setIdUtente(idUser);
                getAbb.getAbbonamentoAttivoUser(res, dbManager);
                });


        CROW_ROUTE(app, "/DeleteAbbonamento")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
                PrintHttpRequestDetails("/DeleteAbbonamento", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }

                int idAbbonamento = reqBody["id"].i();

                Abbonamento deleteAbbonamento(idAbbonamento);
                deleteAbbonamento.deleteAbbonamento(res, dbManager);

                });

        CROW_ROUTE(app, "/AddLezione")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/AddLezione", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id_corso") || !reqBody.has("giorno") || !reqBody.has("ora_inizio")
                    || !reqBody.has("ora_fine") || !reqBody.has("max_partecipanti"))
                {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }

                int idCorso = reqBody["id_corso"].i();
                string giorno = reqBody["giorno"].s();
                string oraInizio = reqBody["ora_inizio"].s();
                string oraFine = reqBody["ora_fine"].s();
                int maxPartecipanti = reqBody["max_partecipanti"].i();

                Lezione addLezione(idCorso, giorno, oraInizio, oraFine, maxPartecipanti);
                addLezione.addLezione(res, dbManager);
                });

        CROW_ROUTE(app, "/UpdateLezione")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

            PrintHttpRequestDetails("/UpdateLezione", req);

            crow::json::rvalue reqBody;
            reqBody = crow::json::load(req.body);

            if (!reqBody || !reqBody.has("id") || !reqBody.has("id_corso") || !reqBody.has("giorno")
                || !reqBody.has("ora_inizio") || !reqBody.has("ora_fine") || !reqBody.has("max_partecipanti"))
            {
                res.code = 400;
                res.body = "Corpo della richiesta non valido";
                res.end();
                return;
            }

            int idLezione = reqBody["id"].i();
            int idCorso = reqBody["id_corso"].i();
            string giorno = reqBody["giorno"].s();
            string oraInizio = reqBody["ora_inizio"].s();
            string oraFine = reqBody["ora_fine"].s();
            int maxPartecipanti = reqBody["max_partecipanti"].i();

            Lezione updateLezione(idLezione, idCorso, giorno, oraInizio, oraFine, maxPartecipanti);
            updateLezione.updateLezione(res, dbManager);
                });

        CROW_ROUTE(app, "/GetLezioni")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/GetLezioni", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);
                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }

                int idCorso = reqBody["id"].i();
                Lezione showLezioni;
                showLezioni.setIdCorso(idCorso);
                showLezioni.getLezioni(res, dbManager);
                });



        CROW_ROUTE(app, "/GetLezione")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/GetLezione", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);
                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }

                int idLezione = reqBody["id"].i();
                Lezione showLezione(idLezione);
                showLezione.getLezione(res, dbManager);
                });


        CROW_ROUTE(app, "/DeleteLezione")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
                PrintHttpRequestDetails("/DeleteLezione", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }

                int idLezione = reqBody["id"].i();

                Lezione delLezione(idLezione);
                delLezione.deleteLezione(res, dbManager);
                });

        CROW_ROUTE(app, "/AddPrenotazione")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/AddPrenotazione", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id_lezione") || !reqBody.has("id_user") || !reqBody.has("data"))
                {
                    res.code = 400;
                    res.body = "Corpo della richiesta non valido";
                    res.end();
                    return;
                }

                int idLezione = reqBody["id_lezione"].i();
                int idUser = reqBody["id_user"].i();
                string data = reqBody["data"].s();

                Prenotazione addPrenotazione(idLezione, idUser, data);
                addPrenotazione.addPrenotazione(res, dbManager);
                });

        CROW_ROUTE(app, "/UpdatePrenotazione")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

            PrintHttpRequestDetails("/UpdatePrenotazione", req);

            crow::json::rvalue reqBody;
            reqBody = crow::json::load(req.body);

            if (!reqBody || !reqBody.has("id") || !reqBody.has("id_lezione") || !reqBody.has("id_user")
                || !reqBody.has("data"))
            {
                res.code = 400;
                res.body = "Corpo della richiesta non valido";
                res.end();
                return;
            }
            int idPrenotazione = reqBody["id"].i();
            int idLezione = reqBody["id_lezione"].i();
            int idUser = reqBody["id_user"].i();
            string data = reqBody["data"].s();

            Prenotazione updatePrenotazione(idPrenotazione, idLezione, idUser, data);
            updatePrenotazione.updatePrenotazione(res, dbManager);
                });

        CROW_ROUTE(app, "/DeletePrenotazione")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
            
                PrintHttpRequestDetails("/DeletePrenotazione", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }

                int idPrenotazione = reqBody["id"].i();

                Prenotazione delPrenotazione(idPrenotazione);
                delPrenotazione.deletePrenotazione(res, dbManager);
                });

        CROW_ROUTE(app, "/GetPrenotazioni")
            .methods("GET"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {
                PrintHttpRequestDetails("/GetPrenotazioni", req);
                Prenotazione::getPrenotazioni(res, dbManager);
                });

        CROW_ROUTE(app, "/GetPrenotazioniUser")
            .methods("POST"_method)
            ([&dbManager](const crow::request& req, crow::response& res) {

                PrintHttpRequestDetails("/GetPrenotazioniUser", req);

                crow::json::rvalue reqBody;
                reqBody = crow::json::load(req.body);

                if (!reqBody || !reqBody.has("id")) {
                    res.code = 400; // Imposta il codice di stato a 400 Bad Request
                    res.body = "Corpo della richiesta non valido";
                    res.end(); // Termina la risposta
                    return;
                }

                int idUser = reqBody["id"].i();
                Prenotazione showPrenotazioni;
                showPrenotazioni.setIdUser(idUser);
                showPrenotazioni.getPrenotazioniUser(res, dbManager);
                });

        app.port(8080).multithreaded().run();
    }
}
