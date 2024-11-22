using UnityEngine;
using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public GameObject normalAvatar; // The player's normal avatar
    public GameObject fightingAvatar; // The player's fighting avatar
    public Button fightButton; // The fight button in the UI
    public float attackDuration = 1f; // How long the fighting avatar will be active

    private bool isAttacking = false;
    private float attackTimer = 0f;

    void Start()
    {
        // Assign the Fight method to the button's onClick listener
        if (fightButton != null)
        {
            fightButton.onClick.AddListener(Fight); // Assign the Fight method to the button
        }
        else
        {
            Debug.LogError("Fight button is not assigned in the Inspector.");
        }
        
        SwitchToNormalAvatar(); // Ensure the player starts with the normal avatar
    }

    void Update()
    {
        // Handle attack duration timer
        if (isAttacking)
        {
            attackTimer -= Time.deltaTime;
            if (attackTimer <= 0)
            {
                SwitchToNormalAvatar();
                isAttacking = false;
            }
        }
    }

    public void Fight()
    {
        if (!isAttacking)
        {
            isAttacking = true;
            attackTimer = attackDuration; // Set the timer for how long the attack lasts
            SwitchToFightingAvatar();
            AttackEnemy(); // Trigger the attack logic
        }
    }

    void SwitchToNormalAvatar()
    {
        if (normalAvatar != null && fightingAvatar != null)
        {
            normalAvatar.SetActive(true); // Enable the normal avatar
            fightingAvatar.SetActive(false); // Disable the fighting avatar
        }
        else
        {
            Debug.LogError("Normal or Fighting avatar is not assigned in the Inspector.");
        }
    }

    void SwitchToFightingAvatar()
    {
        if (normalAvatar != null && fightingAvatar != null)
        {
            normalAvatar.SetActive(false); // Disable the normal avatar
            fightingAvatar.SetActive(true); // Enable the fighting avatar
        }
        else
        {
            Debug.LogError("Normal or Fighting avatar is not assigned in the Inspector.");
        }
    }

    void AttackEnemy()
    {
        // Implement enemy hit detection logic here
        // For example, you can use colliders to detect the enemy within attack range
        Debug.Log("Player is attacking!");
    }
}
