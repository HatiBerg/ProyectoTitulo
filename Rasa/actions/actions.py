# This files contains your custom actions which can be used to run
# custom Python code.
#
# See this guide on how to implement these action:
# https://rasa.com/docs/rasa/custom-actions


# This is a simple example for a custom action which utters "Hello World!"

import json
from typing import Any, Text, Dict, List
from rasa_sdk import Action, Tracker 
from rasa_sdk.executor import CollectingDispatcher
from rasa_sdk.events import SlotSet

class ActionSaveUnclassifiedExample(Action):
    def name(self) -> str:
        return "action_default_fallback"

    def run(self, dispatcher: CollectingDispatcher,
            tracker: Tracker,
            domain: Dict[Text, Any]) -> List[Dict[Text, Any]]:

        unclassified_message = tracker.latest_message.get('text')
        target_intent = "out_of_scope"
        with open("unclassified_examples.json", "a") as file:
            json.dump({"intent": target_intent, "example": unclassified_message}, file)
            file.write("\n")

        dispatcher.utter_message(text="Incorrecto")
        
        return []

class ActionEvaluarRespuesta1(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_year_start"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        entity_year = next(tracker.get_latest_entity_values('year'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity_year)

        if intent == "rebellion_year_start" and entity_year == "1380":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta2(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_start"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        #entity_event = next(tracker.get_latest_entity_values('event'), None)
        #entity_year = next(tracker.get_latest_entity_values('year'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_start":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta3(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_protagonist"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        entity_name = next(tracker.get_latest_entity_values('name'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_protagonist" and entity_name == "Wat Tyler":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta4(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_priest"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        #entity = next(tracker.get_latest_entity_values('entity_name'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_priest":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta5(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_economic_motivation"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        #entity = next(tracker.get_latest_entity_values('entity_name'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_economic_motivation":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta6(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_religious_motivation"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        #entity = next(tracker.get_latest_entity_values('entity_name'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_religious_motivation":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta7(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_social_motivation"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        #entity = next(tracker.get_latest_entity_values('entity_name'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_social_motivation":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta8(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_taxes"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        #entity = next(tracker.get_latest_entity_values('entity_name'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_taxes":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta9(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_antagonist"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        #entity = next(tracker.get_latest_entity_values('entity_name'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_antagonist":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta10(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_county"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        entity_place = next(tracker.get_latest_entity_values('place'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_county" and entity_place == "Essex":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta11(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_demands"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        #entity = next(tracker.get_latest_entity_values('entity_name'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_demands":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta12(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_response_to_demands"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        #entity = next(tracker.get_latest_entity_values('entity_name'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_response_to_demands":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta13(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_end"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        entity_name = next(tracker.get_latest_entity_values('name'), None)
        entity_event = next(tracker.get_latest_entity_values('event'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_end" and entity_name == "Wat Tyler" and entity_event == "muerte":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []
    
class ActionEvaluarRespuesta14(Action):
    def name(self) -> str:
        return "action_evaluate_rebellion_place_end"

    def run(self, dispatcher: CollectingDispatcher, 
            tracker: Tracker, 
            domain: Dict[Text, Any]) -> List[Dict[Text,Any]]:

        intent = tracker.latest_message['intent'].get('name')
        entity_place = next(tracker.get_latest_entity_values('place'), None)

        #dispatcher.utter_message(text=intent)
        #dispatcher.utter_message(text=entity)

        if intent == "rebellion_place_end" and entity_place == "End Mile":
            dispatcher.utter_message(text="¡Correcto!")
        else:
            dispatcher.utter_message(text="Incorrecto")

        return []