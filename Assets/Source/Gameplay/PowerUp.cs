using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public Color color = Color.white;
    public float duration = 3.0f;

    public GameObject pickupEffect;
    public GameObject endEffect;

    private Color initialColor;

    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Pickup(other));
        }
    }

    private IEnumerator Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        audioManager.Play("Powerup_Start");
        audioManager.Play("Powerup_Loop");

        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<Collider>().enabled = false;
        GetComponentInChildren<Light>().enabled = false; ;

        Renderer renderer = player.GetComponentInChildren<Renderer>();
        if (renderer)
        {
            initialColor = renderer.material.color;

            renderer.material.color = color;

            yield return new WaitForSeconds(duration);

            renderer.material.color = initialColor;

            Instantiate(endEffect, player.transform.position, player.transform.rotation);
        }

        audioManager.Stop("Powerup_Loop");
        audioManager.Play("Powerup_Stop");

        Destroy(gameObject);
    }
}
