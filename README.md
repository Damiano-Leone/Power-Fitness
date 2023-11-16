# ﻿Configurazione del Server Crow e Flask

Questo README fornisce istruzioni dettagliate per la configurazione del server Crow e Flask, insieme alle librerie e alla configurazione del database necessarie per il loro corretto funzionamento.

## Configurazione del Server Crow

### Installazione della libreria mysqlconnector per C++

#### *Step 1: Scarica la libreria mysqlconnector*

- Scaricare la libreria *mysqlconnector* dal [sito ufficiale](https://dev.mysql.com/downloads/connector/cpp/), selezionando il sistema operativo e l'architettura appropriati.
- Nel corso del progetto, si è optato per l'utilizzo della versione di *debug* per garantire un'analisi più approfondita e un controllo accurato durante lo sviluppo.
- Estrarre il file *.zip* nella directory desiderata.

#### *Step 2: Configurazione di mysqlconnector in Visual Studio*

- Aprire le *proprietà di configurazione del progetto*.
- Nella sezione *C/C++ -> Directory di inclusione aggiuntive*, il percorso: *C:\[path to mysqlconnector-c++]\mysql-connector-c++-8.0.33-winx64-debug\include*.
- Nella sezione *C/C++ -> Preprocessore*, specificare le definizioni preprocessore: *STATIC\_CONCPP*.
- Nella sezione *Linker -> Generale*, specificare il percorso delle librerie aggiuntive: *D:\[path to mysqlconnector-c++]\mysql-connector-c++-8.0.33-winx64-debug\lib64\vs14\debug*.
- Nella sezione *Linker -> Input*, specificare le dipendenze aggiuntive: *mysqlcppconn-static.lib*.

### Gestione delle librerie con Vcpkg

#### *Installazione di Vcpkg*

Per la gestione dei pacchetti e l'installazione delle librerie, è stato adottato l'utilizzo di *Vcpkg*. Nel caso non fosse già presente nel sistema, ecco come procedere con l'installazione di Vcpkg:

>*Nota:  Assicurati di trovarti nella directory desiderata prima di eseguire i passaggi.*

- *Clonare il repository Vcpkg*:

```
git clone https://github.com/Microsoft/vcpkg.git
```
 
- *Eseguire lo script di bootstrap* per creare Vcpkg:

```
.\vcpkg\bootstrap-vcpkg.bat
```

#### *Installazione delle librerie necessarie*

Aprire un terminale nella directory in cui è stato installato Vcpkg ed eseguire i seguenti comandi:

```
vcpkg install crow:x64-windows
vcpkg install nlohmann-json:x64-windows
vcpkg install jwt-cpp:x64-windows
```

#### *Integrazione delle librerie con Visual Studio*

- Dopo l'installazione delle librerie, se si utilizza Visual Studio o Visual Studio Code, eseguire: *vcpkg integrate install*.
- Assicurarsi che nella sezione delle proprietà del progetto relativa a Vcpkg i campi "*Use Vcpkg*" e "*Install Vcpkg Dependencies*" siano settati su "*Yes*".

## Configurazione del Server Flask

### Installazione delle librerie

Per il server Flask, sono state utilizzate le seguenti librerie. Installale, se non già presenti, con i seguenti comandi:

```
pip install Flask
pip install mysql-connector-python
```

## Configurazione del Database

- Assicurarsi di avere un database *MySQL* in esecuzione e le credenziali necessarie per accedervi.
- Personalizza le impostazioni del database per adattarle alle tue configurazioni specifiche. Sostituisci le credenziali necessarie nei file *CrowServer.cpp* e *ServerFlask.py* per garantire una connessione corretta al tuo database.
- Per inizializzare le tabelle necessarie, esegui le query presenti nel file *database.sql* sul tuo database per creare la struttura delle tabelle richiesta.
