// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class EnemyFollow : MonoBehaviour
// {
//     public Transform player; // Reference to the player
//     public float moveSpeed = 3f; // Speed at which the enemy moves
//     public float stoppingDistance = 1f; // Distance at which the enemy stops moving towards the player

//     private void Update()
//     {
//         if (player != null)
//         {
//             // Calculate the distance to the player
//             float distance = Vector2.Distance(transform.position, player.position);

//             // Check if the enemy should move toward the player
//             if (distance > stoppingDistance)
//             {
//                 // Move towards the player
//                 Vector2 direction = (player.position - transform.position).normalized;
//                 transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
//             }
//         }
//     }
// }

