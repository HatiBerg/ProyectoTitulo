using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance { get; private set; }
    public static DialogueSpeaker actualSpeaker;
    [SerializeField] public DialogueUI diaUI;
    [SerializeField] private GameObject player;

    public QuestionController questionController;
    [HideInInspector] public bool didDialogueStart;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        diaUI = FindObjectOfType<DialogueUI>();
        questionController = FindObjectOfType<QuestionController>();
    }

    private void Start()
    {

        UnlockPlayerController(false);
    }

    public void UnlockPlayerController(bool unlocked)
    {
        diaUI.gameObject.SetActive(unlocked);
        if (!unlocked)
        {
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeRotation;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = true;
            didDialogueStart = false;
        }
        else 
        {
            GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
            GameObject.FindWithTag("Player").GetComponent<PlayerController>().enabled = false;
            GameObject.FindWithTag("Player").GetComponent<Animator>().SetBool("moving", false);
            didDialogueStart = true;
        }
    }

    public void SetDialogue(Dialogue dialog, DialogueSpeaker speaker)
    {
        if (speaker != null)
        {
            actualSpeaker = speaker;
        }
        else
        {
            diaUI.dialogue = dialog;
            diaUI.lineIndex = 0;
            diaUI.UpdateText(0);
        }
        if (dialog.finalized && !dialog.reuse)
        {
            diaUI.dialogue = dialog;
            diaUI.lineIndex = dialog.dialogues.Length;
            diaUI.UpdateText(1);
        }
        else
        {
            diaUI.dialogue = dialog;
            diaUI.lineIndex = actualSpeaker.dialogLineIndex;
            diaUI.UpdateText(0);
        }
    }
    
    public void ChangeReuseStatus(Dialogue dialog, bool reuseStatus)
    {
        dialog.reuse = reuseStatus;
    }
    public void ChangeUnlockedStatus(Dialogue dialog, bool unlockedStatus)
    {
        dialog.unlocked = unlockedStatus;
    }
}
