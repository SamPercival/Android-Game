using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public Rigidbody2D player;
    public float smoothness;
    public float jumpForce;
    public LayerMask groundDef;
    public Transform groundCheck;

    private Vector3 targetVel;
    private Vector3 vel = Vector3.zero;
    private bool facingRight = true;
    private bool grounded = false;
    private float groundCheckRadius = 0.2f;
    private const float jumpTimeLimit = 0.05f;
    private float jumpTimer = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = CheckIfGrounded();
        if (player.velocity.y <= 0)
        {
            player.gravityScale = 5;
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            player.gravityScale = 4;
            jumpTimer = jumpTimeLimit;
            player.AddForce(new Vector2(0, jumpForce));
            
        }
        else if (jumpTimer > 0 && Input.GetKey(KeyCode.Space))
        {
            jumpTimer -= Time.deltaTime;
            player.AddForce(new Vector2(0, jumpForce));
        }
        
    }

    bool CheckIfGrounded()
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
        targetVel = new Vector3(move*20.0f, player.velocity.y, 0);
        player.velocity = Vector3.SmoothDamp(player.velocity, targetVel, ref vel, smoothness);

        if (facingRight && move < 0)
        {
            Flip();
        }
        else if (!facingRight && move > 0)
        {
            Flip();
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
