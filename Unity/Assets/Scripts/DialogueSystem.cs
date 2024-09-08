using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueSystem : MonoBehaviour
{
    public Dialogue dialogue;
    [SerializeField] private GameObject actionMark;
    [SerializeField] private GameObject namePanel;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private GameObject choicePanel;
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text choiceText;

    private bool isPlayerInRange;
    private bool didDialogueStart;
    private bool isPlayerSelectingAChoice;
    private int lineIndex = 0;

    void Start()
    {
        namePanel.SetActive(false);
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
        actionMark.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            Talk();
        }
    }

    public void Talk()
    {
        
    }

    private void StartDialogue()
    {
        GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
        GameObject.FindWithTag("Player").GetComponent<Animator>().SetBool("moving", false);
        didDialogueStart = true;
        dialoguePanel.SetActive(true);
        actionMark.SetActive(false);
        nameText.text = dialogue.dialogues[lineIndex].character.nameCharacter;
        dialogueText.text = dialogue.dialogues[lineIndex].dialogue;
    }

    private void NextDialogueLine()
    {
        lineIndex++;
        if (lineIndex < dialogue.dialogues.Length)
        {
            nameText.text = dialogue.dialogues[lineIndex].character.nameCharacter;
            dialogueText.text = dialogue.dialogues[lineIndex].dialogue;
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
