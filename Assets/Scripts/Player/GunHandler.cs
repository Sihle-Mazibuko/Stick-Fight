using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunHandler : MonoBehaviour
{
    [SerializeField]
    Transform weaponHolder;
    private GameObject weapon;

    Vector2 direction;

    [SerializeField]
    private LayerMask gunMask;


    private void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.B))
        {
            ThrowWeapon();
        }

        HandleGunRotation();
        
    }

    //Pick Up and Throw Gun

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Weapon")
        {
            PickUpWeapon();
        }
    }

    void PickUpWeapon()
    {
        if (weapon == null)
        {
            Collider2D weaponItem = Physics2D.OverlapCircle(transform.position, 1, gunMask.value);

            if (weaponItem)
            {
                weapon = weaponItem.gameObject;
                weapon.transform.position = weaponHolder.position;
                weapon.transform.parent = weaponHolder;
                if (weapon.GetComponent<Rigidbody2D>())
                {
                    weapon.GetComponent<Rigidbody2D>().simulated = false;
                }
            }

        }
    }

    void ThrowWeapon()
    {
        if (weapon != null)
        {
            weapon.transform.position = transform.position + (Vector3)direction;
            weapon.transform.parent = null;
            if (weapon.GetComponent<Rigidbody2D>())
            {
                weapon.GetComponent<Rigidbody2D>().simulated = true;
            }
            weapon = null;
        }
    }

    //Aim

    Vector2 worldPos;
    float angle;

    void HandleGunRotation()
    {
        worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = worldPos - (Vector2)weapon.transform.position;
        weapon.transform.right = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1, 1, 1);
        if(angle > 90 || angle < -90)
        {
            localScale.y = -1;
        }
        else
        {
            localScale.x = 1;
        }

        weapon.transform.localScale = localScale;
    }

    void Shoot()
    {
        if (weapon != null)
        {
            //bull
        }
    }
}
