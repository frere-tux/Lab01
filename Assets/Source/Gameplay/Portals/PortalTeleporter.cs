 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalTeleporter : MonoBehaviour
{
    public Rigidbody player;
    public PortalTeleporter receiver;

    private bool playerIsOverlapping = false;
    private bool playerJustTeleported = false;

	void Update ()
    { 
        if (playerIsOverlapping && !playerJustTeleported)
        {
            receiver.NotifyTeleport();

            Quaternion rotationDiffBetweenPortals = receiver.transform.rotation * Quaternion.Inverse(transform.rotation) * Quaternion.AngleAxis(180.0f, transform.up);

            player.transform.rotation = rotationDiffBetweenPortals * player.transform.rotation;

            Vector3 portalToPlayer = player.transform.position - transform.position;
            Vector3 positionOffset = rotationDiffBetweenPortals * portalToPlayer;
            player.transform.position = receiver.transform.position + positionOffset;

            player.velocity = rotationDiffBetweenPortals * player.velocity;

            playerIsOverlapping = false;
        }
	}

    public void NotifyTeleport()
    {
        playerJustTeleported = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Vector3 portalToPlayer = player.transform.position - transform.position;
            float dotProduct = Vector3.Dot(transform.forward, portalToPlayer);

            if (dotProduct > 0.0f)
            {
                playerIsOverlapping = true;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerIsOverlapping = false;
            playerJustTeleported = false;
        }
    }
}
