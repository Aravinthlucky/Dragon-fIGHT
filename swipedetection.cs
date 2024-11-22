using UnityEngine;

public class SwipeDetection : MonoBehaviour
{
    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;
    private float swipeThreshold = 200f;  // Threshold to detect a swipe

    public delegate void OnSwipeLeft();
    public event OnSwipeLeft SwipeLeftEvent;

    public delegate void OnSwipeRight();
    public event OnSwipeRight SwipeRightEvent;

    public delegate void OnSwipeUp();
    public event OnSwipeUp SwipeUpEvent;

    void Update()
    {
        DetectSwipe();
    }

    private void DetectSwipe()
    {
        // Detect when the user starts touching the screen
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPosition = Input.GetTouch(0).position;
        }

        // Detect when the user ends the touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            endTouchPosition = Input.GetTouch(0).position;

            Vector2 swipeDirection = endTouchPosition - startTouchPosition;

            // Horizontal swipe
            if (Mathf.Abs(swipeDirection.x) > Mathf.Abs(swipeDirection.y))
            {
                if (swipeDirection.x < -swipeThreshold)
                {
                    // Swipe left
                    SwipeLeftEvent?.Invoke();
                }
                else if (swipeDirection.x > swipeThreshold)
                {
                    // Swipe right
                    SwipeRightEvent?.Invoke();
                }
            }
            // Vertical swipe (jump)
            else if (Mathf.Abs(swipeDirection.y) > swipeThreshold)
            {
                if (swipeDirection.y > swipeThreshold)
                {
                    // Swipe up
                    SwipeUpEvent?.Invoke();
                }
            }
        }
    }
}
