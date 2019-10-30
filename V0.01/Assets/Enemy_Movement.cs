using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : Movement
{
    private Vector3 startingPos;
    private float offset = 5;
    private bool movingRight = true;
    private float speed = 0.5f;

    private void Start()
    {
        startingPos = transform.position;
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
        speed = 0.5f;
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
