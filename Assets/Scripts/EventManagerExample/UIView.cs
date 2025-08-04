using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIView : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.Instance.AddListener<EventOnTakeDamage>(OnTakeDamage);
    }

    private void OnDisable()
    {
        EventManager.Instance.DelListener<EventOnTakeDamage>(OnTakeDamage);
    }

    private void OnTakeDamage(EventOnTakeDamage e)
    {
        Debug.Log($"TakeDamage : {e.damage}, CurrentHP : {e.currentHP}");
    }
}
