using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : Movement
{
    private Vector3 startingPos;
    private float offset = 5;
    private bool movingRight = true;

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

    public void Passive()
    {
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


        if (movingRight)
        {
            Move(0.5f);
        }
        else
        {
            Move(-0.5f);
        }
    }
}
