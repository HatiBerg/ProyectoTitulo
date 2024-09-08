using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueSpeaker : MonoBehaviour
{
    [SerializeField] private GameObject actionMark;
    private DialogueUI diaUI;
    public List<Dialogue> availableDialogues = new List<Dialogue>();
    [SerializeField] private int dialogIndex = 0;
    public int dialogLineIndex = 0;
    private bool isPlayerInRange;

    void Start()
    {
        actionMark.SetActive(false);
        diaUI = GameObject.FindObjectOfType<DialogueUI>();

        dialogIndex = 0;
        dialogLineIndex = 0;
    }
    private void Update()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.E) && !DialogueManager.instance.didDialogueStart)
        {
            Talk();
        }
    }

    public void Talk()
    {
        if (dialogIndex <= availableDialogues.Count - 1)
        {
            if (availableDialogues[dialogIndex].unlocked)
            {
                if (availableDialogues[dialogIndex].finalized)
                {
                    if (UpdateDialog())
                    {
                        DialogueManager.instance.UnlockPlayerController(true);
                        DialogueManager.instance.SetDialogue(availableDialogues[dialogIndex], this);
                    }
                    DialogueManager.instance.SetDialogue(availableDialogues[dialogIndex], this);
                    return;
                }
                DialogueManager.instance.UnlockPlayerController(true);
                DialogueManager.instance.SetDialogue(availableDialogues[dialogIndex], this);
            }
            else
            {
                DialogueManager.instance.UnlockPlayerController(false);
            }
        }
        else
        {
            DialogueManager.instance.UnlockPlayerController(false);
        }
    }

    bool UpdateDialog()
    {
        if (!availableDialogues[dialogIndex].reuse)
            if (dialogIndex < availableDialogues.Count -1)
            {
                dialogIndex++;
                return true;
            }
            else
            {
                return false;
            }
        else
        {
            return true;
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
