using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{

    public float delay = 3.0f;
    public float radius = 9.0f;
    public float force = 700.0f;
    public float explosionVerticalOffset = 0.25f;
    public GameObject explosionEffect;

    private float countDown;
    private bool hasExploded = false;


	void Start ()
    {
        countDown = delay;
	}
	
	void Update ()
    {
        countDown -= Time.deltaTime;
        if (countDown <= 0.0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
	}

    private void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);

        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);
        foreach(Collider collider in colliders)
        {
            Destructible destructible = collider.GetComponent<Destructible>();
            if (destructible != null)
            {
                destructible.Destroy();
            }
        }

        colliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider collider in colliders)
        {
            Rigidbody rb = collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius, explosionVerticalOffset, ForceMode.Impulse);
            }
        }

        TimeManager timeManager = FindObjectOfType<TimeManager>();
        if (timeManager != null)
        {
            timeManager.DoSlowMotion();
        }

        Destroy(gameObject);
    }
}
