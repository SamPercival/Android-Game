using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vision : MonoBehaviour
{
    public Enemy_Behavior eb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            Debug.Log("Aggro");
            eb.setAggro(true);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            eb.setAggro(false);
        }
    }
}
