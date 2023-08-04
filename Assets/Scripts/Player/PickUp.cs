using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    //CONTROLLER
    [SerializeField] 
    Transform weaponHolder;
    GameObject weapon;

    [SerializeField]
    private LayerMask gunMask;

    private void FixedUpdate()
    {
        GunRotation();
        if (Input.GetKeyDown("Fire2"))
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

    [SerializeField]float throwForce;

    void ThrowWeapon()
    {
        if (weapon)
        {
            weapon.gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x, 0) * throwForce;
            weapon.transform.parent =null;
            if (weapon.GetComponent<Rigidbody2D>())
            {
                weapon.GetComponent<Rigidbody2D>().simulated = true;
            }
            weapon = null;
        }
    }

    //Aiming

    Vector2 worldPos;
    Vector2 _direction;
    float angle;
    void GunRotation()
    {
        //Get Mouse Position
        worldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        _direction = (worldPos - (Vector2)weapon.transform.parent.position).normalized;
       weapon.transform.parent.right = _direction;

        //Flip when aiming back
        angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = new Vector3(1,1,1);
        if(angle > 90 ||  angle < -90)
        {
            localScale.y = -1;
        }
        else
        {
            localScale.y = 1;
        }

        weapon.transform.localScale = localScale;
        
    }
}
