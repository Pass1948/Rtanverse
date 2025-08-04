using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonEventBase
{
}

public class EventOnTakeDamage : CommonEventBase
{
    public float damage;
    public float currentHP;

    public EventOnTakeDamage(float damage, float currentHP)
    {
        this.damage = damage;
        this.currentHP = currentHP;
    }
}
