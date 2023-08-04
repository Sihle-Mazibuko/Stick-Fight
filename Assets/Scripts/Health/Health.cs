using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startHealth;
    public float currentHealth;

    public GameObject killFloor;

    private void Awake()
    {
        startHealth = 100;
        currentHealth = startHealth;
    }

    public void TakeDamage(float _damage)
    {
        currentHealth -= _damage;

        if (currentHealth > 0)
        {
            //Hurt Player
        }
        else
        {
            //Kill Player
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(90);
        }


    }
}
