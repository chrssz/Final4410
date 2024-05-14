using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gun : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] AudioClip shot;
    [SerializeField] AudioClip reload;


    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 60f;

    public int maxAmmo = 17;
    private int currentAmmo;
    public float reloadTime = 1f;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;

    private float nextTimeToFire = 0f;

    public Animator animator;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        currentAmmo = maxAmmo;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isReloading)
        {
            return;
        }
        if (currentAmmo <= 0)
        {
            StartCoroutine(Reload());
            return;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f/fireRate;
            Shoot();
        }
        
    }

    //OnEnable is called everytime
    private void OnEnable()
    {
        isReloading = false;
        animator.SetBool("Reloading", false);
    }

    IEnumerator Reload()
    {
        isReloading = true;
        audioSource.PlayOneShot(reload);
        Debug.Log("Reloading..");

        animator.SetBool("Reloading", true);
        yield return new WaitForSeconds(reloadTime);

        animator.SetBool("Reloading", false);

        

        currentAmmo = maxAmmo;
        isReloading = false;
    }

    void Shoot()
    {
        muzzleFlash.Play();

        currentAmmo--;
        RaycastHit hit;
        
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

        }

        if(hit.rigidbody != null)
        {
            hit.rigidbody.AddForce(-hit.normal * impactForce);
        }

        audioSource.PlayOneShot(shot);

        //Code below will be for particles of impact of the shooting
        GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impactGO, 1f);
    }

}
