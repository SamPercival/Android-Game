using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Attack : MonoBehaviour
{
    public float lightAttackWaitTime;
    public float lightAttackRadius;

    public LayerMask enemyDef;
    public int dmg;
    private float attackCountdown = 0;
    public void lightAttack()
    {
        if (attackCountdown <= 0)
        {
            attackCountdown = lightAttackWaitTime;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lightAttackRadius, enemyDef);
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<Health>().TakeDamage(dmg);
            }
        }
    }
}
