# This files contains your custom actions which can be used to run
# custom Python code.
#
# See this guide on how to implement these action:
# https://rasa.com/docs/rasa/custom-actions


# This is a simple example for a custom action which utters "Hello World!"

from typing import Any, Text, Dict, List
from rasa_sdk import Action, Tracker 
from rasa_sdk.executor import CollectingDispatcher
from rasa_sdk.events import SlotSet
import sqlite3
import os

class ActionSaveUnclassifiedExample(Action):
    def name(self) -> str:
        return "action_default_fallback"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        dispatcher.utter_message(text = "No te entiendo, vuelve a repetirlo.")
        
        return []
    
class ActionCheckAmount(Action):
    def name(self) -> str:
        return "action_check_amount"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:
        
        amount = tracker.get_slot("amount")

        if amount is None:
            amount = 12

        # Comprobar si el monto es exactamente 12
        if int(amount) == 12:
            dispatcher.utter_message(text = "Gracias por tu contribución. Se lo haré saber al duque.")
        
        # Comprobar si el monto es mayor a 12
        if int(amount) > 12:
            dispatcher.utter_message(text = f"{amount} peniques es demasiado, solo necesitas pagar 12 peniques.")
        
        # Comprobar si el monto es 0
        if int(amount) == 0:
            dispatcher.utter_message(text = "¿Qué no quieres pagar? ¡Esto es inaceptable!, ¿cuál es tu excusa?")
        
        # Si el monto es 1
        if int(amount) == 1:
            dispatcher.utter_message(text = f"¿Solo un penique? ¡Esto es inaceptable!, ¿cuál es tu excusa?")

        # Si el monto es 2 o 3
        if int(amount) == 2 or int(amount) == 3:
            dispatcher.utter_message(text = f"¿Solo {amount} peniques? ¡Esto es inaceptable!, ¿cuál es tu excusa?")

        return [SlotSet("amount", amount)]

class ActionExtractData(Action):
    def name(self) -> str:
        return "action_extract_data"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:

        amount = tracker.get_slot("amount")
        intent = tracker.latest_message["intent"].get("name")    
        excuse = tracker.latest_message['text']  # Obtener el texto de la excusa

        # Definir la ruta de la base de datos en el proyecto de Unity
        db_folder = os.path.join("..", "Unity", "Assets", "Database")  # Ir un nivel arriba para acceder a Unity
        db_path = os.path.join(db_folder, "db.db")

        # Crear la carpeta si no existe
        if not os.path.exists(db_folder):
            os.makedirs(db_folder)

        # Conectar a la base de datos SQLite en la ubicación deseada
        conn = sqlite3.connect(db_path)
        cursor = conn.cursor()

        # Crear la tabla 'DIALOGUES' si no existe
        cursor.execute('''
        CREATE TABLE IF NOT EXISTS DIALOGUES (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            monto INTEGER,
            excusa TEXT
        )
        ''')

        # Insertar los datos según las condiciones
        if amount is not None and (intent == "challenge" or intent == "deny"):
            cursor.execute('''
            INSERT INTO DIALOGUES (monto, excusa) VALUES (?, ?)
            ''', (amount, excuse))

        elif amount is not None:
            cursor.execute('''
            INSERT INTO DIALOGUES (monto, excusa) VALUES (?, ?)
            ''', (amount, None))

        elif amount is None and (intent == "challenge" or intent == "deny"):
            cursor.execute('''
            INSERT INTO DIALOGUES (monto, excusa) VALUES (?, ?)
            ''', (0, excuse))

        conn.commit()
        conn.close()
        return []