using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startHealth;
    public float currentHealth;


    private void Awake()
    {
        startHealth = 100;
        currentHealth = startHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startHealth);

        if (currentHealth > 0)
        {
            //Hurt Player
        }
        else
        {
            //Kill Player
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject killfloor = GameObject.FindGameObjectWithTag("Killfloor");
        if (killfloor != null)
        {
            TakeDamage(100);
        }

    }

}
