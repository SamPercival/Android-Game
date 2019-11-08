using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Health : MonoBehaviour
{
    public int health;
    public LayerMask killZoneDef;
    public float defaultDamageTimer;

    private bool died = false;
    private float damageTimer = 0;
    
    private void Update()
    {
        if (damageTimer > 0)
        {
            damageTimer -= Time.deltaTime;
        }
        if (health <= 0)
        {
            died = true;
            Died();
        }
        if (!died)
        {
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
        
    }
    public virtual void TakeDamage(int dmg)
    {
        if (damageTimer <= 0)
        {
            health -= dmg;
            damageTimer = defaultDamageTimer;
        }
        
    }

    public void Heal(int heal)
    {
        Debug.Log("Heal");
        health += heal;
    }

    public abstract void Died();

    void Kill()
    {
        health = 0;
    }
}
