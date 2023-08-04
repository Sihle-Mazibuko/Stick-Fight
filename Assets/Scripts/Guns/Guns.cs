using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{


    [SerializeField] GameObject bulletPref;
    [SerializeField]Transform bulletSpawnPoint;
    public bool isPlayer1;


    public int damage, clipSize, bulletsPerTap, range;
    public float timeBetweenShooting, spread, timeBetweenShots;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot;

    [SerializeField] LayerMask playerLayer;

    RaycastHit2D hit;

    bool isEquipped;


    private void Awake()
    {
        bulletsLeft = clipSize;
        readyToShoot = true;
    }

    private void Update()
    {
        Debug.Log(readyToShoot);

        if (transform.parent != null)
        {
            readyToShoot = true;
            isEquipped = true;
            MyInput();
        }
        else
        {
            isEquipped = false;
        }
    }

    void MyInput()
    {
        GameObject parent1 = GameObject.Find("WeaponHolder1");
        GameObject parent2 = GameObject.Find("WeaponHolder2");


        if (parent1.GetComponentInParent<Movement1>() != null)
        {
            if (allowButtonHold) shooting = Input.GetButton("Fire1");
            else shooting = Input.GetButtonDown("Fire1");
        }
        
        if (parent2.GetComponentInParent<Movement2>() != null)
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);
        }

        if (readyToShoot && shooting && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    void Shoot()
    {
        Debug.Log("shoot");
        readyToShoot = false;

        GameObject player = bulletSpawnPoint.parent.gameObject;

        //Bullet Spread
        float x = Random.Range(-spread, spread);
        float y = Random.Range(-spread, spread);

        Vector3 _direction = transform.position + new Vector3(x, y, 0);



        //Prefab shooting
        Instantiate(bulletPref, bulletSpawnPoint.position, bulletSpawnPoint.rotation);


        ////Cam Shake
        //camShake.Shake(shakeDuration, shakeMagnitude);

        bulletsLeft--;
        bulletsShot--;
        Invoke("ResetShot", timeBetweenShooting);

        if (bulletsShot > 0 && bulletsLeft > 0)
            Invoke("Shoot", timeBetweenShots);

    }

    private void ResetShot()
    {
        readyToShoot = true;
    }



}
