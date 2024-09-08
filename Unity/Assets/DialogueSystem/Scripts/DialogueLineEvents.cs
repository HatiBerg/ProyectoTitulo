using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueLineEvents : MonoBehaviour
{
    GameManager gameManager;
    Dialogue dialogue;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        
    }

    public void ReduceHealth(int health)
    {
        gameManager.actualHealth -= health;
    }
}
