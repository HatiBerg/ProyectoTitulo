# This files contains your custom actions which can be used to run
# custom Python code.
#
# See this guide on how to implement these action:
# https://rasa.com/docs/rasa/custom-actions


# This is a simple example for a custom action which utters "Hello World!"

# from typing import Any, Text, Dict, List

from rasa_sdk import Action, Tracker 
from rasa_sdk.executor import CollectingDispatcher
from rasa_sdk.events import SlotSet

class ActionHacerPregunta(Action):
    def name(self) -> str:
        return "action_ask_rebellion_year_start"

    def run(self, dispatcher: CollectingDispatcher, tracker: Tracker, domain: dict) -> list:
        pregunta = "¿En qué año empezó la rebelión de los campesinos?"
        dispatcher.utter_message(text=pregunta)
        return []

class ActionEvaluarRespuesta(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_year_start"

    def run(self, dispatcher: CollectingDispatcher, tracker: Tracker, domain: dict) -> list:
        intent = tracker.latest_message['intent'].get('name')

        # Obtener el puntaje actual del tracker
        #puntaje = tracker.get_slot('puntaje') or 0  # Inicializa en 0 si no existe

        if intent == "rebellion_year_start":
            #puntaje += 1  # Incrementa el puntaje si la respuesta es correcta
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        # Establecer el nuevo puntaje en el tracker
        #return [SlotSet("puntaje", puntaje)]