using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public float lightAttackWaitTime;
    public float lightAttackRadius;

    public float heavyAttackWaitTime;
    public float heavyAttackXSize;
    public float heavyAttackYSize;
    private Vector2 heavyAttackCorner0;
    private Vector2 heavyAttackCorner1;

    public LayerMask enemyDef;
    public int lDmg;
    public int hDmg;
    private float attackCountdown = 0;

    private void FixedUpdate()
    {
        heavyAttackCorner0 = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y - heavyAttackYSize / 2);
        heavyAttackCorner1 = new Vector2(transform.position.x + (transform.localScale.x / 2) + heavyAttackXSize, transform.position.y + heavyAttackYSize / 2);
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
        }
    }

    public void lightAttack()
    {
        if (attackCountdown <= 0)
        {
            Debug.Log("Light");
            attackCountdown = lightAttackWaitTime;
            Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lightAttackRadius, enemyDef);
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<Health>().TakeDamage(lDmg);
            }
        }
    }

    public void heavyAttack()
    {
        if (attackCountdown <= 0)
        {
            Debug.Log("Heavy");
            attackCountdown = heavyAttackWaitTime;
            Collider2D[] enemies = Physics2D.OverlapAreaAll(heavyAttackCorner0,heavyAttackCorner1, enemyDef);
            foreach (Collider2D enemy in enemies)
            {
                enemy.GetComponent<Health>().TakeDamage(hDmg);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lightAttackRadius);
        heavyAttackCorner0 = new Vector2(transform.position.x - 0.7f, transform.position.y - heavyAttackYSize / 2);
        heavyAttackCorner1 = new Vector2(transform.position.x+ heavyAttackXSize, transform.position.y + heavyAttackYSize / 2);
        Vector3 v1 = new Vector3(heavyAttackCorner0.x, heavyAttackCorner0.y, 0);
        Vector3 v2 = new Vector3(heavyAttackCorner1.x, heavyAttackCorner1.y, 0);
        Gizmos.DrawLine(v1, v2);
    }
}
