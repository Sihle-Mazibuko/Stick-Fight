using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float speed = 100;
    Rigidbody2D rb;
    Guns guns;
    GameObject weapon;
    GameObject holder;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GameObject.FindWithTag("Weapon");

    }

    private void Start()
    {
        rb.velocity = transform.right * speed;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Health hitPlayer = collision.gameObject.GetComponent<Health>();

        if (hitPlayer != null)
        {
            hitPlayer.TakeDamage(weapon.GetComponentInChildren<Guns>().damage);
        }
        Destroy(gameObject);
    }
}
