
using UnityEngine;

public class AvatarSelector : MonoBehaviour
{
    public GameObject avatarManagerPanel; // Reference to the AvatarManager panel
    public GameObject[] avatarPrefabs; // Array of avatar prefabs
    public Transform avatarDisplayPosition; // Where the avatar will be displayed
    private int currentAvatarIndex = 0; // Track the current avatar
    private GameObject currentAvatarInstance; // The currently displayed avatar instance

    private Vector2 swipeStart; // Start position of the swipe
    private Vector2 swipeEnd;   // End position of the swipe
    private float swipeThreshold = 50f; // Minimum distance for a valid swipe

    void Start()
    {
        ShowAvatar(currentAvatarIndex); // Display the first avatar at start
    }

    void Update()
    {
        HandleSwipeInput();
    }

    // Method to handle swipe input
    private void HandleSwipeInput()
    {
        if (Input.GetMouseButtonDown(0)) // For touch, use Input.GetTouch(0).phase == TouchPhase.Began
        {
            swipeStart = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0)) // For touch, use Input.GetTouch(0).phase == TouchPhase.Ended
        {
            swipeEnd = Input.mousePosition;
            ProcessSwipe();
        }
    }

    // Method to process the swipe direction
    private void ProcessSwipe()
    {
        Vector2 swipeDelta = swipeEnd - swipeStart;

        if (Mathf.Abs(swipeDelta.x) > swipeThreshold) // Check if horizontal swipe
        {
            if (swipeDelta.x > 0)
            {
                // Swipe Right
                NextAvatar();
            }
            else
            {
                // Swipe Left
                PreviousAvatar();
            }
        }
    }

    // Method to show avatar by index
    private void ShowAvatar(int index)
    {
        // Destroy all children of avatarDisplayPosition
        foreach (Transform child in avatarDisplayPosition)
        {
            Destroy(child.gameObject);
        }

        // Instantiate the new avatar
        currentAvatarInstance = Instantiate(avatarPrefabs[index], avatarDisplayPosition.position, Quaternion.identity);
        currentAvatarInstance.transform.SetParent(avatarDisplayPosition, false);
        currentAvatarInstance.transform.localPosition = Vector3.zero;
        currentAvatarInstance.transform.localScale = Vector3.one;
    }

    // Method to go to the next avatar
    private void NextAvatar()
    {
        currentAvatarIndex = (currentAvatarIndex + 1) % avatarPrefabs.Length;
        ShowAvatar(currentAvatarIndex);
    }

    // Method to go to the previous avatar
    private void PreviousAvatar()
    {
        currentAvatarIndex = (currentAvatarIndex - 1 + avatarPrefabs.Length) % avatarPrefabs.Length;
        ShowAvatar(currentAvatarIndex);
    }

    // Method to confirm avatar selection
    public void ConfirmAvatar()
    {
        PlayerPrefs.SetInt("SelectedAvatarIndex", currentAvatarIndex); // Save selected avatar index
        Debug.Log("Avatar " + currentAvatarIndex + " confirmed.");
    }

    // Method to hide the panel when starting the game
    public void StartGame()
    {
        avatarManagerPanel.SetActive(false); // Deactivate the panel
        Debug.Log("AvatarManager panel hidden. Starting the game...");
        
        // Additional logic for starting the game can go here
    }
}
