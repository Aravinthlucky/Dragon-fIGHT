using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f; // Speed of the projectile
    public float lifetime = 2f; // Time before the projectile is destroyed
    public int damage = 1; // Damage dealt to the player

    private void Start()
    {
        Destroy(gameObject, lifetime); // Destroy the projectile after a certain time
    }

    private void Update()
    {
        // Move the projectile forward
        transform.Translate(Vector2.up * speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Call the TakeDamage method on the PlayerHealth component
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(gameObject); // Destroy the projectile on hit
        }
    }
}







