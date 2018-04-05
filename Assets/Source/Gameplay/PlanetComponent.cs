using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetComponent : MonoBehaviour
{
    public float angularSpeed = 15.0f;
    public Color color = Color.white;

    void Start()
    {
        MeshRenderer renderer = GetComponent<MeshRenderer>();
        if (renderer)
        { 
            renderer.material.color = color;
        }
    }

    void Update ()
    {
        transform.Rotate(Vector3.up, angularSpeed * Time.deltaTime, Space.Self);	
	}
}
