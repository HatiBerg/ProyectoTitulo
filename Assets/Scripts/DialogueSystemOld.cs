using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystemOld : MonoBehaviour
{
    [SerializeField] private GameObject actionMark;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private bool doesPlayerSpeakFirst;
    [SerializeField] private string npcName;
    [SerializeField, TextArea(3,4)] private string[] dialogueLines;

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private bool isPlayerSelectingAChoice;
    private int lineIndex;

    void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown("e"))
        {
            if (!didDialogueStart) 
            {
                StartDialogue();
            }
            else if (dialogueText.text == dialogueLines[lineIndex])
            {
                NextDialogueLine();
            }
        }
    }

    private void StartDialogue()
    {
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetBool("moving", false);
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        actionMark.SetActive(false);
        lineIndex = 0;
        SpeakOrder();
        dialogueText.text = dialogueLines[lineIndex];
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogueLines.Length)
        {
            SpeakOrder();
            dialogueText.text = dialogueLines[lineIndex];
        }
        else
        {
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
            didDialogueStart = false;
            dialoguePanel.SetActive(false);
            actionMark.SetActive(true);
            GameObject.FindWithTag("GameManager").GetComponent<GameManager>().IncreaseSuspicion(10);
        }
    }

    private void SpeakOrder()
    {
        if (!doesPlayerSpeakFirst)
        {
            dialogueNameText.text = npcName;
            doesPlayerSpeakFirst = true;
        }
        else
        {
            dialogueNameText.text = "Player";
            doesPlayerSpeakFirst = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = true;
            actionMark.SetActive(true);
            Debug.Log("Diálogo disponible");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isPlayerInRange = false;
            actionMark.SetActive(false);
            Debug.Log("Diálogo no disponible");
        }
    }
}
