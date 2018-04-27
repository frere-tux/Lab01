using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Player))]
public class PlayerInput : MonoBehaviour
{
    public int controllerId = 0;

    string horizontalAxis = "Horizontal";
    string verticalAxis = "Vertical";
    string jumpButton = "Jump";

    Player player;

	void Start ()
    {
		player = GetComponent<Player> ();

        if (controllerId > 0)
        {
            horizontalAxis += "_P" + controllerId;
            verticalAxis += "_P" + controllerId;
            jumpButton += "_P" + controllerId;
        }
    }

	void Update ()
    {
		Vector2 directionalInput = new Vector2 (Input.GetAxisRaw (horizontalAxis), Input.GetAxisRaw (verticalAxis));
		player.SetDirectionalInput (directionalInput);

		if (Input.GetButtonDown(jumpButton))
        {
			player.OnJumpInputDown ();
		}
		if (Input.GetButtonUp(jumpButton))
        {
			player.OnJumpInputUp ();
		}
	}
}
