using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Health : MonoBehaviour
{
    public Player_Health playerHealth;
    public Text healthText;
    // Update is called once per frame
    void Update()
    {
        healthText.text = playerHealth.health.ToString();
    }
}
