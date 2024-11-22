using UnityEngine;

public class DragonMovement : MonoBehaviour
{
    public float speed = 2f; // Speed of movement
    public bool moveFromBottom = true; // Determines if the dragon moves from the bottom or top

    private Vector3 startPosition;
    private Vector3 targetPosition;

    void Start()
    {
        // Set starting positions and target positions based on the dragon's path
        if (moveFromBottom)
        {
            // Dragon starts from the bottom and moves to the middle
            startPosition = new Vector3(transform.position.x, -Screen.height / 2, transform.position.z);
            targetPosition = new Vector3(transform.position.x, Screen.height / 2, transform.position.z);
        }
        else
        {
            // Dragon starts from the top and moves to the middle
            startPosition = new Vector3(transform.position.x, Screen.height / 2, transform.position.z);
            targetPosition = new Vector3(transform.position.x, -Screen.height / 2, transform.position.z);
        }

        transform.position = startPosition; // Set the initial position
    }

    void Update()
    {
        // Move the dragon towards the target position
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
}
