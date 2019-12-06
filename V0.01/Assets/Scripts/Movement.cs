using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float smoothness;
    public float jumpForce;
    public LayerMask groundDef;
    public Transform groundCheck;

    private Vector3 targetVel;
    protected Vector3 vel = Vector3.zero;
    private bool facingRight = true;
    protected bool grounded = false;
    protected float groundCheckRadius = 0.2f;
    protected const float jumpTimeLimit = 0.04f;
    protected float jumpTimer = 0;

    protected float disabled = 0;

    public bool CheckIfGrounded()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(groundCheck.position, groundCheckRadius, groundDef);
        foreach (Collider2D col in collisions)
        {
            if (col.gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }

    public void Move(float move)
    {
        if (disabled <= 0)
        {
            targetVel = new Vector3(move * 20.0f, rb.velocity.y, 0);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref vel, smoothness);

            if (facingRight && move < 0)
            {
                Flip();
            }
            else if (!facingRight && move > 0)
            {
                Flip();
            }
        }
    }

    

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }

}
