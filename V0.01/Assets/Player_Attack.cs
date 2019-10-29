using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public float attackWaitTime;
    public float attackRadius;
    public LayerMask enemyDef;

    public int dmg;

    private float attackCountdown = 0;
    
    private void FixedUpdate()
    {
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
        }
    }
    public void Attack()
    {
        if (attackCountdown <= 0)
        {
            attackCountdown = attackWaitTime;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyDef);
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<Health>().TakeDamage(dmg);
            }

        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,attackRadius);
    }
}
