#include "Sigin.h"

Sigin::Sigin(string Mail, string Password, string Nome, string Cognome, string Ruolo)
{
	mail = Mail; 
	password = Password;
	nome = Nome;
	cognome = Cognome;
	ruolo = Ruolo; 
}
void Sigin::effettuaSigin(crow::response& res, DatabaseManager& dbManager)
{
	string ruoliPossibili[] = { "admin", "dipendente", "responsabile" };
	int lenRuoli = size(ruoliPossibili);
	cout << lenRuoli << endl;

	for (int i = 0; i < lenRuoli; i++) {
		if (ruolo == ruoliPossibili[i]) {
			if (ruolo != "admin") {
				string query = "INSERT INTO dipendenti(mail, password, ruolo, nome, cognome) VALUES('"
					+ mail+ "','" + password + "','" + ruolo + "','" + nome + "','" + cognome + "');";
				dbManager.executeUpdate(query, res);
				return; 
			}
		}
	}
	res.body = "Sigin fallito ";
	res.code = 500;
	res.end();
}



