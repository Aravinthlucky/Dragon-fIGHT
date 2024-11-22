using UnityEngine;

public class PlayerInitializer : MonoBehaviour
{
    public SpriteRenderer playerSpriteRenderer; // The sprite renderer for the player character
    public GameObject[] avatarPrefabs; // Array of avatar prefabs

    void Start()
    {
        int selectedAvatarIndex = PlayerPrefs.GetInt("SelectedAvatarIndex", 0); // Default to first avatar if none selected
        Sprite selectedSprite = avatarPrefabs[selectedAvatarIndex].GetComponent<SpriteRenderer>().sprite;
        playerSpriteRenderer.sprite = selectedSprite;
    }
}
