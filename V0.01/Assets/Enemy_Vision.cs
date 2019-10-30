using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Vision : MonoBehaviour
{
    public Enemy_Behavior eb;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Aggro");
            eb.setAggro(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            eb.setAggro(false);
        }
    }
}
