using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }
    public UnityEngine.UI.Image healthBar;
    public TMP_Text suspicion;
    public float actualHealth;
    public float maxHealth;
    public float suspicionLevel;

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
    }

    private void Start()
    {

    }

    void Update()
    {
        healthBar.fillAmount = actualHealth / maxHealth;
        suspicion.text = "Sospecha: " + suspicionLevel + "%";
    }

    public void IncreaseSuspicion(int increaseLevel)
    {
        if (suspicionLevel < 100)
        {
            suspicionLevel = suspicionLevel + increaseLevel;
        }
    }
}
