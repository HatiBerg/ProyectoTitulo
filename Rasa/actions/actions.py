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
        # excuse = tracker.get_slot("excuse")
        intent = tracker.latest_message["intent"].get("name")    
        excuse = tracker.latest_message['text']  # Obtener el texto de la excusa

        # Conectar a la base de datos SQLite
        conn = sqlite3.connect('db.db')
        cursor = conn.cursor()

        # Crear la tabla 'DIALOGUES' con una columna 'monto'
        cursor.execute('''
        CREATE TABLE IF NOT EXISTS DIALOGUES (
            id INTEGER PRIMARY KEY AUTOINCREMENT,
            monto INTEGER,
            excusa TEXT
        )
        ''')

        # Caso 1: Si hay un monto y excusa"
        if amount is not None and (intent == "challenge" or intent == "deny"):
        # if amount is not None and excuse is not None:
            cursor.execute('''
            INSERT INTO DIALOGUES (monto, excusa) VALUES (?, ?)
            ''', (amount, excuse))

        # Caso 2: Si solo hay un monto y no hay excusa, excusa será None
        if amount is not None and (intent != "challenge" or intent != "deny"):
        # if amount is not None and excuse is None:
            cursor.execute('''
            INSERT INTO DIALOGUES (monto, excusa) VALUES (?, ?)
            ''', (amount, None))

        # Caso 3: Si no hay monto pero sí hay excusa, monto será 0 y se guardará excusa
        if amount is None and (intent == "challenge" or intent == "deny"):
        # if amount is None and excuse is not None:
            cursor.execute('''
            INSERT INTO DIALOGUES (monto, excusa) VALUES (?, ?)
            ''', (0, excuse))

        conn.commit()
        conn.close()
        return []