using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public float dmg;
    public float fwdAttkDist;
    public Transform pos;

    public float lightAttackWaitTime;
    public float lightAttackRadius;
    public float lDmgMult;

    public float heavyAttackWaitTime;
    public float heavyAttackXSize;
    public float heavyAttackYSize;
    private Vector2 heavyAttackCorner0;
    private Vector2 heavyAttackCorner1;
    public float hDmgMult;

    public LayerMask enemyDef;
    private float attackCountdown = 0;

    public float xAxisAttackThreshold;
    public float yAxisAttackThreshold;
    public LayerMask rayLM;
    public Player_Movement pm;
    private void FixedUpdate()
    {
        heavyAttackCorner0 = new Vector2(transform.position.x + base.transform.localScale.x / 2 -7.0f, transform.position.y - heavyAttackYSize / 2);
        heavyAttackCorner1 = new Vector2(transform.position.x + (base.transform.localScale.x / 2) + heavyAttackXSize, transform.position.y + heavyAttackYSize / 2);
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
        }
        FaceNearestEnemy();
    }

    public void LightAttack()
    {
        Debug.Log("Light");
        attackCountdown = lightAttackWaitTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lightAttackRadius, enemyDef);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Health>().TakeDamage(lDmgMult * dmg);
        }
    }

    public void HeavyAttack()
    {
        Debug.Log("Heavy");
        attackCountdown = heavyAttackWaitTime;
        Collider2D[] enemies = Physics2D.OverlapAreaAll(heavyAttackCorner0,heavyAttackCorner1, enemyDef);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Health>().TakeDamage(hDmgMult * dmg);
        }
    }

    public void LightUp()
    {
        //Small stun on enemy in direction facing
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lightAttackRadius, enemyDef);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy_Movement>().Stun(1.0f);
        }
        LightAttack();
    }

    public void HeavyUp()
    {
        //Knocks up enemy in direction facing
        Collider2D[] enemies = Physics2D.OverlapAreaAll(heavyAttackCorner0, heavyAttackCorner1, enemyDef);
        foreach (Collider2D enemy in enemies)
        {
            if (pm.GetFacingRight())
            {
                enemy.GetComponent<Enemy_Movement>().KnockUp(5, 200);
            }
            else
            {
                enemy.GetComponent<Enemy_Movement>().KnockUp(-5, 200);
            }
        }
        HeavyAttack();
    }

    public void LightDown()
    {
        //Small radial sweep all around (no damage)
        attackCountdown = lightAttackWaitTime;
        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, lightAttackRadius, enemyDef);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy_Movement>().KnockDown();
        }
    }

    public void HeavyDown()
    {
        //Crouches and kicks in direction facing, knocking over enemy
        attackCountdown = heavyAttackWaitTime;
        Vector2 heavyAttackCorner2 = new Vector2(transform.position.x + (base.transform.localScale.x / 2) + heavyAttackXSize, (transform.position.y + heavyAttackYSize / 2)/2);
        Collider2D[] enemies = Physics2D.OverlapAreaAll(heavyAttackCorner0, heavyAttackCorner2, enemyDef);
        foreach (Collider2D enemy in enemies)
        {
            enemy.GetComponent<Enemy_Movement>().KnockDown();
            enemy.GetComponent<Health>().TakeDamage(hDmgMult * dmg);
        }
    }

    public void AttackDecider()
    {
        if (attackCountdown <= 0)
        {
            //float xAxis = Input.GetAxisRaw("Horizontal");
            float yAxis = Input.GetAxisRaw("Vertical");
            int key = 0;
            
            if (Input.GetKey("k"))
            {
                key = 1;
            }
            else if (Input.GetKey("l"))
            {
                key = 2;
            }

            if (yAxis > yAxisAttackThreshold)
            {
                Debug.Log("Up Attack");
                if (key == 1)
                {
                    LightUp();
                }
                else if (key == 2)
                {
                    HeavyUp();
                }

            }
            else if (yAxis < -xAxisAttackThreshold)
            {
                Debug.Log("Down Attack");
                if (key == 1)
                {
                    LightDown();
                }
                else if (key == 2)
                {
                    HeavyDown();
                }
            }
            else
            {
                if (key == 1)
                {
                    LightAttack();
                }
                else if (key == 2)
                {
                    HeavyAttack();
                }
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
            Debug.Log(distL);
            Debug.Log(distR);
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
        Gizmos.DrawWireSphere(transform.position, lightAttackRadius);
        heavyAttackCorner0 = new Vector2(transform.position.x + base.transform.localScale.x / 2 -7.0f, transform.position.y - heavyAttackYSize / 2);
        heavyAttackCorner1 = new Vector2(transform.position.x + (base.transform.localScale.x / 2) + heavyAttackXSize, transform.position.y + heavyAttackYSize/2);
        Vector3 v1 = new Vector3(heavyAttackCorner0.x, heavyAttackCorner0.y, 0);
        Vector3 v2 = new Vector3(heavyAttackCorner1.x, heavyAttackCorner1.y, 0);
        Gizmos.DrawLine(v1, v2);
    }
}
