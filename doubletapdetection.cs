using UnityEngine;

public class DoubleTapDetection : MonoBehaviour
{
    private float lastTapTime; // Time of the last tap
    public float doubleTapTime = 0.3f; // Time frame for double tap
    private PlayerController playerController; // Reference to PlayerController

    void Start()
    {
        playerController = GetComponent<PlayerController>(); // Get the PlayerController component
    }

    void Update()
    {
        HandleDoubleTap();
    }

    private void HandleDoubleTap()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            if (Time.time - lastTapTime <= doubleTapTime)
            {
                // Double-tap detected
                playerController.Attack(); // Call the attack method in PlayerController
            }
            lastTapTime = Time.time; // Update the time of the last tap
        }
    }
}
