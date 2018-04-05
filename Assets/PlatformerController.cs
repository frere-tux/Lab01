using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float inertia = 0.1f;
    public Vector3 Jump = new Vector3(0.0f, 5.0f, 0.0f);

    private Rigidbody rb;
    private bool IsFalling = false;
    private float leftRightAxis = 0.0f;


    void Start ()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
    }
	
	void Update ()
    {
        //float leftRightAxis = Input.GetAxis("LeftRight");
        float leftRightInput = Input.GetKey(KeyCode.Q) ? -1.0f : Input.GetKey(KeyCode.D) ? 1.0f : 0.0f;

        leftRightAxis = Mathf.Lerp(leftRightAxis, leftRightInput, inertia);

        rb.velocity = new Vector3(leftRightAxis * speed, rb.velocity.y, 0.0f);

        rb.AddForce(Vector3.right * speed * leftRightAxis);

        if (Input.GetKeyDown(KeyCode.Z))//Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Jump, ForceMode.Impulse);
        }
	}
}
