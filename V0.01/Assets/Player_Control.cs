using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{
    public Player_Movement mover;

    private void FixedUpdate()
    {
        float xAxis = Input.GetAxisRaw("Horizontal");
        mover.Move(xAxis);
    }
}
