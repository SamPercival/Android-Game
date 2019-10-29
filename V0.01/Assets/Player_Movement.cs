using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : Movement
{
    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = CheckIfGrounded();
        if (rb.velocity.y <= 0)
        {
            rb.gravityScale = 5;
        }

        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            rb.gravityScale = 4;
            jumpTimer = jumpTimeLimit;
            rb.AddForce(new Vector2(0, jumpForce));
            
        }
        else if (jumpTimer > 0 && Input.GetKey(KeyCode.Space))
        {
            jumpTimer -= Time.deltaTime;
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
