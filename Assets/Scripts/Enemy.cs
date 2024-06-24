using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] AudioSource hitSound;
    public int maxHealth = 100;
    public int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        // Animación de daño
        gameObject.GetComponent<Animator>().SetTrigger("takeHit");
        // Sonido
        hitSound.Play();

        if (currentHealth <= 0)
        {
            Die();
        }
    }
    void Die()
    {
        Debug.Log("Enemigo muerto");

        // Animación de muerte
        gameObject.GetComponent<Animator>().SetBool("isDead", true);
        // Sonido de muerte


        // Desactivar al enemigo
        GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezePosition;
        GetComponent<Collider2D>().enabled = false;
        //GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }
}
