using UnityEngine;

public class FlashlightFollow : MonoBehaviour
{
    public Rigidbody2D playerRb;
    private bool facingRight = true;

    void Update()
    {
        float moveX = playerRb.linearVelocity.x;
        if (moveX > 0.1f && !facingRight)
        {
            Flip(true);
        }
        else if (moveX < -0.1f && facingRight)
        {
            Flip(false);
        }
    }

    void Flip(bool faceRight)
    {
        facingRight = faceRight;

        if (faceRight)
            transform.localRotation = Quaternion.Euler(0, 0, 0);
        else
            transform.localRotation = Quaternion.Euler(0, 0, 180);
    }
}
