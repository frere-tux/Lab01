using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeThrower : MonoBehaviour
{
    public float throwForce = 40.0f;
    public GameObject grenadePrefab;
    public GameObject flashEffect;

	void Update ()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ThrowGrenade();
        }
	}

    private void ThrowGrenade()
    {
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation * grenadePrefab.transform.rotation);
        Instantiate(flashEffect, transform.position, transform.rotation);

        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * throwForce, ForceMode.VelocityChange);
    }
}
