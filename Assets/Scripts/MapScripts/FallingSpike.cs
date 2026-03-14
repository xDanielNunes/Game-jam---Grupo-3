using UnityEngine;
using System.Collections;

public class FallingSpike : MonoBehaviour
{

    public float fallDelayTime = 0.4f;

    private Rigidbody2D rb;
    private bool hasFallen;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // começa parado
    }

    public void Activate()
    {
        if (hasFallen) return;

        hasFallen = true;
        StartCoroutine(FallDelay());
    }

    private IEnumerator FallDelay()
    {
        yield return new WaitForSeconds(fallDelayTime);
        rb.gravityScale = 4f;
    }
}

