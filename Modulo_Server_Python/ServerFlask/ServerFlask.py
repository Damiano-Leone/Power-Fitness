from mimetypes import init
from connDb import *
from flask import Flask, request
import os
import json
from datetime import datetime

app = Flask(__name__)

@app.route("/getDati", methods=['POST'])
def getDati():
    #connessione al db
    database = Conndb()
    db = database.db
    cursor = database.cursor
    d = dict()
    obj = datetime.now()

    #creo la query
    content = request.json
    q1 = "select * from abbonamenti where data_inizio >= '"+ content["data"] +"' && data_inizio <='"+str(obj.date())+"';"

    try:
        cursor.execute(q1)
        res = cursor.fetchall()    
        for i in res:
            x = d.keys()
            # i[2] terzo elemento della riga i-esima, in pratica la data di inizio
            # i[-1] ultima elemento della riga i-esima, in pratica la durata dell'abbonamento
            if str(i[2]) in x:
                numAbb1Mese, numAbb2Mesi, numAbb3Mesi, numAbb6Mesi, numAbbAnnuale = d[str(i[2])]
                if i[-1] == "1 Mese":
                    d[str(i[2])][0] = numAbb1Mese + 1 
                elif i[-1] == "2 Mesi":
                    d[str(i[2])][1] = numAbb2Mesi + 1
                elif i[-1] == "3 Mesi":
                    d[str(i[2])][2] = numAbb3Mesi + 1
                elif i[-1] == "6 Mesi":
                    d[str(i[2])][3] = numAbb6Mesi + 1
                else:
                    d[str(i[2])][4] = numAbbAnnuale + 1
            else:
                if i[-1] == "1 Mese":
                    d[str(i[2])] = [1, 0, 0, 0, 0]
                elif i[-1] == "2 Mesi":
                    d[str(i[2])] = [0, 1, 0, 0, 0]
                elif i[-1] == "3 Mesi":
                    d[str(i[2])] = [0, 0, 1, 0, 0]
                elif i[-1] == "6 Mesi":
                    d[str(i[2])] = [0, 0, 0, 1, 0]
                else:
                    d[str(i[2])] = [0, 0, 0, 0, 1]  
    except mysql.connector.ProgrammingError as err:
        print("Error: {}".format(err))    
    dataJson = json.dumps(d)
    cursor.close()
    db.close()
    return dataJson

if __name__ == "__main__":
    print("Start...")  
    port = int(os.environ.get('PORT', 8500))
    app.run(debug=True, use_reloader=False, host='0.0.0.0', port=port)
    
