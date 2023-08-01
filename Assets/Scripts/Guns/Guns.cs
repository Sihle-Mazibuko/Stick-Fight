using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{


    [SerializeField] GameObject bullet;
    //GameObject bulletInst;
    //Transform bulletSpawnPoint;


    public int damage, clipSize, bulletsPerTap, range;
    public float timeBetweenShooting, spread, timeBetweenShots;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;
    bool shooting, readyToShoot;

    [SerializeField] LayerMask playerLayer;

    RaycastHit2D hit;

    private void Awake()
    {
        bulletsLeft = clipSize;
        readyToShoot = true;
    }

    private void FixedUpdate()
    {
        MyInput();
    }

    void MyInput()
    {
        if (allowButtonHold) shooting = Input.GetKey(0);
        else shooting = Input.GetKeyDown(0);

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



        //RayCast Shooting
        hit = Physics2D.Raycast(transform.position, _direction, range, playerLayer);
        if (hit.collider.CompareTag("Player"))
        {
            hit.collider.GetComponent<Health>().TakeDamage(damage);


        }

        //Cam Shake
        camShake.Shake(shakeDuration, shakeMagnitude);

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
