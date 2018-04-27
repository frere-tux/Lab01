using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerManager : MonoBehaviour
{
    public Transform[] players;
    public float ceiling = 20.0f;
    public float floor = -20.0f;

	void LateUpdate ()
    {
        TeleportDeadPlayers();
    }

    private void TeleportDeadPlayers()
    {
        foreach (Transform player in players)
        {
            if (player.position.y <= floor)
            {
                Vector3 teleport = Vector3.up * (ceiling - floor);

                player.position += teleport;
            }
        }
    }
}
