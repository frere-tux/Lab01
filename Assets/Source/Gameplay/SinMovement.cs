using UnityEngine;

public class SinMovement: MonoBehaviour {

    public float speed = 1.0f;
    public float distance = 1.0f;
    public float offset = 0.0f;
    public Vector3 direction = Vector3.forward;

    private Vector3 initialPosition;

    private void Start()
    {
        offset = Random.Range(0.0f, offset);
        initialPosition = transform.position;
    }

    private void Update ()
    {
        float sin = Mathf.Sin((Time.time + offset) * speed / distance);
        float movement = distance * sin;

        transform.position = initialPosition + direction.normalized * movement;
	}
}
