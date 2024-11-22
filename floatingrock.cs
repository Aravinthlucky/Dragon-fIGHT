using UnityEngine;

public class FloatingRockMovement : MonoBehaviour
{
    public float speed = 2.0f;          // Speed of the movement
    public float movementRange = 3.0f;  // Range of movement in each direction

    private Vector3 startPosition;

    private void Start()
    {
        // Store the starting position of the rock
        startPosition = transform.position;
    }

    private void Update()
    {
        // Calculate new position
        float offset = Mathf.PingPong(Time.time * speed, movementRange) - (movementRange / 2);
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }
}
