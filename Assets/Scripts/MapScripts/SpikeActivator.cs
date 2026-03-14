using UnityEngine;

public class SpikeActivator : MonoBehaviour
{
    public FallingSpike fallingSpike;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            fallingSpike.Activate();
            gameObject.SetActive(false);
        }
    }
}

