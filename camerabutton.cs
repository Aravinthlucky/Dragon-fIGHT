using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraFaceSwapper : MonoBehaviour
{
    public SpriteRenderer playerFaceRenderer;  // The player face renderer to update
    public Button openCameraButton;            // Button to open the camera in the main menu

    void Start()
    {
        // Assign the button click event to open the camera
        openCameraButton.onClick.AddListener(OpenCamera);
    }

    // Function to open the camera and take a picture
    public void OpenCamera()
    {
        // Request camera permission and open camera to take a picture
        StartCoroutine(TakePicture());
    }

    private IEnumerator TakePicture()
    {
        yield return new WaitForEndOfFrame();

        // Check camera permissions first
        if (NativeCamera.IsCameraBusy())
            yield break;

        // Open the camera and allow the user to take a picture
        NativeCamera.Permission permission = NativeCamera.TakePicture((path) =>
        {
            // If a picture was taken and the file path is valid
            if (path != null)
            {
                Debug.Log("Image path: " + path);

                // Load the image as a texture from the path
                Texture2D texture = NativeCamera.LoadImageAtPath(path, 512);
                if (texture == null)
                {
                    Debug.Log("Failed to load texture from " + path);
                    return;
                }

                // Create a sprite from the texture and apply it to the player face
                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Sprite newFaceSprite = Sprite.Create(texture, rect, new Vector2(0.5f, 0.5f));

                // Set the new face sprite on the player's face
                playerFaceRenderer.sprite = newFaceSprite;
            }
        }, 512);  // Set maximum image dimension

        Debug.Log("Permission result: " + permission);
    }
}
