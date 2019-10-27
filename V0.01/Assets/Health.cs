using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int health;
    public LayerMask killZoneDef;
    private void Update()
    {
        if (health <= 0)
        {
            Died();
        }
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, 0.1f, killZoneDef);
        foreach (Collider2D col in collisions)
        {
            if (col.gameObject != gameObject)
            {
                Kill();
                break;
            }
        }
    }
    public void TakeDamage(int dmg)
    {
        health -= dmg;
    }

    public void Heal(int heal)
    {
        health += heal;
    }

    void Died()
    {
        Debug.Log("Died");
        FindObjectOfType<Game_Manager>().GameOver();
    }

    void Kill()
    {
        health = 0;
    }
}
