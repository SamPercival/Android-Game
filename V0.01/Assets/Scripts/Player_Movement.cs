using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Movement
{
    public Game_Manager gm;
    private float dashTimer = 0;
    public float dashLength;
    private bool dashing = false;
    private Vector3 dashTarget;
    private void Start()
    {
        transform.position = gm.GetRespawn();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (stunTimer <= 0)
        {
            if (dashTimer > 0) { dashTimer -= Time.deltaTime; }
            if (disabled > 0) { disabled -= Time.deltaTime; }
            grounded = CheckIfGrounded();
            if (rb.velocity.y <= 0)
            {
                rb.gravityScale = 50;
            }

            if (Input.GetKey(KeyCode.Space) && grounded)
            {
                rb.gravityScale = 20;
                jumpTimer = jumpTimeLimit;
                rb.AddForce(new Vector2(0, jumpForce));

            }
            else if (jumpTimer > 0 && Input.GetKey(KeyCode.Space))
            {
                jumpTimer -= Time.deltaTime;
                rb.AddForce(new Vector2(0, jumpForce));
            }
        }
        DoDash();
    }

    public void Dash(float dist)
    {
        if (dashTimer <= 0 && stunTimer <= 0)
        {
            dashTimer = 0.5f;
            dashTarget = transform.position + new Vector3(dist - 2, 0, 0);
            dashing = true;
        }

    }

    private void DoDash()
    {
        if (dashing)
        {
            Vector3 location = Vector3.MoveTowards(transform.position, dashTarget, 1);
            rb.MovePosition(location);
            if (dashTarget == location)
            {
                dashing = false;
                disabled = 0.1f;
            }
        }
    }
}
