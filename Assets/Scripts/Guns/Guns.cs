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
        GameObject parent = GameObject.Find("WeaponHolder");
        //Debug.Log(LayerMask.GetMask(mask.ToString()));

        if (parent.GetComponent<Movement1>())
        {
            if (allowButtonHold) shooting = Input.GetKey("Fire1");
            else shooting = Input.GetKeyDown("Fire1");
        }
        
        if (parent.GetComponent<Movement1>())
        {
            if (allowButtonHold) shooting = Input.GetKey(KeyCode.Mouse0);
            else shooting = Input.GetKeyDown(KeyCode.Mouse0);
            Debug.Log("shoot");
        }

        if (readyToShoot && shooting && bulletsLeft > 0)
        {
            bulletsShot = bulletsPerTap;
            Shoot();
        }
    }

    void Shoot()
    {
        readyToShoot = false;

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

    //Cam Shake
    public CamShake camShake;
    [SerializeField] float shakeDuration, shakeMagnitude;


}
