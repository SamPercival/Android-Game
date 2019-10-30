using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Respawn : MonoBehaviour
{
    public LayerMask checkpoint;
    // Update is called once per frame
    public void Respawn(Vector3 location)
    {
        transform.position = location;
    }
}
