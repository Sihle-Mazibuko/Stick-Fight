using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    [SerializeField] Transform weaponHolder;
    GameObject weapon;
    public Vector3 Direction { get; set; }



    private void Update()
    {
        weapon = GameObject.FindGameObjectWithTag("Weapon");
        Debug.Log(weapon.name);

        if (Input.GetKeyDown(KeyCode.P))
        {
            PickUpWeapon();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //if(collision.gameObject.tag == "Weapon")
        //{
        //    PickUpWeapon();
        //}
    }

    void PickUpWeapon()
    {
        if (weapon)
        {

        }
        else
        {
            Collider2D weaponItem = Physics2D.OverlapCircle(transform.position, 1);
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
}
