using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class QuestionController : MonoBehaviour
{
    [SerializeField] private GameObject buttonPrefab;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private Transform choicesContainer;
    private List<UnityEngine.UI.Button> poolButtons = new List<UnityEngine.UI.Button>();

    public void activeDialogue(int amout, string title, Choices[] choices)
    {
        questionText.text = title;
        if (poolButtons.Count >= amout)
        {
            for (int i = 0; i < poolButtons.Count; i++)
            {
                if (i < amout)
                {
                    poolButtons[i].GetComponentInChildren<TMP_Text>().text = choices[i].choice;
                    poolButtons[i].onClick.RemoveAllListeners();
                    Dialogue dia = choices[i].answer;
                    poolButtons[i].onClick.AddListener(() => giveButtomFunction(dia));
                    poolButtons[i].gameObject.SetActive(true);
                }
                else
                {
                    poolButtons[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            int remainingAmount = (amout - poolButtons.Count);
            for (int i = 0; i < remainingAmount; i++)
            {
                var newButtom = Instantiate(buttonPrefab, choicesContainer).GetComponent<UnityEngine.UI.Button>();
                newButtom.gameObject.SetActive(true);
                poolButtons.Add(newButtom);
            }
            activeDialogue(amout, title, choices);
        }
    }

    public void giveButtomFunction(Dialogue dialog)
    {
        DialogueManager.instance.SetDialogue(dialog, null);
    }
}
