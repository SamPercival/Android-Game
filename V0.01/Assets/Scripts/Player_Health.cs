using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Health : Health
{
    public override void Died()
    {
        FindObjectOfType<Game_Manager>().GameOver();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy"))
        {
            TakeDamage(collision.collider.GetComponent<Enemy_Behavior>().dmg);
        }
    }
}
