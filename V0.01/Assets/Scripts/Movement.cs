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
    private bool inCombat = false;

    protected float stunTimer = 0;

    private void FixedUpdate()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
    }

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
        if (disabled <= 0 && stunTimer <= 0)
        {
            targetVel = new Vector3(move * 200.0f, rb.velocity.y, 0);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVel, ref vel, smoothness);
            if (!inCombat)
            {
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
    }

    

    private void Flip()
    {
        if (grounded)
        {
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x *= -1;
            transform.localScale = scale;
        }
    }

    public void FaceNearestEnemy(bool faceRight)
    {
        if (facingRight && !faceRight)
        {
            Flip();
        }
        else if (!facingRight && faceRight)
        {
            Flip();
        }
    }

    public void Stun(float stunTime)
    {
        stunTimer = stunTime;
    }

    public void KnockUp(float xForce, float yForce)
    {
        rb.velocity = new Vector2(xForce, yForce);
    }

    public void SetInCombat(bool val)
    {
        inCombat = val;
    }

    public bool GetFacingRight()
    {
        return facingRight;
    }

}
