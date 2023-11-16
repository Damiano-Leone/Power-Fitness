mysql -u 127.0.0.1:3306 -u root --password=yourPassword

DROP DATABASE powerFitnessDB;
CREATE DATABASE powerFitnessDB;
use powerFitnessDB;


CREATE TABLE corsi (
id INT AUTO_INCREMENT PRIMARY KEY,
nome_corso VARCHAR(50) NOT NULL,
data_inizio_corso DATE NOT NULL,
data_fine_corso DATE NOT NULL,
prezzo DECIMAL(10, 2) NOT NULL,
descrizione VARCHAR(255),
UNIQUE(nome_corso)
);

CREATE TABLE users (
id INT AUTO_INCREMENT PRIMARY KEY,
codice_fiscale VARCHAR(16) NOT NULL,
nome VARCHAR(50) NOT NULL,
cognome VARCHAR(50) NOT NULL,
genere ENUM('Maschio', 'Femmina') NOT NULL,
data_nascita DATE NOT NULL,
cellulare VARCHAR(15),
email VARCHAR(255) NOT NULL,
indirizzo varchar(100),
numero_civico INT,
UNIQUE(codice_fiscale)
);

CREATE TABLE abbonamenti (
id INT AUTO_INCREMENT PRIMARY KEY,
id_utente INT NOT NULL,
data_inizio DATE NOT NULL,
durata_abbonamento ENUM('1 Mese', '2 Mesi', '3 Mesi', '6 Mesi', '12 Mesi') NOT NULL,
FOREIGN KEY (id_utente) REFERENCES users(id) ON DELETE RESTRICT
);

CREATE TABLE lezioni (
id INT AUTO_INCREMENT PRIMARY KEY,
id_corso INT NOT NULL,
giorno VARCHAR(50) NOT NULL,
ora_inizio VARCHAR(50) NOT NULL,
ora_fine VARCHAR(50) NOT NULL,
max_partecipanti INT NOT NULL,
FOREIGN KEY (id_corso) REFERENCES corsi(id) ON DELETE RESTRICT
);

CREATE TABLE prenotazioni (
id INT AUTO_INCREMENT PRIMARY KEY,
id_lezione INT NOT NULL,
id_user INT NOT NULL,
data DATE NOT NULL,
FOREIGN KEY (id_lezione) REFERENCES lezioni(id) ON DELETE RESTRICT,
FOREIGN KEY (id_user) REFERENCES users(id) ON DELETE CASCADE
);

CREATE TABLE dipendenti(
id INT AUTO_INCREMENT PRIMARY KEY,
mail VARCHAR(255) NOT NULL COLLATE utf8_bin,
password VARCHAR(255) COLLATE utf8_bin,
ruolo ENUM('admin', 'dipendente', 'responsabile') NOT NULL DEFAULT 'dipendente',
nome VARCHAR(50),
cognome VARCHAR(50),
confirmed bool NOT NULL DEFAULT false,
UNIQUE(mail)
);

INSERT INTO dipendenti (nome, cognome, mail, password, ruolo, confirmed) VALUES ('admin','admin', 'admin', 'admin', 'admin', 1);

INSERT INTO corsi (nome_corso, data_inizio_corso, data_fine_corso, descrizione)
VALUES ('Corso di Fitness', '2023-09-01', '2024-08-31', 'Un corso completo di fitness.');

INSERT INTO corsi (nome_corso, data_inizio_corso, data_fine_corso, descrizione)
VALUES ('Corso di Yoga', '2023-10-01', '2024-09-30', 'Un corso completo di yoga.');