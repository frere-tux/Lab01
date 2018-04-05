using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    const float G = 667.4f * 0.5f;

    public static List<Attractor> attractors; 

    public Rigidbody rb;
    public bool fixedAttractor = false;

    private List<Rigidbody> collisions;

    private void FixedUpdate()
    {
        foreach (Attractor attractor in attractors)
        {
            if (attractor != this && !attractor.fixedAttractor)
            {
                Attract(attractor);
            }
        }

        collisions.Clear();
    }

    private void Start()
    {
        collisions = new List<Rigidbody>();
    }

    void OnEnable()
    {
        if (attractors == null)
            attractors = new List<Attractor>();

        attractors.Add(this);
    }

    void OnDisable()
    {
        attractors.Remove(this);
    }

    void Attract(Attractor objectToAttract)
    {
        Rigidbody rbToAttract = objectToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        if (distance == 0)
            return;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        if (collisions.Contains(rbToAttract))
        {
            rbToAttract.AddForce(-force * rb.mass * 0.0001f, ForceMode.Impulse);
        }
        else
        {
            rbToAttract.AddForce(force);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision != null && collision.rigidbody != null)
        {
            collisions.Add(collision.rigidbody);
        }
    }
}
