using UnityEngine;

public class AvatarSelectionManager : MonoBehaviour
{
    public GameObject avatarSelectionPanel;

    public void OpenAvatarSelection()
    {
        avatarSelectionPanel.SetActive(true); // Show the avatar selection page
    }
    
    public void CloseAvatarSelection()
    {
        avatarSelectionPanel.SetActive(false); // Hide the avatar selection page
    }
}
