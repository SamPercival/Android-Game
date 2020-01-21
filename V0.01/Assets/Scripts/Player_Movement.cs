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
    public sbyte dashSpeed;
    private sbyte dashesInAir = 1;
    private sbyte dashesDoneInAir = 0;
    private void Start()
    {
        transform.position = gm.GetRespawn();
    }
    // Update is called once per frame
    new private void FixedUpdate()
    {
        grounded = CheckIfGrounded();
        if (grounded) { dashesDoneInAir = 0; }
        if (stunTimer <= 0)
        {
            if (dashTimer > 0) { dashTimer -= Time.deltaTime; }
            if (disabled > 0) { disabled -= Time.deltaTime; }
            else { dashing = false; }

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

    public void Dash(float xAxis)
    {
        
        if (dashTimer <= 0 && stunTimer <= 0 && dashesDoneInAir < dashesInAir)
        {
            if (!grounded) { dashesDoneInAir++; }
            dashTimer = 1.0f;
            disabled = 0.4f;
            if (xAxis > 0) { dashTarget = transform.position + new Vector3(dashLength, 0, 0); }
            else if (xAxis < 0) { dashTarget = transform.position - new Vector3(dashLength, 0, 0); }
            else
            {
                if (facingRight) { dashTarget = transform.position + new Vector3(dashLength, 0, 0); }
                else { dashTarget = transform.position - new Vector3(dashLength, 0, 0); }
            }
            dashing = true;
        }

    }

    private void DoDash()
    {
        if (dashing)
        {
            Vector3 current = transform.position;
            Vector3 location = Vector3.MoveTowards(transform.position, dashTarget, dashSpeed);
            rb.MovePosition(location);
            if (dashTarget == location)
            {
                dashing = false;
                disabled = 0.1f;
            }
        }
    }
}
