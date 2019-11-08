using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public Player_Movement mover;
    public Player_Attack attacker;

    private void FixedUpdate()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        mover.Move(xAxis);

        if (Input.GetKey("k"))
        {
            attacker.lightAttack();
        }
        else if (Input.GetKey("l"))
        {
            attacker.heavyAttack();
        }
    }
}
