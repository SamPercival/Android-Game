using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : Movement
{
    private Vector3 startingPos;
    private float offset = 50;
    private bool movingRight = true;
    private float speed = 0.3f;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void FixedUpdate()
    {
        grounded = CheckIfGrounded();
        Debug.Log("Disabled" + disabled);
        Debug.Log("Stun" + stunTimer);
        Debug.Log("Grounded" + grounded);
    }
    bool EdgeCheck()
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

    private void MoveInDirection()
    {
        if (movingRight)
        {
            Move(speed);
        }
        else
        {
            Move(-speed);
        }
    }

    public void Passive()
    {
        inCombat = false;
        speed = 0.3f;
        if (!EdgeCheck())
        {
            movingRight = !movingRight;
        }
        else
        {
            if (startingPos.x + offset < transform.position.x && movingRight)
            {
                movingRight = false;
            }
            else if (startingPos.x - offset > transform.position.x && !movingRight)
            {
                movingRight = true;
            }
        }
        MoveInDirection();
    }

    public void Aggresive()
    {
        inCombat = true;
        speed = 0.7f;
        if (EdgeCheck())
        {
            MoveInDirection();
        }
        else
        {
            Move(0);
        }
    }
}
