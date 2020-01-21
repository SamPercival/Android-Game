using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public float dmg;
    public float fwdAttkDist;
    public Transform pos;

    public float attackWaitTime;
    public float attackRadius;
    public float dmgMult;

    public LayerMask enemyDef;
    private float attackCountdown = 0;

    public float xAxisAttackThreshold;
    public float yAxisAttackThreshold;
    public LayerMask rayLM;
    public Player_Movement pm;
    private void FixedUpdate()
    {
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
        }
        FaceNearestEnemy();
    }

    public void Attack()
    {
        attackCountdown = attackWaitTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyDef);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Health>().TakeDamage(dmgMult * dmg);
        }
    }

    public void AttackUp()
    {
        //Small stun on enemy in direction facing
        attackCountdown = attackWaitTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyDef);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy_Movement>().KnockUp(10.0f,100.0f);
        }
        Attack();
    }

    public void AttackDown()
    {
        //Small radial sweep all around (no damage)
        attackCountdown = attackWaitTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRadius, enemyDef);
        Debug.Log(enemies);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy_Movement>().KnockDown();
        }
    }

    public void AttackDecider()
    {
        if (attackCountdown <= 0)
        {
            float yAxis = Input.GetAxisRaw("Vertical");

            if (yAxis > yAxisAttackThreshold)
            {
                Debug.Log("Up Attack");
                AttackUp();
            }
            else if (yAxis < -xAxisAttackThreshold)
            {
                Debug.Log("Down Attack");
                AttackDown();
            }
            else
            {
                Debug.Log("Attack");
                Attack();
            }
        }
    }

    void FaceNearestEnemy()
    {
        RaycastHit2D rayHitL, rayHitR;
        float distL = float.PositiveInfinity;
        float distR = float.PositiveInfinity;
        rayHitR = Physics2D.Raycast(pos.position - new Vector3(0, 14f, 0), new Vector2(1, 0), fwdAttkDist, rayLM);
        rayHitL = Physics2D.Raycast(pos.position - new Vector3(0, 14f, 0), new Vector2(-1, 0), fwdAttkDist, rayLM);
        Debug.DrawRay(pos.position - new Vector3(0, 14f, 0), new Vector2(fwdAttkDist, 0), Color.red, 3.0f);
        Debug.DrawRay(pos.position - new Vector3(0, 14f, 0), new Vector2(-fwdAttkDist, 0), Color.red, 3.0f);
        if (rayHitR.collider != null)
        {
            distR = Mathf.Abs(rayHitR.point.x - transform.position.x);
        }
        if (rayHitL.collider != null)
        {
            distL = Mathf.Abs(rayHitL.point.x - transform.position.x);
        }
        if (distL < float.PositiveInfinity || distR < float.PositiveInfinity)
        {
            //Debug.Log(distL);
            //Debug.Log(distR);
            pm.SetInCombat(true);
            if (distL >= distR)
            {
                pm.FaceNearestEnemy(true);
            }
            else
            {
                pm.FaceNearestEnemy(false);
            }
        }
        else
        {
            pm.SetInCombat(false);
        }
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }
}
