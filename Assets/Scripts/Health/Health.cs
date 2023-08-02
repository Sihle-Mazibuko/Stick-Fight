using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public float startHealth { get; private set; }
    public float currentHealth { get; private set; }

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

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.U))
        {
            TakeDamage(90);
        }
    }
}
