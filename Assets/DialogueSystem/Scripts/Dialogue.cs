using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue system/New Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public struct Lines
    {
        public Character character;
        [TextArea(3,4)] public string dialogue;
        public UnityEvent LineEvent;
    }

    public bool unlocked;
    public bool finalized;
    public bool reuse;
    public Lines[] dialogues;
    public Question question;
}
