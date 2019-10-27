using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    public LayerMask checkpoint;
    // Update is called once per frame
    void Update()
    {
        Collider2D[] collisions = Physics2D.OverlapCircleAll(transform.position, transform.localScale.x / 2, checkpoint);
        foreach (Collider2D col in collisions)
        {
            if (col.gameObject != gameObject)
            {
                float height = (float)(col.transform.position.y - (col.transform.localScale.y / 2) + (transform.localScale.y / 2) + 0.5);
                FindObjectOfType<Game_Manager>().SetRespawn(new Vector3(col.transform.position.x, height, 0));
            }
        }
    }
}
