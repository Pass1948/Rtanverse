using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventPlayer : MonoBehaviour
{
    private float currentHP = 100f;

    private void Start()
    {
        TakeDamage(10f);
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;
        EventManager.ExecuteEvent(new EventOnTakeDamage(damage, currentHP));
    }
}
