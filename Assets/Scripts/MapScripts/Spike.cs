using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player morreu!");

            PlayerDeath player = other.GetComponent<PlayerDeath>();
            if (player != null)
            {
                player.Die();
            }
        }
    }
}
