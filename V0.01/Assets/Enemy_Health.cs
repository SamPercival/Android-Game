﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : Health
{
    public override void Died()
    {
        Destroy(gameObject);
    }

    public override void TakeDamage(int dmg)
    {
        //effects
        base.TakeDamage(dmg);
    }
}
