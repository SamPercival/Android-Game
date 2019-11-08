using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Behavior : MonoBehaviour
{
    public int dmg;
    public Enemy_Movement movement;
    public Enemy_Attack attack;
    private bool aggresive = false;

    private void FixedUpdate()
    {
        if (aggresive)
        {
            Debug.Log("ANGRY");
            movement.Aggresive();
            attack.lightAttack();
        }
        else
        {
            movement.Passive();
        }
    }

    public void setAggro(bool val)
    {
        aggresive = val;
    }
}
