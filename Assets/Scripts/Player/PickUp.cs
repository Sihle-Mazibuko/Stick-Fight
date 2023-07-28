using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] 
    Transform weaponHolder;
    GameObject weapon;
    Vector3 Direction = new Vector3(3,0,0); 

    [SerializeField]
    private LayerMask gunMask;

    private void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.B))
        {
            ThrowWeapon();
        }
    }

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
            Debug.Log(weaponItem.gameObject.name);

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
            weapon.transform.position = transform.position + Direction;
            weapon.transform.parent = null;
            if (weapon.GetComponent<Rigidbody2D>())
            {
                weapon.GetComponent<Rigidbody2D>().simulated = true;
            }
            weapon = null;
        }
    }
}
