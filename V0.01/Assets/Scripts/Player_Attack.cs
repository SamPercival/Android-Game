using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public float dmg;
    public float fwdAttkDist;

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
        heavyAttackCorner0 = new Vector2(transform.position.x + transform.localScale.x / 2, transform.position.y - heavyAttackYSize / 2);
        heavyAttackCorner1 = new Vector2(transform.position.x + (transform.localScale.x / 2) + heavyAttackXSize, transform.position.y + heavyAttackYSize / 2);
        if (attackCountdown > 0)
        {
            attackCountdown -= Time.deltaTime;
        }
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

    public void LightForward(float xAxis)
    {
        //Dash to enemy infront if not too far away, do light attack
        RaycastHit2D rayHit;
        rayHit = Physics2D.Raycast(transform.position - new Vector3(0, 1.2f, 0),
                            new Vector2(xAxis, 0), fwdAttkDist,rayLM);

        if (rayHit.collider != null)
        {
            float dist = rayHit.point.x - transform.position.x;
            if (dist > 1 || dist < -1)
            {
                pm.Dash(dist);
            }
        }
    }

    public void HeavyForward()
    {
        //Dash to enemy infront if not too far, do heavy attack
    }
    public void LightUp()
    {
        //Small stun on enemy in direction facing
    }

    public void HeavyUp()
    {
        //Knocks up enemy in direction facing
    }

    public void LightDown()
    {
        //Small radial sweep all around (no damage)
    }

    public void HeavyDown()
    {
        //Crouches and kicks in direction facing, knocking over enemy
    }

    public void AttackDecider()
    {
        if (attackCountdown <= 0)
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
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

            if (xAxis > xAxisAttackThreshold || xAxis < -xAxisAttackThreshold)
            {
                if (key == 1)
                {
                    Debug.Log("Forward Attack l");
                    LightForward(xAxis);
                }
                else if (key == 2)
                {
                    Debug.Log("Forward Attack h");
                }
                
                //Forward attack

            }
            else if (yAxis > yAxisAttackThreshold)
            {
                Debug.Log("Up Attack");
                //Up attack

            }
            else if (yAxis < -xAxisAttackThreshold)
            {
                Debug.Log("Down Attack");
                //Down attack
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
