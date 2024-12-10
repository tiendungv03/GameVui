using System.Collections;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum FireMode { Single, Burst, Auto }
    public FireMode fireMode = FireMode.Single;

    public int damage = 10;
    public float range = 100f;
    public float fireRate = 0.1f;
    public int burstCount = 3;

    public Camera fpsCam;
    public ParticleSystem muzzleFlase;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;
    private bool isShooting = false;

    void Update()
    {
        // Chuyển đổi chế độ bắn
        if (Input.GetKeyDown(KeyCode.V))
        {
            SwitchFireMode();
        }

        // Logic bắn
        if (fireMode == FireMode.Single && Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        else if (fireMode == FireMode.Burst && Input.GetButtonDown("Fire1") && !isShooting)
        {
            StartCoroutine(BurstFire());
        }
        else if (fireMode == FireMode.Auto && Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + fireRate;
            Shoot();
        }
    }

    void SwitchFireMode()
    {
        if (fireMode == FireMode.Single)
            fireMode = FireMode.Burst;
        else if (fireMode == FireMode.Burst)
            fireMode = FireMode.Auto;
        else if (fireMode == FireMode.Auto)
            fireMode = FireMode.Single;

        Debug.Log("Chế độ bắn hiện tại: " + fireMode);
    }

    void Shoot()
    {
        muzzleFlase.Play();

        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
/*
            Target target = hit.transform.GetComponent<Target>();*/
            Enemy target = hit.transform.GetComponent<Enemy>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    IEnumerator BurstFire()
    {
        isShooting = true;
        for (int i = 0; i < burstCount; i++)
        {
            if (Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + fireRate;
                Shoot();
            }
            yield return new WaitForSeconds(fireRate);
        }
        isShooting = false;
    }
}
