using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Choices
{
    [TextArea(3, 4)] public string choice;
    public Dialogue answer;
}

[CreateAssetMenu(fileName = "Question", menuName = "Dialogue system/New Question")]
public class Question : ScriptableObject
{
    [TextArea(3, 4)] public string question;
    public Choices[] choices;
}
