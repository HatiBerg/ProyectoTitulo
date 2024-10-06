using System;
using System.Collections;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using System.Collections.Generic;
using TMPro;

// A struct to help in creating the Json object to be sent to the rasa server
public class PostMessageJson
{
	public string message;
	public string sender;
}

[Serializable]
// A class to extract multiple json objects nested inside a value
public class RootReceiveMessageJson
{
    public ReceiveMessageJson[] messages;
}

[Serializable]
// A class to extract a single message returned from the bot
public class ReceiveMessageJson
{
    public string recipient_id;
    public string text;
}

public class Chatbot : MonoBehaviour
{
    [Header("Attributes")]
    public string npcMsg;
    [Header("UI")]
    public TextMeshProUGUI npcText;
    public TMP_InputField playerText;
    public Button nextQuestion;
    private int currentMessageIndex = 0;
    private bool responseReceived = false;
    private string lastQuestion;
    private List<string> hiddenMessages;
    private int score = 0;

    public GameObject enemySoldier;

    private const string rasa_url = "http://localhost:5005/webhooks/rest/webhook";

    void Start()
    {
        // Mensajes ocultos
        hiddenMessages = new List<string> {
            "ask_rebellion_year_start",
            "ask_rebellion_start",
            "ask_rebellion_protagonist",
            "ask_rebellion_priest",
            "ask_rebellion_economic_motivation",
            "ask_rebellion_religious_motivation",
            "ask_rebellion_social_motivation",
        };

        // Desactivar el botón al inicio del juego
        nextQuestion.interactable = false;

        // Enviar el primer mensaje oculto automáticamente al iniciar el quiz
        SendMessageToRasa(hiddenMessages[currentMessageIndex]);

        // Asignar el evento OnClick al botón para enviar mensajes ocultos después del primero
        nextQuestion.onClick.AddListener(SendNextHiddenMessage);
    }

    void Update()
    {
        if (responseReceived)
        {
            nextQuestion.interactable = true;
            responseReceived = false;  // Resetear para la siguiente interacción
        }
    }

	public void SendMessageToRasa(string s)
	{
        Debug.Log("Input: " + s);
        // Create a json object from user message
        PostMessageJson postMessage = new PostMessageJson
		{
			sender = "user",
            message = s
        };

		string jsonBody = JsonUtility.ToJson(postMessage);
		print("User json : " + jsonBody);

		// Create a post request with the data to send to Rasa server
		StartCoroutine(PostRequest(rasa_url, jsonBody));
	}

	private IEnumerator PostRequest(string url, string jsonBody)
	{
		UnityWebRequest request = new UnityWebRequest(url, "POST");
		byte[] rawBody = new System.Text.UTF8Encoding().GetBytes(jsonBody);
		request.uploadHandler = (UploadHandler)new UploadHandlerRaw(rawBody);
		request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
		request.SetRequestHeader("Content-Type", "application/json");

		yield return request.SendWebRequest();

        ReceiveResponse(request.downloadHandler.text);
    }

    public void ReceiveResponse(string response)
    {
        // Deserialize response recieved from the bot
        RootReceiveMessageJson root = JsonUtility.FromJson<RootReceiveMessageJson>("{\"messages\":" + response + "}");

        // Mostrar la respuesta del chatbot en la consola
        Debug.Log("Bot: " + root.messages[0].text);

        // Mostrar la respuesta del chatbot
        npcText.text = root.messages[0].text;

        // Verificar si la respuesta es "¡Correcto!" y sumar la puntuación
        if (npcText.text == "¡Correcto!")
        {
            // Incrementar la puntuación
            score++;
            Debug.Log("Puntuación: " + score);

            // Desencadenar el evento correspondiente basado en la pregunta
            TriggerEventForCurrentQuestion();
        }

        // Habilitar el botón solo si la respuesta es "¡Correcto!" o "Incorrecto"
        if (npcText.text == "¡Correcto!" || npcText.text == "Incorrecto")
        {
            responseReceived = true;
            playerText.interactable = false;
            playerText.text = string.Empty;
        }
    }

    // Método que desencadena eventos específicos para cada pregunta correcta
    void TriggerEventForCurrentQuestion()
    {
        Enemy enemyScript = enemySoldier.GetComponent<Enemy>();
        switch (currentMessageIndex)
        {
            case 0:
                Debug.Log("Evento para la primera pregunta correcta.");
                enemyScript.TakeDamage(100);
                break;
            case 1:
                Debug.Log("Evento para la segunda pregunta correcta.");
                break;
            case 2:
                Debug.Log("Evento para la tercera pregunta correcta.");
                break;
            case 3:
                Debug.Log("Evento para la cuarta pregunta correcta.");
                break;
            case 4:
                Debug.Log("Evento para la quinta pregunta correcta.");
                break;
            default:
                Debug.Log("No hay más eventos.");
                break;
        }
    }

    void SendNextHiddenMessage()
    {
        nextQuestion.interactable = false;

        // Asegurarse de que el índice no se salga de los límites de la lista
        if (currentMessageIndex < hiddenMessages.Count - 1)
        {
            // Aumenta el índice para enviar el siguiente mensaje oculto
            currentMessageIndex++;
            // Enviar el siguiente mensaje oculto
            SendMessageToRasa(hiddenMessages[currentMessageIndex]);
            playerText.interactable = true;
        }
        else
        {
            Debug.Log("No hay más mensajes ocultos que enviar.");
        }
    }
}