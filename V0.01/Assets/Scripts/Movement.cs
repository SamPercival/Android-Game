﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float jumpForce; //How much force is applied to make entity jump
    public LayerMask groundDef; //What classifies as ground to the ground checker
    public Transform groundCheck;
    public float accelceration;
    public float speedMax;
    public Transform rayPos;
    public LayerMask rayLM;

    private float groundSpeed = 0;
    private Vector3 targetVel;
    private float xSpeed = 0, ySpeed = 0;
    protected Vector3 vel = Vector3.zero;
    protected bool facingRight = true; //Is entity facing right
    protected bool grounded = false; //Is the entity on the ground
    protected float groundCheckRadius = 0.3f;
    protected const float jumpTimeLimit = 0.22f; //How long the player can hold the jump button
    protected float jumpTimer = 0;

    protected float disabled = 0; //How long character movement is disabled for
    protected bool inCombat = false; //Is the entity in combat

    protected float stunTimer = 0; //How long the entity is stunned for
    private bool knockedDown = false;
    protected void FixedUpdate()
    {
        if (stunTimer > 0)
        {
            stunTimer -= Time.deltaTime;
        }
        else if (knockedDown)
        {
            knockedDown = false;
            transform.Rotate(0, 0, -90);
        }
        
    }

    //Checks if entity is on the ground
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

    //Move the entity smoothly in a direction
    public void Move(float move)
    {
        if ((move < -0.35 || move > 0.35) && groundSpeed < speedMax && disabled <= 0 && stunTimer <= 0)
        {
            groundSpeed += accelceration;
        }
        else if (groundSpeed > 0)
        {
            
            groundSpeed -= accelceration;
        }
        else
        {
            groundSpeed = 0;
        }

        RaycastHit2D rayHit;
        rayHit = Physics2D.Raycast(rayPos.position, new Vector2(1, 0), 1, rayLM);
        Debug.DrawRay(rayPos.position, new Vector2(1, 0), Color.red, 3.0f);
        float slopeAngle = 0;
        if (rayHit.collider != null)
        {
            slopeAngle = Vector2.Angle(rayHit.normal, Vector2.up);
            Debug.Log(slopeAngle);
        }
        if (slopeAngle < 45)
        {
            ySpeed = groundSpeed * (float)Math.Sin(slopeAngle);
            if (move < 0)
            {
                xSpeed = -groundSpeed * (float)Math.Cos(slopeAngle);
            }
            else if (move > 0)
            {
                xSpeed = groundSpeed * (float)Math.Cos(slopeAngle);
            }
            else
            {
                if (rb.velocity.x > 0) {xSpeed = groundSpeed * (float)Math.Cos(slopeAngle);}
                else if (rb.velocity.x > 0) {xSpeed = -groundSpeed * (float)Math.Cos(slopeAngle);}
            }
            rb.velocity = new Vector2(xSpeed, rb.velocity.y + ySpeed);
        }
        

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

    
    //Flip the direction the entity is facing
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

    //Face a certain direction defined by the parameter
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

    //Prevent the enemy from moving for a period of time
    public void Stun(float stunTime)
    {
        stunTimer = stunTime;
    }

    //Knocks the entity into the air with the forces given in the parameters
    public void KnockUp(float xForce, float yForce)
    {   
        rb.velocity = new Vector2(xForce, yForce);
        Debug.Log(rb.velocity);
    }

    //Knocks the entity over, stunning them for a short time
    public void KnockDown()
    {
        knockedDown = true;
        Stun(0.9f);
        transform.Rotate(new Vector3(0, 0, 90));
        //Knock down animation
        // get up animation
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
